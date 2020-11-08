namespace ExperimentToolApi.Models
{
    public class CreateTensTestRequest
    {
        public int materialId {get;set;}
        public string title {get;set;}
        public string description {get;set;}
        public string company {get;set;}
        public string machineInfo {get;set;}
        public string testStandard {get;set;}
        public decimal initalForce {get;set;}
        public decimal youngModuleSpeed {get;set;}
        public decimal testSpeed {get;set;}

        public CreateTensTestRequest(){

        }
         public TensileTest returnTest(){
            var tensileTest = new TensileTest{
                MaterialId = this.materialId,
                Title = this.title,
                Description = this.description,
                Company = this.company,
                MachineInfo = this.machineInfo,
                TestStandard = this.testStandard,
                InitialForce = this.initalForce,
                YoungModuleSpeed = this.youngModuleSpeed,
                TestSpeed = this.testSpeed
            };
            return tensileTest;
        }
    }
}