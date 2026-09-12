namespace HamkareBlazor;

public abstract class HamkareException : Exception
{
    public string ErrorCode { get; }

    public ExceptionBehavior Behavior { get; }

    protected HamkareException(
        string errorCode,
        ExceptionBehavior behavior,
        string? message = null,
        Exception? innerException = null)
        : base(message, innerException)
    {
        ErrorCode = errorCode;
        Behavior = behavior;
    }
}

public sealed class SilenceException : HamkareException
{
    public SilenceException(
        string errorCode,
        string? message = null,
        Exception? innerException = null)
        : base(
            errorCode,
            ExceptionBehavior.Silence,
            message,
            innerException)
    {
    }
}

public sealed class ToastException : HamkareException
{
    public ToastException(
        string errorCode,
        string? message = null,
        Exception? innerException = null)
        : base(
            errorCode,
            ExceptionBehavior.Toast,
            message,
            innerException)
    {
    }
}

public sealed class RedirectException : HamkareException
{
    public string Url { get; }

    public bool ForceLoad { get; }

    public RedirectException(
        string url,
        string errorCode = "Unknown",
        string? message = null,
        bool forceLoad = false,
        Exception? innerException = null)
        : base(
            errorCode,
            ExceptionBehavior.Redirect,
            message,
            innerException)
    {
        Url = string.IsNullOrWhiteSpace(url)
            ? "/Error"
            : url;

        ForceLoad = forceLoad;
    }
}
