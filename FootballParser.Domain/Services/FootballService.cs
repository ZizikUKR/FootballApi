using FootballParser.Domain.Entities;
using FootballParser.Domain.Providers.Interfaces;
using FootballParser.Domain.Services.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FootballParser.Domain.Services
{
    public class FootballService : IFootballService
    {
        //private readonly IFootballProvider _footballProvider;
        private ChromeDriver _chromeDriver;
        private WebDriverWait _webDriverWait;
        private readonly string _url;

        public FootballService()
        {
            //_footballProvider = footballProvider;
            _chromeDriver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            // _webDriverWait = new WebDriverWait(_chromeDriver, TimeSpan.FromMilliseconds(100));
            _chromeDriver.Manage().Window.Maximize();
            _url = $"https://www.sofascore.com/football/livescore";
        }
        public async Task GetStatistic()
        {
            //await _footballProvider.GetMatches();
        }

        public async Task GetLive()
        {
            try
            {
                var matchesToSend = new List<LiveMatch>();
                Task.Run(() => _chromeDriver.Navigate().GoToUrl(_url)).Wait();

                var league = _chromeDriver.FindElements(By.ClassName("js-event-list-tournament-events")).ToList();
                for (int i = 0; i < league.Count; i++)
                {
                    try
                    {
                        var matches = league[i].FindElements(By.CssSelector("a")).ToList();
                        for (int z = 0; z < matches.Count; z++)
                        {
                            await GetMatch(matches[z], matchesToSend);
                        }
                    }
                    catch(Exception ex)
                    {

                    }
                    league = _chromeDriver.FindElements(By.ClassName("js-event-list-tournament-events")).ToList();
                }
            }
            catch (Exception ex)
            {

            }
        }
        private async Task GetMatch(IWebElement element,List<LiveMatch> matches)
        {
            try
            {
                var timeDiv = element.FindElement(By.ClassName("event-live"));
                var timeLines = timeDiv.FindElement(By.CssSelector("span")).Text;
                //if (int.TryParse(timeLines, out int time))
                //{
                //if (time >= 60 && time <= 75)
                //{
                Task.Run(() => element.Click()).Wait();
                var statisticDiv = _chromeDriver.FindElement(By.ClassName("js-details-widget-container"));
                var statistic = statisticDiv.FindElement(By.LinkText("Statistics"));
                if (statistic != null)
                {
                    Task.Run(() => statistic.Click()).Wait(100);
                    var statisticContainer = _chromeDriver.FindElement(By.ClassName("statistics-container"));
                    var allStatistic = statisticContainer.FindElements(By.ClassName("stat-group-event")).ToList();
                    var match = new LiveMatch();
                    foreach (var item in allStatistic)
                    {
                        var subStatistic = item.FindElements(By.ClassName("cell--incident"));
                        foreach (var subItem in subStatistic)
                        {

                        }
                    }
                }
                //}
                // }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
