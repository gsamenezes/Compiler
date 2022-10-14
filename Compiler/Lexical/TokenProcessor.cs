using Compiler.Lexical;
using Compiler.Tokens;
using Stateless;

public class TokenProcessor {
    private TokenState State = TokenState.InitialState;
    private StateMachine<TokenState, char> StateMachine { get; }
    private static Dictionary<string, int> DictToSecondaryToken { get; } = new();

    public TokenProcessor() {
        StateMachine = new(() => State, s => State = s);
        ConfigureStateMachine();
    }

    public (TokenIdentifier, int?) ReadToken(string content, int initialChar = 0) {
        if (content.Length < initialChar)
            throw new Exception($"{nameof(ReadToken)} should receive at least one readable char");

        var buffer = "";
        var lastCharRead = initialChar;
        for (; lastCharRead <= content.Length; lastCharRead++) {
            try {
                var curChar = content[lastCharRead];
                StateMachine.Fire(curChar);
            } catch {
                break;
            }

            buffer = $"{buffer}{content[lastCharRead]}";
        }

        var tokenType = StateMachine.State switch {
            TokenState.Identifier => Finder.Find(buffer),
            TokenState.Numeral => TokenEnum.NUMERAL,
            TokenState.Decimal => TokenEnum.NUMERAL,
            TokenState.ClosedCharacter => TokenEnum.CHARACTER,
            TokenState.FinishedString => TokenEnum.STRINGVAL,
            TokenState.Comma => TokenEnum.COMMA,
            TokenState.Dot => TokenEnum.DOT,
            TokenState.Colon => TokenEnum.COLON,
            TokenState.SemiColon => TokenEnum.SEMI_COLON,
            TokenState.LeftSquare => TokenEnum.LEFT_SQUARE,
            TokenState.RightSquare => TokenEnum.RIGHT_SQUARE,
            TokenState.LeftBraces => TokenEnum.LEFT_BRACES,
            TokenState.RightBraces => TokenEnum.RIGHT_BRACES,
            TokenState.LeftParenthesis => TokenEnum.LEFT_PARENTHESIS,
            TokenState.RightParenthesis => TokenEnum.RIGHT_PARENTHESIS,
            TokenState.Times => TokenEnum.TIMES,
            TokenState.Divide => TokenEnum.DIVIDE,
            TokenState.Equals => TokenEnum.EQUALS,
            TokenState.EqualEqual => TokenEnum.EQUAL_EQUAL,
            TokenState.And => TokenEnum.AND,
            TokenState.Or => TokenEnum.OR,
            TokenState.Greater => TokenEnum.GREATER_THAN,
            TokenState.GreaterOrEqual => TokenEnum.GREATER_OR_EQUAL,
            TokenState.Less => TokenEnum.LESS_THAN,
            TokenState.LessOrEqual => TokenEnum.LESS_OR_EQUAL,
            TokenState.Plus => TokenEnum.PLUS,
            TokenState.PlusPlus => TokenEnum.PLUS_PLUS,
            TokenState.Minus => TokenEnum.MINUS,
            TokenState.MinusMinus => TokenEnum.MINUS_MINUS,
            TokenState.Not => TokenEnum.NOT,
            TokenState.NotEqual => TokenEnum.NOT_EQUAL,
            _ => TokenEnum.UNKNOWN
        };
        var substringRead = content.Substring(initialChar, lastCharRead - initialChar);
        if (tokenType == TokenEnum.ID && !DictToSecondaryToken.ContainsKey(substringRead))
            DictToSecondaryToken[substringRead] = DictToSecondaryToken.Count;
        var secToken = tokenType == TokenEnum.ID
            ? DictToSecondaryToken[substringRead]
            : (int?) null;
        var token = new TokenIdentifier {
            Token = tokenType,
            SecondaryToken = secToken
        };

        ResetState();
        return (token, (lastCharRead == content.Length)
                    ? null
                    : lastCharRead);
    }

    private void ResetState() =>
        State = TokenState.InitialState;

