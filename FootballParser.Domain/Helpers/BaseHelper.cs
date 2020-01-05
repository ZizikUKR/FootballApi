using AngleSharp.Dom;
using FootballParser.Domain.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FootballParser.Domain.Helpers
{
    public class BaseHelper : IBaseHelper
    {
        protected IHtmlCollection<IElement> _divs;
        private readonly Dictionary<char, char> _replacements;

        public BaseHelper()
        {
            _replacements = SetDictionary();
        }

        //public T CreatProduct<T>(string name, string desc, int? weight, int? count, int? price, string fullImg, int type, string logo) where T : Product
        //{
        //    T product = (T)Activator.CreateInstance(typeof(T));

        //    product.Name = Converter(name);
        //    product.Description = desc;
        //    product.Weight = weight;
        //    product.Count = count;
        //    product.Price = price;
        //    product.Image = fullImg;
        //    product.RestaurantId = type;
        //    product.Logo = logo;

        //    return product;
        //}

        public int? StringToInt(string str)
        {
            if (str == null)
            {
                return null;
            }

            str = Regex.Replace(str, "[^0-9]", "");
            var number = Int32.Parse(str);
            return number;
        }

        public int[] StringToIntArray(string str)
        {
            string productString = Regex.Replace(str, @"[^0-9$-.;:/]", "");

            if (!int.TryParse(new string(productString[0], 1), out _))
            {
                productString = productString.Substring(1, productString.Length - 1);
            }
            if (!int.TryParse(new string(productString[productString.Length - 1], 1), out _))
            {
                productString = productString.Substring(0, productString.Length - 1);
            }

            string ch = Regex.Replace(productString, @"[0-9]", "");

            int[] setData = productString.Split(ch).Select(x =>
            {
                if (!int.TryParse(x, out int result))
                {
                    return 0;
                }
                return result;
            }).ToArray();

            return setData;
        }

        //public TempSet CreateTempSet(string str)
        //{
        //    int[] setData = StringToIntArray(str);

        //    var set = new TempSet();

        //    if (setData.Length < 2)
        //    {
        //        set.Weight = setData[0];
        //    }
        //    if (setData.Length > 1)
        //    {
        //        if (setData[0] > setData[1])
        //        {
        //            set.Count = setData[1];
        //            set.Weight = setData[0];
        //            return set;
        //        }
        //        set.Count = setData[0];
        //        set.Weight = setData[1];
        //    }

        //    return set;
        //}

        public string GetData(IElement iElement, string selector)
        {
            IElement currentIElement = iElement.QuerySelector(selector);
            if (currentIElement == null)
            {
                return null;
            }

            string result = currentIElement.Text();
            result = result.Replace("\n", " ").Trim();
            if (result.Length < 1)
            {
                return null;
            }
            return result;
        }

        public string GetData(IElement iElement, string selector, string attribute)
        {
            IElement currentIElement = iElement.QuerySelector(selector);
            if (currentIElement == null)
            {
                return null;
            }
            string result = currentIElement.GetAttribute(attribute);
            result = result.Replace("\n", " ").Trim();
            if (result.Length < 1)
            {
                return null;
            }
            return result;
        }

        private string Converter(string str)
        {
            var sb = new StringBuilder(str);
            foreach (var kvp in _replacements)
            {
                sb.Replace(kvp.Key, kvp.Value);
            }
            return sb.ToString();
        }

        private Dictionary<char, char> SetDictionary()
        {
            Dictionary<char, char> Replacements = new Dictionary<char, char>()
            {
                ['a'] = 'а',
                ['A'] = 'А',
                ['B'] = 'В',
                ['c'] = 'с',
                ['C'] = 'С',
                ['e'] = 'е',
                ['E'] = 'Е',
                ['H'] = 'Н',
                ['i'] = 'і',
                ['I'] = 'І',
            };
            return Replacements;
        }

        public string GetLogoPath(string type)
        {
            return $"../../../assets/img/logo/{type}.svg";
        }
    }
}
