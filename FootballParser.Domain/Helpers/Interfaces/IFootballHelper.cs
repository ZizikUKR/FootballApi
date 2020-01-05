using AngleSharp.Dom;
using FootballParser.Domain.Entities;
using System.Collections.Generic;

namespace FootballParser.Domain.Helpers.Interfaces
{
    public interface IFootballHelper : IBaseHelper
    {
        List<T> Parse<T>(IDocument document) where T : TeamMatchStatistics;
    }
}
