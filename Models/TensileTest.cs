using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ExperimentToolApi.Models
{
    [DataContract]
    public class TensileTest
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
        public string Material {get;set;}
        [Required]
        [DataMember]
        public string TestAuthor {get;set;}
        [Required]
        [DataMember]
        public string MachineInfo {get;set;}
        [Required]
        [DataMember]
        public double InitialForce {get;set;}
        [Required]
        [DataMember]
        public double CompressionModuleSpeed {get;set;}
        [Required]
        [DataMember]
        public double YeldPointSpeed {get;set;}
        [Required]
        [DataMember]
        public double TestSpeed {get;set;}
        [Required]
        public ICollection<TensileResult> TensileResults {get; set;}
    }
}