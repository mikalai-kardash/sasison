using System.Collections;
using System.Collections.Generic;

namespace sasison
{
    public class SymbolEnumerable : IEnumerable<char>
    {
        private SymbolEnumerator _enumerator;
        public string Input { get; set; }

        public SymbolEnumerable(string input)
        {
            Input = input;
        }

        public IEnumerator<char> GetEnumerator()
        {
            _enumerator = new SymbolEnumerator(Input);
            return _enumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void AddForProcessing(string contents)
        {
            _enumerator?.AddForProcessing(contents);
        }
    }
}