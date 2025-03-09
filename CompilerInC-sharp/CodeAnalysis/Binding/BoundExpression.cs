namespace CompilerInC_sharp.CodeAnalysis.Binding;

abstract class BoundExpression : BoundNode 
{
    public abstract Type Type { get; }
}
