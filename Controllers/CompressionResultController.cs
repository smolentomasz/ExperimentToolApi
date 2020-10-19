using System;
using System.Globalization;
using ExperimentToolApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        [HttpPost("/tool/compression-results"), DisableRequestSizeLimit]
        public IActionResult AddNewResults()
        {
            string[] lines = System.IO.File.ReadAllLines(@"Secure\SCISKANIE_WC_Walcowka_AL_30-03-2018_Xcf0521.TXT");

            for (int i = 0; i < lines.Length; i++)
            {
                if (i % 2 == 0 && i >= 12)
                {
                    lines[i] = lines[i].Replace("\t", " ");
                    string[] values = lines[i].Split(" ");
                    decimal newNumber = Decimal.Parse(values[0], NumberStyles.Float);
                    Console.WriteLine(newNumber);
                }
            }
            return Ok("Added succesfully!");
        }
        [HttpGet("/tool/compression-results/{testId}")]
        public IActionResult GetResultsByTest(int testId)
        {
            if (compressionTestRepository.isTestPresent(testId))
            {
                if(compressionResultRepository.isResultForTestPresent(testId)){
                    return Ok(compressionResultRepository.GetListByTest(testId));
                }
                else{
                    return Conflict("Results don't exist for this test.");
                }
            }
            else
            {
                return Conflict("Compression test with this id doesn't exist in database!");
            }
        }
        
    }
}