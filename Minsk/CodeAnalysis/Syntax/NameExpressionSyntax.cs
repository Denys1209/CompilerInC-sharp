﻿using Minks.CodeAnalysis.Syntax;

namespace Minsk.CodeAnalysis.Syntax;

public sealed class NameExpressionSyntax : ExpressionSyntax
{
    public NameExpressionSyntax(SyntaxToken identifierToken)
    {
        IdentifierToken = identifierToken;
    }

    public SyntaxToken IdentifierToken { get; }


    public override SyntaxKind Kind => SyntaxKind.NameExpression;

    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return IdentifierToken;
    }
}
