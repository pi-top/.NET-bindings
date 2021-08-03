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
        private readonly int fd;

        public SMBusDevice(I2cDevice device) { }

        public unsafe ReadOnlySpan<byte> ReadBlock(byte length, IntPtr address, byte register)
        {

            // Read a block of byte data from a given register.

            //:param i2c_addr: i2c address
            //:type i2c_addr: int
            //:param register: Start register
            //:type register: int
            //:param length: Desired block length
            //: type length: int
            //:param force:
            //:type force: Boolean
            //: return: List of bytes
            // : rtype: list

            if (length > i2c_smbus_data.I2C_SMBUS_BLOCK_MAX)
            {
                throw new Exception($"Desired block length over {i2c_smbus_data.I2C_SMBUS_BLOCK_MAX} bytes");
            }

            //self._set_address(i2c_addr, force = force)
            Interop.ioctl(fd, (uint)I2cSettings.I2C_SLAVE_FORCE, address);

            var msg = new i2c_smbus_ioctl_data()
            {
                read_write = i2c_smbus_ioctl_data.I2C_SMBUS_READ,
                command = register,
                size = i2c_smbus_data.I2C_SMBUS_I2C_BLOCK_DATA,
                data = {
                    @byte = length
                }
            };


            int result = Interop.ioctl(fd, (uint)I2cSettings.I2C_SMBUS, ref msg);


            if (result < 0)
            {
                throw new IOException($"Error {Marshal.GetLastWin32Error()} performing I2C data transfer.");
            }

            return new ReadOnlySpan<byte>(msg.data.block, 1, length);
        }

    }
}


