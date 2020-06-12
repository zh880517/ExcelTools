using System.Collections.Generic;

namespace ExcelToJson
{
    public static class Tokenizer
    {
        public static List<Token> Parser(string source)
        {
            foreach (var ch in source)
            {
                if (ch > '\u007f')
                {
                    throw new System.Exception(string.Format("包含了非法字符 {0}", ch));
                }
            }
            List<Token> tokens = new List<Token>();
            for (int i=0; i<source.Length; ++i)
            {
                char val = source[i];
                if (IsSeparate(val))
                    continue;
                if (char.IsDigit(val))
                {
                    int len = ReadNumber(source, i);
                    Token token = new Token(source, i, len, TokenType.Number);
                    tokens.Add(token);
                    i += len;
                }
                else if (CheckString(source, i))
                {
                    int len = ReadNumber(source, i);
                    Token token = new Token(source, i, len, TokenType.String);
                    tokens.Add(token);
                    i += len;
                }
                else
                {
                    Token token = new Token(source, i, 1, TokenType.Symbol);
                    tokens.Add(token);
                }
            }
            return tokens;
        }

        public static bool CheckString(string source, int start)
        {
            if (char.IsLetter(source[start]))
                return true;
            if (source[start] == '_')
            {
                for (int i=start+1; i<source.Length; ++i)
                {
                    char ch = source[i];
                    if (char.IsLetterOrDigit(ch))
                        return true;
                    if (ch != '_')
                        return false;
                }
            }
            return false;
        }

        public static int ReadString(string source, int start)
        {
            int count = 1;
            for (int i=start+1; i<source.Length; ++i)
            {
                char val = source[i];
                if (!char.IsLetterOrDigit(val) && val != '_')
                    break;
                count++;
            }
            return count;
        }

        public static int ReadNumber(string source, int start)
        {
            int count = 1;
            for (int i = start + 1; i < source.Length; ++i)
            {
                char val = source[i];
                if (!char.IsDigit(val))
                    break;
                count++;
            }
            return count;
        }

        public static bool IsSeparate(char ch)
        {
            return ch == '\t' || ch == '\r' || ch == '\n' || char.IsWhiteSpace(ch);
        }

        public static bool IsAscii(char val)
        {
            return val <= '\u007f';
        }
    }
}
