using ExperimentToolApi.Interfaces;
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
            var detailsDecode = JObject.Parse(Request.Form["materialDetails"]);

            string materialName = detailsDecode["name"].ToString();
            string materialDescription = detailsDecode["description"].ToString();
            string materialChemicalComposition = detailsDecode["chemicalComposition"].ToString();

            return Ok();
        }
    }
}