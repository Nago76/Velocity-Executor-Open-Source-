using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Threading;
using System.Xml;
using Microsoft.Web.WebView2.Wpf;
using Microsoft.Win32;

namespace ExecutorUI;

public class MainWindow : Window, IComponentConnector
{
	[StructLayout(3)]
	[CompilerGenerated]
	private struct _003C_003CWindow_Loaded_003Eb__16_0_003Ed : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public MainWindow _003C_003E4__this;

		private TaskAwaiter<int> _003C_003Eu__1;

		private void MoveNext()
		{
			int num = _003C_003E1__state;
			MainWindow mainWindow = _003C_003E4__this;
			try
			{
				TaskAwaiter<int> awaiter;
				if (num != 0)
				{
					awaiter = TCP.getOpenPort().GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (_003C_003E1__state = 0);
						_003C_003Eu__1 = awaiter;
						((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter<int>, _003C_003CWindow_Loaded_003Eb__16_0_003Ed>(ref awaiter, ref this);
						return;
					}
				}
				else
				{
					awaiter = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter<int>);
					num = (_003C_003E1__state = -1);
				}
				int result = awaiter.GetResult();
				mainWindow.openPortNumber = result;
				if (mainWindow.openPortNumber == -1)
				{
					MessageBox.Show("Failed to open communication bridge, try again");
					Application.Current.Shutdown();
				}
				_WebSocketServer.StartServer($"http://localhost:{mainWindow.openPortNumber}/");
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

	[Serializable]
	[CompilerGenerated]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

		public static UnhandledExceptionEventHandler _003C_003E9__10_0;

		public static EventHandler<UnobservedTaskExceptionEventArgs> _003C_003E9__10_1;

		internal void _003C_002Ector_003Eb__10_0(object sender, UnhandledExceptionEventArgs args)
		{
		}

		internal void _003C_002Ector_003Eb__10_1(object? sender, UnobservedTaskExceptionEventArgs args)
		{
			args.SetObserved();
		}
	}

	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass16_0
	{
		public Process[] robloxProcesses;

		public MainWindow _003C_003E4__this;

		internal void _003CWindow_Loaded_003Eb__4()
		{
			if (robloxProcesses.Length == 0)
			{
				((UIElement)_003C_003E4__this.NoClientsFoundLabel).Visibility = (Visibility)0;
			}
			else
			{
				((UIElement)_003C_003E4__this.NoClientsFoundLabel).Visibility = (Visibility)1;
			}
		}
	}

	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass16_1
	{
		public Process robloxProcess;

		public _003C_003Ec__DisplayClass16_0 CS_0024_003C_003E8__locals1;

		internal void _003CWindow_Loaded_003Eb__6()
		{
			if (CS_0024_003C_003E8__locals1._003C_003E4__this.clientTemplateXml.Length <= 0)
			{
				CS_0024_003C_003E8__locals1._003C_003E4__this.clientTemplateXml = XamlWriter.Save((object)CS_0024_003C_003E8__locals1._003C_003E4__this.client_template);
				((Panel)CS_0024_003C_003E8__locals1._003C_003E4__this.clients_container).Children.Remove((UIElement)(object)CS_0024_003C_003E8__locals1._003C_003E4__this.client_template);
			}
			Grid val = (Grid)XamlReader.Load(XmlReader.Create((TextReader)new StringReader(CS_0024_003C_003E8__locals1._003C_003E4__this.clientTemplateXml)));
			((FrameworkElement)val).Name = "rbx_client_" + robloxProcess.Id;
			((UIElement)val).Visibility = (Visibility)0;
			((Panel)CS_0024_003C_003E8__locals1._003C_003E4__this.clients_container).Children.Add((UIElement)(object)val);
		}
	}

	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass16_2
	{
		public int STR_PID_TO_INT;

		public MainWindow _003C_003E4__this;
	}

	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass16_3
	{
		public CheckBox client_inject_checkbox;

		public _003C_003Ec__DisplayClass16_2 CS_0024_003C_003E8__locals2;

		internal void _003CWindow_Loaded_003Eb__7(object sender, RoutedEventArgs e)
		{
			if (((ToggleButton)client_inject_checkbox).IsChecked.GetValueOrDefault())
			{
				if (!CS_0024_003C_003E8__locals2._003C_003E4__this.selected_clients.Contains(CS_0024_003C_003E8__locals2.STR_PID_TO_INT))
				{
					CS_0024_003C_003E8__locals2._003C_003E4__this.selected_clients.Add(CS_0024_003C_003E8__locals2.STR_PID_TO_INT);
				}
			}
			else
			{
				CS_0024_003C_003E8__locals2._003C_003E4__this.selected_clients.Remove(CS_0024_003C_003E8__locals2.STR_PID_TO_INT);
			}
		}
	}

	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass16_4
	{
		public CheckBox client_execute_checkbox;

		public _003C_003Ec__DisplayClass16_2 CS_0024_003C_003E8__locals3;

		internal void _003CWindow_Loaded_003Eb__8(object sender, RoutedEventArgs e)
		{
			if (((ToggleButton)client_execute_checkbox).IsChecked.GetValueOrDefault())
			{
				if (!CS_0024_003C_003E8__locals3._003C_003E4__this.allowed_to_execute_clients.Contains(CS_0024_003C_003E8__locals3.STR_PID_TO_INT))
				{
					CS_0024_003C_003E8__locals3._003C_003E4__this.allowed_to_execute_clients.Add(CS_0024_003C_003E8__locals3.STR_PID_TO_INT);
				}
			}
			else
			{
				CS_0024_003C_003E8__locals3._003C_003E4__this.allowed_to_execute_clients.Remove(CS_0024_003C_003E8__locals3.STR_PID_TO_INT);
			}
		}
	}

	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass16_5
	{
		public Label hashLabel;

		public _003C_003Ec__DisplayClass16_2 CS_0024_003C_003E8__locals4;

		internal void _003CWindow_Loaded_003Eb__9()
		{
			((ContentControl)hashLabel).Content = "Client PID: " + CS_0024_003C_003E8__locals4.STR_PID_TO_INT;
		}
	}

	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass16_6
	{
		public Label statusLabel;

		public _003C_003Ec__DisplayClass16_2 CS_0024_003C_003E8__locals5;

		internal void _003CWindow_Loaded_003Eb__10()
		{
			bool num = CS_0024_003C_003E8__locals5._003C_003E4__this.injected_pids.Contains(CS_0024_003C_003E8__locals5.STR_PID_TO_INT);
			if (num)
			{
				((ContentControl)statusLabel).Content = "Status: Attached";
			}
			if (!num)
			{
				((ContentControl)statusLabel).Content = "Status: Not Attached";
			}
		}
	}

	[StructLayout(3)]
	[CompilerGenerated]
	private struct _003CClientsButton_Click_003Ed__24 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncVoidMethodBuilder _003C_003Et__builder;

		public MainWindow _003C_003E4__this;

		private void MoveNext()
		{
			MainWindow mainWindow = _003C_003E4__this;
			try
			{
				if ((int)((UIElement)mainWindow.ExecutorTextBox).Visibility != 1)
				{
					((UIElement)mainWindow.ExecutorTextBox).Visibility = (Visibility)1;
					((UIElement)mainWindow.ClientsFrame).Visibility = (Visibility)0;
					((ContentControl)mainWindow.ClientsButton).Content = "Exit Clients";
					((Control)mainWindow.ClientsButton).FontSize = 11.0;
				}
				else
				{
					((UIElement)mainWindow.ExecutorTextBox).Visibility = (Visibility)0;
					((UIElement)mainWindow.ClientsFrame).Visibility = (Visibility)1;
					((ContentControl)mainWindow.ClientsButton).Content = "Clients";
					((Control)mainWindow.ClientsButton).FontSize = 12.0;
				}
			}
			catch (global::System.Exception exception)
			{
				_003C_003E1__state = -2;
				((AsyncVoidMethodBuilder)(ref _003C_003Et__builder)).SetException(exception);
				return;
			}
			_003C_003E1__state = -2;
			((AsyncVoidMethodBuilder)(ref _003C_003Et__builder)).SetResult();
		}

		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			((AsyncVoidMethodBuilder)(ref _003C_003Et__builder)).SetStateMachine(stateMachine);
		}
	}

	[StructLayout(3)]
	[CompilerGenerated]
	private struct _003CCopyFileAsync_003Ed__21 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public string sourceFile;

		public string destinationFile;

		private FileStream _003CsourceStream_003E5__2;

		private FileStream _003CdestinationStream_003E5__3;

		private TaskAwaiter _003C_003Eu__1;

		private void MoveNext()
		{
			int num = _003C_003E1__state;
			try
			{
				if (num != 0)
				{
					_003CsourceStream_003E5__2 = new FileStream(sourceFile, (FileMode)3, (FileAccess)1, (FileShare)1, 4096, (FileOptions)1207959552);
				}
				try
				{
					if (num != 0)
					{
						_003CdestinationStream_003E5__3 = new FileStream(destinationFile, (FileMode)1, (FileAccess)2, (FileShare)0, 4096, (FileOptions)1207959552);
					}
					try
					{
						TaskAwaiter awaiter;
						if (num != 0)
						{
							awaiter = ((Stream)_003CsourceStream_003E5__2).CopyToAsync((Stream)(object)_003CdestinationStream_003E5__3).GetAwaiter();
							if (!((TaskAwaiter)(ref awaiter)).IsCompleted)
							{
								num = (_003C_003E1__state = 0);
								_003C_003Eu__1 = awaiter;
								((AsyncTaskMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter, _003CCopyFileAsync_003Ed__21>(ref awaiter, ref this);
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
					finally
					{
						if (num < 0 && _003CdestinationStream_003E5__3 != null)
						{
							((global::System.IDisposable)_003CdestinationStream_003E5__3).Dispose();
						}
					}
					_003CdestinationStream_003E5__3 = null;
				}
				finally
				{
					if (num < 0 && _003CsourceStream_003E5__2 != null)
					{
						((global::System.IDisposable)_003CsourceStream_003E5__2).Dispose();
					}
				}
				_003CsourceStream_003E5__2 = null;
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
	private struct _003CExecuteButton_Click_003Ed__23 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncVoidMethodBuilder _003C_003Et__builder;

		public MainWindow _003C_003E4__this;

		private TaskAwaiter<string> _003C_003Eu__1;

		private void MoveNext()
		{
			int num = _003C_003E1__state;
			MainWindow mainWindow = _003C_003E4__this;
			try
			{
				TaskAwaiter<string> awaiter;
				if (num == 0)
				{
					awaiter = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter<string>);
					num = (_003C_003E1__state = -1);
					goto IL_0091;
				}
				if (_WebSocketServer.GetClients().Count > 0)
				{
					awaiter = mainWindow.ExecutorTextBox.CoreWebView2.ExecuteScriptAsync("ace.edit(\"editor\").getValue()").GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (_003C_003E1__state = 0);
						_003C_003Eu__1 = awaiter;
						((AsyncVoidMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter<string>, _003CExecuteButton_Click_003Ed__23>(ref awaiter, ref this);
						return;
					}
					goto IL_0091;
				}
				MessageBox.Show("Please inject first!");
				goto end_IL_000e;
				IL_0091:
				string result = awaiter.GetResult();
				result = Regex.Unescape(result);
				result = result.Substring(0, result.LastIndexOf('"'));
				result = result.Substring(1, result.Length - 1);
				_WebSocketServer.BroadcastMessage(Base64Encode(((object)result).ToString()));
				end_IL_000e:;
			}
			catch (global::System.Exception exception)
			{
				_003C_003E1__state = -2;
				((AsyncVoidMethodBuilder)(ref _003C_003Et__builder)).SetException(exception);
				return;
			}
			_003C_003E1__state = -2;
			((AsyncVoidMethodBuilder)(ref _003C_003Et__builder)).SetResult();
		}

		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			((AsyncVoidMethodBuilder)(ref _003C_003Et__builder)).SetStateMachine(stateMachine);
		}
	}

	[StructLayout(3)]
	[CompilerGenerated]
	private struct _003CInjectButton_Click_003Ed__22 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncVoidMethodBuilder _003C_003Et__builder;

		public MainWindow _003C_003E4__this;

		private void MoveNext()
		{
			int num = _003C_003E1__state;
			MainWindow mainWindow = _003C_003E4__this;
			try
			{
				Process[] processesByName = Process.GetProcessesByName("RobloxPlayerBeta");
				if (processesByName.Length == 0)
				{
					MessageBox.Show("Launch Roblox First");
				}
				else
				{
					Process[] array = processesByName;
					foreach (Process val in array)
					{
						try
						{
							File.WriteAllText(Path.GetDirectoryName(val.MainModule.FileName) + "\\Port.txt", mainWindow.openPortNumber.ToString());
						}
						catch
						{
						}
					}
					if (mainWindow.selected_clients.Count <= 0)
					{
						MessageBox.Show("You must select 1 or more clients to inject!");
					}
					else
					{
						Enumerator<int> enumerator = mainWindow.selected_clients.GetEnumerator();
						try
						{
							while (enumerator.MoveNext())
							{
								int current = enumerator.Current;
								if (mainWindow.injected_pids.Contains(current))
								{
									continue;
								}
								try
								{
									Process.GetProcessById(current);
									try
									{
										Process.Start(new ProcessStartInfo("Bin\\erto3e4rortoergn.exe", $"--pid={current}")
										{
											CreateNoWindow = false,
											UseShellExecute = false,
											RedirectStandardError = false,
											RedirectStandardOutput = false
										});
										mainWindow.injected_pids.Add(current);
									}
									catch (global::System.Exception)
									{
									}
								}
								catch (ArgumentException)
								{
								}
							}
						}
						finally
						{
							if (num < 0)
							{
								((global::System.IDisposable)enumerator).Dispose();
							}
						}
					}
				}
			}
			catch (global::System.Exception exception)
			{
				_003C_003E1__state = -2;
				((AsyncVoidMethodBuilder)(ref _003C_003Et__builder)).SetException(exception);
				return;
			}
			_003C_003E1__state = -2;
			((AsyncVoidMethodBuilder)(ref _003C_003Et__builder)).SetResult();
		}

		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			((AsyncVoidMethodBuilder)(ref _003C_003Et__builder)).SetStateMachine(stateMachine);
		}
	}

	[StructLayout(3)]
	[CompilerGenerated]
	private struct _003COpenScriptButton_Click_003Ed__25 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncVoidMethodBuilder _003C_003Et__builder;

		public MainWindow _003C_003E4__this;

		private void MoveNext()
		{
			MainWindow mainWindow = _003C_003E4__this;
			try
			{
				OpenFileDialog val = new OpenFileDialog
				{
					Title = "Open a File",
					Filter = "(*.lua)|*.lua|(*.txt)|*.txt|All Files (*.*)|*.*",
					InitialDirectory = Environment.GetFolderPath((SpecialFolder)5)
				};
				if (((CommonDialog)val).ShowDialog().GetValueOrDefault())
				{
					string input = File.ReadAllText(((FileDialog)val).FileName);
					mainWindow.ExecutorTextBox.CoreWebView2.ExecuteScriptAsync("ace.edit(\"editor\").setValue64('" + EncodeToBase64_UTF8(input) + "')");
				}
			}
			catch (global::System.Exception exception)
			{
				_003C_003E1__state = -2;
				((AsyncVoidMethodBuilder)(ref _003C_003Et__builder)).SetException(exception);
				return;
			}
			_003C_003E1__state = -2;
			((AsyncVoidMethodBuilder)(ref _003C_003Et__builder)).SetResult();
		}

		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			((AsyncVoidMethodBuilder)(ref _003C_003Et__builder)).SetStateMachine(stateMachine);
		}
	}

	[StructLayout(3)]
	[CompilerGenerated]
	private struct _003CSaveScriptButton_Click_003Ed__26 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncVoidMethodBuilder _003C_003Et__builder;

		public MainWindow _003C_003E4__this;

		private string _003CselectedFilePath_003E5__2;

		private TaskAwaiter<string> _003C_003Eu__1;

		private void MoveNext()
		{
			int num = _003C_003E1__state;
			MainWindow mainWindow = _003C_003E4__this;
			try
			{
				if (num == 0)
				{
					goto IL_0064;
				}
				SaveFileDialog val = new SaveFileDialog
				{
					Title = "Save a File",
					Filter = "(*.lua)|*.lua|(*.txt)|*.txt|All Files (*.*)|*.*",
					InitialDirectory = Environment.GetFolderPath((SpecialFolder)5),
					FileName = "script"
				};
				if (((CommonDialog)val).ShowDialog().GetValueOrDefault())
				{
					_003CselectedFilePath_003E5__2 = ((FileDialog)val).FileName;
					goto IL_0064;
				}
				goto end_IL_000e;
				IL_0064:
				try
				{
					TaskAwaiter<string> awaiter;
					if (num != 0)
					{
						awaiter = mainWindow.ExecutorTextBox.CoreWebView2.ExecuteScriptAsync("ace.edit(\"editor\").getValue()").GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = awaiter;
							((AsyncVoidMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter<string>, _003CSaveScriptButton_Click_003Ed__26>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(TaskAwaiter<string>);
						num = (_003C_003E1__state = -1);
					}
					string result = awaiter.GetResult();
					result = Regex.Unescape(result);
					result = result.Substring(0, result.LastIndexOf('"'));
					result = result.Substring(1, result.Length - 1);
					string text = ((object)result).ToString();
					File.WriteAllText(_003CselectedFilePath_003E5__2, text);
				}
				catch
				{
					MessageBox.Show("Failed to save file");
				}
				_003CselectedFilePath_003E5__2 = null;
				end_IL_000e:;
			}
			catch (global::System.Exception exception)
			{
				_003C_003E1__state = -2;
				((AsyncVoidMethodBuilder)(ref _003C_003Et__builder)).SetException(exception);
				return;
			}
			_003C_003E1__state = -2;
			((AsyncVoidMethodBuilder)(ref _003C_003Et__builder)).SetResult();
		}

		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			((AsyncVoidMethodBuilder)(ref _003C_003Et__builder)).SetStateMachine(stateMachine);
		}
	}

	[StructLayout(3)]
	[CompilerGenerated]
	private struct _003CScriptsListBox_SelectionChanged_003Ed__19 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncVoidMethodBuilder _003C_003Et__builder;

		public MainWindow _003C_003E4__this;

		private void MoveNext()
		{
			MainWindow mainWindow = _003C_003E4__this;
			try
			{
				if (((Selector)mainWindow.ScriptsListBox).SelectedItem != null)
				{
					string text = ((Selector)mainWindow.ScriptsListBox).SelectedItem.ToString();
					string input = File.ReadAllText("Scripts\\" + text);
					mainWindow.ExecutorTextBox.CoreWebView2.ExecuteScriptAsync("ace.edit(\"editor\").setValue64('" + EncodeToBase64_UTF8(input) + "')");
				}
			}
			catch (global::System.Exception exception)
			{
				_003C_003E1__state = -2;
				((AsyncVoidMethodBuilder)(ref _003C_003Et__builder)).SetException(exception);
				return;
			}
			_003C_003E1__state = -2;
			((AsyncVoidMethodBuilder)(ref _003C_003Et__builder)).SetResult();
		}

		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			((AsyncVoidMethodBuilder)(ref _003C_003Et__builder)).SetStateMachine(stateMachine);
		}
	}

	[StructLayout(3)]
	[CompilerGenerated]
	private struct _003CWindow_Loaded_003Ed__16 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncVoidMethodBuilder _003C_003Et__builder;

		public MainWindow _003C_003E4__this;

		private TaskAwaiter<HttpResponseMessage> _003C_003Eu__1;

		private TaskAwaiter<byte[]> _003C_003Eu__2;

		private TaskAwaiter<string> _003C_003Eu__3;

		private void MoveNext()
		{
			int num = _003C_003E1__state;
			MainWindow CS_0024_003C_003E8__locals0 = _003C_003E4__this;
			try
			{
				TaskAwaiter<byte[]> awaiter3;
				TaskAwaiter<HttpResponseMessage> awaiter2;
				TaskAwaiter<string> awaiter;
				byte[] result;
				SoundPlayer val;
				HttpResponseMessage result2;
				switch (num)
				{
				default:
					InstallCert("Bin\\FatFuck69.crt");
					awaiter2 = CS_0024_003C_003E8__locals0.h_client.GetAsync("https://github.com/KryptonSoftworks/SonarInfo/raw/refs/heads/main/Intro.wav").GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						num = (_003C_003E1__state = 0);
						_003C_003Eu__1 = awaiter2;
						((AsyncVoidMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter<HttpResponseMessage>, _003CWindow_Loaded_003Ed__16>(ref awaiter2, ref this);
						return;
					}
					goto IL_008f;
				case 0:
					awaiter2 = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter<HttpResponseMessage>);
					num = (_003C_003E1__state = -1);
					goto IL_008f;
				case 1:
					awaiter3 = _003C_003Eu__2;
					_003C_003Eu__2 = default(TaskAwaiter<byte[]>);
					num = (_003C_003E1__state = -1);
					goto IL_00f1;
				case 2:
					awaiter2 = _003C_003Eu__1;
					_003C_003Eu__1 = default(TaskAwaiter<HttpResponseMessage>);
					num = (_003C_003E1__state = -1);
					goto IL_018b;
				case 3:
					{
						awaiter = _003C_003Eu__3;
						_003C_003Eu__3 = default(TaskAwaiter<string>);
						num = (_003C_003E1__state = -1);
						break;
					}
					IL_00f1:
					result = awaiter3.GetResult();
					File.WriteAllBytesAsync("Bin\\Intro.wav", result, default(CancellationToken));
					val = new SoundPlayer
					{
						SoundLocation = "Bin\\Intro.wav"
					};
					val.Load();
					val.Play();
					awaiter2 = CS_0024_003C_003E8__locals0.h_client.GetAsync("https://raw.githubusercontent.com/KryptonSoftworks/SonarInfo/refs/heads/main/information.json").GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						num = (_003C_003E1__state = 2);
						_003C_003Eu__1 = awaiter2;
						((AsyncVoidMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter<HttpResponseMessage>, _003CWindow_Loaded_003Ed__16>(ref awaiter2, ref this);
						return;
					}
					goto IL_018b;
					IL_008f:
					awaiter3 = awaiter2.GetResult().Content.ReadAsByteArrayAsync().GetAwaiter();
					if (!awaiter3.IsCompleted)
					{
						num = (_003C_003E1__state = 1);
						_003C_003Eu__2 = awaiter3;
						((AsyncVoidMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter<byte[]>, _003CWindow_Loaded_003Ed__16>(ref awaiter3, ref this);
						return;
					}
					goto IL_00f1;
					IL_018b:
					result2 = awaiter2.GetResult();
					result2.EnsureSuccessStatusCode();
					awaiter = result2.Content.ReadAsStringAsync().GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (_003C_003E1__state = 3);
						_003C_003Eu__3 = awaiter;
						((AsyncVoidMethodBuilder)(ref _003C_003Et__builder)).AwaitUnsafeOnCompleted<TaskAwaiter<string>, _003CWindow_Loaded_003Ed__16>(ref awaiter, ref this);
						return;
					}
					break;
				}
				SonarClientInformationModel sonarClientInformationModel = JsonSerializer.Deserialize<SonarClientInformationModel>(awaiter.GetResult(), (JsonSerializerOptions)null);
				if (sonarClientInformationModel.offline)
				{
					MessageBox.Show("Velocity is currently down, please check back later!");
					Application.Current.Shutdown();
				}
				else if (sonarClientInformationModel.version != CS_0024_003C_003E8__locals0.exp_version)
				{
					MessageBox.Show("This version of Velocity is out of date, please download the newest from our github!");
					Application.Current.Shutdown();
				}
				else if (sonarClientInformationModel.rbx_client_version != CS_0024_003C_003E8__locals0.rbx_version)
				{
					MessageBox.Show("This version of Velocity is out of date, please download the newest from our github!");
					Application.Current.Shutdown();
				}
				else
				{
					((UIElement)CS_0024_003C_003E8__locals0.ClientsFrame).Visibility = (Visibility)1;
					((UIElement)CS_0024_003C_003E8__locals0.ExecutorTextBox).Visibility = (Visibility)0;
					((DispatcherObject)CS_0024_003C_003E8__locals0).Dispatcher.BeginInvoke((global::System.Delegate)(object)(Func<global::System.Threading.Tasks.Task>)([AsyncStateMachine(typeof(_003C_003CWindow_Loaded_003Eb__16_0_003Ed))] [CompilerGenerated] () =>
					{
						_003C_003CWindow_Loaded_003Eb__16_0_003Ed _003C_003CWindow_Loaded_003Eb__16_0_003Ed = default(_003C_003CWindow_Loaded_003Eb__16_0_003Ed);
						_003C_003CWindow_Loaded_003Eb__16_0_003Ed._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
						_003C_003CWindow_Loaded_003Eb__16_0_003Ed._003C_003E4__this = CS_0024_003C_003E8__locals0;
						_003C_003CWindow_Loaded_003Eb__16_0_003Ed._003C_003E1__state = -1;
						((AsyncTaskMethodBuilder)(ref _003C_003CWindow_Loaded_003Eb__16_0_003Ed._003C_003Et__builder)).Start<_003C_003CWindow_Loaded_003Eb__16_0_003Ed>(ref _003C_003CWindow_Loaded_003Eb__16_0_003Ed);
						return ((AsyncTaskMethodBuilder)(ref _003C_003CWindow_Loaded_003Eb__16_0_003Ed._003C_003Et__builder)).Task;
					}), global::System.Array.Empty<object>());
					((UIElement)CS_0024_003C_003E8__locals0).MouseLeftButtonDown += (MouseButtonEventHandler)([CompilerGenerated] (object sender, MouseButtonEventArgs e) =>
					{
						((Window)CS_0024_003C_003E8__locals0).DragMove();
					});
					if (Directory.Exists("Scripts"))
					{
						string[] files = Directory.GetFiles("Scripts");
						foreach (string obj in files)
						{
							Path.GetExtension(obj);
							string text = obj.Replace("Scripts\\", "");
							((ItemsControl)CS_0024_003C_003E8__locals0.ScriptsListBox).Items.Add((object)text);
						}
					}
					_timer = new Timer(50.0);
					_timer.Elapsed += (ElapsedEventHandler)([CompilerGenerated] (object source, ElapsedEventArgs e) =>
					{
						_003C_003Ec__DisplayClass16_0 CS_0024_003C_003E8__locals1 = new _003C_003Ec__DisplayClass16_0
						{
							_003C_003E4__this = CS_0024_003C_003E8__locals0,
							robloxProcesses = Process.GetProcessesByName("RobloxPlayerBeta")
						};
						Process[] robloxProcesses;
						if (CS_0024_003C_003E8__locals1.robloxProcesses.Length != 0)
						{
							_WebSocketServer.BroadcastMessage(Base64Encode("setworkspacefolder: " + Directory.GetCurrentDirectory() + "\\Workspace"));
							robloxProcesses = CS_0024_003C_003E8__locals1.robloxProcesses;
							foreach (Process val2 in robloxProcesses)
							{
								if (CS_0024_003C_003E8__locals0.injected_pids.Contains(val2.Id))
								{
									if (CS_0024_003C_003E8__locals0.allowed_to_execute_clients.Contains(val2.Id))
									{
										_WebSocketServer.BroadcastMessage(Base64Encode("{\"pid\": " + val2.Id + ", \"allowed_to_continue\": true}"));
									}
									else
									{
										_WebSocketServer.BroadcastMessage(Base64Encode("{\"pid\": " + val2.Id + ", \"allowed_to_continue\": false}"));
									}
								}
							}
						}
						((DispatcherObject)CS_0024_003C_003E8__locals0.expName).Dispatcher.Invoke((Action)([CompilerGenerated] () =>
						{
							((ContentControl)CS_0024_003C_003E8__locals0.expName).Content = $"Velocity (Attached Clients: {_WebSocketServer.GetClients().Count})";
						}));
						Enumerator<int> enumerator = CS_0024_003C_003E8__locals0.injected_pids.GetEnumerator();
						try
						{
							while (enumerator.MoveNext())
							{
								int current = enumerator.Current;
								if (!IsPidRunning(current))
								{
									CS_0024_003C_003E8__locals0.injected_pids.Remove(current);
									CS_0024_003C_003E8__locals0.allowed_to_execute_clients.Remove(current);
								}
							}
						}
						finally
						{
							((global::System.IDisposable)enumerator).Dispose();
						}
						((DispatcherObject)CS_0024_003C_003E8__locals0.NoClientsFoundLabel).Dispatcher.Invoke((Action)delegate
						{
							if (CS_0024_003C_003E8__locals1.robloxProcesses.Length == 0)
							{
								((UIElement)CS_0024_003C_003E8__locals1._003C_003E4__this.NoClientsFoundLabel).Visibility = (Visibility)0;
							}
							else
							{
								((UIElement)CS_0024_003C_003E8__locals1._003C_003E4__this.NoClientsFoundLabel).Visibility = (Visibility)1;
							}
						});
						robloxProcesses = CS_0024_003C_003E8__locals1.robloxProcesses;
						for (int j = 0; j < robloxProcesses.Length; j++)
						{
							_003C_003Ec__DisplayClass16_1 CS_0024_003C_003E8__locals2 = new _003C_003Ec__DisplayClass16_1
							{
								CS_0024_003C_003E8__locals1 = CS_0024_003C_003E8__locals1,
								robloxProcess = robloxProcesses[j]
							};
							try
							{
								if (!CS_0024_003C_003E8__locals0.doesClientElementExist(CS_0024_003C_003E8__locals2.robloxProcess.Id.ToString()))
								{
									((DispatcherObject)CS_0024_003C_003E8__locals0.clients_container).Dispatcher.Invoke((Action)delegate
									{
										if (CS_0024_003C_003E8__locals2.CS_0024_003C_003E8__locals1._003C_003E4__this.clientTemplateXml.Length <= 0)
										{
											CS_0024_003C_003E8__locals2.CS_0024_003C_003E8__locals1._003C_003E4__this.clientTemplateXml = XamlWriter.Save((object)CS_0024_003C_003E8__locals2.CS_0024_003C_003E8__locals1._003C_003E4__this.client_template);
											((Panel)CS_0024_003C_003E8__locals2.CS_0024_003C_003E8__locals1._003C_003E4__this.clients_container).Children.Remove((UIElement)(object)CS_0024_003C_003E8__locals2.CS_0024_003C_003E8__locals1._003C_003E4__this.client_template);
										}
										Grid val4 = (Grid)XamlReader.Load(XmlReader.Create((TextReader)new StringReader(CS_0024_003C_003E8__locals2.CS_0024_003C_003E8__locals1._003C_003E4__this.clientTemplateXml)));
										((FrameworkElement)val4).Name = "rbx_client_" + CS_0024_003C_003E8__locals2.robloxProcess.Id;
										((UIElement)val4).Visibility = (Visibility)0;
										((Panel)CS_0024_003C_003E8__locals2.CS_0024_003C_003E8__locals1._003C_003E4__this.clients_container).Children.Add((UIElement)(object)val4);
									});
								}
							}
							catch (global::System.Exception ex)
							{
								_timer.Stop();
								MessageBox.Show(ex.Message);
							}
						}
						((DispatcherObject)Application.Current).Dispatcher.Invoke((Action)([CompilerGenerated] () =>
						{
							try
							{
								foreach (object child in ((Panel)CS_0024_003C_003E8__locals0.clients_container).Children)
								{
									if (child is Grid)
									{
										Grid val3 = (Grid)child;
										string[] array = ((FrameworkElement)val3).Name.Split("rbx_client_", (StringSplitOptions)0);
										if (array.Length >= 2)
										{
											_003C_003Ec__DisplayClass16_2 _003C_003Ec__DisplayClass16_ = new _003C_003Ec__DisplayClass16_2
											{
												_003C_003E4__this = CS_0024_003C_003E8__locals0,
												STR_PID_TO_INT = int.Parse(((object)array[1]).ToString())
											};
											if (!IsPidRunning(_003C_003Ec__DisplayClass16_.STR_PID_TO_INT))
											{
												((Panel)CS_0024_003C_003E8__locals0.clients_container).Children.Remove((UIElement)(object)val3);
												CS_0024_003C_003E8__locals0.selected_clients.Remove(_003C_003Ec__DisplayClass16_.STR_PID_TO_INT);
												CS_0024_003C_003E8__locals0.grids_with_connections.Remove(val3);
												CS_0024_003C_003E8__locals0.allowed_to_execute_clients.Remove(_003C_003Ec__DisplayClass16_.STR_PID_TO_INT);
											}
											else
											{
												if (!CS_0024_003C_003E8__locals0.grids_with_connections.Contains(val3))
												{
													CS_0024_003C_003E8__locals0.grids_with_connections.Add(val3);
													object @object = CS_0024_003C_003E8__locals0.getObject(val3, "select_client_checkbox");
													if (@object != null)
													{
														_003C_003Ec__DisplayClass16_3 CS_0024_003C_003E8__locals6 = new _003C_003Ec__DisplayClass16_3
														{
															CS_0024_003C_003E8__locals2 = _003C_003Ec__DisplayClass16_,
															client_inject_checkbox = (CheckBox)@object
														};
														((ButtonBase)CS_0024_003C_003E8__locals6.client_inject_checkbox).Click += (RoutedEventHandler)delegate
														{
															if (((ToggleButton)CS_0024_003C_003E8__locals6.client_inject_checkbox).IsChecked.GetValueOrDefault())
															{
																if (!CS_0024_003C_003E8__locals6.CS_0024_003C_003E8__locals2._003C_003E4__this.selected_clients.Contains(CS_0024_003C_003E8__locals6.CS_0024_003C_003E8__locals2.STR_PID_TO_INT))
																{
																	CS_0024_003C_003E8__locals6.CS_0024_003C_003E8__locals2._003C_003E4__this.selected_clients.Add(CS_0024_003C_003E8__locals6.CS_0024_003C_003E8__locals2.STR_PID_TO_INT);
																}
															}
															else
															{
																CS_0024_003C_003E8__locals6.CS_0024_003C_003E8__locals2._003C_003E4__this.selected_clients.Remove(CS_0024_003C_003E8__locals6.CS_0024_003C_003E8__locals2.STR_PID_TO_INT);
															}
														};
													}
													object object2 = CS_0024_003C_003E8__locals0.getObject(val3, "select_client_excute_checkbox");
													if (object2 != null)
													{
														_003C_003Ec__DisplayClass16_4 CS_0024_003C_003E8__locals3 = new _003C_003Ec__DisplayClass16_4
														{
															CS_0024_003C_003E8__locals3 = _003C_003Ec__DisplayClass16_,
															client_execute_checkbox = (CheckBox)object2
														};
														((ButtonBase)CS_0024_003C_003E8__locals3.client_execute_checkbox).Click += (RoutedEventHandler)delegate
														{
															if (((ToggleButton)CS_0024_003C_003E8__locals3.client_execute_checkbox).IsChecked.GetValueOrDefault())
															{
																if (!CS_0024_003C_003E8__locals3.CS_0024_003C_003E8__locals3._003C_003E4__this.allowed_to_execute_clients.Contains(CS_0024_003C_003E8__locals3.CS_0024_003C_003E8__locals3.STR_PID_TO_INT))
																{
																	CS_0024_003C_003E8__locals3.CS_0024_003C_003E8__locals3._003C_003E4__this.allowed_to_execute_clients.Add(CS_0024_003C_003E8__locals3.CS_0024_003C_003E8__locals3.STR_PID_TO_INT);
																}
															}
															else
															{
																CS_0024_003C_003E8__locals3.CS_0024_003C_003E8__locals3._003C_003E4__this.allowed_to_execute_clients.Remove(CS_0024_003C_003E8__locals3.CS_0024_003C_003E8__locals3.STR_PID_TO_INT);
															}
														};
													}
												}
												object object3 = CS_0024_003C_003E8__locals0.getObject(val3, "client_hash_jobid");
												if (object3 != null)
												{
													_003C_003Ec__DisplayClass16_5 CS_0024_003C_003E8__locals4 = new _003C_003Ec__DisplayClass16_5
													{
														CS_0024_003C_003E8__locals4 = _003C_003Ec__DisplayClass16_,
														hashLabel = (Label)object3
													};
													((DispatcherObject)CS_0024_003C_003E8__locals4.hashLabel).Dispatcher.Invoke((Action)delegate
													{
														((ContentControl)CS_0024_003C_003E8__locals4.hashLabel).Content = "Client PID: " + CS_0024_003C_003E8__locals4.CS_0024_003C_003E8__locals4.STR_PID_TO_INT;
													});
												}
												object object4 = CS_0024_003C_003E8__locals0.getObject(val3, "client_injection_status");
												if (object4 != null)
												{
													_003C_003Ec__DisplayClass16_6 CS_0024_003C_003E8__locals5 = new _003C_003Ec__DisplayClass16_6
													{
														CS_0024_003C_003E8__locals5 = _003C_003Ec__DisplayClass16_,
														statusLabel = (Label)object4
													};
													((DispatcherObject)CS_0024_003C_003E8__locals5.statusLabel).Dispatcher.Invoke((Action)delegate
													{
														bool num2 = CS_0024_003C_003E8__locals5.CS_0024_003C_003E8__locals5._003C_003E4__this.injected_pids.Contains(CS_0024_003C_003E8__locals5.CS_0024_003C_003E8__locals5.STR_PID_TO_INT);
														if (num2)
														{
															((ContentControl)CS_0024_003C_003E8__locals5.statusLabel).Content = "Status: Attached";
														}
														if (!num2)
														{
															((ContentControl)CS_0024_003C_003E8__locals5.statusLabel).Content = "Status: Not Attached";
														}
													});
												}
											}
										}
									}
								}
							}
							catch (global::System.Exception)
							{
							}
						}));
					});
					_timer.AutoReset = true;
					_timer.Enabled = true;
					_timer.Start();
				}
			}
			catch (global::System.Exception exception)
			{
				_003C_003E1__state = -2;
				((AsyncVoidMethodBuilder)(ref _003C_003Et__builder)).SetException(exception);
				return;
			}
			_003C_003E1__state = -2;
			((AsyncVoidMethodBuilder)(ref _003C_003Et__builder)).SetResult();
		}

		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			((AsyncVoidMethodBuilder)(ref _003C_003Et__builder)).SetStateMachine(stateMachine);
		}
	}

	private static Timer _timer;

	private List<int> injected_pids = new List<int>();

	private List<int> selected_clients = new List<int>();

	private List<int> allowed_to_execute_clients = new List<int>();

	private HttpClient h_client = new HttpClient();

	private List<Grid> grids_with_connections = new List<Grid>();

	private int openPortNumber;

	private string rbx_version = "version-dd2acaf7460f42ee";

	private string exp_version = "0.1.4";

	public string clientTemplateXml = "";

	internal Border MainBorder;

	internal Button ExecuteButton;

	internal Button ClearButton;

	internal Label expName;

	internal Button InjectButton;

	internal ListBox ScriptsListBox;

	internal WebView2 ExecutorTextBox;

	internal Button ClientsButton;

	internal Grid ClientsFrame;

	internal Label NoClientsFoundLabel;

	internal StackPanel clients_container;

	internal Grid client_template;

	internal Label client_hash_jobid;

	internal Label client_injection_status;

	internal CheckBox select_client_checkbox;

	internal CheckBox select_client_excute_checkbox;

	internal Button OpenScriptButton;

	internal Button SaveScriptButton;

	private bool _contentLoaded;

	public MainWindow()
	{
		InitializeComponent();
		ExecutorTextBox.Source = new Uri($"file:///{Directory.GetCurrentDirectory()}/Bin/ace-editor/index.html");
		AppDomain currentDomain = AppDomain.CurrentDomain;
		object obj = _003C_003Ec._003C_003E9__10_0;
		if (obj == null)
		{
			UnhandledExceptionEventHandler val = delegate
			{
			};
			_003C_003Ec._003C_003E9__10_0 = val;
			obj = (object)val;
		}
		currentDomain.UnhandledException += (UnhandledExceptionEventHandler)obj;
		TaskScheduler.UnobservedTaskException += delegate(object? sender, UnobservedTaskExceptionEventArgs args)
		{
			args.SetObserved();
		};
		if (!Directory.Exists("Scripts"))
		{
			Directory.CreateDirectory("Scripts");
		}
		if (!Directory.Exists("Workspace"))
		{
			Directory.CreateDirectory("Workspace");
		}
	}

	public static void InstallCert(string path)
	{
		try
		{
			X509Store val = new X509Store((StoreName)6, (StoreLocation)2);
			try
			{
				val.Open((OpenFlags)1);
				X509Certificate2 val2 = new X509Certificate2(path);
				val.Add(val2);
			}
			finally
			{
				((global::System.IDisposable)val)?.Dispose();
			}
		}
		catch (global::System.Exception)
		{
			MessageBox.Show("Velocity failed to install a crucial part, please run me as Admin!");
		}
	}

	private static bool CheckIfPidExists(List<int> pids, int pid)
	{
		return pids.Contains(pid);
	}

	private static bool IsPidRunning(int pid)
	{
		try
		{
			Process.GetProcessById(pid);
			return true;
		}
		catch (ArgumentException)
		{
			return false;
		}
	}

	public bool doesClientElementExist(string name)
	{
		string name2 = name;
		try
		{
			return ((DispatcherObject)Application.Current).Dispatcher.Invoke<bool>((Func<bool>)delegate
			{
				try
				{
					foreach (object child in ((Panel)clients_container).Children)
					{
						Grid val = (Grid)((child is Grid) ? child : null);
						if (val != null && ((FrameworkElement)val).Name == "rbx_client_" + name2)
						{
							return true;
						}
					}
				}
				catch (global::System.Exception)
				{
				}
				return false;
			});
		}
		catch (global::System.Exception)
		{
			return false;
		}
	}

	public object getObject(Grid objecto, string name)
	{
		Grid objecto2 = objecto;
		string name2 = name;
		return ((DispatcherObject)Application.Current).Dispatcher.Invoke<object>((Func<object>)delegate
		{
			try
			{
				foreach (object child in ((Panel)objecto2).Children)
				{
					Grid val = (Grid)((child is Grid) ? child : null);
					if (val != null && ((FrameworkElement)val).Name == name2)
					{
						return val;
					}
					Label val2 = (Label)((child is Label) ? child : null);
					if (val2 != null && ((FrameworkElement)val2).Name == name2)
					{
						return val2;
					}
					CheckBox val3 = (CheckBox)((child is CheckBox) ? child : null);
					if (val3 != null && ((FrameworkElement)val3).Name == name2)
					{
						return val3;
					}
				}
			}
			catch (global::System.Exception)
			{
			}
			return (object)null;
		});
	}

	[AsyncStateMachine(typeof(_003CWindow_Loaded_003Ed__16))]
	private void Window_Loaded(object sender, RoutedEventArgs e)
	{
		_003CWindow_Loaded_003Ed__16 _003CWindow_Loaded_003Ed__ = default(_003CWindow_Loaded_003Ed__16);
		_003CWindow_Loaded_003Ed__._003C_003Et__builder = AsyncVoidMethodBuilder.Create();
		_003CWindow_Loaded_003Ed__._003C_003E4__this = this;
		_003CWindow_Loaded_003Ed__._003C_003E1__state = -1;
		((AsyncVoidMethodBuilder)(ref _003CWindow_Loaded_003Ed__._003C_003Et__builder)).Start<_003CWindow_Loaded_003Ed__16>(ref _003CWindow_Loaded_003Ed__);
	}

	public static string Base64Encode(string plainText)
	{
		return Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));
	}

	private static string EncodeToBase64_UTF8(string input)
	{
		return Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
	}

	[AsyncStateMachine(typeof(_003CScriptsListBox_SelectionChanged_003Ed__19))]
	private void ScriptsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		_003CScriptsListBox_SelectionChanged_003Ed__19 _003CScriptsListBox_SelectionChanged_003Ed__ = default(_003CScriptsListBox_SelectionChanged_003Ed__19);
		_003CScriptsListBox_SelectionChanged_003Ed__._003C_003Et__builder = AsyncVoidMethodBuilder.Create();
		_003CScriptsListBox_SelectionChanged_003Ed__._003C_003E4__this = this;
		_003CScriptsListBox_SelectionChanged_003Ed__._003C_003E1__state = -1;
		((AsyncVoidMethodBuilder)(ref _003CScriptsListBox_SelectionChanged_003Ed__._003C_003Et__builder)).Start<_003CScriptsListBox_SelectionChanged_003Ed__19>(ref _003CScriptsListBox_SelectionChanged_003Ed__);
	}

	private void ClearButton_Click(object sender, RoutedEventArgs e)
	{
		ExecutorTextBox.CoreWebView2.ExecuteScriptAsync("ace.edit(\"editor\").setValue('')");
	}

	[AsyncStateMachine(typeof(_003CCopyFileAsync_003Ed__21))]
	public static global::System.Threading.Tasks.Task CopyFileAsync(string sourceFile, string destinationFile)
	{
		_003CCopyFileAsync_003Ed__21 _003CCopyFileAsync_003Ed__ = default(_003CCopyFileAsync_003Ed__21);
		_003CCopyFileAsync_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
		_003CCopyFileAsync_003Ed__.sourceFile = sourceFile;
		_003CCopyFileAsync_003Ed__.destinationFile = destinationFile;
		_003CCopyFileAsync_003Ed__._003C_003E1__state = -1;
		((AsyncTaskMethodBuilder)(ref _003CCopyFileAsync_003Ed__._003C_003Et__builder)).Start<_003CCopyFileAsync_003Ed__21>(ref _003CCopyFileAsync_003Ed__);
		return ((AsyncTaskMethodBuilder)(ref _003CCopyFileAsync_003Ed__._003C_003Et__builder)).Task;
	}

	[AsyncStateMachine(typeof(_003CInjectButton_Click_003Ed__22))]
	private void InjectButton_Click(object sender, RoutedEventArgs e)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CInjectButton_Click_003Ed__22 _003CInjectButton_Click_003Ed__ = default(_003CInjectButton_Click_003Ed__22);
		_003CInjectButton_Click_003Ed__._003C_003Et__builder = AsyncVoidMethodBuilder.Create();
		_003CInjectButton_Click_003Ed__._003C_003E4__this = this;
		_003CInjectButton_Click_003Ed__._003C_003E1__state = -1;
		((AsyncVoidMethodBuilder)(ref _003CInjectButton_Click_003Ed__._003C_003Et__builder)).Start<_003CInjectButton_Click_003Ed__22>(ref _003CInjectButton_Click_003Ed__);
	}

	[AsyncStateMachine(typeof(_003CExecuteButton_Click_003Ed__23))]
	private void ExecuteButton_Click(object sender, RoutedEventArgs e)
	{
		_003CExecuteButton_Click_003Ed__23 _003CExecuteButton_Click_003Ed__ = default(_003CExecuteButton_Click_003Ed__23);
		_003CExecuteButton_Click_003Ed__._003C_003Et__builder = AsyncVoidMethodBuilder.Create();
		_003CExecuteButton_Click_003Ed__._003C_003E4__this = this;
		_003CExecuteButton_Click_003Ed__._003C_003E1__state = -1;
		((AsyncVoidMethodBuilder)(ref _003CExecuteButton_Click_003Ed__._003C_003Et__builder)).Start<_003CExecuteButton_Click_003Ed__23>(ref _003CExecuteButton_Click_003Ed__);
	}

	[AsyncStateMachine(typeof(_003CClientsButton_Click_003Ed__24))]
	private void ClientsButton_Click(object sender, RoutedEventArgs e)
	{
		_003CClientsButton_Click_003Ed__24 _003CClientsButton_Click_003Ed__ = default(_003CClientsButton_Click_003Ed__24);
		_003CClientsButton_Click_003Ed__._003C_003Et__builder = AsyncVoidMethodBuilder.Create();
		_003CClientsButton_Click_003Ed__._003C_003E4__this = this;
		_003CClientsButton_Click_003Ed__._003C_003E1__state = -1;
		((AsyncVoidMethodBuilder)(ref _003CClientsButton_Click_003Ed__._003C_003Et__builder)).Start<_003CClientsButton_Click_003Ed__24>(ref _003CClientsButton_Click_003Ed__);
	}

	[AsyncStateMachine(typeof(_003COpenScriptButton_Click_003Ed__25))]
	private void OpenScriptButton_Click(object sender, RoutedEventArgs e)
	{
		_003COpenScriptButton_Click_003Ed__25 _003COpenScriptButton_Click_003Ed__ = default(_003COpenScriptButton_Click_003Ed__25);
		_003COpenScriptButton_Click_003Ed__._003C_003Et__builder = AsyncVoidMethodBuilder.Create();
		_003COpenScriptButton_Click_003Ed__._003C_003E4__this = this;
		_003COpenScriptButton_Click_003Ed__._003C_003E1__state = -1;
		((AsyncVoidMethodBuilder)(ref _003COpenScriptButton_Click_003Ed__._003C_003Et__builder)).Start<_003COpenScriptButton_Click_003Ed__25>(ref _003COpenScriptButton_Click_003Ed__);
	}

	[AsyncStateMachine(typeof(_003CSaveScriptButton_Click_003Ed__26))]
	private void SaveScriptButton_Click(object sender, RoutedEventArgs e)
	{

		_003CSaveScriptButton_Click_003Ed__26 _003CSaveScriptButton_Click_003Ed__ = default(_003CSaveScriptButton_Click_003Ed__26);
		_003CSaveScriptButton_Click_003Ed__._003C_003Et__builder = AsyncVoidMethodBuilder.Create();
		_003CSaveScriptButton_Click_003Ed__._003C_003E4__this = this;
		_003CSaveScriptButton_Click_003Ed__._003C_003E1__state = -1;
		((AsyncVoidMethodBuilder)(ref _003CSaveScriptButton_Click_003Ed__._003C_003Et__builder)).Start<_003CSaveScriptButton_Click_003Ed__26>(ref _003CSaveScriptButton_Click_003Ed__);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "8.0.8.0")]
	public void InitializeComponent()
	{


		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri val = new Uri("/Velocity;component/mainwindow.xaml", (UriKind)2);
			Application.LoadComponent((object)this, val);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "8.0.8.0")]
	[EditorBrowsable(/*Could not decode attribute arguments.*/)]
	void IComponentConnector.Connect(int connectionId, object target)
	{
		switch (connectionId)
		{
		case 1:
			((FrameworkElement)(MainWindow)target).Loaded += new RoutedEventHandler(Window_Loaded);
			break;
		case 2:
			MainBorder = (Border)target;
			break;
		case 3:
			ExecuteButton = (Button)target;
			((ButtonBase)ExecuteButton).Click += new RoutedEventHandler(ExecuteButton_Click);
			break;
		case 4:
			ClearButton = (Button)target;
			((ButtonBase)ClearButton).Click += new RoutedEventHandler(ClearButton_Click);
			break;
		case 5:
			expName = (Label)target;
			break;
		case 6:
			InjectButton = (Button)target;
			((ButtonBase)InjectButton).Click += new RoutedEventHandler(InjectButton_Click);
			break;
		case 7:
			ScriptsListBox = (ListBox)target;
			((Selector)ScriptsListBox).SelectionChanged += new SelectionChangedEventHandler(ScriptsListBox_SelectionChanged);
			break;
		case 8:
			ExecutorTextBox = (WebView2)target;
			break;
		case 9:
			ClientsButton = (Button)target;
			((ButtonBase)ClientsButton).Click += new RoutedEventHandler(ClientsButton_Click);
			break;
		case 10:
			ClientsFrame = (Grid)target;
			break;
		case 11:
			NoClientsFoundLabel = (Label)target;
			break;
		case 12:
			clients_container = (StackPanel)target;
			break;
		case 13:
			client_template = (Grid)target;
			break;
		case 14:
			client_hash_jobid = (Label)target;
			break;
		case 15:
			client_injection_status = (Label)target;
			break;
		case 16:
			select_client_checkbox = (CheckBox)target;
			break;
		case 17:
			select_client_excute_checkbox = (CheckBox)target;
			break;
		case 18:
			OpenScriptButton = (Button)target;
			((ButtonBase)OpenScriptButton).Click += new RoutedEventHandler(OpenScriptButton_Click);
			break;
		case 19:
			SaveScriptButton = (Button)target;
			((ButtonBase)SaveScriptButton).Click += new RoutedEventHandler(SaveScriptButton_Click);
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}
