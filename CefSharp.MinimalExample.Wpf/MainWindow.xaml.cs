using System;
using System.Threading;
using System.Windows;
using CefSharp;
using CefSharp.Wpf;
namespace CefSharp.MinimalExample.Wpf
{
    public partial class MainWindow : Window
    {
       // ChromiumWebBrowser m_chromeBrowser = null;
        private JavaScriptAdapter javaScriptCefAdapterObject;
		private string questionnairePath;
		public const string RenderProcessCrashedUrl = "http://test/resource/load";

		public MainWindow()
        {
            InitializeComponent();
			var handler = Browser.ResourceHandlerFactory as DefaultResourceHandlerFactory;
			Browser.IsBrowserInitializedChanged += Browser_IsBrowserInitializedChanged;



			if (Browser != null)
			{
				var factory = Browser.ResourceHandlerFactory as DefaultResourceHandlerFactory;

				if (factory == null)
				{
					return;
				}

				

				questionnairePath = QuestionnairePreviewGenerator.RegisterResources(factory, "Questionnaire PReview");

				//Rajeev - Ucomment the following to reproduce the issue
				//Browser.Load("file:///D:/Delivery/BDO/POC/CefSharp.MinimalExample-master/CefSharp.MinimalExample-master/Questionnaire_Preview.html");



				Browser.Address = questionnairePath;
				//Thread.Sleep(5000);

				javaScriptCefAdapterObject = new JavaScriptAdapter();
				javaScriptCefAdapterObject.SetChromeBrowser(Browser as ChromiumWebBrowser);

				//Existing code is !Browser.IsBrowserInitialized
				//	Browser.BrowserSettings.WebSecurity = CefState.Disabled;
				if ( !Browser.IsBrowserInitialized)
				{
					Browser.RegisterJsObject("jscefAdapterObj", javaScriptCefAdapterObject);

				}
				else
				{
					
				}
				
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
