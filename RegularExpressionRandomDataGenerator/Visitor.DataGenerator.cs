using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace RegularExpressionDataGenerator
{
    public class GeneratorVisitor : IVisitor
    {
        private const int DefaultMaxOccurs = 11;
        private readonly StringBuilder _builder = new StringBuilder();
        
        private GeneratorVisitor()
        { }

        public static string Visit(INode node)
        {
            var visitor = new GeneratorVisitor();
            node.Accept(visitor);
            return visitor._builder.ToString();
        }

        public void Visit(LiteralNode node)
        {
            _builder.Append(node.Token.Character.ToString());
        }

        public void Visit(RepetitionNode node)
        {
            var index = RandomNumberProvider.GetRandomNumber(node.Token.MinOccurs, node.Token.MaxOccurs > -1 ? node.Token.MaxOccurs + 1 : DefaultMaxOccurs);
            for (var i = 0; i < index; i++)
            {
                foreach (var childNode in node.ChildNodes)
                {
                    childNode.Accept(this);
                }
            }
        }

        public void Visit(AlternationNode node)
        {
            var index = RandomNumberProvider.GetRandomNumber(0, 2);
            node.ChildNodes[index].Accept(this);
        }

        public void Visit(ConcatenationNode node)
        {
            foreach (var childNode in node.ChildNodes)
            {
                childNode.Accept(this);
            }
        }

        public void Visit(ParenthesisNode node)
        {
            foreach (var childNode in node.ChildNodes)
            {
                childNode.Accept(this);
            }
        }

        public void Visit(BracketNode node)
        {
            if (node.ChildNodes[0].Token.TokenType == TokenType.Not)
            {
                node.ChildNodes[0].Accept(this);
                return;
            }

            INode current = node;
            while (current.ChildNodes[0].Token.TokenType == TokenType.Concatenation)
            {
                current = node.ChildNodes[0];
            }

            var nodes = new LiteralNodeCollection();
            foreach (var expandedNode in Expand(current))
            {
                if (nodes.Contains(expandedNode) == false)
                {
                    nodes.Add(expandedNode);
                }
            }

            var index = RandomNumberProvider.GetRandomNumber(0, nodes.Count);
            nodes[index].Accept(this);
        }

        public void Visit(RangeNode node)
        {
            var min = (int)((LiteralNode)node.ChildNodes[0]).Token.Character;
            var max = (int)((LiteralNode)node.ChildNodes[1]).Token.Character;
            var index = RandomNumberProvider.GetRandomNumber(min, max + 1);
            var literal = new LiteralNode(TokenBuilder.BuildLiteralToken((char)index));
            literal.Accept(this);             
        }

        public void Visit(NotNode node)
        {
            var nodes = new LiteralNodeCollection();
            for (var i = 32; i < 126; i++)
            {
                nodes.Add(NodeBuilder.BuildLiteralNode(TokenBuilder.BuildLiteralToken((char)i)));
            }

            INode current = node;
            while (current.ChildNodes[0].Token.TokenType == TokenType.Concatenation)
            {
                current = node.ChildNodes[0];
            }

            foreach (var expandedNode in Expand(current))
            {
                if (nodes.Contains(expandedNode))
                {
                    nodes.Remove(expandedNode);
                }
            }

            var index = RandomNumberProvider.GetRandomNumber(0, nodes.Count);
            nodes[index].Accept(this);
        }

        private static IEnumerable<LiteralNode> Expand(INode bracketNode)
        {
            foreach (var childNode in bracketNode.ChildNodes)
            {
                switch (childNode.Token.TokenType)
                {
                    case TokenType.Literal:
                        yield return (LiteralNode)childNode;
                        break;
                    case TokenType.Range:
                        var min = (int)((LiteralNode)childNode.ChildNodes[0]).Token.Character;
                        var max = (int)((LiteralNode)childNode.ChildNodes[1]).Token.Character;
                        for (var i = min; i < max; i++)
                        {
                            var c = (char)i;
                            yield return NodeBuilder.BuildLiteralNode(TokenBuilder.BuildLiteralToken(c));
                        }
                        break;
                    case TokenType.BracketRight:
                        foreach (var node in Expand(childNode))
                        {
                            yield return node;
                        }
                        break;
                }
            }
        }

        private class LiteralNodeCollection : System.Collections.ObjectModel.KeyedCollection<char, LiteralNode>
        {
            protected override char GetKeyForItem(LiteralNode item)
            {
                return item.Token.Character;
            }
        }       

    }

    public static class RandomNumberProvider
    {
        static readonly object Padlock = new object();

        private static RandomNumberGenerator _rnd;
        private static RandomNumberGenerator Rnd
        {
            get
            {
                lock (Padlock)
                {
                    return _rnd ?? (_rnd = RandomNumberGenerator.Create());
                }
            }
        }

        public static int GetRandomNumber(int min, int max)
        {
            var randbyte = new byte[1];
            Rnd.GetNonZeroBytes(randbyte);
            var rand = new Random(Convert.ToInt32(randbyte[0]));

            return rand.Next(min, max);
        }
    }

}
