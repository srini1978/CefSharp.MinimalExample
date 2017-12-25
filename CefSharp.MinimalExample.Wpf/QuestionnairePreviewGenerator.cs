using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CefSharp;
using CefSharp.Wpf;

namespace CefSharp.MinimalExample.Wpf
{
    public static class QuestionnairePreviewGenerator
    {
        private const string HtmlStart = "<html>";
        private const string HtmlEnd = "</html>";

        private const string HeadStart = "<head>";
        private const string HeadEnd = "</head>";

        private const string BodyStart = "<body>";
        private const string BodyEnd = "</body>";

        private const string EncodingElement = "<meta charset=\"utf-8\"/>";
        private const string StylesheetLink = "<link href=\"{0}\" rel=\"stylesheet\" />";
        private const string ScriptReference = "<script src=\"{0}\"></script>";

        private static readonly string CurrentCultureHiddenField = "<input id='hdnculturefield' hidden='hidden' value = '" + CultureInfo.CurrentCulture.Name.ToString() + "'/>";
        private static List<string> styleCollection = new List<string>();
        private static List<string> scriptCollection = new List<string>();
        private static List<string> typeScriptCollection = new List<string>();
        private static List<string> imageCollection = new List<string>();

        private static string baseFilePath = string.Empty;
        private static string basePath = "http://test/resource/load";

        public static string RegisterResources(DefaultResourceHandlerFactory factory, string title)
        {
            if (factory == null)
            {
                return null;
            }

            var previewPath = "http://test/resource/load/questionnaire_preview.html";
            baseFilePath = Path.Combine(RetrieveAppLocation(), "Web");

            Initialize(factory);

            var templateCollection = Directory.GetFiles(Path.Combine(baseFilePath, "templates"), "*.*", SearchOption.AllDirectories);
            foreach (var item in templateCollection)
            {
                factory.RegisterHandler(string.Format(CultureInfo.InvariantCulture, "{0}", item.Replace(baseFilePath, basePath).Replace("\\", "/")), ResourceHandler.FromFilePath(item));
            }

            var htmlPage = GetQuestionnaireHtml(title);
            factory.RegisterHandler(previewPath, ResourceHandler.FromString(htmlPage));
            ClearAll();

            return previewPath;
        }

        private static string GetQuestionnaireHtml(string title)
        {
            var htmlToRender = new StringBuilder();
            htmlToRender.Append(HtmlStart);
            htmlToRender.Append(HeadStart);
            htmlToRender.Append(EncodingElement);
            htmlToRender.Append("<title>" + title + "</title>");

            foreach (var item in styleCollection)
            {
                htmlToRender.Append(string.Format(CultureInfo.InvariantCulture, StylesheetLink, item));
            }

            foreach (var item in scriptCollection)
            {
                htmlToRender.Append(string.Format(CultureInfo.InvariantCulture, ScriptReference, item));
            }

            htmlToRender.Append(HeadEnd);

            htmlToRender.Append(BodyStart);
            var responseBody = string.Join(string.Empty, File.ReadAllLines(baseFilePath + "\\Questionnaire_Preview.html"));
            htmlToRender.Append(responseBody);
            
            htmlToRender.Append(CurrentCultureHiddenField);
            foreach (var item in typeScriptCollection)
            {
                htmlToRender.Append(string.Format(CultureInfo.InvariantCulture, ScriptReference, item));
            }

            htmlToRender.Append(BodyEnd);
            htmlToRender.Append(HtmlEnd);

            return htmlToRender.ToString();
        }

        private static string RetrieveAppLocation()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        private static void ClearAll()
        {
            styleCollection.Clear();
            scriptCollection.Clear();
            typeScriptCollection.Clear();

            imageCollection.Clear();
        }

        private static void Initialize(DefaultResourceHandlerFactory factory)
        {
            InitializeStyleCollection(factory);
            InitializeScriptCollection(factory);
            InitializeTypeScriptCollection(factory);

            InitializeImageCollection(factory);
        }

        private static void InitializeStyleCollection(DefaultResourceHandlerFactory factory)
        {
            styleCollection.Add("packages/bootstrap/content/bootstrap.css");
            styleCollection.Add("packages/bootstrap/content/bootstrap-theme.css");
            styleCollection.Add("packages/angular-loading-bar/content/loading-bar.css");
            styleCollection.Add("packages/angular-loading-bar/content/loading-bar.min.css");
            styleCollection.Add("packages/angular-bootstrap/content/ui-bootstrap-csp.css");
            styleCollection.Add("packages/kendo/styles/kendo.common-material.css");
            styleCollection.Add("packages/kendo/styles/kendo.material.css");
            styleCollection.Add("css/_context-menu.css");
            styleCollection.Add("css/_utilities.css");
            styleCollection.Add("css/edit-questionnaire.css");
            styleCollection.Add("css/form.css");
            styleCollection.Add("css/kendo-editor-override.css");
            styleCollection.Add("css/main.css");
            styleCollection.Add("css/sub-header.css");
            styleCollection.Add("css/toggle-switch.css");
            styleCollection.Add("css/dm.css");
            styleCollection.Add("css/question-preview.css"); 

            foreach (var item in styleCollection)
            {
                var filePath = Path.Combine(baseFilePath, item);
                factory.RegisterHandler(basePath + "/" + item, ResourceHandler.FromFilePath(filePath));
            }
        }

