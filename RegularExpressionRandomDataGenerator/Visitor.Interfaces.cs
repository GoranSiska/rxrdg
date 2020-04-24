
namespace RegularExpressionDataGenerator
{
    public interface IVisitor
    {
        NodeBuilder NodeBuilder { get; }
        void Visit(LiteralNode node);
        void Visit(RepetitionNode node);
        void Visit(ConcatenationNode node);
        void Visit(ParenthesisNode node);
        void Visit(AlternationNode node);
        void Visit(RangeNode node); 
        void Visit(BracketNode node); 
        void Visit(NotNode node); 
    }

    public interface IVisitable
    {
        void Accept(IVisitor visitor);
    }
}
