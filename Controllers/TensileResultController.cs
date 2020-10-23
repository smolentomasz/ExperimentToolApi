using System;
using System.Globalization;
using System.IO;
using ExperimentToolApi.Interfaces;
using ExperimentToolApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace ExperimentToolApi.Controllers
{
    public class TensileResultController: ControllerBase
    {
        private readonly ITensileResultRepository tensileResultRepository;
        private readonly ITensileTestRepository tensileTestRepository;
        public TensileResultController(ITensileResultRepository tensileResultRepository, ITensileTestRepository tensileTestRepository){
            this.tensileResultRepository = tensileResultRepository;
            this.tensileTestRepository = tensileTestRepository;
        }
        [HttpPost("/tool/tensile-results"), DisableRequestSizeLimit]
        public IActionResult AddNewResults()
        {
            var file = Request.Form.Files[0];
            var detailsDecode = JObject.Parse(Request.Form["resultDetails"]);

            var folderName = Path.Combine("Resources", "Results", "Tensile");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            var fullPath = Path.Combine(pathToSave, file.FileName);

            if (file.Length > 0)
            {
                var dbPath = Path.Combine(folderName, file.FileName);

                using (var stream = System.IO.File.Create(fullPath))
                {
                    file.CopyTo(stream);
                }
            }

            string[] lines = System.IO.File.ReadAllLines(fullPath);

            for (int i = 0; i < lines.Length; i++)
            {
                if (i % 2 == 0 && i >= 10)
                {
                    lines[i] = lines[i].Replace("\t", " ");
                    string[] values = lines[i].Split(" ");
                    decimal newNumber = Decimal.Parse(values[0], NumberStyles.Float);
                    var resultLine = new CreateTensResultRequest{
                        TensileTestId = Int32.Parse(detailsDecode["testId"].ToString()),
                        AttemptNumber = Int32.Parse(detailsDecode["attemptNumber"].ToString()),
                        Elongation = Decimal.Parse(values[0], NumberStyles.Float),
                        StandardForce = Decimal.Parse(values[1], NumberStyles.Float),
                        TrueStress = Decimal.Parse(values[2], NumberStyles.Float),
                        PlasticElongation = Decimal.Parse(values[3], NumberStyles.Float),
                        XCorrectElongation = Decimal.Parse(values[4], NumberStyles.Float)
                    };
                    tensileResultRepository.Create(resultLine.returnResult());
                }
            }
            return Ok("Added succesfully!");
        }
        [HttpGet("/tool/tensile-results/{testId}")]
        public IActionResult GetResultsByTest(int testId)
        {
            if (tensileTestRepository.isTestPresent(testId))
            {
                if(tensileResultRepository.isResultForTestPresent(testId)){
                    return Ok(tensileResultRepository.GetListByTest(testId));
                }
                else{
                    return Conflict("Results don't exist for this test.");
                }
            }
            else
            {
                return Conflict("Tensile test with this id doesn't exist in database!");
            }
        }
        [HttpGet("/tool/tensile-results/{testId}/{attemptNumber}")]
        public IActionResult GetAttemptResultsByTest(int testId, int attemptNumber)
        {
            if (tensileTestRepository.isTestPresent(testId))
            {
                if(tensileResultRepository.isAttemptForTestPresent(attemptNumber, testId)){
                    return Ok(tensileResultRepository.GetListByAttempt(attemptNumber, testId));
                }
                else{
                    return Conflict("Attempt don't exist for this test.");
                }
            }
            else
            {
                return Conflict("Compression test with this id doesn't exist in database!");
            }
        }  
    }
}