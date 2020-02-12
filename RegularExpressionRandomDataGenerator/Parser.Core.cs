using System.Collections.Generic;

namespace RegularExpressionDataGenerator
{
    public class Parser
    {
        public NodeBuilder NodeBuilder { get; }
        Stack<ParseState> _states;
        ParseState _currentState;
        IEnumerator<IToken> _tokens;
        
        public Parser(NodeBuilder nodeBuilder)
        {
            NodeBuilder = nodeBuilder;
        }

        public INode Parse(string expression)
        {
            var tokens = new Lexer(NodeBuilder.TokenBuilder).Tokenize(expression);

            return Parse(tokens);
        }

        public INode Parse(IEnumerable<IToken> tokens)
        {
            _tokens = tokens.GetEnumerator();
            _states = new Stack<ParseState>();
            _currentState = new ParseState();
            while (_tokens.MoveNext())
            {
                _currentState.Handle(this);
            }

            while (_states.Count > 0)
            {
                EndState();
            }

            return _currentState.Close();
        }

        public void ToState(ParseState state)
        {
            _states.Push(_currentState);
            _currentState = state;
        }

        public void EndState()
        {
            var toState = _states.Pop();
            toState.AddOperand(_currentState.Close());
            _currentState = toState;
        }

        public IToken Current
        {
            get
            {
                return _tokens.Current;
            }
        }
    }
}
