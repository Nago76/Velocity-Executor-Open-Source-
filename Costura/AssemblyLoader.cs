using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Loader;
using System.Threading;

namespace Costura;

[CompilerGenerated]
internal static class AssemblyLoader
{
	private static object nullCacheLock = new object();

	private static Dictionary<string, bool> nullCache = new Dictionary<string, bool>();

	private static Dictionary<string, string> assemblyNames = new Dictionary<string, string>();

	private static Dictionary<string, string> symbolNames = new Dictionary<string, string>();

	private static int isAttached;

	private static string CultureToString(CultureInfo culture)
	{
		if (culture == null)
		{
			return string.Empty;
		}
		return culture.Name;
	}

	private static Assembly ReadExistingAssembly(AssemblyName name)
	{
		AppDomain currentDomain = AppDomain.CurrentDomain;
		Assembly[] assemblies = currentDomain.GetAssemblies();
		Assembly[] array = assemblies;
		foreach (Assembly val in array)
		{
			AssemblyName name2 = val.GetName();
			if (string.Equals(name2.Name, name.Name, (StringComparison)3) && string.Equals(CultureToString(name2.CultureInfo), CultureToString(name.CultureInfo), (StringComparison)3))
			{
				return val;
			}
		}
		return null;
	}

	private static string GetAssemblyResourceName(AssemblyName requestedAssemblyName)
	{
		string text = requestedAssemblyName.Name.ToLowerInvariant();
		if (requestedAssemblyName.CultureInfo != null && !string.IsNullOrEmpty(requestedAssemblyName.CultureInfo.Name))
		{
			text = (CultureToString(requestedAssemblyName.CultureInfo) + "." + text).ToLowerInvariant();
		}
		return text;
	}

	private static void CopyTo(Stream source, Stream destination)
	{
		byte[] array = new byte[81920];
		int num;
		while ((num = source.Read(array, 0, array.Length)) != 0)
		{
			destination.Write(array, 0, num);
		}
	}

	private static Stream LoadStream(string fullName)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Expected O, but got Unknown
		Assembly executingAssembly = Assembly.GetExecutingAssembly();
		if (fullName.EndsWith(".compressed"))
		{
			Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(fullName);
			try
			{
				DeflateStream val = new DeflateStream(manifestResourceStream, (CompressionMode)0);
				try
				{
					MemoryStream val2 = new MemoryStream();
					CopyTo((Stream)(object)val, (Stream)(object)val2);
					((Stream)val2).Position = 0L;
					return (Stream)(object)val2;
				}
				finally
				{
					((global::System.IDisposable)val)?.Dispose();
				}
			}
			finally
			{
				((global::System.IDisposable)manifestResourceStream)?.Dispose();
			}
		}
		return executingAssembly.GetManifestResourceStream(fullName);
	}

	private static Stream LoadStream(Dictionary<string, string> resourceNames, string name)
	{
		string fullName = default(string);
		if (resourceNames.TryGetValue(name, ref fullName))
		{
			return LoadStream(fullName);
		}
		return null;
	}

	private static byte[] ReadStream(Stream stream)
	{
		byte[] array = new byte[stream.Length];
		stream.Read(array, 0, array.Length);
		return array;
	}

	private static Assembly ReadFromEmbeddedResources(Dictionary<string, string> assemblyNames, Dictionary<string, string> symbolNames, AssemblyName requestedAssemblyName)
	{
		string assemblyResourceName = GetAssemblyResourceName(requestedAssemblyName);
		Stream val = LoadStream(assemblyNames, assemblyResourceName);
		byte[] array;
		try
		{
			if (val == null)
			{
				return null;
			}
			array = ReadStream(val);
		}
		finally
		{
			((global::System.IDisposable)val)?.Dispose();
		}
		Stream val2 = LoadStream(symbolNames, assemblyResourceName);
		try
		{
			if (val2 != null)
			{
				byte[] array2 = ReadStream(val2);
				return Assembly.Load(array, array2);
			}
		}
		finally
		{
			((global::System.IDisposable)val2)?.Dispose();
		}
		return Assembly.Load(array);
	}

	public static Assembly ResolveAssembly(AssemblyLoadContext assemblyLoadContext, AssemblyName assemblyName)
	{
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		string name = assemblyName.Name;
		lock (nullCacheLock)
		{
			if (nullCache.ContainsKey(name))
			{
				return null;
			}
		}
		Assembly val = ReadExistingAssembly(assemblyName);
		if (val != null)
		{
			return val;
		}
		val = ReadFromEmbeddedResources(assemblyNames, symbolNames, assemblyName);
		if (val == null)
		{
			lock (nullCacheLock)
			{
				nullCache[name] = true;
			}
			if ((assemblyName.Flags & 0x100) != 0)
			{
				val = Assembly.Load(assemblyName);
			}
		}
		return val;
	}

	static AssemblyLoader()
	{
		assemblyNames.Add("costura", "costura.costura.dll.compressed");
		symbolNames.Add("costura", "costura.costura.pdb.compressed");
		assemblyNames.Add("microsoft.web.webview2.core", "costura.microsoft.web.webview2.core.dll.compressed");
		assemblyNames.Add("microsoft.web.webview2.winforms", "costura.microsoft.web.webview2.winforms.dll.compressed");
		assemblyNames.Add("microsoft.web.webview2.wpf", "costura.microsoft.web.webview2.wpf.dll.compressed");
		assemblyNames.Add("microsoft.windows.sdk.net", "costura.microsoft.windows.sdk.net.dll.compressed");
		assemblyNames.Add("winrt.runtime", "costura.winrt.runtime.dll.compressed");
	}

	public static void Attach(bool subscribe)
	{
		if (Interlocked.Exchange(ref isAttached, 1) == 1 || !subscribe)
		{
			return;
		}
		AssemblyLoadContext.Default.Resolving += delegate(AssemblyLoadContext assemblyLoadContext, AssemblyName assemblyName)
		{
			//IL_0083: Unknown result type (might be due to invalid IL or missing references)
			//IL_008d: Unknown result type (might be due to invalid IL or missing references)
			string name = assemblyName.Name;
			lock (nullCacheLock)
			{
				if (nullCache.ContainsKey(name))
				{
					return (Assembly)null;
				}
			}
			Assembly val = ReadExistingAssembly(assemblyName);
			if (val != null)
			{
				return val;
			}
			val = ReadFromEmbeddedResources(assemblyNames, symbolNames, assemblyName);
			if (val == null)
			{
				lock (nullCacheLock)
				{
					nullCache[name] = true;
				}
				if ((assemblyName.Flags & 0x100) != 0)
				{
					val = Assembly.Load(assemblyName);
				}
			}
			return val;
		};
	}
}
