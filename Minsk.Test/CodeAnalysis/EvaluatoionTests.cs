using Minks.CodeAnalysis.Syntax;
using Minsk.CodeAnalysis;

namespace Minsk.Test.CodeAnalysis;

public class EvaluatoionTests
{
    [Theory]
    [InlineData("1", 1)]
    [InlineData("+1", 1)]
    [InlineData("-1", -1)]
    [InlineData("1 + 2", 3)]
    [InlineData("1 - 2", -1)]
    [InlineData("1 * 2", 2)]
    [InlineData("2 / 2", 1)]
    [InlineData("(10)", 10)]
    [InlineData("1 == 1", true)]
    [InlineData("1 == 2", false)]
    [InlineData("1 != 1", false)]
    [InlineData("1 != 2", true)]
    [InlineData("true", true)]
    [InlineData("false", false)]
    [InlineData("!true", false)]
    [InlineData("!false", true)]
    [InlineData("true == false", false)]
    [InlineData("true == true", true)]
    [InlineData("true != false", true)]
    [InlineData("true != true", false)]
    [InlineData("true && false", false)]
    [InlineData("true && true", true)]
    [InlineData("true || false", true)]
    [InlineData("true || true", true)]
    [InlineData("a = 10", 10)]
    [InlineData("(s = 100)", 100)]
    [InlineData("(a = 10) * a", 100)]
    public void SyntaxFact_GetText_RoundTrips(string text, object exprectedValue) 
    {
        var syntaxTree = SyntaxTree.Parse(text);
        var compilation = new Compilation(syntaxTree);
        var variables = new Dictionary<VariableSymbol, object>();

        var result = compilation.Evaluate(variables);

        Assert.Empty(result.Diagnostics);
        Assert.Equal(exprectedValue, result.Value);

    }
}
