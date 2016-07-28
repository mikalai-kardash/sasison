using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xunit.Sdk;

namespace sasison.tests
{
    public class SassSpecAttribute : DataAttribute
    {
        private const string InputScss = "input.scss";
        private const string ExpectedOutputCss = "expected_output.css";
        private const string Error = "error";
        private const string OptionsYml = "options.yml";

        public string UniqueId { get; }

        public SassSpecAttribute(string uniqueId = "")
        {
            UniqueId = uniqueId;
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            if (testMethod == null)
            {
                throw new ArgumentNullException(nameof(testMethod));
            }

            var baseDir = AppContext.BaseDirectory;
            var current = baseDir.Substring(0, baseDir.IndexOf("sasison") + "sasison".Length);
            var specs = new DirectoryInfo(current + "\\sass-spec\\spec");
            foreach (var spec in specs.GetDirectories())
            {
                var testCases = spec.GetDirectories();
                foreach (var testCase in testCases)
                {
                    var files = testCase.GetFiles();
                    var sassTestCase = new SassTestCase
                    {
                        DisplayName = $"{spec.Name} / {testCase.Name}",
                        BaseFolder = testCase
                    };

                    if (!string.IsNullOrWhiteSpace(UniqueId))
                    {
                        if (!sassTestCase.DisplayName.Equals(UniqueId, StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }
                    }

                    foreach (var file in files)
                    {
                        switch (file.Name.ToLower())
                        {
                            case InputScss:
                                sassTestCase.Input = File.ReadAllText(file.FullName);
                                break;

                            case ExpectedOutputCss:
                                sassTestCase.ExpectedOutput = File.ReadAllText(file.FullName);
                                break;

                            case Error:
                                sassTestCase.Error = File.ReadAllText(file.FullName);
                                break;

                            case OptionsYml:
                                sassTestCase.Options = File.ReadAllText(file.FullName);
                                break;
                        }
                    }

                    yield return new object[] {sassTestCase};
                }
            }
        }
    }
}