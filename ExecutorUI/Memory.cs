using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace ExecutorUI;

internal class Memory
{
	[StructLayout(0, Size = 1)]
	internal struct Flags
	{
		public const int PROCESS_VM_OPERATION = 8;

		public const int PROCESS_VM_READ = 16;

		public const int PROCESS_VM_WRITE = 32;
	}

	internal static class Imports
	{
		public struct MEMORY_BASIC_INFORMATION
		{
			public nint BaseAddress;

			public nint AllocationBase;

			public uint AllocationProtect;

			public nint RegionSize;

			public uint State;

			public uint Protect;

			public uint Type;
		}

		[DllImport("ntdll.dll", SetLastError = true)]
		public static extern int NtReadVirtualMemory(nint ProcessHandle, nint BaseAddress, byte[] Buffer, int Length, out int BytesRead);

		[DllImport("ntdll.dll", SetLastError = true)]
		public static extern int NtWriteVirtualMemory(nint ProcessHandle, nint BaseAddress, byte[] Buffer, int Length, out int BytesWritten);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern nint VirtualQueryEx(nint hProcess, nint lpAddress, nint lpBuffer, nint dwLength);
	}

	private Process m_iProcess;

	private nint m_iProcessHandle;

	private int m_iBytesWritten;

	private int m_iBytesRead;

	[DllImport("Bin/plane_hijacker.dll", CallingConvention = 2)]
	public static extern nint getHandle(int pid);

	public Memory(Process process)
	{
		if (process != null)
		{
			m_iProcess = process;
			m_iProcessHandle = getHandle(process.Id);
			if (m_iProcessHandle == (nint)global::System.IntPtr.Zero)
			{
				throw new InvalidOperationException("Failed to open process.");
			}
			return;
		}
		throw new ArgumentNullException("process", "Process cannot be null.");
	}

	public void cleanUp()
	{
	}

	public void WriteMemory<T>(nint address, T value)
	{
		byte[] array = StructureToByteArray(value);
		Imports.NtWriteVirtualMemory(m_iProcessHandle, address, array, array.Length, out m_iBytesWritten);
	}

	public void WriteMemory(nint address, char[] value)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(value);
		Imports.NtWriteVirtualMemory(m_iProcessHandle, address, bytes, bytes.Length, out m_iBytesWritten);
	}

	public T ReadMemory<T>(nint address) where T : struct
	{
		byte[] array = new byte[Marshal.SizeOf(typeof(T))];
		Imports.NtReadVirtualMemory(m_iProcessHandle, address, array, array.Length, out m_iBytesRead);
		return ByteArrayToStructure<T>(array);
	}

	public byte[] ReadMemory(nint address, int size)
	{
		byte[] array = new byte[size];
		Imports.NtReadVirtualMemory(m_iProcessHandle, address, array, size, out m_iBytesRead);
		return array;
	}

	public float[] ReadMatrix(nint address, int matrixSize)
	{
		byte[] array = new byte[Marshal.SizeOf(typeof(float)) * matrixSize];
		Imports.NtReadVirtualMemory(m_iProcessHandle, address, array, array.Length, out m_iBytesRead);
		return ConvertToFloatArray(array);
	}

	public nint GetModuleAddress(string name)
	{
		try
		{
			foreach (ProcessModule item in (ReadOnlyCollectionBase)m_iProcess.Modules)
			{
				ProcessModule val = item;
				if (name.Equals(val.ModuleName, (StringComparison)5))
				{
					return val.BaseAddress;
				}
			}
		}
		catch
		{
			Console.ForegroundColor = (ConsoleColor)12;
			Console.WriteLine("ERROR: Cannot find - " + name + " | Check file extension.");
			Console.ResetColor();
		}
		return global::System.IntPtr.Zero;
	}

	public bool IsTrapPage(nint address)
	{
		int num = Marshal.SizeOf(typeof(Imports.MEMORY_BASIC_INFORMATION));
		nint num2 = Marshal.AllocHGlobal(num);
		try
		{
			if (Imports.VirtualQueryEx(m_iProcessHandle, address, num2, num) == (nint)global::System.IntPtr.Zero)
			{
				throw new InvalidOperationException("VirtualQueryEx failed. Error code: " + Marshal.GetLastWin32Error());
			}
			Imports.MEMORY_BASIC_INFORMATION mEMORY_BASIC_INFORMATION = Marshal.PtrToStructure<Imports.MEMORY_BASIC_INFORMATION>((global::System.IntPtr)num2);
			return (mEMORY_BASIC_INFORMATION.State & 0x1000) == 4096 && (mEMORY_BASIC_INFORMATION.Protect & 1) == 1;
		}
		finally
		{
			Marshal.FreeHGlobal((global::System.IntPtr)num2);
		}
	}

	public static byte[] StructureToByteArray(object obj)
	{
		if (obj == null)
		{
			throw new ArgumentNullException("obj");
		}
		int num = Marshal.SizeOf(obj);
		byte[] array = new byte[num];
		nint num2 = Marshal.AllocHGlobal(num);
		try
		{
			Marshal.StructureToPtr(obj, (global::System.IntPtr)num2, true);
			Marshal.Copy((global::System.IntPtr)num2, array, 0, num);
			return array;
		}
		finally
		{
			Marshal.FreeHGlobal((global::System.IntPtr)num2);
		}
	}

	public static T ByteArrayToStructure<T>(byte[] byteArray) where T : struct
	{
		if (byteArray == null)
		{
			throw new ArgumentNullException("byteArray");
		}
		int num = Marshal.SizeOf(typeof(T));
		if (byteArray.Length < num)
		{
			throw new ArgumentException("Byte array is too small to convert to structure");
		}
		nint num2 = Marshal.AllocHGlobal(num);
		try
		{
			Marshal.Copy(byteArray, 0, (global::System.IntPtr)num2, num);
			return Marshal.PtrToStructure<T>((global::System.IntPtr)num2);
		}
		finally
		{
			Marshal.FreeHGlobal((global::System.IntPtr)num2);
		}
	}

	public static float[] ConvertToFloatArray(byte[] bytes)
	{
		if (bytes.Length % 4 != 0)
		{
			throw new ArgumentException("Byte array length must be a multiple of 4");
		}
		float[] array = new float[bytes.Length / 4];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = BitConverter.ToSingle(bytes, i * 4);
		}
		return array;
	}
}
