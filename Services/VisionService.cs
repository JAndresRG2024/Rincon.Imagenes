namespace Rincon.Imagenes.Services
{
    public class VisionService
    {
        private const string SubscriptionKey = "6Eri01lVI9CEeI9k2zSscp1GTDK7HHDayrE4vVkRO2nE5wot27G4JQQJ99BFAC4f1cMXJ3w3AAAFACOGgUqN";
        private const string Endpoint = "https://cv-rincon-utn2025.cognitiveservices.azure.com/";
        private const string Features = "tags,read,caption,denseCaptions,smartCrops,objects,people";

        public async Task<string> AnalizarImagenAsync(string imageUrl)
        {
            var consumerACV = new ConsumerACV
            {
                SubscriptionKey = SubscriptionKey,
                ComputerVisionEndpoint = Endpoint,
                Features = Features,
                ImageUrl = imageUrl
            };

            var result = consumerACV.AnalyzeImage();
            return result.ToString();
        }
    }

}
