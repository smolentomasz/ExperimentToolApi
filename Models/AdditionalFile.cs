using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ExperimentToolApi.Models
{
    [DataContract]
    public class AdditionalFile
    {
        [Key]
        [Required]
        [DataMember]
        public int Id {get;set;}
        [Required]
        [DataMember]
        public string DbPath {get;set;}
        [Required]
        [DataMember]
        public string Name {get;set;}
        [Required]
        [DataMember]
        public string Reference {get;set;}
    }
}