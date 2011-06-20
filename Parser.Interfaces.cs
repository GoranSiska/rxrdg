using System.Collections.Generic;

namespace RegularExpressionDataGenerator
{
    public enum NodeType
    {
        UnaryOperator,
        BinaryOperator,
        Operand
    }

    public interface INode : IVisitable
    {
        IList<INode> ChildNodes { get; }
        IToken Token { get; }
        NodeType NodeType { get; }
        int Precedence { get; }
    }
}
