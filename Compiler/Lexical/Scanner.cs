using Compiler.Tokens;

namespace Compiler;

public class Scanner {
    private static HashSet<char> Separators => new() {' ', '\t', '\r', '\n', '\v', '\f'};
    private static TokenProcessor Processor => new TokenProcessor();

    public static IReadOnlyCollection<TokenIdentifier> Run(string content) {
        var identifiers = new List<(int, char, string)>();
        var tokens = new List<TokenIdentifier>();
        int? nextChar = 0;
        while (nextChar.HasValue) {
            if (content.Length <= nextChar) break;

            if (Separators.Contains(content[nextChar.Value])) {
                nextChar++;
                continue;
            }

            (var token, nextChar) = Processor.ReadToken(content, nextChar.Value);
            token.Consts = identifiers;
            tokens.Add(token);
        }

        tokens.Add(new TokenIdentifier {Token = TokenEnum.ENDFILE});
        return tokens;
    }
}