using System.IO;
using ExperimentToolApi.Interfaces;
using ExperimentToolApi.Models;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("/tool/additional-files/{reference}")]
        public IActionResult GetByReference(string reference)
        {
            if (reference.Equals(""))
            {
                return BadRequest("Missing or invalid data");
            }
            else
            {
                if (additionalFileRepository.isPresent(reference))
                {
                    return Ok(additionalFileRepository.GetByReference(reference));
                }
                else
                {
                    return Conflict("Additional files for this case don't exist in database!");
                }
            }
        }
        [HttpPost("/tool/additional-files")]
        public IActionResult AddNewFile(){
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

                string reference = detailsDecode["reference"].ToString();

                var additionalFile = new CreateFileRequest{
                    Name = file.Name,
                    DbPath = dbPath,
                    Reference = reference
                };
                additionalFileRepository.Create(additionalFile.returnFile());
            }

            return Ok("Added succesfully!");
        }
    }
}