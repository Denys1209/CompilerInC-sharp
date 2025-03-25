using Minks.CodeAnalysis.Syntax;
using Minsk.CodeAnalysis;
using Minsk.CodeAnalysis.Syntax;
using System.Net.WebSockets;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;

namespace Minks.CodeAnalysis.Binding;

internal sealed class Binder
{

    public readonly DiagnosticBag _diagnostics = new DiagnosticBag();

    public DiagnosticBag Diagnostics => _diagnostics;


    public BoundExpression BindExpression(ExpressionSyntax syntax)
    {
        switch (syntax.Kind)
        {

            case SyntaxKind.ParenthesizedExpression:
                return BindParenthesizedExpression((ParenthesizedExpressionSyntax)syntax);
            case SyntaxKind.LiteralExpression:
                return BindLiteralExpression((LiteralExpressionSyntax)syntax);
            case SyntaxKind.NameExpression:
                return BindNameExpression(((NameExpressionSyntax)syntax).Expression);
            case SyntaxKind.AssigmentExpression:
                return BindAssigmentExpressionSyntax(((AssigmentExpressionSyntax)syntax).Expression);
            case SyntaxKind.UnaryExpression:
                return BindUnaryExpression((UnaryExpressionSyntax)syntax);
            case SyntaxKind.BinaryExpression:
                return BindBinaryExpression((BinaryExpressionSyntax)syntax);
            default:
                throw new Exception($"Unexprecte syntax {syntax.Kind}");
        }

    }

    private BoundExpression BindAssigmentExpressionSyntax(ExpressionSyntax expression)
    {
        throw new NotImplementedException();
    }

    private BoundExpression BindNameExpression(object expression)
    {
        throw new NotImplementedException();
    }

    private BoundExpression BindParenthesizedExpression(ParenthesizedExpressionSyntax syntax)
    {
        return BindExpression(syntax.Expression);
    }

    private BoundExpression BindLiteralExpression(LiteralExpressionSyntax syntax)
    {

        var value = syntax.Value ?? 0;
        return new BoundLiteralExpression(value);
    }

    private BoundExpression BindUnaryExpression(UnaryExpressionSyntax syntax)
    {


        var boundOperand = BindExpression(syntax.Operand);
        var boundOperator = BoundUnaryOperator.Bind(syntax.OperatorToken.Kind, boundOperand.Type);

        if (boundOperator == null)
        {
            _diagnostics.ReportUnderFinedUnaryOperator(syntax.OperatorToken.Span, syntax.OperatorToken.Text, boundOperand.Type);

            return boundOperand;
        }

        return new BoundUnaryExpression(boundOperator, boundOperand);
    }

    private BoundExpression BindBinaryExpression(BinaryExpressionSyntax syntax)
    {

        var boundLeft = BindExpression(syntax.Left);
        var boundRight = BindExpression(syntax.Right);
        var boundOperator = BoundBinaryOperator.Bind(syntax.OperatorToken.Kind, boundLeft.Type, boundRight.Type);

        if (boundOperator == null)
        {
            _diagnostics.ReportUndefinedBinaryOperator(syntax.OperatorToken.Span, syntax.OperatorToken.Text, boundLeft.Type, boundRight.Type);

            return boundLeft;
        }
        return new BoundBinaryExpression(boundLeft, boundOperator, boundRight);
    }


    private BoundUnaryOperatorKind? BindUnaryOperatorKind(SyntaxKind kind, Type operandType)
    {
        if (operandType == typeof(int))
        {
            switch (kind)
            {
                case SyntaxKind.PlusToken:
                    return BoundUnaryOperatorKind.Identity;
                case SyntaxKind.MinusToken:
                    return BoundUnaryOperatorKind.Negation;
            }
        }

        if (operandType == typeof(bool))
        {
            switch (kind)
            {
                case SyntaxKind.BangToken:
                    return BoundUnaryOperatorKind.LogicalNegation;
            }
        }
        return null;
    }

    private BoundBinaryOperatorKind? BindBinaryOperator(SyntaxKind kind, Type leftType, Type rightType)
    {
        if (leftType == typeof(int) || rightType == typeof(int))
        {
            switch (kind)
            {
                case SyntaxKind.PlusToken:
                    return BoundBinaryOperatorKind.Addition;
                case SyntaxKind.MinusToken:
                    return BoundBinaryOperatorKind.Subtraction;
                case SyntaxKind.StarToken:
                    return BoundBinaryOperatorKind.Multiplication;
                case SyntaxKind.SlashToken:
                    return BoundBinaryOperatorKind.Division;
            }
        }

        if (leftType == typeof(bool) || rightType == typeof(bool))
        {
            switch (kind)
            {
                case SyntaxKind.AmpersandAmpersandToken:
                    return BoundBinaryOperatorKind.LogicalAnd;
                case SyntaxKind.PipePipeToken:
                    return BoundBinaryOperatorKind.LogicalOr;
            }
        }

        return null;
    }
}
