namespace Common.Exceptions;

public sealed class IncidentException(ExceptionCode code) : Exception
{
    public ExceptionCode Code { get; } = code;
}
