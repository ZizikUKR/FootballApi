using FootballParser.Domain.Providers.Interfaces;
using FootballParser.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FootballParser.Domain.Services
{
    public class FootballService : IFootballService
    {
        private readonly IFootballProvider _footballProvider;
        public FootballService(IFootballProvider footballProvider)
        {
            _footballProvider = footballProvider;
        }
        public async Task GetStatistic()
        {
            await _footballProvider.GetMatches();
        }
    }
}
