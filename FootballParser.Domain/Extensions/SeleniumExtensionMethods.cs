﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace FootballParser.Domain.Extensions
{
    public static class SeleniumExtensionMethods
    {
        public static IWebElement RepeatUntil<T>(this T obj,
             Func<T, IEnumerable<IWebElement>> func,
                Func<IWebElement, bool> compare,
                  int MaxRetry = 20)
        {
            //call function to get elements
            var eles = func(obj);
            IWebElement element = null;
            while (element == null && MaxRetry > 0)
            {
                MaxRetry -= 1;
                //call the iterator
                element = IterateCollection(compare, eles);
                if (element == null)
                {
                    Thread.Sleep(500);
                    //get new collection of elements
                    eles = func(obj);
                }
            };

            return element;
        }

        private static IWebElement IterateCollection(
           Func<IWebElement, bool> compare,
           IEnumerable<IWebElement> eles)
        {
            IWebElement element = null;
            eles.ToList().ForEach(
                ele =>
                {
                    //call the comparator
                    var found = compare(ele);
                    if (found) element = ele;
                });
            return element;
        }
    }
}