    private void ConfigureStateMachine() {
        // Helpers
        var chars = Enumerable.Range('a', 26)
                              .Concat(Enumerable.Range('A', 26))
                              .Select(x => (char) x)
                              .Concat(new char[] {'_'});
        var ascii = Enumerable.Range('\x1', 127)
                              .Select(x => (char) x);
        var digits = Enumerable.Range('0', 10)
                               .Select(x => (char) x);

        // Keywords and identifiers
        StateMachine.Configure(TokenState.InitialState)
                    .PermitForAll(chars, TokenState.Identifier);
        StateMachine.Configure(TokenState.Identifier)
                    .IgnoreForAll(chars);

        // String
        StateMachine.Configure(TokenState.InitialState)
                    .Permit('\"', TokenState.String);
        StateMachine.Configure(TokenState.String)
                    .IgnoreForAll(ascii.Where(c => c != '\"'));
        StateMachine.Configure(TokenState.String)
                    .Permit('\"', TokenState.FinishedString);

        // Character
        StateMachine.Configure(TokenState.InitialState)
                    .Permit('\'', TokenState.Charater);
        StateMachine.Configure(TokenState.Charater)
                    .PermitForAll(ascii.Where(c => c != '\''), TokenState.FilledCharater);
        StateMachine.Configure(TokenState.FilledCharater)
                    .Permit('\'', TokenState.ClosedCharacter);

        // Numerals
        StateMachine.Configure(TokenState.InitialState)
                    .PermitForAll(digits, TokenState.Numeral);
        StateMachine.Configure(TokenState.Numeral)
                    .IgnoreForAll(digits);
        StateMachine.Configure(TokenState.Numeral)
                    .Permit('.', TokenState.Decimal);
        StateMachine.Configure(TokenState.Decimal)
                    .IgnoreForAll(digits);

        // Equals
        StateMachine.Configure(TokenState.InitialState)
                    .Permit('=', TokenState.Equals);
        StateMachine.Configure(TokenState.Equals)
                    .Permit('=', TokenState.EqualEqual);

        // And
        StateMachine.Configure(TokenState.InitialState)
                    .Permit('&', TokenState.SingleAnd);
        StateMachine.Configure(TokenState.SingleAnd)
                    .Permit('&', TokenState.And);

        // Or
        StateMachine.Configure(TokenState.InitialState)
                    .Permit('|', TokenState.SingleOr);
        StateMachine.Configure(TokenState.SingleOr)
                    .Permit('|', TokenState.Or);

        // Greater
        StateMachine.Configure(TokenState.InitialState)
                    .Permit('>', TokenState.Greater);
        StateMachine.Configure(TokenState.Greater)
                    .Permit('=', TokenState.GreaterOrEqual);

        // Less
        StateMachine.Configure(TokenState.InitialState)
                    .Permit('<', TokenState.Less);
        StateMachine.Configure(TokenState.Less)
                    .Permit('=', TokenState.LessOrEqual);

        // Plus
        StateMachine.Configure(TokenState.InitialState)
                    .Permit('+', TokenState.Plus);
        StateMachine.Configure(TokenState.Plus)
                    .Permit('+', TokenState.PlusPlus);

        // Minus
        StateMachine.Configure(TokenState.InitialState)
                    .Permit('-', TokenState.Minus);
        StateMachine.Configure(TokenState.Minus)
                    .Permit('-', TokenState.MinusMinus);

        // Not
        StateMachine.Configure(TokenState.InitialState)
                    .Permit('!', TokenState.Not);
        StateMachine.Configure(TokenState.Not)
                    .Permit('=', TokenState.NotEqual);

        // Comma/Dot/...
        StateMachine.Configure(TokenState.InitialState)
                    .Permit(',', TokenState.Comma);
        StateMachine.Configure(TokenState.InitialState)
                    .Permit('.', TokenState.Dot);
        StateMachine.Configure(TokenState.InitialState)
                    .Permit(':', TokenState.Colon);
        StateMachine.Configure(TokenState.InitialState)
                    .Permit(';', TokenState.SemiColon);
        StateMachine.Configure(TokenState.InitialState)
                    .Permit('[', TokenState.LeftSquare);
        StateMachine.Configure(TokenState.InitialState)
                    .Permit(']', TokenState.RightSquare);
        StateMachine.Configure(TokenState.InitialState)
                    .Permit('{', TokenState.LeftBraces);
        StateMachine.Configure(TokenState.InitialState)
                    .Permit('}', TokenState.RightBraces);
        StateMachine.Configure(TokenState.InitialState)
                    .Permit('(', TokenState.LeftParenthesis);
        StateMachine.Configure(TokenState.InitialState)
                    .Permit(')', TokenState.RightParenthesis);
        StateMachine.Configure(TokenState.InitialState)
                    .Permit('*', TokenState.Times);
        StateMachine.Configure(TokenState.InitialState)
                    .Permit('/', TokenState.Divide);
    }
}

enum TokenState {
    InitialState,
    Identifier,
    Numeral,
    Decimal,
    Charater,
    FilledCharater,
    ClosedCharacter,
    String,
    FinishedString,
    Comma,
    Dot,
    Colon,
    SemiColon,
    LeftSquare,
    RightSquare,
    LeftBraces,
    RightBraces,
    LeftParenthesis,
    RightParenthesis,
    Times,
    Divide,
    Equals,
    EqualEqual,
    SingleAnd,
    And,
    SingleOr,
    Or,
    Greater,
    GreaterOrEqual,
    Less,
    LessOrEqual,
    Plus,
    PlusPlus,
    Minus,
    MinusMinus,
    Not,
    NotEqual
}

static class StateMachineExtensions {
    public static StateMachine<TState, T>.StateConfiguration PermitForAll<TState, T>(this StateMachine<TState, T>.StateConfiguration config,
                                                                                     IEnumerable<T> objects,
                                                                                     TState destination) {
        foreach (var obj in objects)
            config.Permit(obj, destination);
        return config;
    }

    public static StateMachine<TState, T>.StateConfiguration IgnoreForAll<TState, T>(this StateMachine<TState, T>.StateConfiguration config,
                                                                                     IEnumerable<T> objects) {
        foreach (var obj in objects)
            config.Ignore(obj);
        return config;
    }
}