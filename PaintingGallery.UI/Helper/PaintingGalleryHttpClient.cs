using PaintingGallery.Constants;
using System.Net.Http;
using System.Net.Http.Headers;

namespace PaintingGallery.UI.Helper
{
    public class PaintingGalleryHttpClient
    {
        public static HttpClient GetClient()
        {
            var client = new HttpClient();

            client.BaseAddress = new System.Uri(PaintingGalleryConstants.PaintingGalleryApi);

            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }
}
