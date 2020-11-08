using ExperimentToolApi.Interfaces;
using ExperimentToolApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExperimentToolApi.Controllers
{
    public class TensileTestController : ControllerBase
    {
        private readonly ITensileTestRepository tensileTestRepository;
        private readonly IMaterialRepository materialRepository;

        public TensileTestController(ITensileTestRepository tensileTestRepository, IMaterialRepository materialRepository)
        {
            this.tensileTestRepository = tensileTestRepository;
            this.materialRepository = materialRepository;
        }
        [HttpGet("/tool/tensile-tests")]
        public IActionResult GetList()
        {
            return Ok(tensileTestRepository.GetList());
        }
        [HttpPost("/tool/tensile-tests")]
        public IActionResult AddNewTest([FromBody] CreateTensTestRequest newTest)
        {
            if (newTest.materialId.Equals("") || newTest.title.Equals("") ||
                newTest.description.Equals("") || newTest.company.Equals("") ||
                newTest.machineInfo.Equals("") || newTest.youngModuleSpeed.Equals("") ||
                newTest.initalForce.Equals("") || newTest.testStandard.Equals("") ||
                newTest.testSpeed.Equals(""))
            {
                return BadRequest(new ApiResponse("Missing or invalid data"));
            }
            else
            {
                if (materialRepository.isMaterialPresent(newTest.materialId))
                {
                    return Ok(tensileTestRepository.Create(newTest.returnTest()));
                }
                else
                {
                    return NotFound(new ApiResponse("Material with this id doesn't exist."));
                }

            }
        }
        [HttpGet("/tool/tensile-tests/{testId}")]
        public IActionResult GetTestById(int testId)
        {
            if (tensileTestRepository.isTestPresent(testId))
                return Ok(tensileTestRepository.GetById(testId));
            else
                return Conflict(new ApiResponse("Compression test with this id doesn't exist in database!"));
        }
    }
}