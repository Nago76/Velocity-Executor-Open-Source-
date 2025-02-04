using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Markup;

namespace MuuLvfQrcoiF2IMO;

public class LoaderUI : Window, IComponentConnector
{
	internal LoaderUI loaderWindow;

	private bool _contentLoaded;

	public LoaderUI()
	{
		InitializeComponent();
	}

	private void Loader_Loaded(object sender, RoutedEventArgs e)
	{
		if (!Directory.Exists("Workspace"))
		{
			Directory.CreateDirectory("Workspace");
		}
		if (!Directory.Exists("Bin"))
		{
			Directory.CreateDirectory("Bin");
		}
		if (!Directory.Exists("Scripts"))
		{
			Directory.CreateDirectory("Scripts");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "8.0.8.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri val = new Uri("/Velocity;component/loader.xaml", (UriKind)2);
			Application.LoadComponent((object)this, val);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "8.0.8.0")]
	[EditorBrowsable(/*Could not decode attribute arguments.*/)]
	void IComponentConnector.Connect(int connectionId, object target)
	{
		if (connectionId == 1)
		{
			loaderWindow = (LoaderUI)target;
			((FrameworkElement)loaderWindow).Loaded += new RoutedEventHandler(Loader_Loaded);
		}
		else
		{
			_contentLoaded = true;
		}
	}
}
