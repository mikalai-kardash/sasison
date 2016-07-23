using System;
using Xunit;
using Xunit.Abstractions;

namespace sasison.tests
{
    public class SassCompilerTest
    {
        private readonly ITestOutputHelper _output;

        public SassCompilerTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        //[SassSpec("basic / 01_simple_css")]
        [SassSpec("basic / 02_simple_nesting")]
        //[SassSpec]
        public void VerifyTestCase(SassTestCase sassTestCase)
        {
            var actualOutput = string.Empty;
            Exception thrown = null;

            var compiler = new SassCompiler();
            try
            {
                actualOutput = compiler.Compile(sassTestCase.Input);
            }
            catch (Exception ex)
            {
                thrown = ex;
            }
            

            if (thrown != null || !sassTestCase.ExpectedOutput.Equals(actualOutput, StringComparison.OrdinalIgnoreCase))
            {
                ReportError(sassTestCase, actualOutput, thrown);
            }
        }

        private void ReportError(SassTestCase sassTestCase, string actualOutput, Exception ex)
        {
            _output.WriteLine("{0} FAILED!", sassTestCase.DisplayName);
            if (!string.IsNullOrWhiteSpace(sassTestCase.Error))
            {
                _output.WriteLine("");
                _output.WriteLine(sassTestCase.Error);
            }
            if (!string.IsNullOrWhiteSpace(sassTestCase.Options))
            {
                _output.WriteLine("");
                _output.WriteLine(sassTestCase.Options);
            }

            _output.WriteLine("");
            _output.WriteLine(sassTestCase.Input);

            _output.WriteLine(ex.ToString());

            Assert.Null(ex);
            Assert.Equal(sassTestCase.ExpectedOutput, actualOutput);
        }
    }
}