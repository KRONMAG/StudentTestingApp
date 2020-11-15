using RestSharp;

namespace StudentTestingApp.Model.DataAccess
{
    public static class RestResponseExtension
    {
        public static dynamic JsonContentToDynamic(this IRestResponse response) =>
            (dynamic)SimpleJson.DeserializeObject(response.Content);
    }
}
