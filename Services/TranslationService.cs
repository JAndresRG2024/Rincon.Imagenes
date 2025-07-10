using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Rincon.Imagenes.Services
{
    public class TranslationService
    {
        private const string SubscriptionKey = "31tkGxVF3JFFvmfUuntIPZPDLkvsBAdFRieuuL9rO6mwzOQUZA95JQQJ99BGACYeBjFXJ3w3AAAbACOG68iP";
        private const string Endpoint = "https://api.cognitive.microsofttranslator.com";
        private const string Region = "eastus";

        public async Task<string> TraducirAsync(string texto, string idiomaDestino = "es")
        {
            var route = $"/translate?api-version=3.0&to={idiomaDestino}";
            var uri = Endpoint + route;

            var body = new object[] { new { Text = texto } };
            var requestBody = JsonSerializer.Serialize(body);

            using var client = new HttpClient();
            using var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(uri),
                Content = new StringContent(requestBody, Encoding.UTF8, "application/json")
            };

            request.Headers.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);
            request.Headers.Add("Ocp-Apim-Subscription-Region", Region);

            var response = await client.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error de traducción: {response.StatusCode} - {responseContent}");
            }

            using var jsonDoc = JsonDocument.Parse(responseContent);
            var traduccion = jsonDoc.RootElement[0]
                                     .GetProperty("translations")[0]
                                     .GetProperty("text")
                                     .GetString();

            return traduccion;
        }
    }
}
