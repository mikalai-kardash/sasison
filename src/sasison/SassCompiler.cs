using System.Text;
using sasison.Expressions;

namespace sasison
{
    public class SassCompiler : ISassCompiler
    {
        public string Compile(string input)
        {
            using (var parser = new SassParser())
            {
                var syntaxTree = parser.Parse(input);
                var correctedSyntaxTree = new Tranclucator().Process(syntaxTree);

                var sb = new StringBuilder();
                correctedSyntaxTree?.PrintOut(sb);
                return sb.ToString();
            }
        }
    }
}