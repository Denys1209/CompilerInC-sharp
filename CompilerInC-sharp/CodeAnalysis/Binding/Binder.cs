﻿using CompilerInC_sharp.CodeAnalysis.Syntax;
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
            case SyntaxKind.ParenthesizedExpression:
                return BindExpression(((ParenthesizedExpressionSyntax)syntax).Expression);
            default:
                throw new Exception($"Unexprecte syntax {syntax.Kind}");
        }

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
            _diagnostics.Add($"UNary operator '{syntax.OperatorToken.Text}' is not defined for type {boundOperand.Type}");

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
            _diagnostics.Add($"UNary operator '{syntax.OperatorToken.Text}' is not defined for type {boundLeft.Type} and {boundRight.Type}");

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
