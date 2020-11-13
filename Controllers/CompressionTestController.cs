using System;
using System.Globalization;
using ExperimentToolApi.Interfaces;
using ExperimentToolApi.Models;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        [HttpPost("/tool/compression-tests")]
        public IActionResult AddNewTest([FromBody] CreateCompTestRequest newTest)
        {
            if (newTest.materialId.Equals("") || newTest.title.Equals("") ||
                newTest.description.Equals("") || newTest.testAuthor.Equals("") ||
                newTest.compressionModuleSpeed.Equals("") || newTest.machineInfo.Equals("") ||
                newTest.initalForce.Equals("") || newTest.yeldPointSpeed.Equals("") ||
                newTest.testSpeed.Equals(""))
            {
                return BadRequest(new ApiResponse("Missing or invalid data"));
            }
            else
            {
                if (materialRepository.isMaterialPresent(newTest.materialId))
                {
                    compressionTestRepository.Create(newTest.returnTest());
                   
                    return Ok( new ApiResponse("New compression test added succesfully!"));
                }
                else
                {
                    return NotFound(new ApiResponse("Material with this id doesn't exist."));
                }

            }
        }
        [HttpGet("/tool/compression-tests/{testId}")]
        public IActionResult GetTestById(int testId)
        {
            if (compressionTestRepository.isTestPresent(testId))
                return Ok(compressionTestRepository.GetById(testId));
            else
                return Conflict(new ApiResponse("Compression test with this id doesn't exist in database!"));
        }
    }
}