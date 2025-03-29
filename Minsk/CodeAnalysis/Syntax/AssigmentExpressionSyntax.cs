using Minks.CodeAnalysis.Syntax;

namespace Minsk.CodeAnalysis.Syntax;

class AssigmentExpressionSyntax : ExpressionSyntax
{
    public AssigmentExpressionSyntax(SyntaxToken identifierToken, SyntaxToken equalsToken, ExpressionSyntax expression)
    {
        IdentifierToken = identifierToken;
        EqualsToken = equalsToken;
        Expression = expression;
    }
    public override SyntaxKind Kind => SyntaxKind.AssigmentExpression;

    public SyntaxToken IdentifierToken { get; }
    public SyntaxToken EqualsToken { get; }
    public ExpressionSyntax Expression { get; }

    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return IdentifierToken;
        yield return EqualsToken;
        yield return Expression;
    }
}
