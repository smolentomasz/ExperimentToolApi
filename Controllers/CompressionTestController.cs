using System;
using System.Globalization;
using ExperimentToolApi.Interfaces;
using ExperimentToolApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExperimentToolApi.Controllers
{
    public class CompressionTestController : ControllerBase
    {
        private readonly ICompressionTestRepository compressionTestRepository;
        private readonly IMaterialRepository materialRepository;
        public CompressionTestController(ICompressionTestRepository compressionTestRepository, IMaterialRepository materialRepository)
        {
            this.compressionTestRepository = compressionTestRepository;
            this.materialRepository = materialRepository;

        }
        [HttpGet("/tool/compression-tests")]
        public IActionResult GetList()
        {
            return Ok(compressionTestRepository.GetList());
        }
        [HttpPost("/tool/compression-tests")]
        public IActionResult AddNewTest([FromBody] CreateCompTestRequest newTest)
        {
            if (newTest.materialId.Equals("") || newTest.title.Equals("") ||
                newTest.description.Equals("") || newTest.company.Equals("") ||
                newTest.testStandard.Equals("") || newTest.machineInfo.Equals("") ||
                newTest.initalForce.Equals("") || newTest.youngModuleSpeed.Equals("") ||
                newTest.testSpeed.Equals(""))
            {
                return BadRequest("Missing or invalid data");
            }
            else
            {
                if (materialRepository.isMaterialPresent(newTest.materialId))
                {
                    return Ok(compressionTestRepository.Create(newTest.returnTest()));
                }
                else
                {
                    return NotFound("Material with this id doesn't exist.");
                }

            }
        }
        [HttpGet("/tool/compression-tests/{testId}")]
        public IActionResult GetTestById(int testId)
        {
            if (compressionTestRepository.isTestPresent(testId))
                return Ok(compressionTestRepository.GetById(testId));
            else
                return Conflict("Compression test with this id doesn't exist in database!");
        }
    }
}