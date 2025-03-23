using Minks.CodeAnalysis.Syntax;
using System.Collections;

namespace Minsk.CodeAnalysis;


internal sealed class DiagnosticBag : IEnumerable<Diagnostic>
{ 
    private readonly List<Diagnostic> _diagnostics = new List<Diagnostic>();

    public IEnumerator<Diagnostic> GetEnumerator() => _diagnostics.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void AddRange(DiagnosticBag diagnostics) 
    {
        _diagnostics.AddRange(diagnostics._diagnostics);
    }

    private void Report(TextSpan span, string message) 
    {
        var diagnostic = new Diagnostic(span, message);
        _diagnostics.Add(diagnostic);
    }

    public void ReportInvalideNumber(TextSpan span, string text, Type type)
    {
        var message = $"The number {text} isn't valid {type}.";
        Report(span, message);
    }

    public void ReportBadCharacter(int position, char character)
    {

        TextSpan span = new TextSpan(position, 1);
        var message = $"Dad character input: '{character}'";

        Report(span, message);
    }

    internal void ReportUnexpectedToken(TextSpan span, SyntaxKind actualKind, SyntaxKind expectedKind)
    {
        var message = $"Unexpected token <{actualKind}>, expected <{expectedKind}>";
        Report(span, message);
    }

    internal void ReportUnderFinedUnaryOperator(TextSpan span, string operatorText, Type operandType)
    {
        var message = $"Unary operator '{operatorText}' is not defined for type {operandType}";
        Report(span, message);
    }

    internal void ReportUndefinedBinaryOperator(TextSpan span, string operatorToken, Type leftType, Type rightType)
    {
        var message = $"Unary operator '{operatorToken}' is not defined for type {leftType} and {rightType}";
        Report(span, message);
    }
}

