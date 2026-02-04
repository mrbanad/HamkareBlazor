using HamkareBlazor.Resources;

namespace HamkareBlazor;

public class BusinessException(string message) : Exception(message)
{
    public BusinessException(long code, List<string> message) : this(code, string.Join(',', message))
    {
        Code = code;
    }

    public BusinessException(long code, string message) : this($"{(int)code}: {message}")
    {
        Code = code;
    }

    public long? Code { get; }
}

public class NotFoundException(string entityDisplayName)
    : Exception(string.Format(LanguageResource.ErrorNotFound, entityDisplayName));

public class SilenceException()
    : Exception();
