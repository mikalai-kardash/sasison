using System.Text;

namespace sasison
{
    public class Token
    {
        private StringBuilder _token = new StringBuilder();

        public void Accumulate(char next)
        {
            _token.Append(next);
        }

        public override string ToString()
        {
            return _token.ToString();
        }

        public void Clear()
        {
            _token = new StringBuilder();
        }
    }
}