
namespace RegularExpressionDataGenerator
{
    public class LiteralToken : IToken
    {
        public char Character { get; set; }
        public TokenType TokenType { get { return TokenType.Literal; } }
    }

    public class RepetitionToken : IToken
    {
        public int MinOccurs { get; set; }
        public int MaxOccurs { get; set; }
        public TokenType TokenType { get { return TokenType.Repetition; } }
    }

    public class ParenthesisLeftToken : IToken
    {
        public TokenType TokenType { get { return TokenType.ParenthesisLeft; } }
    }

    public class ParenthesisRightToken : IToken
    {
        public TokenType TokenType { get { return TokenType.ParenthesisRight; } }
    }

    public class ConcatenationToken : IToken
    {
        public TokenType TokenType { get { return TokenType.Concatenation; } }
    }

    public class AlternationToken : IToken
    {
        public TokenType TokenType { get { return TokenType.Alternation; } }
    }

    public class BracketLeftToken : IToken 
    {
        public TokenType TokenType { get { return TokenType.BracketLeft; } }
    }

    public class BracketRightToken : IToken 
    {
        public TokenType TokenType { get { return TokenType.BracketRight; } }
    }

    public class RangeToken : IToken
    {
        public TokenType TokenType { get { return TokenType.Range; } }
    }

    public class NotToken : IToken
    {
        public TokenType TokenType { get { return TokenType.Not; } }
    }

    public class AnyToken : IToken
    {
        public TokenType TokenType { get { return TokenType.Any; } }
    }

    public class NumericToken : IToken
    {
        public TokenType TokenType { get { return TokenType.Numeric; } }
    }

    public class WordToken : IToken
    {
        public TokenType TokenType { get { return TokenType.Word; } }
    }

    public class WhitespaceToken : IToken
    {
        public TokenType TokenType { get { return TokenType.Whitespace; } }
    }

    public class NonNumericToken : IToken
    {
        public TokenType TokenType { get { return TokenType.NonNumeric; } }
    }

    public class NonWordToken : IToken
    {
        public TokenType TokenType { get { return TokenType.NonWord; } }
    }

    public class NonWhitespaceToken : IToken
    {
        public TokenType TokenType { get { return TokenType.NonWhitespace; } }
    }
}
