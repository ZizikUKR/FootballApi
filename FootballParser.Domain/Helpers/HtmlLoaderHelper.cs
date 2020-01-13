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
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Reflection;
using OpenQA.Selenium;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using FootballParser.Domain.Entities;

namespace FootballParser.Domain.Helpers
{
    public class HtmlLoaderHelper : IHtmlLoaderHelper
    {
        readonly HttpClient client;
        private ChromeDriver _chromeDriver;

        public HtmlLoaderHelper(/*IHabraSettings settings,*/)


        
        {
            client = new HttpClient();
            //_url = $"{settings.BaseUrl}/{settings.Prefix}/";
            _chromeDriver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        }

        public async Task<IHtmlDocument> GetPageSource(string url)
        {
            var domParser = new HtmlParser();
            Task.Run(() => _chromeDriver.Navigate().GoToUrl(url)).Wait();
            var categories = _chromeDriver.FindElements(By.ClassName("js-sidebar-tournament")).ToList();
            var categoriesNum = categories.Count;
            for (int i = 2; i < categoriesNum; i++)
            {
                try
                {
                    Task.Run(()=> categories[i].Click()).Wait();
                    var teamsTab = _chromeDriver.FindElements(By.ClassName("js-standings-tables-part-panels")).ToList();
                    var activeTab = teamsTab.FirstOrDefault().FindElement(By.ClassName("active"));
                    var teams = activeTab.FindElements(By.ClassName("cell--standings")).ToList();
                    var teamsCount = teams.Count;
                    for (int j = 0; j < teamsCount; j++)
                    {
                        var click = teams[j].FindElement(By.ClassName("standings__team-name"));
                        var href = click.FindElement(By.ClassName("js-link"));
                        Task.Run(() => href.Click()).Wait();// href.Click();
                        var team = new Team();
                        team.Name = _chromeDriver.FindElement(By.ClassName("page-title")).Text;
                        team.Url = _chromeDriver.Url;
                        var squad = _chromeDriver.FindElement(By.ClassName("squad"));
                        var players = squad.FindElements(By.TagName("a")).ToList();
                        foreach (var player in players)
                        {
                            var newPlayer = new Player();
                            newPlayer.Name = player.FindElement(By.ClassName("squad-player__name")).Text;
                            var number = player.FindElement(By.ClassName("squad-player__img-wrapper"))?.FindElement(By.TagName("span")).Text;
                            int.TryParse(number, out int payerNum);
                            newPlayer.Number = payerNum;
                            newPlayer.TeamId = team.Id;
                            team.Players.Add(newPlayer);
                        }
                        var matches = _chromeDriver.FindElements(By.ClassName("js-event-list-tournament-events")).ToList();
                        foreach (var match in matches)
                        {
                            var matchLinks = match.FindElements(By.TagName("a"));
                            foreach (var link in matchLinks)
                            {
                                var date = link.FindElement(By.ClassName("u-w64")).Text;
                                Task.Run(() => link.Click()).Wait();
                            }
                        }
                        _chromeDriver.Navigate().Back();

                        teamsTab = _chromeDriver.FindElements(By.ClassName("js-standings-tables-part-panels")).ToList();
                        activeTab = teamsTab.FirstOrDefault().FindElement(By.ClassName("active"));
                        teams = activeTab.FindElements(By.ClassName("cell--standings")).ToList();
                    }
                    //await GetTeam(teams);
                    Task.Run(() =>_chromeDriver.Navigate().Back()).Wait();
                    categories = _chromeDriver.FindElements(By.ClassName("js-sidebar-tournament")).ToList();
                }
                catch (Exception ex)
                {

                }
            }
            try
            {
                var config = AngleSharp.Configuration.Default.WithDefaultLoader().WithJavaScript();

                var context = BrowsingContext.New(config);
                var document = await context.OpenAsync(url);
                var document1 = await domParser.ParseDocumentAsync(document.ToHtml());
                return document1;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private async Task ParseClub()
        {

        }

        private async Task GetTeam(List<IWebElement> teams)
        {
            // 
            var teamsCount = teams.Count;
            for (int i = 0; i < teamsCount; i++)
            {

                teams[i].Click();
                _chromeDriver.Navigate().Back();
                teams = _chromeDriver.FindElements(By.ClassName("cell--standings")).ToList();
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
