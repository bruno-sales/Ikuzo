using System;
using System.Collections.Generic; 
using Ikuzo.Domain.Entities;

namespace Ikuzo.Domain.Helpers
{
    public static class EnumHelper
    {
        public static List<int> GetTagIds(List<string> tags)
        {
            var enumTags = new List<int>();

            if (tags == null || tags.Count == 0) return enumTags;

            foreach (var tag in tags)
            {
                var parsed = Enum.TryParse<TagTypes>(tag, true, out var enumTag);
                enumTags.Add(parsed ? (int)enumTag : (int)TagTypes.None);
            }

            return enumTags;
        }
    }
}
