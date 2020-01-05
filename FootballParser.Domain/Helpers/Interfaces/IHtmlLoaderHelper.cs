using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using System.Threading.Tasks;

namespace FootballParser.Domain.Helpers.Interfaces
{
    public interface IHtmlLoaderHelper
    {
        Task<IHtmlDocument> GetPageSource(string url);
    }
}
