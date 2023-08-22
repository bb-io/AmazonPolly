namespace Apps.AmazonPolly.Utils;

public static class PollyRequestsHandler
{
    public static async Task<TResponse> ExecutePollyAction<TResponse, TRequest>(
        Func<TRequest, CancellationToken, Task<TResponse>> func,
        TRequest request)
    {
        try
        {
            return await func(request, CancellationToken.None);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}