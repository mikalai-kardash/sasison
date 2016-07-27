namespace sasison
{
    public static class Grammar
    {
        public const char HashChar = '#';
        public const char AtChar = '@';
        public const char VarChar = '$';
        public const char ColonChar = ':';
        public const char EndDeclarationChar = ';';
        public const char NewLineChar = '\n';
        public const char ReturnChar = '\r';
        public const char TabChar = '\t';
        public const char SpaceChar = ' ';
        public const char CommaChar = ',';
        public const char StarChar = '*';
        public const char ForwardSlashChar = '/';
        public const char EqChar = '=';
        public const char BackReferenceChar = '&';
        public const char DoubelQuoteChar = '"';

        public const char OpeningCurlyBraceChar = '{';
        public const char ClosingCurlyBraceChar = '}';

        public const char CssDirectChildChar = '>';
        public const char CssSiblingChar = '~';
        public const char CssAdjacentSiblingChar = '+';

        public static string Color = $"{HashChar}([0-9]{3}|[0-9]{6})";
        public static string Variable = $"{VarChar}\\w+";
        public static string Property = "\\w+";
        public static string PropertyAssignment = $"{Property} {ColonChar} ({Variable}|value)".FixSpaces();
        public static string Declaration = $"{Variable} {ColonChar} ({Variable}|string) {EndDeclarationChar}".FixSpaces();
    }
}