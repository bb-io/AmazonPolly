using Amazon.Polly.Model;
using Apps.AmazonPolly.Factories;
using Apps.AmazonPolly.Utils;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.AmazonPolly.DataSourceHandlers;

public class LexiconDataHandler : BaseInvocable, IAsyncDataSourceHandler
{
    private IEnumerable<AuthenticationCredentialsProvider> Creds =>
        InvocationContext.AuthenticationCredentialsProviders;
    
    public LexiconDataHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        var client = AmazonPollyClientsFactory.CreateClientWithCreds(Creds.ToArray());
    
        var items = await PollyRequestsHandler
            .ExecutePollyAction(client.ListLexiconsAsync, new ListLexiconsRequest());

        return items.Lexicons
            .Where(x => context.SearchString is null ||
                        x.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(x => x.Attributes.LastModified)
            .Take(20)
            .ToDictionary(x => x.Name, x => x.Name);
    }
}