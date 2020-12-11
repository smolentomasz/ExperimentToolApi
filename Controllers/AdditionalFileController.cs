using System;
using System.IO;
using ExperimentToolApi.Interfaces;
using ExperimentToolApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Newtonsoft.Json.Linq;

namespace ExperimentToolApi.Controllers
{
    public class AdditionalFileController : ControllerBase
    {
        private readonly IAdditionalFileRepository additionalFileRepository;
        public AdditionalFileController(IAdditionalFileRepository additionalFileRepository)
        {
            this.additionalFileRepository = additionalFileRepository;
        }
        [HttpGet("/tool/additional-files")]
        public IActionResult GetList()
        {
            return Ok(additionalFileRepository.GetList());
        }
        [HttpGet("/tool/additional-files/reference/{reference}")]
        public IActionResult GetByReference(string reference)
        {
            if (reference.Equals(""))
            {
                return BadRequest(new ApiResponse("Missing or invalid data"));
            }
            else
            {
                if (additionalFileRepository.isPresent(reference))
                {
                    return Ok(additionalFileRepository.GetByReference(reference));
                }
                else
                {
                    return Conflict(new ApiResponse("Additional files for this case don't exist in database!"));
                }
            }
        }
        [Authorize]
        [HttpGet("/tool/additional-files/id/{id}")]
        public IActionResult GetFileById(int id)
        {
            if (additionalFileRepository.isPresentById(id))
            {
                AdditionalFile getFile = additionalFileRepository.GetFileById(id);

                var filePath = getFile.DbPath;

                if (!System.IO.File.Exists(filePath))
                    return NotFound(new ApiResponse("Not exist!"));
                else
                {
                    var bytes = System.IO.File.ReadAllBytes(filePath);
                    FileContentResult newResult = File(bytes, MimeTypes.GetMimeType(getFile.Name), getFile.Name);
                    return Ok(newResult);
                }

            }
            else
            {
                return Conflict(new ApiResponse("File with this id does not exist in database!"));
            }
        }
        [Authorize]
        [HttpPost("/tool/additional-files")]
        public IActionResult AddNewFile()
        {
            var file = Request.Form.Files[0];
            var detailsDecode = JObject.Parse(Request.Form["aditionalDetails"]);

            var folderName = Path.Combine("Resources", "AdditionalFiles");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            var fullPath = Path.Combine(pathToSave, file.FileName);

            if (file.Length > 0)
            {
                var dbPath = Path.Combine(folderName, file.FileName);

                using (var stream = System.IO.File.Create(fullPath))
                {
                    file.CopyTo(stream);
                }

                string referenceType = detailsDecode["referenceType"].ToString();
                string referenceTypeName = detailsDecode["referenceTypeName"].ToString();

                var additionalFile = new CreateFileRequest
                {
                    Name = file.FileName,
                    DbPath = dbPath,
                    ReferenceType = referenceType,
                    ReferenceTypeName = referenceTypeName
                };
                additionalFileRepository.Create(additionalFile.returnFile());
            }

            return Ok(new ApiResponse("Added succesfully!"));
        }
        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}