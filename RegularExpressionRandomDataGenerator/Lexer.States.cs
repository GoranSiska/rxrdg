using System;

namespace RegularExpressionDataGenerator
{
    internal class LiteralState : IState
    {
        public IToken Handle(IContext context)
        {
            var tokenBuilder = context.TokenBuilder;
            switch (context.Current)
            {
                case '(':
                    return tokenBuilder.BuildParenthesisLeftToken();
                case ')':
                    return tokenBuilder.BuildParenthesisRightToken();
                case '{':
                    context.ToState(new RepetitionState());
                    break;
                case '\\':
                    context.ToState(new EscapeState());
                    break;
                case '[':
                    context.ToState(new BeginSetState());
                    return tokenBuilder.BuildBracketLeftToken();
                case '*':
                    return tokenBuilder.BuildZeroOrMoreToken();
                case '?':
                    return tokenBuilder.BuildZeroOrOneToken();
                case '+':
                    return tokenBuilder.BuildOneOrMoreToken();
                case '|':
                    return tokenBuilder.BuildAlternationToken();
                case '.':
                    return tokenBuilder.BuildAnyToken();
                default:
                    return tokenBuilder.BuildLiteralToken(context.Current);
            }
            return null;
        }
    }

    internal class BeginSetState : IState
    {
        public IToken Handle(IContext context)
        {
            var tokenBuilder = context.TokenBuilder;
            switch (context.Current)
            {
                case '^':
                    return tokenBuilder.BuildNotToken();
                case '\\':
                    context.ToState(new SetState());
                    context.ToState(new EscapeState());
                    break;
                default:
                    context.ToState(new SetState());
                    return tokenBuilder.BuildLiteralToken(context.Current);
            }
            return null;
        }
    }

    internal class SetState : IState
    {
        public IToken Handle(IContext context)
        {
            var tokenBuilder = context.TokenBuilder;
            switch (context.Current)
            {
                case ']':
                    context.EndState();
                    context.EndState();
                    return tokenBuilder.BuildBracketRightToken();
                case '-':
                    return tokenBuilder.BuildRangeToken();
                case '\\':
                    context.ToState(new EscapeState());
                    break;
                default:
                    return tokenBuilder.BuildLiteralToken(context.Current);
            }
            return null;
        }
    }

    internal class EscapeState : IState
    {
        public IToken Handle(IContext context)
        {
            var tokenBuilder = context.TokenBuilder;
            context.EndState();
            switch (context.Current)
            {
                case 'd':
                    return tokenBuilder.BuildNumericToken();
                case 'w':
                    return tokenBuilder.BuildWordToken();
                case 's':
                    return tokenBuilder.BuildWhitespaceToken();
                case 'D':
                    return tokenBuilder.BuildNonNumericToken();
                case 'W':
                    return tokenBuilder.BuildNonWordToken();
                case 'S':
                    return tokenBuilder.BuildNonWhitespaceToken();
                default:
                    return tokenBuilder.BuildLiteralToken(context.Current);
            }
        }
    }

    internal class RepetitionState : IState
    {
        int _minOccurs = -1;
        int _maxOccurs = -1;
        bool _isParsingMinOccurs = true;
        bool _isParsingFirstValue = true;
        int _currentNumber;
        
        public IToken Handle(IContext context)
        {
            var tokenBuilder = context.TokenBuilder;
            var character = context.Current;
            switch (character)
            {
                case ',':
                    if (_isParsingMinOccurs)
                    {
                        if (_isParsingFirstValue)
                        {
                            //missing minOccurs
                            throw new ArgumentException();
                        }
                        //end
                        _minOccurs = _currentNumber;
                        _currentNumber = 0;
                        _isParsingMinOccurs = false;
                        _isParsingFirstValue = true;
                    }
                    else
                    {
                        //too many ,
                        throw new ArgumentException();
                    }
                    break;
                case '}':
                    if (_isParsingMinOccurs)
                    {
                        if (_isParsingFirstValue)
                        {
                            //missing minOccurs
                            throw new ArgumentException();
                        }
                        //minOccurs equals maxOccurs
                        _minOccurs = _currentNumber;
                        _maxOccurs = _currentNumber;
                    }
                    else
                    {
                        if (_isParsingFirstValue)
                        {
                            //maxOccus = unlimited
                            _maxOccurs = -1;
                        }
                        else
                        {
                            //end
                            _maxOccurs = _currentNumber;
                        }
                    }
                    context.EndState();
                    return tokenBuilder.BuildRepetitionToken(_minOccurs, _maxOccurs);
                default:
                    var result = 0;
                    if(int.TryParse(character.ToString(), out result))
                    {
                        _currentNumber = _currentNumber*10 + result;
                        _isParsingFirstValue = false;
                    }
                    else
                    {
                        throw   new ArgumentException();
                    }
                    break;
            }
            return null;
        }
    }
}
