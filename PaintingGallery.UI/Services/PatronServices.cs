using Newtonsoft.Json;
using PaintindGallery.Dtos;
using PaintingGallery.UI.Helper;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PaintingGallery.UI.Services
{
    public class PatronServices
    {
        public async Task<List<PatronDto>> GetPatrons()
        {
            try
            {
                var client = PaintingGalleryHttpClient.GetClient();

                var response = await client.GetAsync("api/patrons");

                List<PatronDto> patrons = null;

                if (response.IsSuccessStatusCode)
                {
                    string patronContext = await response.Content.ReadAsStringAsync();

                    patrons = JsonConvert.DeserializeObject<List<PatronDto>>(patronContext);
                }

                return patrons;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<PatronDto> GetPatron(int id)
        {
            try
            {
                var client = PaintingGalleryHttpClient.GetClient();
                var result = await client.GetAsync("api/patrons?id=" + id);
                PatronDto patron = null;

                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();

                    patron = JsonConvert.DeserializeObject<PatronDto>(content);
                }

                return patron;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeletePatron(int id)
        {
            try
            {
                var client = PaintingGalleryHttpClient.GetClient();
                var response = await client.DeleteAsync("api/patrons/" + id);

                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    return true;

                return false;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<PatronDto> EditPatron(PatronDto patronDto)
        {
            try
            {
                var client = PaintingGalleryHttpClient.GetClient();
                var serealizedItemToCreate = JsonConvert.SerializeObject(patronDto);
                PatronDto patronEdited = null;

                var response = await client.PutAsync("api/patrons",
                    new StringContent(serealizedItemToCreate, System.Text.Encoding.Unicode, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    patronEdited = JsonConvert.DeserializeObject<PatronDto>(content);
                }

                return patronEdited;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<PatronDto> CreatePatron(PatronDto patron)
        {
            try
            {
                var client = PaintingGalleryHttpClient.GetClient();
                var serealizedItemToCreate = JsonConvert.SerializeObject(patron);
                PatronDto patronDto = null;

                var reponse = await client.PostAsync("api/patrons",
                    new StringContent(serealizedItemToCreate,
                    System.Text.Encoding.Unicode, "application/json"));

                if (reponse.IsSuccessStatusCode)
                {
                    string patronContent = await reponse.Content.ReadAsStringAsync();

                    patronDto = JsonConvert.DeserializeObject<PatronDto>(patronContent);
                }

                return patronDto;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
