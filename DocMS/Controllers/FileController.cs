using BLL.DTOs;
using BLL.Services;
using DocMS.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Document_Management_System.Controllers
{
    public class FileController : ApiController
    {
        [Logged]
        [HttpGet]
        [Route("api/files/all")]
        public HttpResponseMessage GetFiles()
        {
            try
            {
                var files = FileService.Get()
                    .Select(f => new {
                        f.Id,
                        f.F_Name,
                        f.Path,
                        f.Upload_Time,
                        f.User_Id,
                        f.Tag_Id
                    }).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, files);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Logged]
        [HttpGet]
        [Route("api/files/{id}")]
        public HttpResponseMessage GetFile(int id)
        {
            try
            {
                var file = FileService.Get(id);
                if (file != null)
                    return Request.CreateResponse(HttpStatusCode.OK, file);
                return Request.CreateResponse(HttpStatusCode.NotFound, "File not found");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message + " | " + (ex.InnerException?.Message ?? ""));
            }
        }

        [Logged]
        [HttpPost]
        [Route("api/files/upload")]
        public HttpResponseMessage UploadFile()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;

                if (httpRequest.Files.Count > 0)
                {
                    var token = httpRequest.Headers["Authorization"];
                    token = token?.Replace("Bearer ", "").Trim();

                    int? userId = BLL.Services.AuthService.GetUserIdFromToken(token);
                    if (userId == null)
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid or expired token");

                    var tagId = Convert.ToInt32(httpRequest.Form["TagId"]);
                    var postedFile = httpRequest.Files[0];
                    var filePath = HttpContext.Current.Server.MapPath("~/Uploads/" + postedFile.FileName);
                    postedFile.SaveAs(filePath);

                    var fileDto = new FileDTO
                    {
                        F_Name = postedFile.FileName,
                        Path = "/Uploads/" + postedFile.FileName,
                        Upload_Time = DateTime.Now,
                        User_Id = userId.Value,
                        Tag_Id = tagId
                    };

                    var created = FileService.Create(fileDto);
                    return Request.CreateResponse(HttpStatusCode.Created, created);
                }

                return Request.CreateResponse(HttpStatusCode.BadRequest, "No file uploaded");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message + " | " + (ex.InnerException?.Message ?? ""));
            }
        }

        // UPDATE
        [Logged]
        [HttpPut]
        [Route("api/files/update")]
        public HttpResponseMessage UpdateFile(FileDTO file)
        {
            try
            {
                var updated = FileService.Update(file);
                return Request.CreateResponse(HttpStatusCode.OK, updated);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message + " | " + (ex.InnerException?.Message ?? ""));
            }
        }

        [Logged]
        [HttpGet]
        [Route("api/by-tag/{tagId}")]
        public HttpResponseMessage GetFilesByTag(int tagId)
        {
            try
            {
                // Optional: Verify tag exists
                var tag = TagService.Get(tagId);
                if (tag == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Tag not found");
                }

                // Get files and transform for JSON
                var files = FileService.GetByTag(tagId)
                    .Select(f => new
                    {
                        f.Id,
                        f.F_Name,
                        f.Upload_Time,
                        ViewUrl = $"/api/files/view/{f.Id}",
                        DownloadUrl = $"/api/files/download/{f.Id}",
                        Tag = new { Id = tag.Id, Name = tag.Name }
                    }).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, files);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [Logged] 
        [HttpDelete]
        [Route("api/files/delete/{id}")]
        public HttpResponseMessage DeleteFile(int id)
        {
            try
            {
                bool deleted = FileService.Delete(id);
                if (deleted)
                    return Request.CreateResponse(HttpStatusCode.OK, "File deleted successfully");
                return Request.CreateResponse(HttpStatusCode.NotFound, "File not found");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message + " | " + (ex.InnerException?.Message ?? ""));
            }
        }
    }
}
