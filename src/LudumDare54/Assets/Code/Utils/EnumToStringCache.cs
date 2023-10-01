using System;
using System.Collections.Generic;

namespace Savidiy.Utils
{
    public sealed class EnumToStringCache<T> where T : Enum
    {
        private readonly Dictionary<T, string> _typesStrings = new ();

        public EnumToStringCache()
        {
            foreach (T value in Enum.GetValues(typeof(T)))
            {
                _typesStrings.Add(value,value.ToString().ToLower());
            }
        }
        public string ToStringCached(T type)
        {
            return _typesStrings[type];
        }
    }
}