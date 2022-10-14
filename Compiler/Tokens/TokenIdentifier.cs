namespace Compiler.Tokens;

public class TokenIdentifier {
    public TokenEnum Token { get; init; }
    public int? SecondaryToken { get; init; }

    /// <summary>
    ///     Values of regular tokens <seealso cref="TokenEnum" />
    /// </summary>
    public IReadOnlyCollection<(int, char, string)> Consts { get; set; }
}