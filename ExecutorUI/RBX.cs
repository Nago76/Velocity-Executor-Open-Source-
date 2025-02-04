using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ExecutorUI;

internal class RBX
{
	public static string ReadString(Memory memory, nint address)
	{
		try
		{
			int size = memory.ReadMemory<int>(address + 16);
			if (memory.ReadMemory<int>(address + 24) > 15)
			{
				nint address2 = memory.ReadMemory<nint>(address);
				if (memory.IsTrapPage(address2))
				{
					return "";
				}
				if (((global::System.IntPtr)address2).Equals(global::System.IntPtr.Zero))
				{
					return "";
				}
				byte[] array = memory.ReadMemory(address2, size);
				return Encoding.ASCII.GetString(array);
			}
			byte[] array2 = memory.ReadMemory(address, size);
			return Encoding.ASCII.GetString(array2);
		}
		catch (global::System.Exception)
		{
			return "???";
		}
	}

	public static List<nint> getChildren(Process rbxProcess, nint Address, nint ChildrenOffset)
	{
		List<nint> val = new List<nint>();
		Memory memory = new Memory(rbxProcess);
		nint num = memory.ReadMemory<nint>(Address + ChildrenOffset);
		nint num2 = memory.ReadMemory<nint>(num + 8);
		for (nint num3 = memory.ReadMemory<nint>(num); num3 != num2; num3 += 16)
		{
			if (!((global::System.IntPtr)num3).Equals((global::System.IntPtr)0) && !memory.IsTrapPage(num3))
			{
				nint num4 = memory.ReadMemory<nint>(num3);
				if (!((global::System.IntPtr)num4).Equals((global::System.IntPtr)0) && !memory.IsTrapPage(num4))
				{
					val.Add(num4);
				}
			}
		}
		memory.cleanUp();
		return val;
	}

	public static nint GetPlayerService(Process rbxProcess)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		nint dataModel = RBXTaskScheduler.GetDataModel(rbxProcess);
		List<nint> children = getChildren(rbxProcess, dataModel, RobloxReversing.ChildrenOffset);
		Memory memory = new Memory(rbxProcess);
		nint result = global::System.IntPtr.Zero;
		Enumerator<nint> enumerator = children.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				nint current = enumerator.Current;
				nint num = memory.ReadMemory<nint>(current + 24);
				nint address = memory.ReadMemory<nint>(num + 8);
				if (ReadString(memory, address) == "Players")
				{
					result = current;
					break;
				}
			}
		}
		finally
		{
			((global::System.IDisposable)enumerator).Dispose();
		}
		memory.cleanUp();
		return result;
	}
}
