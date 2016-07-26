namespace sasison
{
    public class SassCompiler : ISassCompiler
    {
        public string Compile(string input)
        {
            using (var parser = new SassParser())
            using (var tranclucator = new Tranclucator())
            using (var poet = new Poet())
            {
                var syntaxTree = parser.Parse(input);
                var tranclucatedSyntaxTree = tranclucator.Process(syntaxTree);

                //var sb = new StringBuilder();
                //tranclucatedSyntaxTree?.PrintOut(sb);
                var text = poet.Write(tranclucatedSyntaxTree);
                return text;
            }
        }
    }
}