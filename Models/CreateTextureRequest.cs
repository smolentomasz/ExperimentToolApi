namespace ExperimentToolApi.Models
{
    public class CreateTextureRequest
    {
        public int MaterialId {get;set;}
        public string EbsdPhoto {get;set;}
        public string EbsdDescription {get;set;}

        public CreateTextureRequest(){

        }

        public TextureAnalysis returnAnalysis(){
            var analysis = new TextureAnalysis{
                MaterialId = this.MaterialId,
                EbsdPhoto = this.EbsdPhoto,
                EbsdDescription = this.EbsdDescription
            };
            return analysis;
        }
    }
}