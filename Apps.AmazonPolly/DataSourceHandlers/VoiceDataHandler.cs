using Amazon.Polly.Model;
using Apps.AmazonPolly.Extensions;
using Apps.AmazonPolly.Factories;
using Apps.AmazonPolly.Utils;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.AmazonPolly.DataSourceHandlers;

public class VoiceDataHandler : BaseInvocable, IAsyncDataSourceHandler
{
    private IEnumerable<AuthenticationCredentialsProvider> Creds =>
        InvocationContext.AuthenticationCredentialsProviders;
    
    public VoiceDataHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        var allVoices = await GetAllVoices();

        return allVoices
            .Where(x => context.SearchString is null ||
                        x.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .ToDictionary(x => x.Id.Value, x => x.Describe());
    }

    private async Task<List<Voice>> GetAllVoices()
    {
        var client = AmazonPollyClientsFactory.CreateClientWithCreds(Creds.ToArray());
        var voices = new List<Voice>();

        var request = new DescribeVoicesRequest();
        do
        {
            var voicesResponse = await PollyRequestsHandler.ExecutePollyAction(client.DescribeVoicesAsync, request);
            request.NextToken = voicesResponse.NextToken;

            voices.AddRange(voicesResponse.Voices);
        } while (request.NextToken is not null);

        return voices;
    }
}