        private static void InitializeScriptCollection(DefaultResourceHandlerFactory factory)
        {
            var currentCulture = CultureInfo.CurrentCulture;

            scriptCollection.Add("packages/jquery/jquery-1.9.1.js");
            scriptCollection.Add("packages/bootstrap/scripts/bootstrap.js");
            scriptCollection.Add("packages/angular-core/angular.js");
            scriptCollection.Add("packages/angular-route/angular-route.js");
            scriptCollection.Add("packages/angular-ui-router/angular-ui-router.js");
            scriptCollection.Add("packages/ng-storage/ngStorage.min.js");
            scriptCollection.Add("packages/angular-bootstrap/scripts/angular-ui/ui-bootstrap.js");
            scriptCollection.Add("packages/angular-bootstrap/scripts/angular-ui/ui-bootstrap-tpls.js");
            scriptCollection.Add("packages/misc/jquery.mCustomScrollbar.concat.min.js");
            scriptCollection.Add("packages/misc/scrollbars.js");
            scriptCollection.Add("packages/angular-loading-bar/scripts/loading-bar.js");
            scriptCollection.Add("packages/misc/ocLazyLoad.js");
            scriptCollection.Add("packages/kendo/js/kendo.all.min.js");
            scriptCollection.Add("packages/kendo/js/kendo.grid.min.js");
            if (currentCulture != null && currentCulture.ToString() != "en-US")
            {
                scriptCollection.Add("packages/kendo/cultures/kendo.culture." + currentCulture + ".min.js");
            }

            foreach (var item in scriptCollection)
            {
                var filePath = Path.Combine(baseFilePath, item);
                factory.RegisterHandler(basePath + "/" + item, ResourceHandler.FromFilePath(filePath));
            }
        }

        private static void InitializeTypeScriptCollection(DefaultResourceHandlerFactory factory)
        {
            typeScriptCollection.Add("jsout/base/app.js");
            typeScriptCollection.Add("jsout/base/app.config.js");
            typeScriptCollection.Add("jsout/base/app.run.js");
            typeScriptCollection.Add("jsout/base/app.controller.js");
            typeScriptCollection.Add("Js/ExpandCollapseBehaviour.js");
            typeScriptCollection.Add("jsout/typescripts/questionnaire-helper.js");
            typeScriptCollection.Add("jsout/typescripts/questionnaire-resources.js");
            typeScriptCollection.Add("jsout/typescripts/document-manager-resources.js");
            typeScriptCollection.Add("jsout/typescripts/questionnaire-constants.js");
            typeScriptCollection.Add("jsout/typescripts/questionnaire-entity.js");
            typeScriptCollection.Add("jsout/typescripts/questionnaire-helper.js");
            typeScriptCollection.Add("jsout/typescripts/questionnaire-item-type.js");
            typeScriptCollection.Add("jsout/typescripts/questionnaire-item.js");
            typeScriptCollection.Add("jsout/typescripts/questionnaire-kendo-wrapper.js");
            typeScriptCollection.Add("jsout/typescripts/questionnaire-preview-base.js");
            typeScriptCollection.Add("jsout/typescripts/questionnaire-preview-components.js");
            typeScriptCollection.Add("jsout/typescripts/questionnaire-preview-controller.js");
            typeScriptCollection.Add("jsout/typescripts/questionnaire-question.js");
            typeScriptCollection.Add("jsout/typescripts/questionnaire-resources.js");
            typeScriptCollection.Add("jsout/typescripts/questionnaire-rule-engine.js");
            typeScriptCollection.Add("jsout/typescripts/questionnaire-rule.js");
            typeScriptCollection.Add("jsout/typescripts/questionnaire-section.js");
            typeScriptCollection.Add("jsout/typescripts/questionnaire-table.js");
            typeScriptCollection.Add("jsout/typescripts/questionnaire-text-help.js");
            typeScriptCollection.Add("jsout/typescripts/questionnaire-text-instruction.js");
            typeScriptCollection.Add("jsout/typescripts/questionnaire-text-warning.js");

            foreach (var item in typeScriptCollection)
            {
                var filePath = Path.Combine(baseFilePath, item);
                factory.RegisterHandler(basePath + "/" + item, ResourceHandler.FromFilePath(filePath));
            }
        }

        private static void InitializeImageCollection(DefaultResourceHandlerFactory factory)
        {
            imageCollection.Add("images/icon-close.svg");
            imageCollection.Add("images/icon-refresh.svg");
            imageCollection.Add("images/icon-arrow-right.svg");
            imageCollection.Add("images/icon-dots.svg");
            imageCollection.Add("images/icon-questionnaire-warning.svg");

            imageCollection.Add("css/Material/sprite_2x.png");
            imageCollection.Add("css/Material/sprite.png");
            imageCollection.Add("css/Material/loading.gif");

            imageCollection.Add("css/fonts/proximanova-regularit.ttf");
            imageCollection.Add("css/fonts/proximanova-regular.woff");
            imageCollection.Add("packages/kendo/styles/fonts/glyphs/WebComponentsIcons.woff");
            imageCollection.Add("packages/kendo/styles/fonts/glyphs/WebComponentsIcons.ttf");

            foreach (var item in imageCollection)
            {
                var filePath = Path.Combine(baseFilePath, item);
                if (Path.GetExtension(filePath).Equals(".svg", StringComparison.OrdinalIgnoreCase))
                {
                    factory.RegisterHandler(basePath + "/" + item, ResourceHandler.FromFilePath(filePath, "image/svg+xml"));
                }
                else
                {
                    factory.RegisterHandler(basePath + "/" + item, ResourceHandler.FromFilePath(filePath));
                }
            }
        }
    }
}
