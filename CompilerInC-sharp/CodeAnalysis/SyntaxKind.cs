namespace D_Compiler_MyOwnLanguage_.CodeAnalysis;

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
    NumberExpression,
    BinaryExpression,
    ParenthesizedExpression
}
