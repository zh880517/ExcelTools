using System;
using System.Collections.Generic;
using System.Text;

namespace LibExcel
{
    public interface IStringMatch
    {
        bool Match(string val);
    }

    public class StringSetMatch : IStringMatch
    {
        private readonly HashSet<string> names;
        public StringSetMatch(IEnumerable<string> set)
        {
            names = new HashSet<string>(set);
        }
        public bool Match(string val)
        {
            return names.Contains(val);
        }
    }

    public class StringEndMatch : IStringMatch
    {
        private readonly string end;

        public StringEndMatch(string key)
        {
            end = key;
        }

        public bool Match(string val)
        {
            return val.EndsWith(end);
        }
    }

    public class StringMatch
    {
        private List<IStringMatch> matchList = new List<IStringMatch>();

        public StringMatch Add(IStringMatch match)
        {
            matchList.Add(match);
            return this;
        }

        public bool Match(string val)
        {
            if (val == null)
                return false;
            foreach (var match in matchList)
            {
                if (match.Match(val))
                    return true;
            }
            return false;
        }
    }
}
