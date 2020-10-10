using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ExperimentToolApi.Models
{
    public class TextureAnalysis
    {
        [Key]
        [Required]
        [DataMember]
        public int Id {get;set;}
        [Required]
        [DataMember]
        public int MaterialId {get;set;}
        [Required]
        [DataMember]
        public Material Material {get;set;}
        [Required]
        [DataMember]
        public string EbsdPhoto {get;set;}
        [Required]
        [DataMember]
        public string EbsdDescription {get;set;}
    }
}