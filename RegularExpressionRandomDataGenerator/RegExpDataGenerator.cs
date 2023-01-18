
namespace RegularExpressionDataGenerator
{
    /// <summary>
    /// RegExpDataGenerator object generates random data that conforms to the regular expression pattern given.
    /// </summary>
    /// <example>
    /// //this will output a string "Hello" with zero or more following o's to the console.
    /// RegExpDataGenerator dataGenerator = new RegExpDataGenerator("He[l]{2,2}o*");
    /// Console.WriteLine(dataGenerator.Next());
    /// </example>
    public class RegExpDataGenerator
    {
        readonly INode _syntaxTreeRootNode;
        private NodeBuilder nodeBuilder;
        
        public RegExpDataGenerator(string pattern)
        {
            nodeBuilder = new NodeBuilder();
            var parser = new Parser(nodeBuilder);
            _syntaxTreeRootNode = parser.Parse(pattern);
        }

        public string Next()
        {
            return GeneratorVisitor.Visit(_syntaxTreeRootNode, nodeBuilder);
        }
    }
}
