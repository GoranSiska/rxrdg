using System;

namespace RegularExpressionDataGenerator
{
    internal class LiteralState : IState
    {
        public IToken Handle(IContext context)
        {
            switch (context.Current)
            {
                case '(':
                    return TokenBuilder.BuildParenthesisLeftToken();
                case ')':
                    return TokenBuilder.BuildParenthesisRightToken();
                case '{':
                    context.ToState(new RepetitionState());
                    break;
                case '\\':
                    context.ToState(new EscapeState());
                    break;
                case '[':
                    context.ToState(new BeginSetState());
                    return TokenBuilder.BuildBracketLeftToken();
                case '*':
                    return TokenBuilder.BuildZeroOrMoreToken();
                case '?':
                    return TokenBuilder.BuildZeroOrOneToken();
                case '+':
                    return TokenBuilder.BuildOneOrMoreToken();
                case '|':
                    return TokenBuilder.BuildAlternationToken();
                case '.':
                    return TokenBuilder.BuildAnyToken();
                default:
                    return TokenBuilder.BuildLiteralToken(context.Current);
            }
            return null;
        }
    }

    internal class BeginSetState : IState
    {
        public IToken Handle(IContext context)
        {
            switch (context.Current)
            {
                case '^':
                    return TokenBuilder.BuildNotToken();
                case '\\':
                    context.ToState(new SetState());
                    context.ToState(new EscapeState());
                    break;
                default:
                    context.ToState(new SetState());
                    return TokenBuilder.BuildLiteralToken(context.Current);
            }
            return null;
        }
    }

    internal class SetState : IState
    {
        public IToken Handle(IContext context)
        {
            switch (context.Current)
            {
                case ']':
                    context.EndState();
                    context.EndState();
                    return TokenBuilder.BuildBracketRightToken();
                case '-':
                    return TokenBuilder.BuildRangeToken();
                case '\\':
                    context.ToState(new EscapeState());
                    break;
                default:
                    return TokenBuilder.BuildLiteralToken(context.Current);
            }
            return null;
        }
    }

    internal class EscapeState : IState
    {
        public IToken Handle(IContext context)
        {
            context.EndState();
            switch (context.Current)
            {
                case 'd':
                    return TokenBuilder.BuildNumericToken();
                case 'w':
                    return TokenBuilder.BuildWordToken();
                case 's':
                    return TokenBuilder.BuildWhitespaceToken();
                case 'D':
                    return TokenBuilder.BuildNonNumericToken();
                case 'W':
                    return TokenBuilder.BuildNonWordToken();
                case 'S':
                    return TokenBuilder.BuildNonWhitespaceToken();
                default:
                    return TokenBuilder.BuildLiteralToken(context.Current);
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
                    return TokenBuilder.BuildRepetitionToken(_minOccurs, _maxOccurs);
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
