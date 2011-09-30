using System.Collections.Generic;
using System.Linq;

namespace RegularExpressionDataGenerator
{
    public class ParseState
    {
        public void Handle(Parser context)
        {
            var token = context.Current;
            switch (token.TokenType)
            {
                case TokenType.Literal:
                    var literal = (LiteralToken)token;
                    INode literalNode = new LiteralNode(literal);
                    AddOperand(literalNode);
                    break;
                case TokenType.Repetition:
                    var repetition = (RepetitionToken)token;
                    INode repetitionNode = new RepetitionNode(repetition);
                    AddOperator(repetitionNode);
                    break;
                case TokenType.ParenthesisLeft:
                    context.ToState(new ParseState());
                    break;
                case TokenType.ParenthesisRight:
                    var paranthesis = (ParenthesisRightToken)token;
                    INode paranthesisRightNode = new ParenthesisNode(paranthesis);
                    AddOperator(paranthesisRightNode);
                    context.EndState();
                    break;
                case TokenType.Alternation:
                    var alternation = (AlternationToken)token;
                    INode alternationNode = new AlternationNode(alternation);
                    AddOperator(alternationNode);                  
                    break;
                case TokenType.Range: 
                    var range = (RangeToken)token;
                    INode rangeNode = new RangeNode(range);
                    AddOperator(rangeNode);
                    break;
                case TokenType.BracketLeft: 
                    context.ToState(new ParseState());
                    break;
                case TokenType.BracketRight:
                    var set = (BracketRightToken)token;
                    INode setNode = new BracketNode(set);
                    AddOperator(setNode);
                    context.EndState();
                    break;
                case TokenType.Any:
                    var anyNode = NodeBuilder.BuildAnyNode();
                    AddOperand(anyNode);
                    break;
                case TokenType.Not:
                    var not = (NotToken)token;
                    INode notNode = new NotNode(not);
                    AddOperator(notNode);
                    break;
                case TokenType.Numeric:
                    var numeric = (NumericToken)token;
                    INode numericNode = NodeBuilder.BuildNumericNode();
                    AddOperand(numericNode);
                    break;
                case TokenType.Word:
                    var word = (WordToken)token;
                    INode wordNode = NodeBuilder.BuildWordNode();
                    AddOperand(wordNode);
                    break;
                case TokenType.Whitespace:
                    var whitespace = (WhitespaceToken)token;
                    INode whitespaceNode = NodeBuilder.BuildWhitespaceNode();
                    AddOperand(whitespaceNode);
                    break;
                case TokenType.NonNumeric:
                    var nonNumeric = (NonNumericToken)token;
                    INode nonNumericNode = NodeBuilder.BuildNonNumericNode();
                    AddOperand(nonNumericNode);
                    break;
                case TokenType.NonWord:
                    var nonWord = (NonWordToken)token;
                    INode nonWordNode = NodeBuilder.BuildNonWordNode();
                    AddOperand(nonWordNode);
                    break;
                case TokenType.NonWhitespace:
                    var nonWhitespace = (NonWhitespaceToken)token;
                    INode nonWhitespaceNode = NodeBuilder.BuildNonWhitespaceNode();
                    AddOperand(nonWhitespaceNode);
                    break;
                default:
                    break;
            }
        }

//        Word - \w
//Whitespace - \s
//NotDigit? - \D
//NotWord? - \W
//NotWhitespace? - \S

        private readonly Stack<INode> _operators = new Stack<INode>();
        private readonly Stack<INode> _operands = new Stack<INode>();

        public void AddOperator(INode operatorNode)
        {
            while (_operators.Count > 0 && _operators.Peek().Precedence > operatorNode.Precedence)
            {
                ProcessOperator(_operators.Pop());
            }
            _operators.Push(operatorNode);
        }

        public void AddOperand(INode operandNode)
        {
            if (IsBalanced() == false)
            {
                AddOperator(new ConcatenationNode(new ConcatenationToken()));
            }
            _operands.Push(operandNode);
        }

        public void ProcessOperator(INode operatorNode)
        {
            switch (operatorNode.NodeType)
            {
                case NodeType.UnaryOperator:
                    operatorNode.ChildNodes.Insert(0, _operands.Pop());
                    _operands.Push(operatorNode);
                    break;
                case NodeType.BinaryOperator:
                    var lastOperand = _operands.Pop();
                    if (lastOperand.Token.TokenType == TokenType.Concatenation && operatorNode.Token.TokenType == TokenType.Concatenation)
                    {
                        foreach (var childNode in lastOperand.ChildNodes)
                        {
                            operatorNode.ChildNodes.Add(childNode);
                        }
                    }
                    else
                    {
                        operatorNode.ChildNodes.Add(lastOperand);
                    }
                    operatorNode.ChildNodes.Insert(0, _operands.Pop());
                    _operands.Push(operatorNode);
                    break;
                case NodeType.Operand:
                default:
                    break;
            }
        }

        private bool IsBalanced()
        {
            var binaryOperatorsCount = _operators.Count(operatorNode => operatorNode.NodeType == NodeType.BinaryOperator);
            
            return binaryOperatorsCount == _operands.Count;
        }

        public INode Close()
        {
            while (_operators.Count > 0)
            {
                ProcessOperator(_operators.Pop());
            }
            return _operands.Pop();
        }
    }
}
