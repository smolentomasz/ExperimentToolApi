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
        public decimal RelativeReduction {get;set;}
        [Required]
        [DataMember]
        public decimal StandardForce {get;set;}
        [Required]
        [DataMember]
        public decimal PlasticRelativeReduction {get;set;}
        [Required]
        [DataMember]
        public decimal XCorrectRelativeReduction {get;set;}
    }
}