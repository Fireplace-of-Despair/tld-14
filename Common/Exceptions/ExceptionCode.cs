namespace Common.Exceptions;

public enum ExceptionCode
{
    Test = 0,

    General = 1000,

    Forbidden = 1403,
    Unauthorized = 1401,
    Conflict = 1409,
    NotFound = 1404,

    Unavailable = 1503,
    Timeout = 1504,
}