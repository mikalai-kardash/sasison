using System.IO;

namespace sasison.tests
{
    public class TestCaseFileLoader : IFileLoader
    {
        private readonly DirectoryInfo _baseFolder;

        public TestCaseFileLoader(DirectoryInfo baseFolder)
        {
            _baseFolder = baseFolder;
        }

        public string LoadFile(string fileName)
        {
            var filePath = Path.Combine(_baseFolder.FullName, fileName);
            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }
            return string.Empty;
        }
    }
}