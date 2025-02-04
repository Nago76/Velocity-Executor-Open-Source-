using System;
using System.Runtime.InteropServices;

namespace ExecutorUI;

public static class HandleHijack
{
	private struct LUID
	{
		public uint LowPart;

		public int HighPart;
	}

	private struct TOKEN_PRIVILEGES
	{
		public int PrivilegeCount;

		public LUID Luid;

		public int Attributes;
	}

	private struct SYSTEM_HANDLE_TABLE_ENTRY_INFO
	{
		public int ProcessId;

		public byte ObjectTypeNumber;

		public byte Flags;

		public ushort Handle;

		public nint Object;

		public uint GrantedAccess;
	}

	private const int SystemExtendedHandleInformation = 64;

	private const uint STATUS_INFO_LENGTH_MISMATCH = 3221225476u;

	private const uint PROCESS_DUP_HANDLE = 64u;

	private const uint PROCESS_ALL_ACCESS = 2035711u;

	[DllImport("ntdll.dll")]
	private static extern int NtQuerySystemInformation(int systemInformationClass, nint systemInformation, int systemInformationLength, out int returnLength);

	[DllImport("advapi32.dll", SetLastError = true)]
	private static extern bool OpenProcessToken(nint processHandle, int desiredAccess, out nint tokenHandle);

	[DllImport("advapi32.dll", SetLastError = true)]
	private static extern bool LookupPrivilegeValue(string lpSystemName, string lpName, out LUID luid);

	[DllImport("advapi32.dll", SetLastError = true)]
	private static extern bool AdjustTokenPrivileges(nint tokenHandle, bool disableAllPrivileges, ref TOKEN_PRIVILEGES newState, int bufferLength, nint previousState, nint returnLength);

	[DllImport("kernel32.dll")]
	private static extern nint OpenProcess(uint desiredAccess, bool inheritHandle, int processId);

	[DllImport("kernel32.dll")]
	private static extern bool CloseHandle(nint handle);

	[DllImport("kernel32.dll")]
	private static extern nint GetCurrentProcess();

	[DllImport("ntdll.dll")]
	private static extern int NtDuplicateObject(nint sourceProcessHandle, nint sourceHandle, nint targetProcessHandle, out nint targetHandle, int desiredAccess, int attributes, int options);

	private static void EnableDebugPrivilege()
	{
		if (!OpenProcessToken(GetCurrentProcess(), 40, out var tokenHandle))
		{
			throw new global::System.Exception($"OpenProcessToken failed with error: {Marshal.GetLastWin32Error()}");
		}
		try
		{
			if (!LookupPrivilegeValue(null, "SeDebugPrivilege", out var luid))
			{
				throw new global::System.Exception($"LookupPrivilegeValue failed with error: {Marshal.GetLastWin32Error()}");
			}
			TOKEN_PRIVILEGES tOKEN_PRIVILEGES = default(TOKEN_PRIVILEGES);
			tOKEN_PRIVILEGES.PrivilegeCount = 1;
			tOKEN_PRIVILEGES.Luid = luid;
			tOKEN_PRIVILEGES.Attributes = 2;
			TOKEN_PRIVILEGES newState = tOKEN_PRIVILEGES;
			if (!AdjustTokenPrivileges(tokenHandle, disableAllPrivileges: false, ref newState, 0, global::System.IntPtr.Zero, global::System.IntPtr.Zero))
			{
				throw new global::System.Exception($"AdjustTokenPrivileges failed with error: {Marshal.GetLastWin32Error()}");
			}
		}
		finally
		{
			CloseHandle(tokenHandle);
		}
	}

	public static nint HijackHandle(int targetProcessId)
	{
		EnableDebugPrivilege();
		int num = 65536;
		nint num2 = Marshal.AllocHGlobal(num);
		try
		{
			Console.WriteLine("Starting handle enumeration...");
			int num3;
			do
			{
				num3 = NtQuerySystemInformation(64, num2, num, out var returnLength);
				if (num3 == 3221225476u)
				{
					Console.WriteLine($"Buffer too small, resizing to {returnLength} bytes.");
					num = returnLength;
					Marshal.FreeHGlobal((global::System.IntPtr)num2);
					num2 = Marshal.AllocHGlobal(num);
				}
				else if (num3 < 0)
				{
					throw new global::System.Exception($"NtQuerySystemInformation failed with status: {num3:X} (0x{num3:X})");
				}
			}
			while (num3 == 3221225476u);
			Console.WriteLine("Successfully retrieved handle information.");
			int num4 = Marshal.ReadInt32((global::System.IntPtr)num2);
			nint num5 = global::System.IntPtr.Add((global::System.IntPtr)num2, global::System.IntPtr.Size);
			for (int i = 0; i < num4; i++)
			{
				SYSTEM_HANDLE_TABLE_ENTRY_INFO sYSTEM_HANDLE_TABLE_ENTRY_INFO = Marshal.PtrToStructure<SYSTEM_HANDLE_TABLE_ENTRY_INFO>((global::System.IntPtr)num5);
				num5 = global::System.IntPtr.Add((global::System.IntPtr)num5, Marshal.SizeOf<SYSTEM_HANDLE_TABLE_ENTRY_INFO>());
				if (sYSTEM_HANDLE_TABLE_ENTRY_INFO.ProcessId != targetProcessId || sYSTEM_HANDLE_TABLE_ENTRY_INFO.ObjectTypeNumber != 7)
				{
					continue;
				}
				nint num6 = OpenProcess(64u, inheritHandle: false, sYSTEM_HANDLE_TABLE_ENTRY_INFO.ProcessId);
				if (num6 != (nint)global::System.IntPtr.Zero)
				{
					if (NtDuplicateObject(num6, sYSTEM_HANDLE_TABLE_ENTRY_INFO.Handle, GetCurrentProcess(), out var targetHandle, 2035711, 0, 0) == 0)
					{
						CloseHandle(num6);
						return targetHandle;
					}
					CloseHandle(num6);
				}
			}
			throw new global::System.Exception("Failed to find a suitable handle.");
		}
		finally
		{
			Marshal.FreeHGlobal((global::System.IntPtr)num2);
		}
	}
}
