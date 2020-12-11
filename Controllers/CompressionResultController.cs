using System.IO;
using System;
using System.Globalization;
using ExperimentToolApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ExperimentToolApi.Models;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

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
            if (Path.GetExtension(Request.Form.Files[0].FileName) != ".TXT")
            {
                return Ok(new ApiResponse("Missing or invalid data!"));
            }
            else
            {
                if (String.IsNullOrEmpty(Request.Form["compressionResultDetails"]))
                {
                    return BadRequest(new ApiResponse("Missing or invalid data!"));
                }
                else
                {
                    var file = Request.Form.Files[0];
                    var detailsDecode = JObject.Parse(Request.Form["compressionResultDetails"]);

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
                    string[] values;

                    List<CompressionResult> resultsList = new List<CompressionResult>();

                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (i >= 12 && lines[i] != "")
                        {
                            lines[i] = lines[i].Replace("\t", " ");
                            values = lines[i].Split(" ");

                            var resultLine = new CreateCompResultRequest
                            {
                                CompressionTestId = Int32.Parse(detailsDecode["testId"].ToString()),
                                AttemptNumber = Int32.Parse(detailsDecode["attemptNumber"].ToString()),
                                RelativeReduction = Decimal.Parse(values[0], NumberStyles.Float),
                                StandardForce = Decimal.Parse(values[1], NumberStyles.Float),
                                PlasticRelativeReduction = Decimal.Parse(values[2], NumberStyles.Float),
                                XCorrectRelativeReduction = Decimal.Parse(values[3], NumberStyles.Float),
                                D0 = Decimal.Parse(detailsDecode["d0"].ToString(), NumberStyles.Float),
                                H0 = Decimal.Parse(detailsDecode["h0"].ToString(), NumberStyles.Float),
                                S0 = Decimal.Parse(detailsDecode["s0"].ToString(), NumberStyles.Float)
                            };

                            if (i >= 12 && i <= 30)
                            {
                                Console.WriteLine(resultLine.RelativeReduction);
                            }
                            resultsList.Add(resultLine.returnResult());
                            Array.Clear(values, 0, values.Length);

                        }
                    }

                    resultsList.Sort((value1, value2) => value1.Id.CompareTo(value2.Id));

                    compressionResultRepository.Create(resultsList);
                    System.IO.File.Delete(fullPath);
                    return Ok(new ApiResponse("Added compression test result succesfully!"));
                }
            }
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
        [HttpPost("/tool/compression-results/getResults")]
        public IActionResult GetAttemptResultsByTest()
        {
           if (String.IsNullOrEmpty(Request.Form["requestRecords"]))
            {
                return BadRequest(new ApiResponse("Missing or invalid data!"));
            }
            else
            {
                var decodedData = JArray.Parse(Request.Form["requestRecords"]);
                List<RecordRequest> recordsList = decodedData.ToObject<List<RecordRequest>>();

                List<RecordCompressionResponse> resultResponseList = new List<RecordCompressionResponse>();


                foreach (RecordRequest record in recordsList)
                {
                    if (compressionTestRepository.isTestPresent(record.testId))
                    {
                        if (compressionResultRepository.isAttemptForTestPresent(record.attemptNumber, record.testId))
                        {
                            var newResultRecords = new RecordCompressionResponse
                            {
                                attemptNumber = record.attemptNumber,
                                testResult = compressionResultRepository.GetListByAttempt(record.attemptNumber, record.testId)
                            };
                            resultResponseList.Add(newResultRecords);
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
                return Ok(resultResponseList);
            }
        }

    }
}