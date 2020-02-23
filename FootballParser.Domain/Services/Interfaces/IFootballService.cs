using System.Threading.Tasks;

namespace FootballParser.Domain.Services.Interfaces
{
    public interface IFootballService
    {
        Task GetStatistic();
        Task GetLive();
    }
}
