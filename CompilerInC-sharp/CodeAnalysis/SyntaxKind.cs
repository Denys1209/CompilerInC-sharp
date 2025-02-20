﻿namespace CompilerInC_sharp.CodeAnalysis;

public enum SyntaxKind
{
    // Tokens
    BadToken,
    EndOfFileToken,
    WhitespaceToken,
    NumberToken,
    PlusToken,
    MinusToken,
    StarToken,
    SlashToken,
    OpenParenthesisToken,
    CloseParenthesisToken,

    // Expression
    LiteralExpression,
    BinaryExpression,
    ParenthesizedExpression
}
