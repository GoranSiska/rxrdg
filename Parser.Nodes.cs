using System.Collections.Generic;

namespace RegularExpressionDataGenerator
{
    public abstract class NodeBase : INode
    {
        protected NodeBase(IToken token)
        {
            ChildNodes = new List<INode>();
            Token = token;
        }

        public IList<INode> ChildNodes { get; internal set; }
        public IToken Token { get; internal set; }
        public abstract NodeType NodeType { get; }
        public abstract int Precedence { get; }

        public abstract void Accept(IVisitor visitor);
    }

    public class LiteralNode : NodeBase
    {

        public LiteralNode(LiteralToken token) :
            base(token) {}

        public new LiteralToken Token
        {
            get
            {
                return (LiteralToken)base.Token;
            }
        }

        public override NodeType NodeType
        {
            get { return NodeType.Operand; }
        }

        public override int Precedence
        {
            get { return 100; }
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class RepetitionNode : NodeBase
    {
        public RepetitionNode(RepetitionToken token) :
            base(token){}

        public new RepetitionToken Token
        {
            get
            {
                return (RepetitionToken)base.Token;
            }
        }

        public override NodeType NodeType
        {
            get { return NodeType.UnaryOperator; }
        }

        public override int Precedence
        {
            get { return 150; }
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class ParenthesisNode : NodeBase
    {
        public ParenthesisNode(ParenthesisRightToken token) :
            base(token) {}

        public override NodeType NodeType
        {
            get { return NodeType.UnaryOperator; }
        }

        public override int Precedence
        {
            get { return 25; }
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
     }

    public class ConcatenationNode : NodeBase
    {
        public ConcatenationNode(ConcatenationToken token) :
            base(token) {}

        public override NodeType NodeType
        {
            get { return NodeType.BinaryOperator; }
        }

        public override int Precedence
        {
            get { return 50; }
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class AlternationNode : NodeBase
    {
        public AlternationNode(AlternationToken token)
            : base(token) { }

        public override NodeType NodeType
        {
            get { return NodeType.BinaryOperator; }
        }

        public override int Precedence
        {
            get { return 30; }
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class RangeNode : NodeBase
    {
        public RangeNode(RangeToken token)
            : base(token) { }

        public override NodeType NodeType
        {
            get { return NodeType.BinaryOperator; }
        }

        public override int Precedence
        {
            get { return 130; }
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class BracketNode : NodeBase
    {
        public BracketNode(BracketRightToken token)
            : base(token) { }

        public override NodeType NodeType
        {
            get { return NodeType.UnaryOperator; }
        }

        public override int Precedence
        {
            get { return 10; }
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class NotNode : NodeBase
    {
        public NotNode(NotToken token)
            : base(token) { }

        public override NodeType NodeType
        {
            get { return NodeType.UnaryOperator; }
        }

        public override int Precedence
        {
            get { return 11; }
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
