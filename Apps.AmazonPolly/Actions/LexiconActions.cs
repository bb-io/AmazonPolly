using Amazon.Polly.Model;
using Apps.AmazonPolly.Factories;
using Apps.AmazonPolly.Models.Request;
using Apps.AmazonPolly.Models.Response;
using Apps.AmazonPolly.Utils;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;

namespace Apps.AmazonPolly.Actions;

[ActionList]
public class LexiconActions
{
    // TODO: Uncoment these actions after lexicons are integrated into synthesize speech action
    // [Action("List lexicons", Description = "List pronunciation lexicons")]
    // public async Task<List<LexiconModel>> ListLexicons(
    //     IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders)
    // {
    //     var client = AmazonPollyClientsFactory.CreateClientWithCreds(authenticationCredentialsProviders.ToArray());
    //
    //     var request = new ListLexiconsRequest();
    //     var lexiconsResponse = await PollyRequestsHandler.ExecutePollyAction(client.ListLexiconsAsync, request);
    //
    //     return lexiconsResponse.Lexicons
    //         .Select(x => new LexiconModel(x.Name, string.Empty))
    //         .ToList();
    // }
    //
    // [Action("Get lexicon", Description = "Get pronunciation lexicon by name")]
    // public async Task<LexiconModel> GetLexicon(
    //     IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
    //     [ActionParameter] [Display("Lexicon name")]
    //     string lexiconName)
    // {
    //     var client = AmazonPollyClientsFactory.CreateClientWithCreds(authenticationCredentialsProviders.ToArray());
    //
    //     var request = new GetLexiconRequest
    //     {
    //         Name = lexiconName
    //     };
    //
    //     var lexiconResponse = await PollyRequestsHandler.ExecutePollyAction(client.GetLexiconAsync, request);
    //
    //     return new(lexiconResponse.Lexicon.Name, lexiconResponse.Lexicon.Content);
    // }
    //
    // [Action("Delete lexicon", Description = "Delete pronunciation lexicon")]
    // public async Task DeleteLexicon(
    //     IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
    //     [ActionParameter] [Display("Lexicon name")]
    //     string lexiconName)
    // {
    //     var client = AmazonPollyClientsFactory.CreateClientWithCreds(authenticationCredentialsProviders.ToArray());
    //
    //     var request = new DeleteLexiconRequest
    //     {
    //         Name = lexiconName
    //     };
    //
    //     await PollyRequestsHandler.ExecutePollyAction(client.DeleteLexiconAsync, request);
    // }
    //
    // public async Task<LexiconModel> CreateLexicon(
    //     IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
    //     [ActionParameter] CreateLexiconRequestModel inputData)
    // {
    //     var client = AmazonPollyClientsFactory.CreateClientWithCreds(authenticationCredentialsProviders.ToArray());
    //
    //     var request = new PutLexiconRequest
    //     {
    //         Name = inputData.Name,
    //         Content = inputData.Content,
    //     };
    //
    //     await PollyRequestsHandler.ExecutePollyAction(client.PutLexiconAsync, request);
    //
    //     return new(inputData.Name, inputData.Content);
    // }
}