using System;
using System.Collections.Generic;
using System.Linq;

namespace TicksPublisher.Extensions
{
    public static class EnumerableExtension
    {
        #region Privates

        private static Random m_randomizer = new Random();

        #endregion

        #region Methods

        public static T GetAny<T>(this IEnumerable<T> elements)
        {
            var index = m_randomizer.Next(0, elements.Count() - 1);
            return elements.ElementAt(index);
        }

        #endregion
    }
}
