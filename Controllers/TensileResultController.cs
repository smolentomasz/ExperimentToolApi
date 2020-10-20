using System;
using System.Globalization;
using ExperimentToolApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
            string[] lines = System.IO.File.ReadAllLines(@"Secure\Rozc_PRETY_Stal_EXT_CO_27-03-20181.TXT");

            for (int i = 0; i < lines.Length; i++)
            {
                if (i % 2 == 0 && i >= 10)
                {
                    lines[i] = lines[i].Replace("\t", " ");
                    string[] values = lines[i].Split(" ");
                    decimal newNumber = Decimal.Parse(values[0], NumberStyles.Float);
                    Console.WriteLine(newNumber);
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