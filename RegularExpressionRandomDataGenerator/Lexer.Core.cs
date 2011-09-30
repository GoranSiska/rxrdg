using System.Collections.Generic;

namespace RegularExpressionDataGenerator
{
    public class Lexer : IContext
    {
        Stack<IState> _states;
        IState _currentState;
        IEnumerator<char> _characters;

        public IEnumerable<IToken> Tokenize(string expression)
        {
            _states = new Stack<IState>();
            _currentState = new LiteralState();
            _characters = expression.GetEnumerator();
            while (_characters.MoveNext())
            {
                var token = _currentState.Handle(this);
                if (token != null)
                {
                    yield return token;
                }
            }
        }

        public void ToState(IState state)
        {
            _states.Push(_currentState);
            _currentState = state;
        }

        public void EndState()
        {
            _currentState = _states.Pop();
        }

        public char Current
        {
            get
            {
                return _characters.Current;
            }
        }
    }
}
