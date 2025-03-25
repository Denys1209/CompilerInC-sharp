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
    EqualsToken,
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
    NameExpression,
    AssigmentExpression,
}
