using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ExecutorUI;

internal class TCP
{
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass0_0
	{
		public int port;

		internal bool _003CIsPortUnusedAsync_003Eb__0()
		{
			try
			{
				Process val = new Process
				{
					StartInfo = new ProcessStartInfo
					{
						FileName = "cmd.exe",
						Arguments = $"/c netstat -ano | findstr :{port}",
						RedirectStandardOutput = true,
						RedirectStandardError = true,
						UseShellExecute = false,
						CreateNoWindow = true
					}
				};
				val.Start();
				string text = ((TextReader)val.StandardOutput).ReadToEnd();
				((TextReader)val.StandardError).ReadToEnd();
				val.WaitForExit();
				return string.IsNullOrWhiteSpace(text);
			}
			catch (global::System.Exception ex)
			{
				Console.WriteLine("Error checking port: " + ex.Message);
				return false;
			}
		}
	}

	[StructLayout(3)]
	[CompilerGenerated]
	private struct _003CIsPortUnusedAsync_003Ed__0 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder<bool> _003C_003Et__builder;

		public int port;

		private TaskAwaiter<bool> _003C_003Eu__1;

		private void MoveNext()
		{
			int num = _003C_003E1__state;
			bool result;
			try
			{
				TaskAwaiter<bool> awaiter;
				if (num != 0)
				{
					awaiter = global::System.Threading.Tasks.Task.Run<bool>((Func<bool>)new _003C_003Ec__DisplayClass0_0
					{
						port = port
					}._003CIsPortUnusedAsync_003Eb__0).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (_003C_003E1__state = 0);
						_003C_003Eu__1 = awaiter;
						_003C_003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, _003CIsPortUnusedAsync_003Ed__0>(ref awaiter, ref this);
						return;
					}
				}
				else
				{
					awaiter = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter<bool>);
					num = (_003C_003E1__state = -1);
				}
				result = awaiter.GetResult();
			}
			catch (global::System.Exception exception)
			{
				_003C_003E1__state = -2;
				_003C_003Et__builder.SetException(exception);
				return;
			}
			_003C_003E1__state = -2;
			_003C_003Et__builder.SetResult(result);
		}

		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			_003C_003Et__builder.SetStateMachine(stateMachine);
		}
	}

	[StructLayout(3)]
	[CompilerGenerated]
	private struct _003CgetOpenPort_003Ed__1 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder<int> _003C_003Et__builder;

		private int _003CendPort_003E5__2;

		private int _003Cport_003E5__3;

		private TaskAwaiter<bool> _003C_003Eu__1;

		private void MoveNext()
		{
			int num = _003C_003E1__state;
			int result;
			try
			{
				if (num != 0)
				{
					int num2 = 30000;
					_003CendPort_003E5__2 = 65535;
					_003Cport_003E5__3 = num2;
					goto IL_00a1;
				}
				TaskAwaiter<bool> awaiter = _003C_003Eu__1;
				_003C_003Eu__1 = default(TaskAwaiter<bool>);
				num = (_003C_003E1__state = -1);
				goto IL_007d;
				IL_007d:
				if (!awaiter.GetResult())
				{
					_003Cport_003E5__3++;
					goto IL_00a1;
				}
				result = _003Cport_003E5__3;
				goto end_IL_0007;
				IL_00a1:
				if (_003Cport_003E5__3 <= _003CendPort_003E5__2)
				{
					awaiter = IsPortUnusedAsync(_003Cport_003E5__3).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (_003C_003E1__state = 0);
						_003C_003Eu__1 = awaiter;
						_003C_003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, _003CgetOpenPort_003Ed__1>(ref awaiter, ref this);
						return;
					}
					goto IL_007d;
				}
				result = -1;
				end_IL_0007:;
			}
			catch (global::System.Exception exception)
			{
				_003C_003E1__state = -2;
				_003C_003Et__builder.SetException(exception);
				return;
			}
			_003C_003E1__state = -2;
			_003C_003Et__builder.SetResult(result);
		}

		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			_003C_003Et__builder.SetStateMachine(stateMachine);
		}
	}

	[AsyncStateMachine(typeof(_003CIsPortUnusedAsync_003Ed__0))]
	public static async global::System.Threading.Tasks.Task<bool> IsPortUnusedAsync(int port)
	{
		return await global::System.Threading.Tasks.Task.Run<bool>((Func<bool>)delegate
		{
			try
			{
				Process val = new Process
				{
					StartInfo = new ProcessStartInfo
					{
						FileName = "cmd.exe",
						Arguments = $"/c netstat -ano | findstr :{port}",
						RedirectStandardOutput = true,
						RedirectStandardError = true,
						UseShellExecute = false,
						CreateNoWindow = true
					}
				};
				val.Start();
				string text = ((TextReader)val.StandardOutput).ReadToEnd();
				((TextReader)val.StandardError).ReadToEnd();
				val.WaitForExit();
				return string.IsNullOrWhiteSpace(text);
			}
			catch (global::System.Exception ex)
			{
				Console.WriteLine("Error checking port: " + ex.Message);
				return false;
			}
		});
	}

	[AsyncStateMachine(typeof(_003CgetOpenPort_003Ed__1))]
	public static async global::System.Threading.Tasks.Task<int> getOpenPort()
	{
		int num = 30000;
		int endPort = 65535;
		for (int port = num; port <= endPort; port++)
		{
			if (await IsPortUnusedAsync(port))
			{
				return port;
			}
		}
		return -1;
	}
}
