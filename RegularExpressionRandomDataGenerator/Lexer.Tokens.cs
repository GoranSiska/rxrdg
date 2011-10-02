
namespace RegularExpressionDataGenerator
{
    public struct LiteralToken : IToken
    {
        public char Character { get; set; }
        public TokenType TokenType { get { return TokenType.Literal; } }
    }

    public struct RepetitionToken : IToken
    {
        public int MinOccurs { get; set; }
        public int MaxOccurs { get; set; }
        public TokenType TokenType { get { return TokenType.Repetition; } }
    }

    public struct ParenthesisLeftToken : IToken
    {
        public TokenType TokenType { get { return TokenType.ParenthesisLeft; } }
    }

    public struct ParenthesisRightToken : IToken
    {
        public TokenType TokenType { get { return TokenType.ParenthesisRight; } }
    }

    public struct ConcatenationToken : IToken
    {
        public TokenType TokenType { get { return TokenType.Concatenation; } }
    }

    public struct AlternationToken : IToken
    {
        public TokenType TokenType { get { return TokenType.Alternation; } }
    }

    public struct BracketLeftToken : IToken 
    {
        public TokenType TokenType { get { return TokenType.BracketLeft; } }
    }

    public struct BracketRightToken : IToken 
    {
        public TokenType TokenType { get { return TokenType.BracketRight; } }
    }

    public struct RangeToken : IToken
    {
        public TokenType TokenType { get { return TokenType.Range; } }
    }

    public struct NotToken : IToken
    {
        public TokenType TokenType { get { return TokenType.Not; } }
    }

    public struct AnyToken : IToken
    {
        public TokenType TokenType { get { return TokenType.Any; } }
    }

    public struct NumericToken : IToken
    {
        public TokenType TokenType { get { return TokenType.Numeric; } }
    }

    public struct WordToken : IToken
    {
        public TokenType TokenType { get { return TokenType.Word; } }
    }

    public struct WhitespaceToken : IToken
    {
        public TokenType TokenType { get { return TokenType.Whitespace; } }
    }

    public struct NonNumericToken : IToken
    {
        public TokenType TokenType { get { return TokenType.NonNumeric; } }
    }

    public struct NonWordToken : IToken
    {
        public TokenType TokenType { get { return TokenType.NonWord; } }
    }

    public struct NonWhitespaceToken : IToken
    {
        public TokenType TokenType { get { return TokenType.NonWhitespace; } }
    }
}
