namespace ExperimentToolApi.Models
{
    public class CreateCompTestRequest
    {
        public int materialId {get;set;}
        public string title {get;set;}
        public string description {get;set;}
        public string testAuthor {get;set;}
        public string machineInfo {get;set;}
        public decimal initalForce {get;set;}
        public decimal compressionModuleSpeed {get;set;}
       public decimal yeldPointSpeed {get;set;}
        public decimal testSpeed {get;set;}

        public CreateCompTestRequest(){

        }
        public CompressionTest returnTest(){
            var compressionTest = new CompressionTest{
                MaterialId = this.materialId,
                Title = this.title,
                Description = this.description,
                TestAuthor = this.testAuthor,
                MachineInfo = this.machineInfo,
                InitialForce = this.initalForce,
                CompressionModuleSpeed = this.compressionModuleSpeed,
                YeldPointSpeed = this.yeldPointSpeed,
                TestSpeed = this.testSpeed
            };
            return compressionTest;
        }

    }
}