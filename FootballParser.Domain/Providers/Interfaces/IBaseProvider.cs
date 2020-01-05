using AngleSharp.Dom;
using System.Threading.Tasks;

namespace FootballParser.Domain.Providers.Interfaces
{
    public interface IBaseProvider
    {
        Task<IDocument> GetPage(string url);
    }
}
