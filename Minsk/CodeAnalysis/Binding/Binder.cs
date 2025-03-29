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
    private readonly Dictionary<VariableSymbol, object> _variables;

    public Binder(Dictionary<VariableSymbol, object> variables)
    {
        _variables = variables;
    }

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
                return BindNameExpression((NameExpressionSyntax)syntax);
            case SyntaxKind.AssigmentExpression:
                return BindAssigmentExpression((AssigmentExpressionSyntax)syntax);
            case SyntaxKind.UnaryExpression:
                return BindUnaryExpression((UnaryExpressionSyntax)syntax);
            case SyntaxKind.BinaryExpression:
                return BindBinaryExpression((BinaryExpressionSyntax)syntax);
            default:
                throw new Exception($"Unexprecte syntax {syntax.Kind}");
        }

    }

    private BoundExpression BindAssigmentExpression(AssigmentExpressionSyntax syntax)
    {
        var name = syntax.IdentifierToken.Text;
        var boundExpression = BindExpression(syntax.Expression);


        var variable = new VariableSymbol(name, boundExpression.Type);

        var existingVariable = _variables.Keys.FirstOrDefault(v => v.Name == name);
        if (existingVariable != null) 
            _variables.Remove(existingVariable);
            

        return new BoundAssignmentExpression(variable, boundExpression);

    }

    private BoundExpression BindNameExpression(NameExpressionSyntax syntax)
    {

        var name = syntax.IdentifierToken.Text;

        var variable = _variables.Keys.FirstOrDefault(v => v.Name == name);

        if (variable == null)
        {
            _diagnostics.ReportundefinedName(syntax.IdentifierToken.Span, name);
            return new BoundLiteralExpression(0);
        }


        return new BoundVariableExpression(variable);
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
