using AngleSharp.Dom;
using FootballParser.Domain.Entities;
using FootballParser.Domain.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballParser.Domain.Helpers
{
    public class FootballHelper: BaseHelper, IFootballHelper
    {
        public List<T> Parse<T>(IDocument document) where T : TeamMatchStatistics
        {
            IHtmlCollection<IElement> iElementList = document.QuerySelectorAll("div.js-event-list-tournament-events");

            List<T> products = Enumerable
                .Range(0, iElementList.Count())
                .Select(i => CreateProduct<T>(iElementList[i]))
                .ToList();

            return products;
        }

        private T CreateProduct<T>(IElement td) where T : TeamMatchStatistics
        {
            IElement imgElement = td.QuerySelector("a.product-img");
            IElement nameElemnt = td.QuerySelector("div.product-title");

            string desc = GetData(td, "div.product-description");
            string priceStr = GetData(td, "div.price");
            string img = imgElement.Children.First().GetAttribute("src");
            string fullImg = $"https://mafia.ua{img}";
            string name = nameElemnt.Children.First().Text().Replace("\n", " ").Trim();

            string test = GetData(td, "div.product-weight");
            //TempSet set = CreateTempSet(test);

            //int? price = StringToInt(priceStr);
            //string logo = GetLogoPath(RestaurantType.Mafia.ToString());

            //T product = CreatProduct<T>(name, desc, set.Weight, set.Count, price, fullImg, (int)RestaurantType.Mafia, logo);
            TeamMatchStatistics product = new TeamMatchStatistics();
            return (T)product;
        }
    }
}
