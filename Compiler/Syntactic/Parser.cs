using Compiler.Tokens;

namespace Compiler.Syntactic;

public static class Parser {
    public static IReadOnlyCollection<TokenIdentifier> Parse(this IReadOnlyCollection<TokenIdentifier> tokens) {
        var actionTable = SyntacticState.Initialize();
        long final_state = 1;
        long q = 0;
        var stateStack = new Stack<long>();
        stateStack.Push(q);
        var tokenEnumerator = tokens.GetEnumerator();
        tokenEnumerator.MoveNext();
        do {
            var token = tokenEnumerator.Current?.Token;
            if (!actionTable.ContainsKey((q, (long) token)))
                throw new Exception($"Syntax error: Didnt contain key: (q:{q}, token:{token}");

            var p = actionTable[(q, (long) token)];
            if (IsShift(p)) {
                stateStack.Push(p);
                if (token != TokenEnum.ENDFILE) tokenEnumerator?.MoveNext();
            } else if (IsReduction(p)) {
                var r = Rule(p);
                for (long i = 0; i < SyntacticState.RuleLengths[(int) r]; i++)
                    stateStack.Pop();
                stateStack.Push(actionTable[(stateStack.Peek(), SyntacticState.RuleLeft[(int) r])]);
            } else {
                throw new Exception("Syntax error");
            }

            q = stateStack.Peek();
        } while (q != final_state);

        return tokens;
    }

    private static bool IsShift(long p) => p > 0;
    private static bool IsReduction(long p) => p < 0;
    private static long Rule(long p) => -p;
}