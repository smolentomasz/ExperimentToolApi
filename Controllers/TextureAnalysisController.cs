using System.Text;
using System;
using System.Drawing;
using System.IO;
using ExperimentToolApi.Interfaces;
using ExperimentToolApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace ExperimentToolApi.Controllers
{
    public class TextureAnalysisController : ControllerBase
    {
        private readonly ITextureAnalysisRepository textureAnalysisRepository;
        private readonly IMaterialRepository materialRepository;
        public TextureAnalysisController(ITextureAnalysisRepository textureAnalysisRepository, IMaterialRepository materialRepository)
        {
            this.textureAnalysisRepository = textureAnalysisRepository;
            this.materialRepository = materialRepository;
        }
        [HttpGet("/tool/analyses")]
        public IActionResult GetList()
        {
            return Ok(textureAnalysisRepository.GetList());
        }
        [HttpGet("/tool/analyses/{materialId}")]
        public IActionResult GetAnalysisByMaterialId(int materialId)
        {
            if (materialRepository.isMaterialPresent(materialId))
            {
                return Ok(textureAnalysisRepository.GetAnalysisByMaterialId(materialId));
            }
            else
            {
                return Conflict(new ApiResponse("Material with this id doesn't exist in database!"));
            }
        }
        [HttpPost("/tool/analyses"), DisableRequestSizeLimit]
        public IActionResult AddNewAnalyse()
        {
            var file = Request.Form.Files[0];
            var detailsDecode = JObject.Parse(Request.Form["analysisDetails"]);

            var folderName = Path.Combine("Resources", "Textures");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            var fullPath = Path.Combine(pathToSave, file.FileName);

            string[] fileNameParts = file.FileName.Split(".");
        
            if (file.Length > 0)
            {                
                using (var stream = System.IO.File.Create(fullPath))
                {
                    file.CopyTo(stream);
                }

                var dbPath = "";

                if(Path.GetExtension(file.FileName).Equals(".tif")){
                    StringBuilder newStringBuilder = new StringBuilder();
                    newStringBuilder.Append(fileNameParts[0]);
                    newStringBuilder.Append(".jpg");
                    
                    var changedPath = Path.Combine(pathToSave, newStringBuilder.ToString());
                    dbPath = Path.Combine(folderName, newStringBuilder.ToString());
                    Bitmap.FromFile(fullPath).Save(changedPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                else{
                    dbPath = Path.Combine(folderName, file.FileName);
                }

                string materialId = detailsDecode["materialId"].ToString();
                string textureDescription = detailsDecode["textureDescription"].ToString();
                
                var textureAnalysis = new CreateTextureRequest{
                   MaterialId = Int32.Parse(materialId),
                   EbsdPhoto = dbPath,
                   EbsdDescription = textureDescription
                };
                textureAnalysisRepository.Create(textureAnalysis.returnAnalysis());
            }

            return Ok(new ApiResponse("Added succesfully!"));
        }
    }
}