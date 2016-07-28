using System.IO;

namespace sasison.tests
{
    public class SassTestCase
    {
        public string Input { get; set; }
        public string ExpectedOutput { get; set; }
        public string Error { get; set; }
        public string Options { get; set; }
        public string DisplayName { get; set; }
        public DirectoryInfo BaseFolder { get; set; }
    }
}