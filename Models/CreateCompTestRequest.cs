namespace ExperimentToolApi.Models
{
    public class CreateCompTestRequest
    {
        public int materialId {get;set;}
        public string title {get;set;}
        public string description {get;set;}
        public string company {get;set;}
        public string testStandard {get;set;}
        public string machineInfo {get;set;}
        public decimal initalForce {get;set;}
        public decimal youngModuleSpeed {get;set;}
        public decimal testSpeed {get;set;}

        public CreateCompTestRequest(){

        }
        public CompressionTest returnTest(){
            var compressionTest = new CompressionTest{
                MaterialId = this.materialId,
                Title = this.title,
                Description = this.description,
                Company = this.company,
                TestStandard = this.testStandard,
                MachineInfo = this.machineInfo,
                InitialForce = this.initalForce,
                YoungModuleSpeed = this.youngModuleSpeed,
                TestSpeed = this.testSpeed
            };
            return compressionTest;
        }

    }
}