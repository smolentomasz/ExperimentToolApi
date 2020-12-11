namespace ExperimentToolApi.Models
{
    public class CreateFileRequest
    {
        public string DbPath {get;set;}
        public string Name {get;set;}
        public string ReferenceType {get;set;}
         public string ReferenceTypeName {get;set;}

        public CreateFileRequest(){

        }

        public AdditionalFile returnFile(){
            var file = new AdditionalFile{
                Name = this.Name,
                DbPath = this.DbPath,
                ReferenceType = this.ReferenceType,
                ReferenceTypeName = this.ReferenceTypeName
            };
            return file;
        }
    }
}