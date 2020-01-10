using AngleSharp.Dom;
using FootballParser.Domain.Entities;
using FootballParser.Domain.Helpers.Interfaces;
using FootballParser.Domain.Providers.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FootballParser.Domain.Providers
{
    public class FootballProvider : BaseProvider, IFootballProvider
    {

        private readonly IFootballHelper _footballHelper;
        public FootballProvider(IHtmlLoaderHelper htmlLoaderHelper, IConfiguration сonfiguration, IFootballHelper footballHelper)
            : base(htmlLoaderHelper, сonfiguration)
        {
            _footballHelper = footballHelper;
        }
        public async Task<IEnumerable<TeamMatchStatistics>> GetMatches()
        {
            List<TeamMatchStatistics> result = await ParsePage<TeamMatchStatistics>("https://www.sofascore.com/football");
            return result;
        }

        private async Task<List<T>> ParsePage<T>(string url) where T : TeamMatchStatistics
        {
            IDocument document = await GetPage(url);
            List<T> result = _footballHelper.Parse<T>(document);

            return result;
        }
    }
}
