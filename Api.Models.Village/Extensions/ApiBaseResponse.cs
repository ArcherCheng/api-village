//file:///D:/MySample/Ultimate%20ASP.NET%20Core%208.0/Ultimate%20ASP.NET%20Core%20Web%20API%20-%20Premium.pdf

namespace Api.Models;
public abstract class ApiBaseResponse
{
    public bool Success { get; set; }

    protected ApiBaseResponse(bool success)
    {
        Success = success;
    }
}

public sealed class ApioKResponse<TResult> : ApiBaseResponse
{
    public TResult? Result { get; set; }

    public ApioKResponse(TResult result) : base(true)
    {
        Result = result;
    }
}


public abstract class ApiNotFoundResponse : ApiBaseResponse
{
    public string Message { get; set; }

    protected ApiNotFoundResponse(string message) : base(false)
    {
        Message = message;
    }
}

public abstract class ApiBadRequestResponse : ApiBaseResponse
{
    public string Message { get; set; }

    protected ApiBadRequestResponse(string message) : base(false)
    {
        Message = message;
    }
}