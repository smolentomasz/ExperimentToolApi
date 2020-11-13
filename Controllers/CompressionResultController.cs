using System.IO;
using System;
using System.Globalization;
using ExperimentToolApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ExperimentToolApi.Models;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;

namespace ExperimentToolApi.Controllers
{
    public class CompressionResultController : ControllerBase
    {
        private readonly ICompressionResultRepository compressionResultRepository;
        private readonly ICompressionTestRepository compressionTestRepository;
        public CompressionResultController(ICompressionResultRepository compressionResultRepository, ICompressionTestRepository compressionTestRepository)
        {
            this.compressionResultRepository = compressionResultRepository;
            this.compressionTestRepository = compressionTestRepository;

        }
        [Authorize]
        [HttpPost("/tool/compression-results"), DisableRequestSizeLimit]
        public IActionResult AddNewResults()
        {
            var file = Request.Form.Files[0];
            var detailsDecode = JObject.Parse(Request.Form["resultDetails"]);

            var folderName = Path.Combine("Resources", "Results");
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
                if (i % 2 == 0 && i >= 12)
                {
                    lines[i] = lines[i].Replace("\t", " ");
                    string[] values = lines[i].Split(" ");
                    decimal newNumber = Decimal.Parse(values[0], NumberStyles.Float);
                    var resultLine = new CreateCompResultRequest{
                        CompressionTestId = Int32.Parse(detailsDecode["testId"].ToString()),
                        AttemptNumber = Int32.Parse(detailsDecode["attemptNumber"].ToString()),
                        RelativeReduction = Decimal.Parse(values[0], NumberStyles.Float),
                        StandardForce = Decimal.Parse(values[1], NumberStyles.Float),
                        PlasticRelativeReduction = Decimal.Parse(values[2], NumberStyles.Float),
                        XCorrectRelativeReduction = Decimal.Parse(values[3], NumberStyles.Float)
                    };
                    compressionResultRepository.Create(resultLine.returnResult());
                }
            }
            System.IO.File.Delete(fullPath);
            return Ok(new ApiResponse("Added compression test result succesfully!"));
        }
        [HttpGet("/tool/compression-results/{testId}")]
        public IActionResult GetResultsByTest(int testId)
        {
            if (compressionTestRepository.isTestPresent(testId))
            {
                if (compressionResultRepository.isResultForTestPresent(testId))
                {
                    return Ok(compressionResultRepository.GetListByTest(testId));
                }
                else
                {
                    return Conflict(new ApiResponse("Results don't exist for this test."));
                }
            }
            else
            {
                return Conflict(new ApiResponse("Compression test with this id doesn't exist in database!"));
            }
        }
        [HttpGet("/tool/compression-results/{testId}/{attemptNumber}")]
        public IActionResult GetAttemptResultsByTest(int testId, int attemptNumber)
        {
            if (compressionTestRepository.isTestPresent(testId))
            {
                if (compressionResultRepository.isAttemptForTestPresent(attemptNumber, testId))
                {
                    return Ok(compressionResultRepository.GetListByAttempt(attemptNumber, testId));
                }
                else
                {
                    return Conflict(new ApiResponse("Attempt don't exist for this test."));
                }
            }
            else
            {
                return Conflict(new ApiResponse("Compression test with this id doesn't exist in database!"));
            }
        }

    }
}