namespace ExperimentToolApi.Models
{
    public class CreateFileRequest
    {
        public string DbPath {get;set;}
        public string Name {get;set;}
        public string Reference {get;set;}

        public CreateFileRequest(){

        }

        public AdditionalFile returnFile(){
            var file = new AdditionalFile{
                Name = this.Name,
                DbPath = this.DbPath,
                Reference = this.Reference
            };
            return file;
        }
    }
}