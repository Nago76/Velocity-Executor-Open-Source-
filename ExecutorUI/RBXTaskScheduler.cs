using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ExecutorUI;

internal class RBXTaskScheduler
{
	public static nint GetRawScheduler(Process rbxProcess)
	{
		Memory memory = new Memory(rbxProcess);
		nint num = memory.ReadMemory<nint>((nint)rbxProcess.MainModule.BaseAddress + 96160168);
		if (memory.IsTrapPage(num))
		{
			memory.cleanUp();
			return global::System.IntPtr.Zero;
		}
		memory.cleanUp();
		if (((global::System.IntPtr)num).Equals((global::System.IntPtr)0))
		{
			return global::System.IntPtr.Zero;
		}
		return num;
	}

	public static List<ValueTuple<string, nint>> GetJobs(Process rbxProcess)
	{
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		List<ValueTuple<string, nint>> val = new List<ValueTuple<string, nint>>();
		nint rawScheduler = GetRawScheduler(rbxProcess);
		if (((global::System.IntPtr)rawScheduler).Equals((global::System.IntPtr)0))
		{
			return val;
		}
		Memory memory = new Memory(rbxProcess);
		nint num = memory.ReadMemory<nint>(rawScheduler + 408);
		if (memory.IsTrapPage(num))
		{
			memory.cleanUp();
			return val;
		}
		nint num2 = memory.ReadMemory<nint>(rawScheduler + 416);
		if (memory.IsTrapPage(num2))
		{
			memory.cleanUp();
			return val;
		}
		for (nint num3 = num; num3 != num2; num3 += 16)
		{
			nint num4 = memory.ReadMemory<nint>(num3);
			if (!((global::System.IntPtr)num4).Equals((global::System.IntPtr)0) && !memory.IsTrapPage(num4))
			{
				string text = RBX.ReadString(memory, num4 + 144);
				if (!(text == ""))
				{
					val.Add(new ValueTuple<string, nint>(text, num4));
				}
			}
		}
		memory.cleanUp();
		return val;
	}

	public static List<string> GetJobNames(Process rbxProcess)
	{
		List<string> val = new List<string>();
		List<ValueTuple<string, nint>> jobs = GetJobs(rbxProcess);
		Enumerator<ValueTuple<string, nint>> enumerator = jobs.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				val.Add(enumerator.Current.Item1);
			}
		}
		finally
		{
			((global::System.IDisposable)enumerator).Dispose();
		}
		jobs.Clear();
		return val;
	}

	public static nint GetJobByName(Process rbxProcess, string JobToFind)
	{
		Enumerator<ValueTuple<string, nint>> enumerator = GetJobs(rbxProcess).GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				ValueTuple<string, nint> current = enumerator.Current;
				if (current.Item1 == JobToFind)
				{
					return current.Item2;
				}
			}
		}
		finally
		{
			((global::System.IDisposable)enumerator).Dispose();
		}
		return global::System.IntPtr.Zero;
	}

	public static nint GetRenderView(Process rbxProcess)
	{
		nint jobByName = GetJobByName(rbxProcess, "RenderJob");
		if (((global::System.IntPtr)jobByName).Equals(global::System.IntPtr.Zero))
		{
			return global::System.IntPtr.Zero;
		}
		nint zero = global::System.IntPtr.Zero;
		Memory memory = new Memory(rbxProcess);
		zero = memory.ReadMemory<nint>(jobByName + 536);
		if (memory.IsTrapPage(zero))
		{
			memory.cleanUp();
			return global::System.IntPtr.Zero;
		}
		memory.cleanUp();
		return zero;
	}

	public static nint GetDataModel(Process rbxProcess)
	{
		nint renderView = GetRenderView(rbxProcess);
		if (((global::System.IntPtr)renderView).Equals(global::System.IntPtr.Zero))
		{
			return global::System.IntPtr.Zero;
		}
		Memory memory = new Memory(rbxProcess);
		nint zero = global::System.IntPtr.Zero;
		zero = memory.ReadMemory<nint>(renderView + 280);
		if (memory.IsTrapPage(zero))
		{
			memory.cleanUp();
			return global::System.IntPtr.Zero;
		}
		zero = memory.ReadMemory<nint>(zero + 424);
		if (memory.IsTrapPage(zero))
		{
			memory.cleanUp();
			return global::System.IntPtr.Zero;
		}
		memory.cleanUp();
		return zero;
	}
}
