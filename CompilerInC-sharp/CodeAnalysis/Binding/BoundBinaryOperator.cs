﻿using CompilerInC_sharp.CodeAnalysis.Syntax;

namespace CompilerInC_sharp.CodeAnalysis.Binding;

internal sealed class BoundBinaryOperator
{

    private BoundBinaryOperator(SyntaxKind syntaxKind, BoundBinaryOperatorKind kind, Type type)
        : this(syntaxKind, kind, type, type, type)
    {

    }

    private BoundBinaryOperator(SyntaxKind syntaxKind, BoundBinaryOperatorKind kind, Type leftType, Type rightType, Type resultType)
    {
        SyntaxKind = syntaxKind;
        Kind = kind;
        ResultType = resultType;
        LeftType = leftType;
        RightType = rightType;

    }

    public SyntaxKind SyntaxKind { get; }
    public BoundBinaryOperatorKind Kind { get; }
    public Type ResultType { get; }
    public Type LeftType { get; }

    public Type RightType { get; }

    private static BoundBinaryOperator[] _operators =
    {
    new BoundBinaryOperator(SyntaxKind.PlusToken, BoundBinaryOperatorKind.Addition, typeof(int)),
    new BoundBinaryOperator(SyntaxKind.MinusToken, BoundBinaryOperatorKind.Subtraction, typeof(int)),
    new BoundBinaryOperator(SyntaxKind.StarToken, BoundBinaryOperatorKind.Multiplication, typeof(int)),
    new BoundBinaryOperator(SyntaxKind.SlashToken, BoundBinaryOperatorKind.Division, typeof(int)),

    new BoundBinaryOperator(SyntaxKind.AmpersandAmpersandToken, BoundBinaryOperatorKind.LogicalAnd, typeof(bool)),
    new BoundBinaryOperator(SyntaxKind.PipePipeToken, BoundBinaryOperatorKind.LogicalOr, typeof(bool)),

};

    public static BoundBinaryOperator Bind(SyntaxKind syntaxKind, Type leftType, Type rightType)
    {
        foreach (var op in _operators)
        {
            if (op.SyntaxKind == syntaxKind && op.LeftType == leftType && op.RightType == rightType)
                return op;
        }

        return null;
    }

}
