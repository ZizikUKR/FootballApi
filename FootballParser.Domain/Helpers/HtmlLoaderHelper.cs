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
using OpenQA.Selenium.Support.UI;

namespace FootballParser.Domain.Helpers
{
    public class HtmlLoaderHelper : IHtmlLoaderHelper
    {
        readonly HttpClient client;
        private ChromeDriver _chromeDriver;
        private WebDriverWait _webDriverWait;

        public HtmlLoaderHelper(/*IHabraSettings settings,*/)
        {
            // client = new HttpClient();
            //_url = $"{settings.BaseUrl}/{settings.Prefix}/";
            _chromeDriver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            _chromeDriver.Manage().Window.Maximize();
            _webDriverWait = new WebDriverWait(_chromeDriver,TimeSpan.FromMilliseconds(100));
        }

        public async Task<IHtmlDocument> GetPageSource(string url)
        {
            //var domParser = new HtmlParser();
            Task.Run(() => _chromeDriver.Navigate().GoToUrl(url)).Wait(100);
            var categories = _chromeDriver.FindElements(By.ClassName("js-sidebar-tournament")).ToList();
            var categoriesNum = categories.Count;
            for (int i = 2; i < categoriesNum; i++)
            {
                try
                {
                    categories[i].Click();
                    _webDriverWait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("js-standings-tables-part-panels")));
                    var teamsTab = _chromeDriver.FindElements(By.ClassName("js-standings-tables-part-panels")).ToList();
                    var activeTab = teamsTab.FirstOrDefault().FindElement(By.ClassName("active"));
                    var teams = activeTab.FindElements(By.ClassName("cell--standings")).ToList();
                    var teamsCount = teams.Count;
                    for (int j = 0; j < teamsCount; j++)
                    {
                        var click = teams[j].FindElement(By.ClassName("standings__team-name"));
                        var href = click.FindElement(By.ClassName("js-link"));
                        Task.Run(() => href.Click()).Wait();
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
                        await ParseTeamMatches(matches, team.Id);

                        Task.Run(() => _chromeDriver.Navigate().Back()).Wait();

                        teamsTab = _chromeDriver.FindElements(By.ClassName("js-standings-tables-part-panels")).ToList();
                        activeTab = teamsTab.FirstOrDefault().FindElement(By.ClassName("active"));
                        teams = activeTab.FindElements(By.ClassName("cell--standings")).ToList();
                    }
                    //await GetTeam(teams);
                    Task.Run(() => _chromeDriver.Navigate().Back()).Wait();
                    categories = _chromeDriver.FindElements(By.ClassName("js-sidebar-tournament")).ToList();
                    return null;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return null;
        }

        private async Task ParseTeamMatches(List<IWebElement> matches, Guid teamId)
        {
            foreach (var item in matches)
            {
                var matchLinks = item.FindElements(By.TagName("a"));
                foreach (var link in matchLinks)
                {
                    var date = link.FindElement(By.ClassName("u-w64")).Text;
                    DateTime.TryParse(date, out DateTime matchTime);
                    var match = new Match();
                    match.TeamId = teamId;
                    match.FootballDate = matchTime;
                    Task.Run(() => link.Click()).Wait();
                }
            }
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
