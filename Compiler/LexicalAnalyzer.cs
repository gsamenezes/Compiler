using static Compiler.Enums;
using static Compiler.LexicalHelper;

namespace Compiler {
    public class LexicalAnalyzer {
        #region Fields
        private t_token _token;
        private FileStream? _file;
        private bool _eof;
        private const int MaximumConstantsCount = 1000;
        private readonly t_const[] _constants = new t_const[MaximumConstantsCount];
        #endregion
        public int ErrorsCount { get; internal set; }
        public int ConstantsCount { get; internal set; }
        public int SecondaryToken { get; set; }
        public char NextChar { get; set; }
        public List<string> SecondaryTokens { get; set; } = new();

        public LexicalAnalyzer(FileStream? file) => _file = file;


        public bool Analyze() {
            var allGood = true;
            NextChar = ReadChar();
            _token = NextToken();
            while (_token != t_token.END) {
                if (_token == t_token.UNKNOWN) {
                    allGood = false;
                    ErrorsCount++;
                }
                _token = NextToken();
            }
            return allGood;
        }

        private t_token NextToken() {

            while (char.IsWhiteSpace(NextChar))
                NextChar = ReadChar();

            if (char.IsLetter(NextChar)) {
                var text = "";

                do {
                    text += NextChar;
                    NextChar = ReadChar();
                } while (char.IsNumber(NextChar) || char.IsLetter(NextChar) || NextChar == '_');

                _token = SearchKeyword(text);
                if (_token == t_token.ID) {
                    SecondaryToken = SearchName(text);
                }
            } else {
                if (char.IsDigit(NextChar)) {
                    var numeral = "";
                    do {
                        numeral += NextChar;
                        NextChar = ReadChar();
                    } while (char.IsDigit(NextChar));

                    _token = t_token.NUMERAL;
                    SecondaryToken = AddIntConst(int.Parse(numeral));
                } else {
                    if (NextChar == '"') {
                        var str = "";
                        do {
                            str += NextChar;
                            NextChar = ReadChar();
                        } while (NextChar != '"');
                        NextChar = ReadChar();
                        _token = t_token.STRINGVAL;
                        SecondaryToken = AddStringConst(str);
                    } else {
                        if (NextChar == '\0')
                            return t_token.END;

                        switch (NextChar) {

                            case '\'':
                                NextChar = ReadChar();
                                _token = t_token.CHARACTER;
                                SecondaryToken = AddCharConst(NextChar);
                                NextChar = ReadChar();
                                NextChar = ReadChar();
                                break;

                            case ':':
                                NextChar = ReadChar();
                                _token = t_token.COLON;
                                break;

                            case '+':
                                NextChar = ReadChar();
                                if (NextChar == '+') {
                                    _token = t_token.PLUS_PLUS;
                                    NextChar = ReadChar();
                                } else {
                                    _token = t_token.PLUS;
                                }
                                break;

                            case ';':
                                NextChar = ReadChar();
                                _token = t_token.SEMI_COLON;
                                break;

                            case ',':
                                NextChar = ReadChar();
                                _token = t_token.COMMA;
                                break;

                            case '[':
                                NextChar = ReadChar();
                                _token = t_token.LEFT_SQUARE;
                                break;

                            case ']':
                                NextChar = ReadChar();
                                _token = t_token.RIGHT_SQUARE;
                                break;

                            case '{':
                                NextChar = ReadChar();
                                _token = t_token.LEFT_BRACES;
                                break;

                            case '}':
                                NextChar = ReadChar();
                                _token = t_token.RIGHT_BRACES;
                                break;

                            case '(':
                                NextChar = ReadChar();
                                _token = t_token.LEFT_PARENTHESIS;
                                break;

                            case ')':
                                NextChar = ReadChar();
                                _token = t_token.RIGHT_PARENTHESIS;
                                break;

                            case '&':
                                NextChar = ReadChar();
                                _token = NextChar == '&'
                                    ? t_token.AND
                                    : t_token.UNKNOWN;
                                NextChar = ReadChar();
                                break;

                            case '|':
                                NextChar = ReadChar();
                                _token = NextChar == '|'
                                    ? t_token.OR
                                    : t_token.UNKNOWN;
                                NextChar = ReadChar();
                                break;

                            case '*':
                                NextChar = ReadChar();
                                _token = t_token.TIMES;
                                break;

                            case '/':
                                NextChar = ReadChar();
                                _token = t_token.DIVIDE;
                                break;

                            case '.':
                                NextChar = ReadChar();
                                _token = t_token.DOT;
                                break;

                            case '!':
                                NextChar = ReadChar();
                                if (NextChar == '=') {
                                    _token = t_token.NOT_EQUAL;
                                    NextChar = ReadChar();
                                } else {
                                    _token = t_token.NOT;
                                }
                                break;

                            case '=':
                                NextChar = ReadChar();
                                if (NextChar == '=') {
                                    _token = t_token.EQUAL_EQUAL;
                                    NextChar = ReadChar();
                                } else {
                                    _token = t_token.EQUALS;
                                }
                                break;

                            case '-':
                                NextChar = ReadChar();
                                if (NextChar == '-') {
                                    _token = t_token.MINUS_MINUS;
                                    NextChar = ReadChar();
                                } else {
                                    _token = t_token.MINUS;
                                }
                                break;
                            case '<':
                                NextChar = ReadChar();
                                if (NextChar == '=') {
                                    _token = t_token.LESS_OR_EQUAL;
                                    NextChar = ReadChar();
                                } else {
                                    _token = t_token.LESS_THAN;
                                }
                                break;

                            case '>':
                                NextChar = ReadChar();
                                if (NextChar == '=') {
                                    _token = t_token.GREATER_OR_EQUAL;
                                    NextChar = ReadChar();
                                } else {
                                    _token = t_token.GREATER_THAN;
                                }
                                break;
                            default:
                                _token = t_token.UNKNOWN;
                                break;
                        }
                    }
                }
            }
            return _token;
        }

        private t_token SearchKeyword(string name) {
            t_token token;
            try {
                token = (t_token)Enum.Parse(typeof(t_token), name, true);
            } catch (ArgumentException) {
                return t_token.ID;
            }
            return token;
        }

        private char ReadChar() {
            if (_eof || _file == null)
                return '\0';
            var read = (char)_file.ReadByte();
            if (_file.Position < _file.Length)
                return read;
            if (_file.Position != _file.Length)
                return '\0';
            _eof = true;
            return read;
        }

        private int SearchName(string name) {
            if (SecondaryTokens.Contains(name))
                return SecondaryTokens.IndexOf(name);
            SecondaryTokens.Add(name);
            return SecondaryTokens.Count - 1;
        }

        private int AddCharConst(char c) {
            _constants[ConstantsCount] = new t_const {
                Type = 0,
                CharValue = c
            };
            ConstantsCount++;
            return ConstantsCount - 1;
        }

        private int AddIntConst(int n) {
            _constants[ConstantsCount] = new t_const {
                Type = 1,
                NumberValue = n
            };
            ConstantsCount++;
            return ConstantsCount - 1;
        }

        private int AddStringConst(string s) {
            _constants[ConstantsCount] = new t_const {
                Type = 2,
                StringValue = s
            };
            ConstantsCount++;
            return ConstantsCount - 1;
        }
    }


}
