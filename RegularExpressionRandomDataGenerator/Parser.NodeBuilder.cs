
namespace RegularExpressionDataGenerator
{
    public class NodeBuilder
    {
        public TokenBuilder TokenBuilder { get; }

        public NodeBuilder()
        {
            TokenBuilder = new TokenBuilder();
        }
        
        public LiteralNode BuildLiteralNode(LiteralToken literalToken)
        {
            return new LiteralNode(literalToken);
        }

        public RangeNode BuildRangeNode(LiteralToken from, LiteralToken to)
        {
            var rangeNode = new RangeNode(TokenBuilder.BuildRangeToken());
            rangeNode.ChildNodes.Add(BuildLiteralNode(from));
            rangeNode.ChildNodes.Add(BuildLiteralNode(to));
            return rangeNode;
        }

        public RangeNode BuildAnyNode()
        {
            return BuildRangeNode(TokenBuilder.BuildLiteralToken((char)32), TokenBuilder.BuildLiteralToken((char)126));
        }

        public RangeNode BuildNumericNode()
        {
            return BuildRangeNode(TokenBuilder.BuildLiteralToken((char)48), TokenBuilder.BuildLiteralToken((char)57));
        }

        public BracketNode BuildWordNode()
        {
            var numeric = BuildNumericNode();
            var uppercase = BuildRangeNode(TokenBuilder.BuildLiteralToken((char)65), TokenBuilder.BuildLiteralToken((char)90));
            var lowercase = BuildRangeNode(TokenBuilder.BuildLiteralToken((char)97), TokenBuilder.BuildLiteralToken((char)122));
            var underscore = BuildLiteralNode(TokenBuilder.BuildLiteralToken((char) 95));
            var set = new BracketNode(new BracketRightToken());
            set.ChildNodes.Add(numeric);
            set.ChildNodes.Add(uppercase);
            set.ChildNodes.Add(lowercase);
            set.ChildNodes.Add(underscore);

            return set;
        }

        public BracketNode BuildWhitespaceNode()
        {
            var space = BuildLiteralNode(TokenBuilder.BuildLiteralToken((char)32));
            var t = BuildLiteralNode(TokenBuilder.BuildLiteralToken((char)9));
            /*
            var cr = BuildLiteralNode(tokenBuilder.BuildLiteralToken((char)13));
            var nl = BuildLiteralNode(tokenBuilder.BuildLiteralToken((char)10));
            var vt = BuildLiteralNode(tokenBuilder.BuildLiteralToken((char)11));
            var ff = BuildLiteralNode(tokenBuilder.BuildLiteralToken((char)12));
            */

            var set = new BracketNode(new BracketRightToken());
            set.ChildNodes.Add(space);
            set.ChildNodes.Add(t);
            /*
            set.ChildNodes.Add(cr);
            set.ChildNodes.Add(nl);
            set.ChildNodes.Add(vt);
            set.ChildNodes.Add(ff);
            */
            return set;
        }

        public NotNode BuildNonNumericNode()
        {
            var numeric = BuildNumericNode();
            var not = new NotNode(new NotToken());
            not.ChildNodes.Add(numeric);
            
            return not;
        }

        public NotNode BuildNonWordNode()
        {
            var word = BuildWordNode();
            var not = new NotNode(new NotToken());
            not.ChildNodes.Add(word);

            return not;
        }

        public NotNode BuildNonWhitespaceNode()
        {
            var whitespace = BuildWhitespaceNode();
            var not = new NotNode(new NotToken());
            not.ChildNodes.Add(whitespace);

            return not;
        }
    }
}
