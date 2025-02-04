using System;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExecutorUI;

public class _WebSocketClient
{
	[StructLayout(3)]
	[CompilerGenerated]
	private struct _003CCloseAsync_003Ed__10 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public _WebSocketClient _003C_003E4__this;

		private TaskAwaiter _003C_003Eu__1;

		private void MoveNext()
		{
			int num = _003C_003E1__state;
			_WebSocketClient webSocketClient = _003C_003E4__this;
			try
			{
				TaskAwaiter awaiter;
				if (num == 0)
				{
					awaiter = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_009d;
				}
				if ((int)((WebSocket)webSocketClient._webSocket).State == 2 || (int)((WebSocket)webSocketClient._webSocket).State == 4)
				{
					awaiter = ((WebSocket)webSocketClient._webSocket).CloseAsync((WebSocketCloseStatus)1000, "Closing", webSocketClient._cancellationTokenSource.Token).GetAwaiter();
					if (!((TaskAwaiter)(ref awaiter)).IsCompleted)
					{
						num = (_003C_003E1__state = 0);
						_003C_003Eu__1 = awaiter;
						((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003CCloseAsync_003Ed__10>(ref awaiter, ref this);
						return;
					}
					goto IL_009d;
				}
				goto end_IL_000e;
				IL_009d:
				((TaskAwaiter)(ref awaiter)).GetResult();
				end_IL_000e:;
			}
			catch (global::System.Exception exception)
			{
				_003C_003E1__state = -2;
				((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).SetException(exception);
				return;
			}
			_003C_003E1__state = -2;
			((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).SetResult();
		}

		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).SetStateMachine(stateMachine);
		}
	}

	[StructLayout(3)]
	[CompilerGenerated]
	private struct _003CConnectAsync_003Ed__7 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public _WebSocketClient _003C_003E4__this;

		public Uri uri;

		private CancellationTokenSource _003Ccts_003E5__2;

		private TaskAwaiter _003C_003Eu__1;

		private void MoveNext()
		{
			int num = _003C_003E1__state;
			_WebSocketClient webSocketClient = _003C_003E4__this;
			try
			{
				if (num != 0)
				{
					if ((int)((WebSocket)webSocketClient._webSocket).State == 2)
					{
						throw new InvalidOperationException("WebSocket is already connected.");
					}
					_003Ccts_003E5__2 = new CancellationTokenSource(webSocketClient.OperationTimeout);
				}
				try
				{
					TaskAwaiter awaiter;
					if (num != 0)
					{
						awaiter = webSocketClient._webSocket.ConnectAsync(uri, _003Ccts_003E5__2.Token).GetAwaiter();
						if (!((TaskAwaiter)(ref awaiter)).IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = awaiter;
							((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003CConnectAsync_003Ed__7>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(TaskAwaiter);
						num = (_003C_003E1__state = -1);
					}
					((TaskAwaiter)(ref awaiter)).GetResult();
				}
				catch (OperationCanceledException)
				{
					throw new TimeoutException("Failed to connect within the timeout period.");
				}
				finally
				{
					if (num < 0 && _003Ccts_003E5__2 != null)
					{
						((global::System.IDisposable)_003Ccts_003E5__2).Dispose();
					}
				}
				_003Ccts_003E5__2 = null;
			}
			catch (global::System.Exception exception)
			{
				_003C_003E1__state = -2;
				((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).SetException(exception);
				return;
			}
			_003C_003E1__state = -2;
			((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).SetResult();
		}

		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).SetStateMachine(stateMachine);
		}
	}

	[StructLayout(3)]
	[CompilerGenerated]
	private struct _003CReceiveAsync_003Ed__9 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder<string> _003C_003Et__builder;

		public _WebSocketClient _003C_003E4__this;

		private byte[] _003Cbuffer_003E5__2;

		private CancellationTokenSource _003Ccts_003E5__3;

		private TaskAwaiter<WebSocketReceiveResult> _003C_003Eu__1;

		private TaskAwaiter _003C_003Eu__2;

		private void MoveNext()
		{
			int num = _003C_003E1__state;
			_WebSocketClient webSocketClient = _003C_003E4__this;
			string result2;
			try
			{
				if ((uint)num > 1u)
				{
					if ((int)((WebSocket)webSocketClient._webSocket).State != 2)
					{
						throw new InvalidOperationException("WebSocket is not connected.");
					}
					_003Cbuffer_003E5__2 = new byte[1024];
					_003Ccts_003E5__3 = new CancellationTokenSource(webSocketClient.OperationTimeout);
				}
				try
				{
					_ = 1;
					try
					{
						TaskAwaiter awaiter;
						TaskAwaiter<WebSocketReceiveResult> awaiter2;
						if (num != 0)
						{
							if (num == 1)
							{
								awaiter = _003C_003Eu__2;
								_003C_003Eu__2 = default(TaskAwaiter);
								num = (_003C_003E1__state = -1);
								goto IL_0136;
							}
							awaiter2 = ((WebSocket)webSocketClient._webSocket).ReceiveAsync(new ArraySegment<byte>(_003Cbuffer_003E5__2), _003Ccts_003E5__3.Token).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								num = (_003C_003E1__state = 0);
								_003C_003Eu__1 = awaiter2;
								_003C_003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter<WebSocketReceiveResult>, _003CReceiveAsync_003Ed__9>(ref awaiter2, ref this);
								return;
							}
						}
						else
						{
							awaiter2 = _003C_003Eu__1;
							_003C_003Eu__1 = default(TaskAwaiter<WebSocketReceiveResult>);
							num = (_003C_003E1__state = -1);
						}
						WebSocketReceiveResult result = awaiter2.GetResult();
						if ((int)result.MessageType == 2)
						{
							awaiter = webSocketClient.CloseAsync().GetAwaiter();
							if (!((TaskAwaiter)(ref awaiter)).IsCompleted)
							{
								num = (_003C_003E1__state = 1);
								_003C_003Eu__2 = awaiter;
								_003C_003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter, _003CReceiveAsync_003Ed__9>(ref awaiter, ref this);
								return;
							}
							goto IL_0136;
						}
						result2 = Encoding.UTF8.GetString(_003Cbuffer_003E5__2, 0, result.Count);
						goto end_IL_0052;
						IL_0136:
						((TaskAwaiter)(ref awaiter)).GetResult();
						result2 = null;
						end_IL_0052:;
					}
					catch (OperationCanceledException)
					{
						throw new TimeoutException("Failed to receive a message within the timeout period.");
					}
				}
				finally
				{
					if (num < 0 && _003Ccts_003E5__3 != null)
					{
						((global::System.IDisposable)_003Ccts_003E5__3).Dispose();
					}
				}
			}
			catch (global::System.Exception exception)
			{
				_003C_003E1__state = -2;
				_003Cbuffer_003E5__2 = null;
				_003C_003Et__builder.SetException(exception);
				return;
			}
			_003C_003E1__state = -2;
			_003Cbuffer_003E5__2 = null;
			_003C_003Et__builder.SetResult(result2);
		}

		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			_003C_003Et__builder.SetStateMachine(stateMachine);
		}
	}

