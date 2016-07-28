namespace sasison
{
    public class SassCompiler : ISassCompiler
    {
        private readonly IFileLoader _fileLoader;

        public SassCompiler(IFileLoader fileLoader)
        {
            _fileLoader = fileLoader;
        }

        public string Compile(string input)
        {
            using (var parser = new SassParser(_fileLoader))
            using (var tranclucator = new Tranclucator())
            using (var poet = new Poet())
            {
                var syntaxTree = parser.Parse(input);
                var tranclucatedSyntaxTree = tranclucator.Process(syntaxTree);
                return poet.Write(tranclucatedSyntaxTree);
            }
        }
    }
}