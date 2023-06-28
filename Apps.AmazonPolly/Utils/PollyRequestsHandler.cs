namespace Apps.AmazonPolly.Utils;

public static class PollyRequestsHandler
{
    public static async Task<T> ExecutePollyAction<T>(Func<Task<T>> func)
    {
        try
        {
            return await func();
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}