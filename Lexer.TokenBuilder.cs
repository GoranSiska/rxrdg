
namespace RegularExpressionDataGenerator
{
    internal class LiteralTokenCollection : System.Collections.ObjectModel.KeyedCollection<char, LiteralToken>
    {
        protected override char GetKeyForItem(LiteralToken item)
        {
            return item.Character;
        }
    }

    public static class TokenBuilder
    {
        private static readonly object Padlock = new object();
        private static readonly LiteralTokenCollection LiteralTokens = new LiteralTokenCollection();
        public static LiteralToken BuildLiteralToken(char character)
        {
            //return new LiteralToken() { Character = character };
            if (LiteralTokens.Contains(character) == false)
            {
                lock (Padlock)
                {
                    LiteralTokens.Add(new LiteralToken { Character = character });
                }
            }
            return LiteralTokens[character];
        }

        public static RepetitionToken BuildZeroOrMoreToken()
        {
            return BuildRepetitionToken(0, -1);
        }

        public static RepetitionToken BuildOneOrMoreToken()
        {
            return BuildRepetitionToken(1, -1);
        }

        public static RepetitionToken BuildZeroOrOneToken()
        {
            return BuildRepetitionToken(0, 1);
        }

        public static RepetitionToken BuildRepetitionToken(int minOccurs, int maxOccurs)
        {
            return new RepetitionToken { MinOccurs = minOccurs, MaxOccurs = maxOccurs};
        }

        public static ParenthesisLeftToken BuildParenthesisLeftToken()
        {
            return new ParenthesisLeftToken();
        }

        public static ParenthesisRightToken BuildParenthesisRightToken()
        {
            return new ParenthesisRightToken();
        }

        public static AlternationToken BuildAlternationToken()
        {
            return new AlternationToken();
        }

        public static BracketRightToken BuildBracketRightToken()
        {
            return new BracketRightToken();
        }

        public static BracketLeftToken BuildBracketLeftToken()
        {
            return new BracketLeftToken();
        }

        public static RangeToken BuildRangeToken()
        {
            return new RangeToken();
        }

        public static NotToken BuildNotToken()
        {
            return new NotToken();
        }

        public static AnyToken BuildAnyToken()
        {
            return new AnyToken();
        }

        public static NumericToken BuildNumericToken()
        {
            return new NumericToken();
        }

        public static WordToken BuildWordToken()
        {
            return new WordToken();
        }

        public static WhitespaceToken BuildWhitespaceToken()
        {
            return new WhitespaceToken();
        }

        public static NonNumericToken BuildNonNumericToken()
        {
            return new NonNumericToken();
        }

        public static NonWordToken BuildNonWordToken()
        {
            return new NonWordToken();
        }

        public static NonWhitespaceToken BuildNonWhitespaceToken()
        {
            return new NonWhitespaceToken();
        }
    }
}
