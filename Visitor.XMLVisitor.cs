using System;
using System.Text;

namespace RegularExpressionDataGenerator
{  
    public class XmlVisitor : IVisitor
    {
        readonly string _lr = Environment.NewLine;
        const int TabSpaces = 3;

        private int _level;
        private readonly StringBuilder _builder = new StringBuilder();

        private XmlVisitor()
        { }

        public static string Visit(INode node)
        {
            var visitor = new XmlVisitor();
            node.Accept(visitor);
            return visitor._builder.ToString();
        }

        public void Visit(LiteralNode node)
        {
            var tab = GetTab(_level);
            _builder.AppendFormat("{0}<{1}>{2}</{1}>{3}", tab, "literal", node.Token.Character, _lr);
        }

        public void Visit(RepetitionNode node)
        {
            var tab = GetTab(_level);
            _builder.AppendFormat("{0}<{1} min='{2}' max='{3}'>{4}", tab, "repeatition", node.Token.MinOccurs, node.Token.MaxOccurs, _lr);
            _level++;
            foreach (var childNode in node.ChildNodes)
            {
                childNode.Accept(this);
            }
            _level--;
            _builder.AppendFormat("{0}</{1}>{2}", tab, "repeatition", _lr);
        }

        public void Visit(ConcatenationNode node)
        {
            var tab = GetTab(_level);
            _builder.AppendFormat("{0}<{1}>{2}", tab, "sequence", _lr);
            _level++;
            foreach (var childNode in node.ChildNodes)
            {
                childNode.Accept(this);
            }
            _level--;
            _builder.AppendFormat("{0}</{1}>{2}", tab, "sequence", _lr);
        }

        public void Visit(ParenthesisNode node)
        {
            var tab = GetTab(_level);
            _builder.AppendFormat("{0}<{1}>{2}", tab, "parenthesis", _lr);
            _level++;
            foreach (var childNode in node.ChildNodes)
            {
                childNode.Accept(this);
            }
            _level--;
            _builder.AppendFormat("{0}</{1}>{2}", tab, "parenthesis", _lr);
        }

        public void Visit(AlternationNode node)
        {
            var tab = GetTab(_level);
            _builder.AppendFormat("{0}<{1}>{2}", tab, "alternation", _lr);
            _level++;
            foreach (var childNode in node.ChildNodes)
            {
                childNode.Accept(this);
            }
            _level--;
            _builder.AppendFormat("{0}</{1}>{2}", tab, "alternation", _lr);
        }

        public void Visit(RangeNode node)
        {
            var tab = GetTab(_level);
            _builder.AppendFormat("{0}<{1}>{2}", tab, "range", _lr);
            _level++;
            foreach (var childNode in node.ChildNodes)
            {
                childNode.Accept(this);
            }
            _level--;
            _builder.AppendFormat("{0}</{1}>{2}", tab, "range", _lr);
        }

        public void Visit(BracketNode node)
        {
            var tab = GetTab(_level);
            _builder.AppendFormat("{0}<{1}>{2}", tab, "set", _lr);
            _level++;
            foreach (var childNode in node.ChildNodes)
            {
                childNode.Accept(this);
            }
            _level--;
            _builder.AppendFormat("{0}</{1}>{2}", tab, "set", _lr);
        }

        public void Visit(NotNode node)
        {
            var tab = GetTab(_level);
            _builder.AppendFormat("{0}<{1}>{2}", tab, "not", _lr);
            _level++;
            foreach (var childNode in node.ChildNodes)
            {
                childNode.Accept(this);
            }
            _level--;
            _builder.AppendFormat("{0}</{1}>{2}", tab, "not", _lr);
        }

        private static string GetTab(int tabLevel)
        {
            var tab = string.Empty;
            return tab.PadLeft(tabLevel * TabSpaces, ' ');
        }
    }
}
