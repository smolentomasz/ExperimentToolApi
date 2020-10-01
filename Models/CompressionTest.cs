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
        public double InitialForce {get;set;}
        [Required]
        [DataMember]
        public double YoungModuleSpeed {get;set;}
        [Required]
        [DataMember]
        public double TestSpeed {get;set;}
        [Required]
        public ICollection<CompressionResult> CompressionResults {get; set;}
    }
}