using CompilerInC_sharp.CodeAnalysis.Syntax;
using System.Net.WebSockets;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;

namespace CompilerInC_sharp.CodeAnalysis.Binding;

internal sealed class Binder
{

    public readonly List<string> _diagnostics = new List<string>();

    public IEnumerable<string> Diagnostics => _diagnostics;


    public BoundExpression BindExpression(ExpressionSyntax syntax) 
    {
        switch (syntax.Kind) 
        {
            case SyntaxKind.LiteralExpression:
                return BindLiteralExpression((LiteralExpressionSyntax)syntax);
            case SyntaxKind.UnaryExpression:
                return BindUnaryExpression((UnaryExpressionSyntax)syntax);
            case SyntaxKind.BinaryExpression:
                return BindBinaryExpression((BinaryExpressionSyntax)syntax);
            default:
                throw new Exception($"Unexprecte syntax {syntax.Kind}");
        }

    }
    private BoundExpression BindLiteralExpression(LiteralExpressionSyntax syntax)
    {
        var value = syntax.LiteralToken.Value as int? ?? 0;
        return new BoundLiteralExpression(value);
    }

    private BoundExpression BindUnaryExpression(UnaryExpressionSyntax syntax)
    {


        var boundOperand = BindExpression(syntax.Operand);
        var boundOperatorKind = BindUnaryOperatorKind(syntax.OperatorToken.Kind, boundOperand.Type);

        if (boundOperatorKind == null) 
        {
            _diagnostics.Add($"UNary operator '{syntax.OperatorToken.Text}' is not defined for type {boundOperand.Type}");

            return boundOperand;
        }

        return new BoundUnaryExpression(boundOperatorKind.Value, boundOperand);
    }

    private BoundExpression BindBinaryExpression(BinaryExpressionSyntax syntax)
    {

        var boundLeft = BindExpression(syntax.Left);
        var boundRight = BindExpression(syntax.Right);
        var boundOperatorKind = BindBinaryOperator(syntax.OperatorToken.Kind, boundLeft.Type, boundRight.Type);

        if (boundOperatorKind == null) 
        {
            _diagnostics.Add($"UNary operator '{syntax.OperatorToken.Text}' is not defined for type {boundLeft.Type} and {boundRight.Type}");

            return boundLeft;
        }
        return new BoundBinaryExpression(boundLeft,boundOperatorKind.Value, boundRight);
    }


    private BoundUnaryOperatorKind? BindUnaryOperatorKind(SyntaxKind kind, Type operandType)
    {
        if (operandType != typeof(int)) 
        {
            return null;
        }

        switch (kind) 
        {
            case SyntaxKind.PlusToken:
                return BoundUnaryOperatorKind.Identity;
            case SyntaxKind.MinusToken:
                return BoundUnaryOperatorKind.Negation;
            default:
                throw new Exception($"unexprected unary operator {kind}");
        }
    }

    private BoundBinaryOperatorKind? BindBinaryOperator(SyntaxKind kind, Type leftType, Type rightType)
    {
        if (leftType != typeof(int) || rightType != typeof(int)) 
        {
            return null;
        }

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

            default:
                throw new Exception($"unexprected binary operator {kind}");
        }
    }
}