	[StructLayout(3)]
	[CompilerGenerated]
	private struct _003CSendAsync_003Ed__8 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public _WebSocketClient _003C_003E4__this;

		public string message;

		private CancellationTokenSource _003Ccts_003E5__2;

		private TaskAwaiter _003C_003Eu__1;

		private void MoveNext()
		{

			int num = _003C_003E1__state;
			_WebSocketClient webSocketClient = _003C_003E4__this;
			try
			{
				byte[] bytes = default(byte[]);
				if (num != 0)
				{
					if ((int)((WebSocket)webSocketClient._webSocket).State != 2)
					{
						throw new InvalidOperationException("WebSocket is not connected.");
					}
					bytes = Encoding.UTF8.GetBytes(message);
					_003Ccts_003E5__2 = new CancellationTokenSource(webSocketClient.OperationTimeout);
				}
				try
				{
					TaskAwaiter awaiter;
					if (num != 0)
					{
						awaiter = ((WebSocket)webSocketClient._webSocket).SendAsync(new ArraySegment<byte>(bytes), (WebSocketMessageType)0, true, _003Ccts_003E5__2.Token).GetAwaiter();
						if (!((TaskAwaiter)(ref awaiter)).IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = awaiter;
							((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003CSendAsync_003Ed__8>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(TaskAwaiter);
						num = (_003C_003E1__state = -1);
					}
					((TaskAwaiter)(ref awaiter)).GetResult();
				}
				catch (OperationCanceledException)
				{
					throw new TimeoutException("Failed to send the message within the timeout period.");
				}
				finally
				{
					if (num < 0 && _003Ccts_003E5__2 != null)
					{
						((global::System.IDisposable)_003Ccts_003E5__2).Dispose();
					}
				}
				_003Ccts_003E5__2 = null;
			}
			catch (global::System.Exception exception)
			{
				_003C_003E1__state = -2;
				((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).SetException(exception);
				return;
			}
			_003C_003E1__state = -2;
			((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).SetResult();
		}

		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).SetStateMachine(stateMachine);
		}
	}

	private readonly ClientWebSocket _webSocket;

	private readonly CancellationTokenSource _cancellationTokenSource;

	[field: CompilerGenerated]
	public TimeSpan OperationTimeout
	{
		[CompilerGenerated]
		get;
		[CompilerGenerated]
		set;
	} = TimeSpan.FromSeconds(5.0);


	public _WebSocketClient()
	{
		_webSocket = new ClientWebSocket();
		_cancellationTokenSource = new CancellationTokenSource();
	}

	[AsyncStateMachine(typeof(_003CConnectAsync_003Ed__7))]
	public global::System.Threading.Tasks.Task ConnectAsync(Uri uri)
	{

		_003CConnectAsync_003Ed__7 _003CConnectAsync_003Ed__ = default(_003CConnectAsync_003Ed__7);
		_003CConnectAsync_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
		_003CConnectAsync_003Ed__._003C_003E4__this = this;
		_003CConnectAsync_003Ed__.uri = uri;
		_003CConnectAsync_003Ed__._003C_003E1__state = -1;
		((AsyncTaskMethodBuilder)(ref _003CConnectAsync_003Ed__._003C_003Et__builder)).Start<_003CConnectAsync_003Ed__7>(ref _003CConnectAsync_003Ed__);
		return ((AsyncTaskMethodBuilder)(ref _003CConnectAsync_003Ed__._003C_003Et__builder)).Task;
	}

	[AsyncStateMachine(typeof(_003CSendAsync_003Ed__8))]
	public global::System.Threading.Tasks.Task SendAsync(string message)
	{

		_003CSendAsync_003Ed__8 _003CSendAsync_003Ed__ = default(_003CSendAsync_003Ed__8);
		_003CSendAsync_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
		_003CSendAsync_003Ed__._003C_003E4__this = this;
		_003CSendAsync_003Ed__.message = message;
		_003CSendAsync_003Ed__._003C_003E1__state = -1;
		((AsyncTaskMethodBuilder)(ref _003CSendAsync_003Ed__._003C_003Et__builder)).Start<_003CSendAsync_003Ed__8>(ref _003CSendAsync_003Ed__);
		return ((AsyncTaskMethodBuilder)(ref _003CSendAsync_003Ed__._003C_003Et__builder)).Task;
	}

	[AsyncStateMachine(typeof(_003CReceiveAsync_003Ed__9))]
	public async global::System.Threading.Tasks.Task<string> ReceiveAsync()
	{

		if ((int)((WebSocket)_webSocket).State != 2)
		{
			throw new InvalidOperationException("WebSocket is not connected.");
		}
		byte[] buffer = new byte[1024];
		CancellationTokenSource cts = new CancellationTokenSource(OperationTimeout);
		try
		{
			_ = 1;
			try
			{
				WebSocketReceiveResult val = await ((WebSocket)_webSocket).ReceiveAsync(new ArraySegment<byte>(buffer), cts.Token);
				if ((int)val.MessageType == 2)
				{
					await CloseAsync();
					return null;
				}
				return Encoding.UTF8.GetString(buffer, 0, val.Count);
			}
			catch (OperationCanceledException)
			{
				throw new TimeoutException("Failed to receive a message within the timeout period.");
			}
		}
		finally
		{
			((global::System.IDisposable)cts)?.Dispose();
		}
	}

	[AsyncStateMachine(typeof(_003CCloseAsync_003Ed__10))]
	public global::System.Threading.Tasks.Task CloseAsync()
	{
		_003CCloseAsync_003Ed__10 _003CCloseAsync_003Ed__ = default(_003CCloseAsync_003Ed__10);
		_003CCloseAsync_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
		_003CCloseAsync_003Ed__._003C_003E4__this = this;
		_003CCloseAsync_003Ed__._003C_003E1__state = -1;
		((AsyncTaskMethodBuilder)(ref _003CCloseAsync_003Ed__._003C_003Et__builder)).Start<_003CCloseAsync_003Ed__10>(ref _003CCloseAsync_003Ed__);
		return ((AsyncTaskMethodBuilder)(ref _003CCloseAsync_003Ed__._003C_003Et__builder)).Task;
	}

	public void Dispose()
	{
		ClientWebSocket webSocket = _webSocket;
		if (webSocket != null)
		{
			((WebSocket)webSocket).Dispose();
		}
		CancellationTokenSource cancellationTokenSource = _cancellationTokenSource;
		if (cancellationTokenSource != null)
		{
			cancellationTokenSource.Cancel();
		}
		CancellationTokenSource cancellationTokenSource2 = _cancellationTokenSource;
		if (cancellationTokenSource2 != null)
		{
			cancellationTokenSource2.Dispose();
		}
	}
}
