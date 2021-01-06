using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ExperimentToolApi.Models
{
    public class Material
    {
        [Key]
        [Required]
        public int Id {get;set;}
        [Required]
        public string Name {get;set;}
        [Required]
        public string MaterialPhoto {get;set;}
        [Required]
        public string AdditionalInformations {get;set;}
        [Required]
        public string ChemicalComposition {get;set;}
        [Required]
        public ICollection<TensileTest> TensileTest {get; set;}
        [Required]
        public ICollection<CompressionTest> CompressionTest {get; set;}
        [Required]
        public TextureAnalysis TextureAnalysis {get; set;}
    }
}