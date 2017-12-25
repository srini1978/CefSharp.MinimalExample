
using System.Windows;

namespace CefSharp.MinimalExample.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
              string questionnairePath = "file:///{0}HTMLResources/html/alpaca.html";
		//	Browser.Load(page.ToString());

			if (Browser != null)
			{
				var factory = Browser.ResourceHandlerFactory as DefaultResourceHandlerFactory;

				if (factory == null)
				{
					return;
				}

				questionnairePath = QuestionnairePreviewGenerator.RegisterResources(factory, "Questionnaire PReview");

				//javaScriptCefAdapterObject = new JavaScriptAdapter(this);
				//javaScriptCefAdapterObject.SetChromeBrowser(browser as ChromiumWebBrowser);
				//if (!browser.IsBrowserInitialized)
				//{
				//	browser.RegisterJsObject("jscefAdapterObj", javaScriptCefAdapterObject);
				//}
				Browser.Address = questionnairePath;
        }
    }
}
