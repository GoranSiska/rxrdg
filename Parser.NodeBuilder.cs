
namespace RegularExpressionDataGenerator
{
    public static class NodeBuilder
    {
        public static LiteralNode BuildLiteralNode(LiteralToken literalToken)
        {
            return new LiteralNode(literalToken);
        }

        public static RangeNode BuildRangeNode(LiteralToken from, LiteralToken to)
        {
            var rangeNode = new RangeNode(TokenBuilder.BuildRangeToken());
            rangeNode.ChildNodes.Add(BuildLiteralNode(from));
            rangeNode.ChildNodes.Add(BuildLiteralNode(to));
            return rangeNode;
        }

        public static RangeNode BuildAnyNode()
        {
            return BuildRangeNode(TokenBuilder.BuildLiteralToken((char)32), TokenBuilder.BuildLiteralToken((char)126));
        }

        public static RangeNode BuildNumericNode()
        {
            return BuildRangeNode(TokenBuilder.BuildLiteralToken((char)48), TokenBuilder.BuildLiteralToken((char)57));
        }

        public static BracketNode BuildWordNode()
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

        public static BracketNode BuildWhitespaceNode()
        {
            var space = BuildLiteralNode(TokenBuilder.BuildLiteralToken((char)32));
            var t = BuildLiteralNode(TokenBuilder.BuildLiteralToken((char)9));
            var cr = BuildLiteralNode(TokenBuilder.BuildLiteralToken((char)13));
            var nl = BuildLiteralNode(TokenBuilder.BuildLiteralToken((char)10));
            var vt = BuildLiteralNode(TokenBuilder.BuildLiteralToken((char)11));
            var ff = BuildLiteralNode(TokenBuilder.BuildLiteralToken((char)12));

            var set = new BracketNode(new BracketRightToken());
            set.ChildNodes.Add(space);
            set.ChildNodes.Add(t);
            set.ChildNodes.Add(cr);
            set.ChildNodes.Add(nl);
            set.ChildNodes.Add(vt);
            set.ChildNodes.Add(ff);

            return set;
        }

        public static NotNode BuildNonNumericNode()
        {
            var numeric = BuildNumericNode();
            var not = new NotNode(new NotToken());
            not.ChildNodes.Add(numeric);
            
            return not;
        }

        public static NotNode BuildNonWordNode()
        {
            var word = BuildWordNode();
            var not = new NotNode(new NotToken());
            not.ChildNodes.Add(word);

            return not;
        }

        public static NotNode BuildNonWhitespaceNode()
        {
            var whitespace = BuildWhitespaceNode();
            var not = new NotNode(new NotToken());
            not.ChildNodes.Add(whitespace);

            return not;
        }
    }
}
