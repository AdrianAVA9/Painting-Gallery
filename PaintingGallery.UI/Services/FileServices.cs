using PaintindGallery.Dtos;
using PaintingGallery.Constants;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PaintingGallery.UI.Services
{
    public class FileServices
    {
        private static FileServices _instance { get; set; }

        public static FileServices GetInstance()
        {
            if (_instance == null)
                _instance = new FileServices();

            return _instance;
        }

        public async Task<bool> UploadFile(string imageFilePath, PatronDto patronDto)
        {
            try
            {
                HttpClient client = new HttpClient();

                var fileStream = File.OpenRead(imageFilePath);

                var content = new MultipartFormDataContent();
                var streamContent = new StreamContent(fileStream);
                var requestUri = PaintingGalleryConstants.PaintingGalleryApi + "api/uploads/files/image/patron/" + patronDto.Id;

                streamContent.Headers.ContentType = new MediaTypeHeaderValue("multipart/form-data");
                content.Add(streamContent, "image", imageFilePath);

                var reponse = await client.PostAsync(requestUri, content);

                fileStream.Close();

                if (reponse.IsSuccessStatusCode)
                    return true;

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private byte[] GetImageAsByteArray(string imageFilePath)
        {
            var fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);
        }
    }
}
