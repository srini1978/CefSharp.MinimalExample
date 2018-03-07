using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using CefSharp;
using CefSharp.Wpf;
namespace CefSharp.MinimalExample.Wpf
{
    public partial class MainWindow : Window
    {
        
        private JavaScriptAdapter javaScriptCefAdapterObject;
		private string questionnairePath;

		public ChromiumWebBrowser chromiumWebBrowserInstance =new ChromiumWebBrowser() ;
		public MainWindow()
        {
			
            InitializeComponent();
			
			
			
			chromiumWebBrowserInstance.BrowserSettings.Javascript = CefState.Enabled;
			chromiumWebBrowserInstance.BrowserSettings.LocalStorage = CefState.Enabled;
			chromiumWebBrowserInstance.BrowserSettings.RemoteFonts = CefState.Enabled;
			chromiumWebBrowserInstance.BrowserSettings.ImageLoading = CefState.Enabled;
			chromiumWebBrowserInstance.BrowserSettings.AcceptLanguageList = CultureInfo.CurrentCulture.Name;



			chromiumWebBrowserInstance.IsBrowserInitializedChanged += Browser_IsBrowserInitializedChanged;
			chromiumWebBrowserInstance.LoadError += Browser_LoadError;
			chromiumWebBrowserInstance.LoadingStateChanged += ChromiumWebBrowserInstance_LoadingStateChanged;

			if (chromiumWebBrowserInstance.WebBrowser != null)
			{
				var factory = chromiumWebBrowserInstance.WebBrowser.ResourceHandlerFactory as DefaultResourceHandlerFactory;

				if (factory == null)
				{
					return;
				}




				//Thread.Sleep(5000);

				javaScriptCefAdapterObject = new JavaScriptAdapter();
				javaScriptCefAdapterObject.SetChromeBrowser(chromiumWebBrowserInstance);

				//Existing code is !Browser.IsBrowserInitialized
				if (!chromiumWebBrowserInstance.IsBrowserInitialized)
				{

					chromiumWebBrowserInstance.RegisterAsyncJsObject("jscefAdapterObj", javaScriptCefAdapterObject);

				}
				else
				{

				}
				questionnairePath = QuestionnairePreviewGenerator.RegisterResources(factory, "Questionnaire PReview");
				chromiumWebBrowserInstance.Address = questionnairePath;
				Grid1.Children.Add(chromiumWebBrowserInstance);

			}







		}

		private void ChromiumWebBrowserInstance_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
		{
			throw new NotImplementedException();
		}

		


		private void Browser_LoadError(object sender, LoadErrorEventArgs e)
		{
			if (e.Browser.HasDocument)
			{

			}
		}

		private void Browser_IsBrowserInitializedChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			if ((bool)e.NewValue == true)
			{
				//Browser.Load("www.google.com");
				
			}
		}
		void OnRenderProcessTerminated(IWebBrowser browserControl, IBrowser browser, CefTerminationStatus status)
		{
			browser.Reload(true);
		}
		public static string GetAppLocation()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }
		
	
	}
}
