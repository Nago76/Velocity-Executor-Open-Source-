using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Windows;

namespace ExecutorUI;

public class App : Application
{
	protected override void OnStartup(StartupEventArgs e)
	{
		((Application)this).OnStartup(e);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "8.0.8.0")]
	public void InitializeComponent()
	{
		((Application)this).StartupUri = new Uri("MainWindow.xaml", (UriKind)2);
	}

	[STAThread]
	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "8.0.8.0")]
	public static void Main()
	{
		App app = new App();
		app.InitializeComponent();
		((Application)app).Run();
	}
}
