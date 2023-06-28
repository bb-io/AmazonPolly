using Amazon;
using Amazon.Polly;
using Apps.AmazonPolly.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;

namespace Apps.AmazonPolly.Factories;

public static class AmazonPollyClientsFactory
{
    public static AmazonPollyClient CreateClientWithCreds(
        AuthenticationCredentialsProvider[] authenticationCredentialsProviders)
    {
        var key = authenticationCredentialsProviders.First(p => p.KeyName == "access_key");
        var secret = authenticationCredentialsProviders.First(p => p.KeyName == "access_secret");

        if (string.IsNullOrEmpty(key.Value) || string.IsNullOrEmpty(secret.Value))
            throw new Exception(ExceptionMessages.CredentialsMissing);

        return new(key.Value, secret.Value, new AmazonPollyConfig()
        {
            RegionEndpoint = RegionEndpoint.USWest1
        });
    }
}