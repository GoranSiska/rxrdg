
namespace RegularExpressionDataGenerator
{
    internal class LiteralTokenCollection : System.Collections.ObjectModel.KeyedCollection<char, LiteralToken>
    {
        protected override char GetKeyForItem(LiteralToken item)
        {
            return item.Character;
        }
    }

    public class TokenBuilder
    {
        private readonly object Padlock = new object();
        private readonly LiteralTokenCollection LiteralTokens = new LiteralTokenCollection();
        public LiteralToken BuildLiteralToken(char character)
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

        public RepetitionToken BuildZeroOrMoreToken()
        {
            return BuildRepetitionToken(0, -1);
        }

        public RepetitionToken BuildOneOrMoreToken()
        {
            return BuildRepetitionToken(1, -1);
        }

        public RepetitionToken BuildZeroOrOneToken()
        {
            return BuildRepetitionToken(0, 1);
        }

        public RepetitionToken BuildRepetitionToken(int minOccurs, int maxOccurs)
        {
            return new RepetitionToken { MinOccurs = minOccurs, MaxOccurs = maxOccurs};
        }

        public ParenthesisLeftToken BuildParenthesisLeftToken()
        {
            return new ParenthesisLeftToken();
        }

        public ParenthesisRightToken BuildParenthesisRightToken()
        {
            return new ParenthesisRightToken();
        }

        public AlternationToken BuildAlternationToken()
        {
            return new AlternationToken();
        }

        public BracketRightToken BuildBracketRightToken()
        {
            return new BracketRightToken();
        }

        public BracketLeftToken BuildBracketLeftToken()
        {
            return new BracketLeftToken();
        }

        public RangeToken BuildRangeToken()
        {
            return new RangeToken();
        }

        public NotToken BuildNotToken()
        {
            return new NotToken();
        }

        public AnyToken BuildAnyToken()
        {
            return new AnyToken();
        }

        public NumericToken BuildNumericToken()
        {
            return new NumericToken();
        }

        public WordToken BuildWordToken()
        {
            return new WordToken();
        }

        public WhitespaceToken BuildWhitespaceToken()
        {
            return new WhitespaceToken();
        }

        public NonNumericToken BuildNonNumericToken()
        {
            return new NonNumericToken();
        }

        public NonWordToken BuildNonWordToken()
        {
            return new NonWordToken();
        }

        public NonWhitespaceToken BuildNonWhitespaceToken()
        {
            return new NonWhitespaceToken();
        }
    }
}
