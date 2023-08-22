using Amazon.Polly.Model;
using Apps.AmazonPolly.Factories;
using Apps.AmazonPolly.Models.Request.Lexicon;
using Apps.AmazonPolly.Models.Response.Lexicon;
using Apps.AmazonPolly.Utils;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.AmazonPolly.Actions;

[ActionList]
public class LexiconActions : BaseInvocable
{
    private IEnumerable<AuthenticationCredentialsProvider> Creds =>
        InvocationContext.AuthenticationCredentialsProviders;
    
    public LexiconActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }
    
    [Action("List lexicons", Description = "List pronunciation lexicons")]
    public async Task<List<LexiconModel>> ListLexicons()
    {
        var client = AmazonPollyClientsFactory.CreateClientWithCreds(Creds.ToArray());
    
        var lexiconsResponse = await PollyRequestsHandler
            .ExecutePollyAction(client.ListLexiconsAsync, new ListLexiconsRequest());
    
        return lexiconsResponse.Lexicons
            .Select(x => new LexiconModel(x.Name, string.Empty))
            .ToList();
    }
    
    [Action("Get lexicon", Description = "Get pronunciation lexicon by name")]
    public async Task<LexiconModel> GetLexicon([ActionParameter] LexiconRequest lexicon)
    {
        var client = AmazonPollyClientsFactory.CreateClientWithCreds(Creds.ToArray());
    
        var request = new GetLexiconRequest
        {
            Name = lexicon.LexiconName
        };
    
        var lexiconResponse = await PollyRequestsHandler.ExecutePollyAction(client.GetLexiconAsync, request);
    
        return new(lexiconResponse.Lexicon.Name, lexiconResponse.Lexicon.Content);
    }
    
    [Action("Delete lexicon", Description = "Delete pronunciation lexicon")]
    public async Task DeleteLexicon([ActionParameter] LexiconRequest lexicon)
    {
        var client = AmazonPollyClientsFactory.CreateClientWithCreds(Creds.ToArray());
    
        var request = new DeleteLexiconRequest
        {
            Name = lexicon.LexiconName
        };
    
        await PollyRequestsHandler.ExecutePollyAction(client.DeleteLexiconAsync, request);
    }
    
    [Action("Create lexicon", Description = "Create a new lexicon")]
    public async Task<LexiconModel> CreateLexicon([ActionParameter] CreateLexiconRequestModel inputData)
    {
        var client = AmazonPollyClientsFactory.CreateClientWithCreds(Creds.ToArray());
    
        var request = new PutLexiconRequest
        {
            Name = inputData.Name,
            Content = inputData.Content,
        };
    
        await PollyRequestsHandler.ExecutePollyAction(client.PutLexiconAsync, request);
    
        return new(inputData.Name, inputData.Content);
    }
}