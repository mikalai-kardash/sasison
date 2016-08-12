using System.Collections;
using System.Collections.Generic;

namespace sasison
{
    public class SymbolEnumerator : IEnumerator<char>
    {
        private readonly string _initialInput;
        private int _currentIndex;

        public SymbolEnumerator(string input)
        {
            _initialInput = input;
            _currentIndex = 0;

            Input = input;
        }

        public bool MoveNext()
        {
            if (string.IsNullOrEmpty(Input))
            {
                return false;
            }
            return Input.Length > _currentIndex;
        }

        public void Reset()
        {
            _currentIndex = 0;
            Input = _initialInput;
        }

        public string Input { get; set; }

        public char Current => Input[_currentIndex++];

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }

        public void AddForProcessing(string contents)
        {
            var left = Input.Substring(0, _currentIndex + 1);
            var right = Input.Substring(_currentIndex + 1);
            Input = string.Concat(left, contents, right);
        }
    }
}