﻿using System;
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
        //[SassSpec("basic / 02_simple_nesting")]
        //[SassSpec("basic / 03_simple_variable")]
        //[SassSpec("basic / 04_basic_variables")]
        //[SassSpec("basic / 05_empty_levels")]
        [SassSpec]
        public void VerifyTestCase(SassTestCase sassTestCase)
        {
            var actualOutput = string.Empty;
            Exception thrown = null;
            
            try
            {
                var compiler = new SassCompiler();
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
            
            WriteSection("-- INPUT:", sassTestCase.Input);
            WriteSection("-- EXPECTED:", sassTestCase.ExpectedOutput);
            WriteSection("-- ACTUAL:", actualOutput);
            WriteSection("-- OPTIONS:", sassTestCase.Options);
            WriteSection("-- ERROR:", sassTestCase.Error);
            WriteSection("-- EXCEPTION:", ex?.ToString() ?? "");

            Assert.Null(ex);
            Assert.Equal(sassTestCase.ExpectedOutput, actualOutput);
        }

        private void WriteSection(string section, string value) {
            if (!string.IsNullOrWhiteSpace(value))
            {
                _output.WriteLine("");
                _output.WriteLine(section);
                _output.WriteLine(value);
            }
        }
    }
}