using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ExperimentToolApi.Models
{
    public class Material
    {
        [Key]
        [Required]
        [DataMember]
        public int Id {get;set;}
        [Required]
        [DataMember]
        public string Name {get;set;}
        [Required]
        [DataMember]
        public string MaterialPhoto {get;set;}
        [Required]
        [DataMember]
        public string AdditionalInformations {get;set;}
        [Required]
        [DataMember]
        public string ChemicalComposition {get;set;}
        [Required]
        public ICollection<TensileTest> TensileTest {get; set;}
        [Required]
        public ICollection<CompressionTest> CompressionTest {get; set;}
        [Required]
        public TextureAnalysis TextureAnalysis {get; set;}
    }
}