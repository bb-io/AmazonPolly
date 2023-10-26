using Amazon.Polly.Model;
using Apps.AmazonPolly.Factories;
using Apps.AmazonPolly.Utils;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;

namespace Apps.AmazonPolly.Connections;

public class ConnectionValidator : IConnectionValidator
{
    public async ValueTask<ConnectionValidationResponse> ValidateConnection(
        IEnumerable<AuthenticationCredentialsProvider> authProviders, CancellationToken cancellationToken)
    {
        var client = AmazonPollyClientsFactory.CreateClientWithCreds(authProviders.ToArray());

        try
        {
            await PollyRequestsHandler
                .ExecutePollyAction(client.ListLexiconsAsync, new ListLexiconsRequest());

            return new()
            {
                IsValid = true
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                IsValid = false,
                Message = ex.Message
            };
        }
    }
}