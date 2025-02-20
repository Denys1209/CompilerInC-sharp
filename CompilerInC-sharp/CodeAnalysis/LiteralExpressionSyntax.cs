namespace D_Compiler_MyOwnLanguage_.CodeAnalysis;

internal sealed class LiteralExpressionSyntax : ExpressionSyntax
{
    public LiteralExpressionSyntax(SyntaxToken littertalToken)
    {
        LiteralToken = littertalToken;
    }

    public override SyntaxKind Kind => SyntaxKind.LiteralExpression;
    public SyntaxToken LiteralToken { get; }

    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return LiteralToken;
    }
}