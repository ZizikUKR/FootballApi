using FootballParser.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootballParser.Domain.Providers.Interfaces
{
    public interface IFootballProvider : IBaseProvider
    {
        Task<IEnumerable<TeamMatchStatistics>> GetMatches();
    }
}
