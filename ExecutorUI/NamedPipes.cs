using System;
using System.IO;
using System.IO.Pipes;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows;

namespace ExecutorUI;

internal class NamedPipes
{
	public static string luapipename = "sgijnedringnerginjerngrejigjergnjerjignjergnireijgnerijgnijrenijgjergn";

	[DllImport("kernel32.dll", CharSet = 4, SetLastError = true)]
	[return: MarshalAs(2)]
	private static extern bool WaitNamedPipe(string name, int timeout);

	public static bool NamedPipeExist(string pipeName)
	{
		try
		{
			if (!WaitNamedPipe("\\\\.\\pipe\\" + pipeName, 0))
			{
				switch (Marshal.GetLastWin32Error())
				{
				case 0:
					return false;
				case 2:
					return false;
				}
			}
			return true;
		}
		catch (global::System.Exception)
		{
			return false;
		}
	}

	public static void LuaPipe(string script)
	{
		string script2 = script;
		if (NamedPipeExist(luapipename))
		{
			new Thread((ThreadStart)delegate
			{
				try
				{
					NamedPipeClientStream val = new NamedPipeClientStream(".", luapipename, (PipeDirection)2);
					try
					{
						val.Connect();
						StreamWriter val2 = new StreamWriter((Stream)(object)val, Encoding.Default, 999999);
						try
						{
							((TextWriter)val2).Write(script2);
							((TextWriter)val2).Dispose();
						}
						finally
						{
							((global::System.IDisposable)val2)?.Dispose();
						}
						((Stream)val).Dispose();
					}
					finally
					{
						((global::System.IDisposable)val)?.Dispose();
					}
				}
				catch (IOException)
				{
					MessageBox.Show("Error occured connecting to the pipe.", "Connection Failed!");
				}
				catch (global::System.Exception ex)
				{
					MessageBox.Show(((object)ex.Message).ToString());
				}
			}).Start();
		}
		else
		{
			MessageBox.Show("Oh now dada");
		}
	}
}
