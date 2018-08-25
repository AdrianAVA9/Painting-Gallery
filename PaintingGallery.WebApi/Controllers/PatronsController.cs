using AutoMapper;
using PaintindGallery.Dtos;
using PaintingGallery.Manager.IManagers;
using PaintingGallery.Repository;
using PaintingGallery.Repository.Entities;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;

namespace PaintingGallery.WebApi.Controllers
{
    [RoutePrefix("api/patrons")]
    public class PatronsController : ApiController
    {
        private IPatronManager _manager { get; set; }
        private IMapper _mapper { get; set; }

        public PatronsController(IPatronManager manager, IMapper mapper, string connectionString)
        {
            _manager = manager;
            _mapper = mapper;
        }

        [HttpGet]
        public IHttpActionResult GetPatron(int id)
        {
            var patron = _mapper.Map<Patron>(_manager.GetPatron(id));

            var imageUrl = Request.RequestUri.AbsoluteUri.Replace("?id=" + id, string.Format("/{0}/userImage?filename={1}", patron.Id, patron.Picture));

            patron.Picture = imageUrl;

            return Ok(patron);
        }

        [HttpGet]
        [Route("{userId}/userImage")]
        public IHttpActionResult GetUserPicture(int userId, string filename)
        {
            var uploadsController = new UploadsController(_manager, _mapper)
            {
                Request = new System.Net.Http.HttpRequestMessage(HttpMethod.Post,
                Request.RequestUri.AbsoluteUri.Replace("/patrons", string.Concat("/files/image/", userId))),
            };

            uploadsController.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = new HttpConfiguration();

            return uploadsController.GetFile(userId, filename);
        }

        [HttpGet]
        public IHttpActionResult GetPatrons()
        {
            try
            {
                var patrons = _manager.GetPatrons();
                List<PatronDto> patronsDto = null;

                if (patrons == null)
                    return Ok(patronsDto);

                patronsDto = new List<PatronDto>();

                foreach (var patron in patrons)
                {
                    var patronDto = _mapper.Map<PatronDto>(patron);

                    patronDto.Picture = string.Format("{0}/{1}/userImage?filename={2}", Request.RequestUri, patron.Id, patron.Picture);

                    patronsDto.Add(patronDto);
                }

                return Ok(patronsDto);
            }
            catch (System.Exception)
            {
                return InternalServerError();
            }
        }

        [HttpPut]
        public IHttpActionResult PutPatron([FromBody]PatronDto patronDto)
        {
            try
            {
                if (patronDto == null)
                    return BadRequest();

                var result = _manager.EditPatron(_mapper.Map<Patron>(patronDto));

                if (result.Status == RepositoryActionStatus.Updated)
                    return Ok(_mapper.Map<PatronDto>(result.Entity));

                if (result.Status == RepositoryActionStatus.NotFound)
                    return NotFound();

                return BadRequest();
            }
            catch (System.Exception)
            {
                return InternalServerError();
            }
        }

        [HttpPost]
        public IHttpActionResult PostPatron([FromBody]PatronDto patronDto)
        {
            try
            {
                if (patronDto == null)
                    return BadRequest();

                var result = _manager.RegisterPatron(_mapper.Map<Patron>(patronDto));

                if (result.Status == RepositoryActionStatus.Created)
                {
                    var imageUrl = string.Format("{0}/{1}/userImage?filename={2}", Request.RequestUri, result.Entity.Id, result.Entity.Picture);

                    result.Entity.Picture = imageUrl;

                    return Created(Request.RequestUri + "/" + result.Entity.Id.ToString(), _mapper.Map<PatronDto>(result.Entity));
                }

                if (result.Status == RepositoryActionStatus.Error)
                    return InternalServerError();

                return BadRequest();
            }
            catch (System.Exception)
            {
                return InternalServerError();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeletePatron(int id)
        {
            try
            {
                var result = _manager.DeletePatron(id);

                if (result.Status == RepositoryActionStatus.Deleted)
                    return StatusCode(System.Net.HttpStatusCode.NoContent);

                if (result.Status == RepositoryActionStatus.NotFound)
                    return NotFound();

                return BadRequest();
            }
            catch (System.Exception)
            {
                return InternalServerError();
            }
        }
    }
}

