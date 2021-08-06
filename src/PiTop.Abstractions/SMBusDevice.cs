using System;
using System.Device.I2c;
using System.IO;
using System.Runtime.InteropServices;

namespace PiTop.Abstractions
{

    /// <summary>
    /// Wraps I2C device to expose SMBus functions.
    /// See http://smbus.org/specs/SMBus_3_0_20141220.pdf
    /// </summary>
    public class SMBusDevice
    {
        private byte _address;
        private SMBus _bus;

        public SMBusDevice(I2cDevice device)
        {
            _address = (byte)device.ConnectionSettings.DeviceAddress;
            _bus = SMBus.GetOrCreate(device.ConnectionSettings.BusId);
        }

        public byte ReadByte(byte register)
        { return _bus.ReadByte(this._address, register); }

        public ushort ReadUShort(byte register, bool littleEndian = false)
        {
            var data = _bus.ReadBlock(this._address, register, 2);
            // todo: find better way to convert with right endian-ness
            if (BitConverter.IsLittleEndian != littleEndian) { throw new InvalidOperationException("Expected other-endian system"); }
            return BitConverter.ToUInt16(data);
        }


        public void WriteByte(byte register, byte data)
        { _bus.WriteByte(this._address, register, data); }
    }

    public class SMBus
    {
        private int _fd;
        // current operating address
        private uint _address;

        public static SMBus GetOrCreate(int busId)
        {
            throw new NotImplementedException();
        }

        public SMBus(int busId)
        {
            // get fd from busID
            throw new NotImplementedException();
        }

        private void SetAddress(byte address)
        {
            if (_address != address)
            {
                int result = Interop.ioctl(_fd, (uint)I2cSettings.I2C_SLAVE_FORCE, address);
                if (result < 0)
                {
                    throw new IOException($"Error {Marshal.GetLastWin32Error()} setting I2C address {address}.");
                }
                _address = address;
            }
        }

        private i2c_smbus_ioctl_data SendSMBusMessage(i2c_smbus_ioctl_data msg)
        {
            int result = Interop.ioctl(_fd, (uint)I2cSettings.I2C_SMBUS, ref msg);

            if (result < 0)
            {
                throw new IOException($"Error {Marshal.GetLastWin32Error()} performing I2C data transfer.");
            }

            return msg;
        }

        public byte ReadByte(byte address, byte register)
        {
            throw new NotImplementedException();
        }

        public ushort ReadWord(byte address, byte register)
        {
            throw new NotImplementedException();
        }

        // Read a block of byte data from a given register.
        public ReadOnlySpan<byte> ReadBlock(byte address, byte register, byte length)
        {

            if (length > i2c_smbus_data.I2C_SMBUS_BLOCK_MAX)
            {
                throw new Exception($"Desired block length over {i2c_smbus_data.I2C_SMBUS_BLOCK_MAX} bytes");
            }

            SetAddress(address);

            var msg = new i2c_smbus_ioctl_data()
            {
                read_write = i2c_smbus_ioctl_data.I2C_SMBUS_READ,
                command = register,
                size = SMBusDataSize.I2C_SMBUS_I2C_BLOCK_DATA,
                data = {
                    @byte = length
                }
            };

            msg = SendSMBusMessage(msg);

            return new ReadOnlySpan<byte>(msg.data.block, 1, length);
        }

        public void WriteByte(byte address, byte register, byte data)
        {

            SetAddress(address);
            var msg = new i2c_smbus_ioctl_data
            {
                read_write = i2c_smbus_ioctl_data.I2C_SMBUS_WRITE,
                command = register,
                size = SMBusDataSize.I2C_SMBUS_BYTE_DATA,


                data = { @byte = data }
            };

            SendSMBusMessage(msg);
        }
    }
}


