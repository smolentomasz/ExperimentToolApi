using System.IO;
using ExperimentToolApi.Interfaces;
using ExperimentToolApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace ExperimentToolApi.Controllers
{
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialRepository materialRepository;
        public MaterialController(IMaterialRepository materialRepository)
        {
            this.materialRepository = materialRepository;
        }
        [HttpGet("/tool/materials")]
        public IActionResult GetList()
        {
            return Ok(materialRepository.GetList());
        }
        [HttpGet("/tool/materials/{materialId}")]
        public IActionResult GetMaterialById(int materialId)
        {
            if (materialRepository.isMaterialPresent(materialId))
            {
                return Ok(materialRepository.GetMaterialById(materialId));
            }
            else
            {
                return Conflict("Compression test with this id doesn't exist in database!");
            }
        }
        [HttpPost("/tool/materials"), DisableRequestSizeLimit]
        public IActionResult AddNewTest()
        {
            var file = Request.Form.Files[0];
            var detailsDecode = JObject.Parse(Request.Form["materialDetails"]);

            var folderName = Path.Combine("Resources", "Materials");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            var fullPath = Path.Combine(pathToSave, file.FileName);

            if (file.Length > 0)
            {
                var dbPath = Path.Combine(folderName, file.FileName);

                using (var stream = System.IO.File.Create(fullPath))
                {
                    file.CopyTo(stream);
                }

                string materialName = detailsDecode["name"].ToString();
                string materialInformations = detailsDecode["informations"].ToString();
                string materialChemicalComposition = detailsDecode["chemicalComposition"].ToString();

                var material = new CreateMaterialRequest{
                    Name = materialName,
                    MaterialPhoto = dbPath,
                    AdditionalInformations = materialInformations,
                    ChemicalComposition = materialChemicalComposition
                };
                materialRepository.Create(material.returnMaterial());
            }

            return Ok("Added succesfully!");
        }
    }
}