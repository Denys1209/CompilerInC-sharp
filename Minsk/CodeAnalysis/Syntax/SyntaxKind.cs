namespace Minks.CodeAnalysis.Syntax;

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
    BangToken,
    AmpersandAmpersandToken,
    PipePipeToken,
    EqualsEqualsToken,
    OpenParenthesisToken,
    CloseParenthesisToken,

    //Keywords
    IdentifierToken,
    FalseKeyword,
    TrueKeyword,

    // Expression
    LiteralExpression,
    UnaryExpression,
    BinaryExpression,
    ParenthesizedExpression,
    BangEqualsToken,
}
