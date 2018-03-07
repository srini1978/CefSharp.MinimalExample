using System;
using System.IO;
using System.Windows;

namespace CefSharp.MinimalExample.Wpf
{
    public partial class App : Application
    {
        public App()
        {
          using (var settings = new CefSettings())
			{
				settings.SetOffScreenRenderingBestPerformanceArgs();
				settings.DisableGpuAcceleration();
				settings.Locale = CultureInfo.CurrentCulture.Name;
				Cef.Initialize(settings);
			}
        }
    }
}
