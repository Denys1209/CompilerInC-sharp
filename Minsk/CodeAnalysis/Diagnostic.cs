namespace Minsk.CodeAnalysis;

public sealed class Diagnostic 
{
    public Diagnostic(TextSpan span, string message) 
    {
        Span = span;
        Message = message;
    }

    public string Message { get; set; }
    public TextSpan Span { get; set; }

    public override string ToString() => Message;
}

