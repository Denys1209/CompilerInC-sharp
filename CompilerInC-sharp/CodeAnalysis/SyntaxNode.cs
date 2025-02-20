
namespace D_Compiler_MyOwnLanguage_.CodeAnalysis;

public abstract class SyntaxNode
{
    public abstract SyntaxKind Kind { get; }

    public abstract IEnumerable<SyntaxNode> GetChildren();
}
