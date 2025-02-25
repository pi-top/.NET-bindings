using SixLabors.ImageSharp.PixelFormats;
using PiTop.Abstractions;
using PiTop.OledDevice;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.Processing;

namespace PiTop
{
    public class Sh1106Display : Display
    {
        private readonly Sh1106 _device;

        public Sh1106Display(DisplaySpiConnectionSettings settings, IGpioControllerFactory controllerFactory, ISPiDeviceFactory spiDeviceFactory) : base(Sh1106.Width, Sh1106.Height)
        {
            _device = new Sh1106(settings.SpiConnectionSettings, settings.DcPin, settings.RstPin, spiDeviceFactory, controllerFactory);
            _device.Init();
            Clear();
            _device.SetContrast(255);
            RegisterForDisposal(_device);
        }

        public override void Show()
        {
            _device.Show();
        }

        public override void Hide()
        {
            _device.Hide();
        }

        protected override void CommitBuffer()
        {
            InternalBitmap.Mutate(c => c.Dither().BlackWhite());

            var luminanceSource = InternalBitmap
                .CloneAs<L8>();

            var pages = Height / 8;

            for (var pageAddress = 0; pageAddress < pages; pageAddress++)
            {


                var scan = new byte[Width]; // each byte represents 8 pixels in column
                for (var y = 0; y < 8; y++)
                {

                    // row scan inside page
                    var luminance = luminanceSource.DangerousGetPixelRowMemory(y + pageAddress * 8).ToArray();
                    for (var x = 0; x < Width; x++)
                    {
                        if (y == 0) scan[x] = 0;
                        if (luminance[x].PackedValue >= 128)
                        {
                            scan[x] |= (byte)(0x01 << y);
                        }
                    }
                }
                _device.WritePage(pageAddress, scan);
            }
        }
    }
}