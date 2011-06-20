using System.Collections.Generic;

namespace RegularExpressionDataGenerator
{
    public enum TokenType
    {
        Literal,
        Repetition,
        ParenthesisLeft,
        ParenthesisRight,
        Concatenation,
        Alternation,
        BracketLeft,
        BracketRight, 
        Range, 
        Not,
        Any,
        Numeric,
        Word,
        Whitespace,
        NonNumeric,
        NonWord,
        NonWhitespace

    }

    public interface IToken
    {
        TokenType TokenType { get; }
    }

    public interface IState
    {
        IToken Handle(IContext context);
    }

    public interface IContext
    {
        IEnumerable<IToken> Tokenize(string expression);
        void ToState(IState state);
        void EndState();
        char Current { get; }
    }
}
