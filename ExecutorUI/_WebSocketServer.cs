using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExecutorUI;

internal class _WebSocketServer
{
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass4_0
	{
		public HttpListenerWebSocketContext wsContext;

		internal global::System.Threading.Tasks.Task? _003CStartServer_003Eb__0()
		{
			return HandleClient(((WebSocketContext)wsContext).WebSocket);
		}
	}

	[StructLayout(3)]
	[CompilerGenerated]
	private struct _003CBroadcastMessage_003Ed__2 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public string message;

		private byte[] _003CmessageBytes_003E5__2;

		private global::System.Collections.Generic.IEnumerator<WebSocket> _003C_003E7__wrap2;

		private TaskAwaiter _003C_003Eu__1;

		private void MoveNext()
		{
			int num = _003C_003E1__state;
			try
			{
				if (num != 0)
				{
					_003CmessageBytes_003E5__2 = Encoding.UTF8.GetBytes(message);
					_003C_003E7__wrap2 = _clients.GetEnumerator();
				}
				try
				{
					if (num == 0)
					{
						goto IL_004f;
					}
					goto IL_00c4;
					IL_00c4:
					WebSocket current = default(WebSocket);
					while (((global::System.Collections.IEnumerator)_003C_003E7__wrap2).MoveNext())
					{
						current = _003C_003E7__wrap2.Current;
						if ((int)current.State != 2)
						{
							continue;
						}
						goto IL_004f;
					}
					goto end_IL_0031;
					IL_004f:
					try
					{
						TaskAwaiter awaiter;
						if (num != 0)
						{
							awaiter = current.SendAsync(new ArraySegment<byte>(_003CmessageBytes_003E5__2), (WebSocketMessageType)0, true, CancellationToken.None).GetAwaiter();
							if (!((TaskAwaiter)(ref awaiter)).IsCompleted)
							{
								num = (_003C_003E1__state = 0);
								_003C_003Eu__1 = awaiter;
								((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003CBroadcastMessage_003Ed__2>(ref awaiter, ref this);
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
					catch (global::System.Exception)
					{
					}
					goto IL_00c4;
					end_IL_0031:;
				}
				finally
				{
					if (num < 0 && _003C_003E7__wrap2 != null)
					{
						((global::System.IDisposable)_003C_003E7__wrap2).Dispose();
					}
				}
				_003C_003E7__wrap2 = null;
			}
			catch (global::System.Exception exception)
			{
				_003C_003E1__state = -2;
				_003CmessageBytes_003E5__2 = null;
				((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).SetException(exception);
				return;
			}
			_003C_003E1__state = -2;
			_003CmessageBytes_003E5__2 = null;
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
	private struct _003CHandleClient_003Ed__1 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public WebSocket webSocket;

		private byte[] _003Cbuffer_003E5__2;

		private TaskAwaiter<WebSocketReceiveResult> _003C_003Eu__1;

		private TaskAwaiter _003C_003Eu__2;

		private void MoveNext()
		{
			int num = _003C_003E1__state;
			try
			{
				if ((uint)num > 2u)
				{
					_clients.Add(webSocket);
					_003Cbuffer_003E5__2 = new byte[4096];
				}
				WebSocket val = default(WebSocket);
				try
				{
					if ((uint)num <= 2u)
					{
						goto IL_0034;
					}
					goto IL_01ce;
					IL_0034:
					try
					{
						TaskAwaiter<WebSocketReceiveResult> awaiter2;
						TaskAwaiter awaiter;
						WebSocketReceiveResult result;
						string @string;
						switch (num)
						{
						default:
							awaiter2 = webSocket.ReceiveAsync(new ArraySegment<byte>(_003Cbuffer_003E5__2), CancellationToken.None).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								num = (_003C_003E1__state = 0);
								_003C_003Eu__1 = awaiter2;
								((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter<WebSocketReceiveResult>, _003CHandleClient_003Ed__1>(ref awaiter2, ref this);
								return;
							}
							goto IL_00af;
						case 0:
							awaiter2 = _003C_003Eu__1;
							_003C_003Eu__1 = default(TaskAwaiter<WebSocketReceiveResult>);
							num = (_003C_003E1__state = -1);
							goto IL_00af;
						case 1:
							awaiter = _003C_003Eu__2;
							_003C_003Eu__2 = default(TaskAwaiter);
							num = (_003C_003E1__state = -1);
							goto IL_013b;
						case 2:
							{
								awaiter = _003C_003Eu__2;
								_003C_003Eu__2 = default(TaskAwaiter);
								num = (_003C_003E1__state = -1);
								break;
							}
							IL_013b:
							((TaskAwaiter)(ref awaiter)).GetResult();
							goto end_IL_0034;
							IL_00af:
							result = awaiter2.GetResult();
							if ((int)result.MessageType == 2)
							{
								_clients.TryTake(ref val);
								awaiter = webSocket.CloseAsync((WebSocketCloseStatus)1000, "Closing", CancellationToken.None).GetAwaiter();
								if (!((TaskAwaiter)(ref awaiter)).IsCompleted)
								{
									num = (_003C_003E1__state = 1);
									_003C_003Eu__2 = awaiter;
									((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003CHandleClient_003Ed__1>(ref awaiter, ref this);
									return;
								}
								goto IL_013b;
							}
							@string = Encoding.UTF8.GetString(_003Cbuffer_003E5__2, 0, result.Count);
							awaiter = BroadcastMessage("Broadcast: " + @string).GetAwaiter();
							if (!((TaskAwaiter)(ref awaiter)).IsCompleted)
							{
								num = (_003C_003E1__state = 2);
								_003C_003Eu__2 = awaiter;
								((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003CHandleClient_003Ed__1>(ref awaiter, ref this);
								return;
							}
							break;
						}
						((TaskAwaiter)(ref awaiter)).GetResult();
						end_IL_0034:;
					}
					catch (global::System.Exception)
					{
					}
					goto IL_01ce;
					IL_01ce:
					if ((int)webSocket.State == 2)
					{
						goto IL_0034;
					}
				}
				catch (global::System.Exception)
				{
				}
				finally
				{
					if (num < 0)
					{
						_clients.TryTake(ref val);
					}
				}
			}
			catch (global::System.Exception exception)
			{
				_003C_003E1__state = -2;
				_003Cbuffer_003E5__2 = null;
				((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).SetException(exception);
				return;
			}
			_003C_003E1__state = -2;
			_003Cbuffer_003E5__2 = null;
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
	private struct _003CMain_003Ed__5 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		private TaskAwaiter _003C_003Eu__1;

		private void MoveNext()
		{
			int num = _003C_003E1__state;
			try
			{
				TaskAwaiter awaiter;
				if (num != 0)
				{
					awaiter = StartServer("http://localhost:8080/").GetAwaiter();
					if (!((TaskAwaiter)(ref awaiter)).IsCompleted)
					{
						num = (_003C_003E1__state = 0);
						_003C_003Eu__1 = awaiter;
						((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003CMain_003Ed__5>(ref awaiter, ref this);
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
	private struct _003CStartServer_003Ed__4 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public string uriPrefix;

		private _003C_003Ec__DisplayClass4_0 _003C_003E8__1;

		private HttpListener _003ChttpListener_003E5__2;

		private TaskAwaiter<HttpListenerContext> _003C_003Eu__1;

		private TaskAwaiter<HttpListenerWebSocketContext> _003C_003Eu__2;

		private void MoveNext()
		{
			int num = _003C_003E1__state;
			try
			{
				TaskAwaiter<HttpListenerWebSocketContext> awaiter;
				if (num != 0)
				{
					if (num != 1)
					{
						_003ChttpListener_003E5__2 = new HttpListener();
						_003ChttpListener_003E5__2.Prefixes.Add(uriPrefix);
						_003ChttpListener_003E5__2.Start();
						goto IL_003d;
					}
					awaiter = _003C_003Eu__2;
					_003C_003Eu__2 = default(TaskAwaiter<HttpListenerWebSocketContext>);
					num = (_003C_003E1__state = -1);
					goto IL_0111;
				}
				TaskAwaiter<HttpListenerContext> awaiter2 = _003C_003Eu__1;
				_003C_003Eu__1 = default(TaskAwaiter<HttpListenerContext>);
				num = (_003C_003E1__state = -1);
				goto IL_0096;
				IL_003d:
				awaiter2 = _003ChttpListener_003E5__2.GetContextAsync().GetAwaiter();
				if (!awaiter2.IsCompleted)
				{
					num = (_003C_003E1__state = 0);
					_003C_003Eu__1 = awaiter2;
					((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter<HttpListenerContext>, _003CStartServer_003Ed__4>(ref awaiter2, ref this);
					return;
				}
				goto IL_0096;
				IL_0096:
				HttpListenerContext result = awaiter2.GetResult();
				if (result.Request.IsWebSocketRequest)
				{
					_003C_003E8__1 = new _003C_003Ec__DisplayClass4_0();
					awaiter = result.AcceptWebSocketAsync((string)null).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (_003C_003E1__state = 1);
						_003C_003Eu__2 = awaiter;
						((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter<HttpListenerWebSocketContext>, _003CStartServer_003Ed__4>(ref awaiter, ref this);
						return;
					}
					goto IL_0111;
				}
				result.Response.StatusCode = 400;
				result.Response.Close();
				goto IL_003d;
				IL_0111:
				HttpListenerWebSocketContext result2 = awaiter.GetResult();
				_003C_003E8__1.wsContext = result2;
				global::System.Threading.Tasks.Task.Run((Func<global::System.Threading.Tasks.Task>)(() => HandleClient(((WebSocketContext)_003C_003E8__1.wsContext).WebSocket)));
				_003C_003E8__1 = null;
				goto IL_003d;
			}
			catch (global::System.Exception exception)
			{
				_003C_003E1__state = -2;
				_003ChttpListener_003E5__2 = null;
				((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).SetException(exception);
			}
		}

		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).SetStateMachine(stateMachine);
		}
	}

	private static ConcurrentBag<WebSocket> _clients = new ConcurrentBag<WebSocket>();

	[AsyncStateMachine(typeof(_003CHandleClient_003Ed__1))]
	private static global::System.Threading.Tasks.Task HandleClient(WebSocket webSocket)
	{
		_003CHandleClient_003Ed__1 _003CHandleClient_003Ed__ = default(_003CHandleClient_003Ed__1);
		_003CHandleClient_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
		_003CHandleClient_003Ed__.webSocket = webSocket;
		_003CHandleClient_003Ed__._003C_003E1__state = -1;
		((AsyncTaskMethodBuilder)(ref _003CHandleClient_003Ed__._003C_003Et__builder)).Start<_003CHandleClient_003Ed__1>(ref _003CHandleClient_003Ed__);
		return ((AsyncTaskMethodBuilder)(ref _003CHandleClient_003Ed__._003C_003Et__builder)).Task;
	}

	[AsyncStateMachine(typeof(_003CBroadcastMessage_003Ed__2))]
	public static global::System.Threading.Tasks.Task BroadcastMessage(string message)
	{
		_003CBroadcastMessage_003Ed__2 _003CBroadcastMessage_003Ed__ = default(_003CBroadcastMessage_003Ed__2);
		_003CBroadcastMessage_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
		_003CBroadcastMessage_003Ed__.message = message;
		_003CBroadcastMessage_003Ed__._003C_003E1__state = -1;
		((AsyncTaskMethodBuilder)(ref _003CBroadcastMessage_003Ed__._003C_003Et__builder)).Start<_003CBroadcastMessage_003Ed__2>(ref _003CBroadcastMessage_003Ed__);
		return ((AsyncTaskMethodBuilder)(ref _003CBroadcastMessage_003Ed__._003C_003Et__builder)).Task;
	}

	public static ConcurrentBag<WebSocket> GetClients()
	{
		return _clients;
	}

	[AsyncStateMachine(typeof(_003CStartServer_003Ed__4))]
	public static global::System.Threading.Tasks.Task StartServer(string uriPrefix)
	{
		_003CStartServer_003Ed__4 _003CStartServer_003Ed__ = default(_003CStartServer_003Ed__4);
		_003CStartServer_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
		_003CStartServer_003Ed__.uriPrefix = uriPrefix;
		_003CStartServer_003Ed__._003C_003E1__state = -1;
		((AsyncTaskMethodBuilder)(ref _003CStartServer_003Ed__._003C_003Et__builder)).Start<_003CStartServer_003Ed__4>(ref _003CStartServer_003Ed__);
		return ((AsyncTaskMethodBuilder)(ref _003CStartServer_003Ed__._003C_003Et__builder)).Task;
	}

	[AsyncStateMachine(typeof(_003CMain_003Ed__5))]
	public static global::System.Threading.Tasks.Task Main(string[] args)
	{
		_003CMain_003Ed__5 _003CMain_003Ed__ = default(_003CMain_003Ed__5);
		_003CMain_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
		_003CMain_003Ed__._003C_003E1__state = -1;
		((AsyncTaskMethodBuilder)(ref _003CMain_003Ed__._003C_003Et__builder)).Start<_003CMain_003Ed__5>(ref _003CMain_003Ed__);
		return ((AsyncTaskMethodBuilder)(ref _003CMain_003Ed__._003C_003Et__builder)).Task;
	}
}
