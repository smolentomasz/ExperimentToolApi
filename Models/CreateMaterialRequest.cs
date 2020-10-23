namespace ExperimentToolApi.Models
{
    public class CreateMaterialRequest
    {
        public string Name {get;set;}
        public string MaterialPhoto {get;set;}
        public string AdditionalInformations {get;set;}
        public string ChemicalComposition {get;set;}

        public CreateMaterialRequest(){

        }

        public Material returnMaterial(){
            var material = new Material{
                Name = this.Name,
                MaterialPhoto = this.MaterialPhoto,
                AdditionalInformations = this.AdditionalInformations,
                ChemicalComposition = this.ChemicalComposition
            };
            return material;
        }
    }
}