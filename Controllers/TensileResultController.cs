using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using ExperimentToolApi.Interfaces;
using ExperimentToolApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace ExperimentToolApi.Controllers
{
    public class TensileResultController : ControllerBase
    {
        private readonly ITensileResultRepository tensileResultRepository;
        private readonly ITensileTestRepository tensileTestRepository;
        public TensileResultController(ITensileResultRepository tensileResultRepository, ITensileTestRepository tensileTestRepository)
        {
            this.tensileResultRepository = tensileResultRepository;
            this.tensileTestRepository = tensileTestRepository;
        }
        [Authorize]
        [HttpPost("/tool/tensile-results"), DisableRequestSizeLimit]
        public IActionResult AddNewResults()
        {
            if (Path.GetExtension(Request.Form.Files[0].FileName) != ".TXT")
            {
                return BadRequest(new ApiResponse("Missing or invalid data! - file"));
            }
            else
            {
                if (String.IsNullOrEmpty(Request.Form["tensileResultDetails"]))
                {
                    return BadRequest(new ApiResponse("Missing or invalid data!"));
                }
                else
                {
                    var file = Request.Form.Files[0];
                    var detailsDecode = JObject.Parse(Request.Form["tensileResultDetails"]);

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

                    List<TensileResult> resultsList = new List<TensileResult>();

                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (i >= 10 && lines[i] != "")
                        {
                            lines[i] = lines[i].Replace("\t", " ");
                            values = lines[i].Split(" ");

                            var resultLine = new CreateTensResultRequest
                            {
                                TensileTestId = Int32.Parse(detailsDecode["testId"].ToString()),
                                AttemptNumber = Int32.Parse(detailsDecode["attemptNumber"].ToString()),
                                Elongation = Decimal.Parse(values[0], NumberStyles.Float),
                                StandardForce = Decimal.Parse(values[1], NumberStyles.Float),
                                TrueStress = Decimal.Parse(values[2], NumberStyles.Float),
                                PlasticElongation = Decimal.Parse(values[3], NumberStyles.Float),
                                XCorrectElongation = Decimal.Parse(values[4], NumberStyles.Float),
                                L0 = Decimal.Parse(detailsDecode["l0"].ToString(), NumberStyles.Float),
                                Lu = Decimal.Parse(detailsDecode["lu"].ToString(), NumberStyles.Float),
                                Lc = Decimal.Parse(detailsDecode["lc"].ToString(), NumberStyles.Float),
                            };
                            resultsList.Add(resultLine.returnResult());

                            Array.Clear(values, 0, values.Length);
                        }
                    }

                    resultsList.Sort((value1, value2) => value1.Id.CompareTo(value2.Id));

                    tensileResultRepository.Create(resultsList);
                    System.IO.File.Delete(fullPath);
                    return Ok(new ApiResponse("Added tensile test results succesfully!"));
                }
            }

        }
        [HttpGet("/tool/tensile-results/{testId}")]
        public IActionResult GetResultsByTest(int testId)
        {
            if (tensileTestRepository.isTestPresent(testId))
            {
                if (tensileResultRepository.isResultForTestPresent(testId))
                {
                    return Ok(tensileResultRepository.GetListByTest(testId));
                }
                else
                {
                    return Conflict(new ApiResponse("Results don't exist for this test."));
                }
            }
            else
            {
                return Conflict(new ApiResponse("Tensile test with this id doesn't exist in database!"));
            }
        }
        [HttpPost("/tool/tensile-results/getResults")]
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

                List<RecordTensileResponse> resultResponseList = new List<RecordTensileResponse>();


                foreach (RecordRequest record in recordsList)
                {
                    if (tensileTestRepository.isTestPresent(record.testId))
                    {
                        if (tensileResultRepository.isAttemptForTestPresent(record.attemptNumber, record.testId))
                        {
                            var newResultRecords = new RecordTensileResponse
                            {
                                attemptNumber = record.attemptNumber,
                                testResult = tensileResultRepository.GetListByAttempt(record.attemptNumber, record.testId)
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