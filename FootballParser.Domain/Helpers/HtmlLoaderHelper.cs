using AngleSharp.Scripting;
using FootballParser.Domain.Helpers.Interfaces;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Scripting.JavaScript;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;

namespace FootballParser.Domain.Helpers
{
    public class HtmlLoaderHelper : IHtmlLoaderHelper
    {
        readonly HttpClient client;

        public HtmlLoaderHelper(/*IHabraSettings settings,*/)
        {
            client = new HttpClient();
            //_url = $"{settings.BaseUrl}/{settings.Prefix}/";
        }

        public async Task<IHtmlDocument> GetPageSource(string url)
        {
            var domParser = new HtmlParser();

            //var currentUrl = url.Replace("{CurrentId}", id.ToString());
            //var response = await client.GetAsync(url);
            //string source = null;
            //using (var webClient = new WebClient())
            //{
            //    var html = await webClient.DownloadStringTaskAsync(url);
            //    var htmlParser = new HtmlParser();
            //    //var _document = await htmlParser.ParseAsync(html);
            //}
            try
            {
                var config = Configuration.Default
            .WithDefaultLoader()
            .WithJavaScript();

                var context = BrowsingContext.New(config);
                var document = await context.OpenAsync(url);
                var document1 = await domParser.ParseDocumentAsync(document.ToHtml());
                // var config = new JavaScriptEngine();
                // var context = BrowsingContext.New(config);
                //var config = new Configuration().WithDefaultLoader();
                //var document = await BrowsingContext.New(config).OpenAsync(url);

                //var source = await GetaPageSource(url);
                //var res = await context.OpenAsync(req => req.Content(source));
                //var som = res.DocumentElement.OuterHtml;
                ////return res;
                //var document = await domParser.ParseDocumentAsync(source);
                //return document;
                return document1;
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }

        private async Task<string> GetaPageSource(string url)
        {
            var response = await client.GetAsync(url);
            string source = null;

            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                source = await response.Content.ReadAsStringAsync();
            }
            return source;
        }

    }
}
