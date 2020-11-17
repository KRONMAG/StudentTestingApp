using RestSharp;

namespace StudentTestingApp.Model.DataAccess
{
    /// <summary>
    /// Расширение интерфейса REST-ответа
    /// </summary>
    public static class IRestResponseExtension
    {
        /// <summary>
        /// Преобразование JSON-тела REST-ответа в динамический объект
        /// </summary>
        /// <param name="response">REST-ответ</param>
        /// <returns>Динамический объект, соответствующий JSON-телу ответа</returns>
        public static dynamic JsonContentToDynamic(this IRestResponse response) =>
            (dynamic)SimpleJson.DeserializeObject(response.Content);
    }
}
