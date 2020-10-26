using FootballParser.Domain.Entities;
using FootballParser.Domain.Extensions;
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
using System.Threading;
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

                //var league = _chromeDriver.FindElements(By.ClassName("js-event-list-tournament-events")).ToList();
                var league = _chromeDriver.FindElements(By.ClassName("EventCellstyles__Link-sc-1m83enb-0")).ToList();
                //for (int i = 0; i < league.Count; i++)
                //{
                //    try
                //    {
                //        var matches = league[i].FindElements(By.CssSelector("a")).ToList();
                //        for (int z = 0; z < matches.Count; z++)
                //        {
                //            await GetMatch(matches[z], matchesToSend);
                //        }
                //    }
                //    catch(Exception ex)
                //    {

                //    }
                //    league = _chromeDriver.FindElements(By.ClassName("js-event-list-tournament-events")).ToList();
                //}
                for (int i = 0; i < league.Count; i++)
                {
                    try
                    {
                        await GetMatch(league[i], matchesToSend);
                    }
                    catch (Exception ex)
                    {

                    }
                    league = _chromeDriver.FindElements(By.ClassName("EventCellstyles__Link-sc-1m83enb-0")).ToList();
                }

                var res = await CompareStatistick(matchesToSend);
            }
            catch (Exception ex)
            {

            }

        }

        private async Task<List<LiveMatch>> CompareStatistick(List<LiveMatch> liveMatches)
        {
            return null;
        }
        private async Task GetMatch(IWebElement element, List<LiveMatch> matches)
        {
            try
            {
                Task.Run(() => element.Click()).Wait();
                var teamNames = element.FindElements(By.ClassName("kCvfzg"));
                string homeTeamName = teamNames[0].Text;
                string awayTeamName = teamNames[1].Text;

                Thread.Sleep(300);
               
                var timeSpan = _chromeDriver.FindElement(By.ClassName("cLKHcg"));
                string timeLines = timeSpan.Text.Substring(0, 2);
                var match = new LiveMatch();

                isScoreRight(match);
                if (int.TryParse(timeLines, out int time))
                {
                    //if (time >= 60 && time <= 75 && isScoreRight())
                    //{
                    var statistic = _chromeDriver.FindElement(By.XPath("//a[normalize-space() = 'Statistics']"));
                    if (statistic != null)
                    {
                        Task.Run(() => statistic.Click()).Wait();
                        Thread.Sleep(300);


                        var allStatistic = _chromeDriver.FindElements(By.ClassName("StatisticsStyles__StatisticsItemCell-zf4n59-2"));
                        match.AwayTeamName = awayTeamName;
                        match.HomeTeamName = homeTeamName;
                        // match.AwayTeam = new LiveTeam { Name = awayTeamName };iuGgIl
                        // match.HomeTeam = new LiveTeam { Name = homeTeamName };

                        foreach (var item in allStatistic)
                        {
                            try
                            {
                                string homeTeamValue = string.Empty;
                                string statisticItemName = string.Empty;
                                string awayTeamValue = string.Empty;
                                var threeStatisticBlocks = item.FindElements(By.ClassName("Section-sc-1a7xrsb-0"));
                                homeTeamValue = threeStatisticBlocks[0].FindElement(By.TagName("div")).Text;
                                statisticItemName = threeStatisticBlocks[1].FindElement(By.TagName("div")).Text;
                                awayTeamValue = threeStatisticBlocks[2].FindElement(By.TagName("div")).Text;

                                match.Statistics.Add(new LiveStatistic { StatisticName = statisticItemName, HomeTeamValue = homeTeamValue, AwayTeamValue = awayTeamValue });
                                //   match.Statistics.Add(new LiveStatistic { Name = statisticItemName, Value = awayTeamValue });
                            }
                            catch (Exception e)
                            {

                            }

                        }
                        matches.Add(match);
                    }
                    //}
                }
            }
            catch (Exception ex)
            {

            }
        }

        private bool isAwayTeamMadeEnough(LiveMatch match)
        {

        }

        private bool isScoreRight(LiveMatch match)
        {
            var score = _chromeDriver.FindElement(By.ClassName("iuGgIl")).FindElements(By.TagName("span"));
            string homeGoalsText = score[0].Text;
            string awayGoalsText = score[1].Text;
            int.TryParse(awayGoalsText, out int awayGoals);
            int.TryParse(homeGoalsText, out int homeGoals);

            match.AwayGoals = awayGoals;
            match.HomeGoals = homeGoals;

            return awayGoals == 0 || homeGoals == 0;
        }

        private bool IsElementExist(string className, IWebElement element)
        {
            try
            {
                element.FindElement(By.ClassName(className));
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
