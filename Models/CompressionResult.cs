using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ExperimentToolApi.Models
{
    [DataContract]
    public class CompressionResult
    {
        [Key]
        [Required]
        [DataMember]
        public int Id {get;set;}
        [Required]
        [DataMember]
        public int TestId {get;set;}
        [Required]
        [DataMember]
        public CompressionTest CompressionTest {get;set;}
        [Required]
        [DataMember]
        public int AttemptNumber {get;set;}
        [Required]
        [DataMember]
        public double RelativeReduction {get;set;}
        [Required]
        [DataMember]
        public double StandardForce {get;set;}
        [Required]
        [DataMember]
        public double PlasticRelativeReduction {get;set;}
        [Required]
        [DataMember]
        public double XCorrectRelativeReduction {get;set;}
    }
}