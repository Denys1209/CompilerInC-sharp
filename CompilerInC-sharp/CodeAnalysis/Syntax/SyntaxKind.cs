namespace CompilerInC_sharp.CodeAnalysis.Syntax;

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
    UnaryExpression,
    BinaryExpression,
    ParenthesizedExpression
}
