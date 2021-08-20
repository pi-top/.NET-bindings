// // Licensed to the .NET Foundation under one or more agreements.
// // The .NET Foundation licenses this file to you under the MIT license.

// // Disable these StyleCop rules for this file, as we are using native names here.
// #pragma warning disable SA1300 // Element should begin with upper-case letter
// #pragma warning disable SA1307 // Accessible fields should begin with upper-case letter

// using System;
// using System.Runtime.InteropServices;

// internal partial class Interop
// {
//     private const string LibcLibrary = "libc";

//     [DllImport(LibcLibrary, SetLastError = true)]
//     internal static extern int ioctl(int fd, uint request, IntPtr argp);

//     [DllImport(LibcLibrary, SetLastError = true)]
//     internal static extern int ioctl(int fd, uint request, ulong argp);

//     [DllImport(LibcLibrary, SetLastError = true)]
//     internal static extern int ioctl(int fd, uint request, ref i2c_smbus_ioctl_data i2c_smbus_ioctl_data);

//     [DllImport(LibcLibrary, SetLastError = true)]
//     internal static extern int open([MarshalAs(UnmanagedType.LPStr)] string pathname, FileOpenFlags flags);
// }

// [Flags]
// internal enum I2cFunctionalityFlags : ulong
// {
//     I2C_FUNC_I2C = 0x00000001,
//     I2C_FUNC_SMBUS_BLOCK_DATA = 0x03000000
// }

// internal enum I2cSettings : uint
// {
//     /// <summary>Get the adapter functionality mask.</summary>
//     I2C_FUNCS = 0x0705,

//     /// <summary>Use this replica address, even if it is already in use by a driver.</summary>
//     I2C_SLAVE_FORCE = 0x0706,

//     /// <summary>Combined R/W transfer (one STOP only).</summary>
//     I2C_RDWR = 0x0707,

//     /// <summary>Smbus transfer.</summary>
//     I2C_SMBUS = 0x0720
// }

// [StructLayout(LayoutKind.Sequential)]
// internal unsafe struct i2c_msg
// {
//     public ushort addr;
//     public I2cMessageFlags flags;
//     public ushort len;
//     public byte* buf;
// }

// [Flags]
// internal enum I2cMessageFlags : ushort
// {
//     /// <summary>Write data to replica.</summary>
//     I2C_M_WR = 0x0000,

//     /// <summary>Read data from replica.</summary>
//     I2C_M_RD = 0x0001
// }

// [StructLayout(LayoutKind.Sequential)]
// internal unsafe struct i2c_rdwr_ioctl_data
// {
//     public i2c_msg* msgs;
//     public uint nmsgs;
// }

// [StructLayout(LayoutKind.Sequential)]
// internal unsafe struct i2c_smbus_ioctl_data
// {
//     public const byte I2C_SMBUS_WRITE = 0;
//     public const byte I2C_SMBUS_READ = 1;

//     public byte read_write;
//     public byte command;
//     public SMBusDataSize size;

//     [MarshalAs(UnmanagedType.ByValArray)]
//     public i2c_smbus_data data;
// };

// [StructLayout(LayoutKind.Explicit)]
// internal unsafe struct i2c_smbus_data
// {
//     [FieldOffset(0)]
//     public byte @byte;
//     [FieldOffset(0)]
//     public ushort word;
//     [FieldOffset(0)]
//     [MarshalAs(UnmanagedType.ByValArray, SizeConst = I2C_SMBUS_BLOCK_MAX + 2)]
//     public byte[] block;

//     internal const byte I2C_SMBUS_BLOCK_MAX = 32;
// }

// internal enum SMBusDataSize : uint
// {
//     // Size identifiers uapi/linux/i2c.h
//     I2C_SMBUS_QUICK = 0,
//     I2C_SMBUS_BYTE = 1,
//     I2C_SMBUS_BYTE_DATA = 2,
//     I2C_SMBUS_WORD_DATA = 3,
//     I2C_SMBUS_PROC_CALL = 4,
//     I2C_SMBUS_BLOCK_DATA = 5,  // This isn't supported by Pure-I2C drivers with SMBUS emulation, like those in RaspberryPi, OrangePi, etc :(
//     I2C_SMBUS_BLOCK_PROC_CALL = 7,  // Like I2C_SMBUS_BLOCK_DATA, it isn't supported by Pure-I2C drivers either.
//     I2C_SMBUS_I2C_BLOCK_DATA = 8,
// }

// [Flags]
// internal enum UnixSpiMode : byte
// {
//     None = 0x00,
//     SPI_CPHA = 0x01,
//     SPI_CPOL = 0x02,
//     SPI_CS_HIGH = 0x04,
//     SPI_LSB_FIRST = 0x08,
//     SPI_3WIRE = 0x10,
//     SPI_LOOP = 0x20,
//     SPI_NO_CS = 0x40,
//     SPI_READY = 0x80,
//     SPI_MODE_0 = None,
//     SPI_MODE_1 = SPI_CPHA,
//     SPI_MODE_2 = SPI_CPOL,
//     SPI_MODE_3 = SPI_CPOL | SPI_CPHA
// }

// internal enum SpiSettings : uint
// {
//     /// <summary>Set SPI mode.</summary>
//     SPI_IOC_WR_MODE = 0x40016b01,

//     /// <summary>Get SPI mode.</summary>
//     SPI_IOC_RD_MODE = 0x80016b01,

//     /// <summary>Set bits per word.</summary>
//     SPI_IOC_WR_BITS_PER_WORD = 0x40016b03,

//     /// <summary>Get bits per word.</summary>
//     SPI_IOC_RD_BITS_PER_WORD = 0x80016b03,

//     /// <summary>Set max speed (Hz).</summary>
//     SPI_IOC_WR_MAX_SPEED_HZ = 0x40046b04,

//     /// <summary>Get max speed (Hz).</summary>
//     SPI_IOC_RD_MAX_SPEED_HZ = 0x80046b04
// }

// [StructLayout(LayoutKind.Sequential)]
// internal struct spi_ioc_transfer
// {
//     public ulong tx_buf;
//     public ulong rx_buf;
//     public uint len;
//     public uint speed_hz;
//     public ushort delay_usecs;
//     public byte bits_per_word;
//     public byte cs_change;
//     public byte tx_nbits;
//     public byte rx_nbits;
//     public ushort pad;
// }

// internal enum FileOpenFlags
// {
//     O_RDONLY = 0x00,
//     O_RDWR = 0x02,
//     O_NONBLOCK = 0x800,
//     O_SYNC = 0x101000
// }
