﻿using CompilerInC_sharp.CodeAnalysis.Syntax;

namespace CompilerInC_sharp.CodeAnalysis.Binding;

internal sealed class BoundUnaryOperator 
{

    private BoundUnaryOperator(SyntaxKind syntaxKind, BoundUnaryOperatorKind kind, Type operandType) 
        : this(syntaxKind, kind, operandType, operandType)
    {

    }

    private BoundUnaryOperator(SyntaxKind syntaxKind, BoundUnaryOperatorKind kind, Type operandType, Type resultType) 
    {
        SyntaxKind = syntaxKind;
        Kind = kind;
        Type = operandType;
        Type = resultType;
    }

    public SyntaxKind SyntaxKind { get; }
    public BoundUnaryOperatorKind Kind { get; }
    public Type OperandType { get; }
    public Type Type { get; }

    private static BoundUnaryOperator[] _operators =
    {
        new BoundUnaryOperator(SyntaxKind.BangToken, BoundUnaryOperatorKind.LogicalNegation, typeof(bool)),
        new BoundUnaryOperator(SyntaxKind.PlusToken, BoundUnaryOperatorKind.Identity, typeof(int)),
        new BoundUnaryOperator(SyntaxKind.MinusToken, BoundUnaryOperatorKind.Identity, typeof(int)),

    }; 

    public static BoundUnaryOperator Bind(SyntaxKind syntaxKind, Type operandType) 
    {
        foreach (var op in _operators)
        {
            if (op.SyntaxKind == syntaxKind && op.OperandType == operandType)
                return op;
        }

        return null;
    }

}