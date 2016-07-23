using System.Text;

namespace sasison
{
    public class SassCompiler : ISassCompiler
    {
        public string Compile(string input)
        {
            using (var parser = new SassParser())
            {
                var ast = parser.Parse(input);
                var sb = new StringBuilder();

                ast?.PrintOut(sb);
                return sb.ToString();
            }
        }
    }
}