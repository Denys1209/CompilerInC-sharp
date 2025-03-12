namespace CompilerInC_sharp.CodeAnalysis.Syntax;

internal sealed class LiteralExpressionSyntax : ExpressionSyntax
{
    public LiteralExpressionSyntax(SyntaxToken littertalToken)
        : this(littertalToken, littertalToken.Value)
    {
    }

    public LiteralExpressionSyntax(SyntaxToken littertalToken, object value)

    {
        LiteralToken = littertalToken;
        Value = value;
    }

    public override SyntaxKind Kind => SyntaxKind.LiteralExpression;
    public SyntaxToken LiteralToken { get; }

    public object Value { get; }

    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return LiteralToken;
    }
}