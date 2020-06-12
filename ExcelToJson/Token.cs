namespace ExcelToJson
{
    public enum TokenType
    {
        None,
        String,
        Number,
        Symbol,
    }
    public struct Token
    {
        public Token(string str, int start, int len, TokenType type)
        {
            SrcStr = str;
            Start = start;
            Length = len;
            Type = type;
        }
        public readonly string SrcStr;
        public readonly int Start;
        public readonly int Length;
        public readonly TokenType Type;

        public string AsNewString()
        {
            return SrcStr.Substring(Start, Length);
        }

        public int AsNumber()
        {
            return int.Parse(AsNewString());
        }

        public char AsChar()
        {
            return SrcStr[Start];
        }

        public bool Is(char val)
        {
            return SrcStr[Start] == val;
        }

        public bool Is(string val)
        {
            if (val.Length == Length)
            {
                for (int i = 0; i < Length; ++i)
                {
                    if (val[i] != SrcStr[i + Start])
                        return false;
                }
                return true;
            }
            return false;
        }

    }
}
