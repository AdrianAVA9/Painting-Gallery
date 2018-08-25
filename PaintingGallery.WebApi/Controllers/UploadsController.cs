using AutoMapper;
using PaintingGallery.Manager.IManagers;
using PaintingGallery.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace PaintingGallery.WebApi.Controllers
{
    [RoutePrefix("api/uploads")]
    public class UploadsController : ApiController
    {
        private string ServerPathFolder { get; set; }
        private IPatronManager _manager { get; set; }
        private IMapper _mapper { get; set; }
        private IList<string> allowedFilesExtensions = new List<string> { ".png", ".jpg", ".jpeg", ".gif" };
        private IList<string> UserType = new List<string> { "patron" };

        public UploadsController(IPatronManager manager, IMapper mapper)
        {
            ServerPathFolder = ConfigurationManager.AppSettings["UPLOAD_DIR"];
            _manager = manager;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("files/image/{id}")]
        public IHttpActionResult GetFile(int id, string filename)
        {
            try
            {
                var route = HttpContext.Current.Server.MapPath(ServerPathFolder);
                var user = _manager.GetPatron(id);
                var completeRoute = route + "/" + filename;

                if (user == null && filename == null)
                    return BadRequest();

                if (!user.Picture.Equals(filename))
                    return BadRequest();

                if (File.Exists(completeRoute))
                {
                    var response = new HttpResponseMessage(HttpStatusCode.OK);
                    var fileStream = File.OpenRead(completeRoute);

                    response.Content = new StreamContent(fileStream);
                    response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/" + Path.GetExtension(completeRoute).Substring(1));
                    response.Content.Headers.ContentLength = fileStream.Length;

                    return ResponseMessage(response);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("files/image/{userType}/{userId}")]
        public IHttpActionResult PostFiles(string userType, int userId)
        {
            try
            {
                if (!UserType.Contains(userType.ToLower()))
                    return BadRequest("User type don't exists");

                if (!Request.Content.IsMimeMultipartContent())
                    return StatusCode(HttpStatusCode.UnsupportedMediaType);

                var httpRequest = HttpContext.Current.Request;

                var MaxContentLength = 3072;

                if (httpRequest.Files.Count > 0)
                {
                    foreach (string file in httpRequest.Files)
                    {
                        var postedFile = httpRequest.Files[file];

                        if (postedFile?.ContentLength > 0)
                        {
                            var extension = Path.GetExtension(postedFile.FileName);

                            if (allowedFilesExtensions.Contains(extension))
                            {
                                if (postedFile.ContentLength > MaxContentLength)
                                {
                                    var pictureName = string.Concat(Guid.NewGuid().ToString(), "-", DateTime.Now.ToString("dd-MM-yyyy"));
                                    pictureName += extension;

                                    var route = HttpContext.Current.Server.MapPath(ServerPathFolder);

                                    route = route + "/" + pictureName;

                                    postedFile.SaveAs(route);

                                    var result = _manager.EditPatronImage(new Patron { Id = userId, Picture = pictureName });

                                    if (result.Status == Repository.RepositoryActionStatus.Updated)
                                        return Ok(result.Entity);

                                    if (File.Exists(route))
                                        File.Delete(route);

                                    if (result.Status == Repository.RepositoryActionStatus.NotFound)
                                        return NotFound();

                                    if (result.Status == Repository.RepositoryActionStatus.Error)
                                        return InternalServerError();


                                    return Ok(result.Entity);
                                }
                                else
                                {
                                    return BadRequest("Please upload a file upto 3 mb");
                                }
                            }
                            else
                            {
                                return BadRequest("File extension is not allowed, Please images type of .jpg, .jpeg, .png, .gif");
                            }
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                }

                return NotFound();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
