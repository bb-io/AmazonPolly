using Blackbird.Applications.Sdk.Common;

namespace Apps.AmazonPolly;

public class AmazonPollyApplication : IApplication
{
    public string Name
    {
        get => "Amazon Polly";
        set { }
    }

    public T GetInstance<T>()
    {
        throw new NotImplementedException();
    }
}