using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ExperimentToolApi.Models
{
    [DataContract]
    public class CompressionTest
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
        public string Title {get;set;}
        [Required]
        [DataMember]
        public string Description {get;set;}
        [Required]
        [DataMember]
        public string Company {get;set;}
        [Required]
        [DataMember]
        public string TestStandard {get;set;}
        [Required]
        [DataMember]
        public string MachineInfo {get;set;}
        [Required]
        [DataMember]
        public decimal InitialForce {get;set;}
        [Required]
        [DataMember]
        public decimal YoungModuleSpeed {get;set;}
        [Required]
        [DataMember]
        public decimal TestSpeed {get;set;}
        [Required]
        public ICollection<CompressionResult> CompressionResults {get; set;}
    }
}