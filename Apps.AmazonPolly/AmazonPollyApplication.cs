using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Metadata;

namespace Apps.AmazonPolly;

public class AmazonPollyApplication : IApplication, ICategoryProvider
{
    public IEnumerable<ApplicationCategory> Categories
    {
        get => [ApplicationCategory.AmazonApps, ApplicationCategory.Multimedia];
        set { }
    }
    
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