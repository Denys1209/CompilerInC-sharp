namespace Minks.CodeAnalysis.Binding;

abstract class BoundExpression : BoundNode 
{
    public abstract Type Type { get; }
}
