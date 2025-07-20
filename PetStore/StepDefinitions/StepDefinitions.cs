using System.Net;
using PetStore.Helpers;
using PetStore.Models;
using Reqnroll;
using RestSharp;

namespace PetStore.StepDefinitions;

[Binding]
public class StepDefinitions(RestClient restClient, ScenarioContext scenarioContext)
{
    [When("User creates new pet in pet store")]
    public async Task PostPet()
    {
        var postRequest = new RestRequest("pet", Method.Post);

        var pet = new PetModel
        {
            Id = Random.Shared.NextInt64(1, long.MaxValue),
            Name = "Pet1",
            PhotoUrls = ["Test"],
            Tags = [new Tag { Id = 1, Name = "SuperDogTag" }],
            Category = new Category
            {
                Id = 1,
                Name = "SuperDogCategory",
            },
            Status = "available"
        };
        
        postRequest.AddJsonBody(pet);
        
        var response = await restClient.ExecutePostAsync<PetModel>(postRequest);
        
        scenarioContext[ScenarioKeys.PostRestResponse] = response;
        scenarioContext[ScenarioKeys.PetPostData] = pet;
    }
    
    [When("User updates pet in pet store")]
    public async Task PutPet()
    {
        var request = new RestRequest("pet", Method.Put);

        var pet = new PetModel
        {
            Id = Random.Shared.NextInt64(1, long.MaxValue),
            Name = "Pet2",
            PhotoUrls = ["Test"],
            Tags = [new Tag { Id = 2, Name = "SuperDogTagUpdated" }],
            Category = new Category
            {
                Id = 2,
                Name = "SuperDogCategoryUpdated",
            },
            Status = "available"
        };

        request.AddJsonBody(pet);
        
        var response = await restClient.ExecutePutAsync<PetModel>(request);
        
        scenarioContext[ScenarioKeys.PutRestResponse] = response;
        scenarioContext[ScenarioKeys.PetPutData] = pet;
    }
    
    [When(@"User deletes non existing pet")]
    public async Task DeleteNonExistingPet()
    {
        var request = new RestRequest("pet/{petId}", Method.Delete);
        request.AddUrlSegment("petId", int.MaxValue);
        
        var response = await restClient.ExecuteDeleteAsync(request);
        
        scenarioContext[ScenarioKeys.DeleteRestResponse] = response;
    }
    
    [When(@"User searches for pets with given status")]
    public async Task FindPetByStatus(Table table)
    {
        var status = table.CreateInstance<StatusModel>().Status.ToLower();
        
        var request = new RestRequest("pet/findByStatus");
        request.AddQueryParameter("status", status);

        var response = await restClient.ExecuteGetAsync<List<PetModel>>(request);
        
        scenarioContext[ScenarioKeys.SearchForPetsRestResponse] = response;
    }
    
    [When("User gets the created pet")]
    public async Task GetPostPet()
    {
        var createdPetId = scenarioContext.Get<PetModel>(ScenarioKeys.PetPostData).Id;
        
        var request = new RestRequest("pet/{petId}");
        request.AddUrlSegment("petId", createdPetId);
        
        // Sometimes get does not return immediately the data that was created by POST so
        // Either it can be a new method with retry mechanism or just a task delay for the exercise purpose
        await Task.Delay(10000);
        var response = await restClient.ExecuteGetAsync<PetModel>(request);
        
        scenarioContext[ScenarioKeys.GetPetRestResponse] = response;
    }

    [Then(@"User should receive NotFound status code")]
    public void CheckDeleteResponse()
    {
        var response = scenarioContext.Get<RestResponse>(ScenarioKeys.DeleteRestResponse);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound),
            string.Format(ErrorMessages.InvalidStatusCode, response.Request.Method));
    }

    [Then(@"User should get list of pets with expected status")]
    public void CheckFindByStatusResponse(Table table)
    {
        var response = scenarioContext.Get<RestResponse<List<PetModel>>>( ScenarioKeys.SearchForPetsRestResponse);
        var expectedStatus = table.CreateInstance<StatusModel>().Status.ToLower();
        
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), string.Format(ErrorMessages.InvalidStatusCode, response.Request.Method));
            Assert.That(response.Data?.All(pm => pm.Status == expectedStatus), Is.True, ErrorMessages.UnexpectedStatusReturned);
        });
    }

    [Then("New pet should be created")]
    public void CheckPostPetResponse()
    {
        var response = scenarioContext.Get<RestResponse<PetModel>>(ScenarioKeys.PostRestResponse);
        var expectedPet = scenarioContext.Get<PetModel>(ScenarioKeys.PetPostData);

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), string.Format(ErrorMessages.InvalidStatusCode, response.Request.Method));
            Assert.That(response.Data, Is.EqualTo(expectedPet));
        });
    }

    [Then("Created pet should match the Get response")]
    public void CheckPostPetMatchGetPetResponse()
    {
        var response = scenarioContext.Get<RestResponse<PetModel>>(ScenarioKeys.GetPetRestResponse);
        var expectedPet = scenarioContext.Get<PetModel>(ScenarioKeys.PetPostData);
        
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), string.Format(ErrorMessages.InvalidStatusCode, response.Request.Method));
            Assert.That(response.Data, Is.EqualTo(expectedPet));
        });
    }

    [Then("Pet should be updated")]
    public void CheckPutPetResponse()
    {
        var response = scenarioContext.Get<RestResponse<PetModel>>(ScenarioKeys.PutRestResponse);
        var expectedPet = scenarioContext.Get<PetModel>(ScenarioKeys.PetPutData);

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), string.Format(ErrorMessages.InvalidStatusCode, response.Request.Method));
            Assert.That(response.Data, Is.EqualTo(expectedPet));
        });
    }
}
