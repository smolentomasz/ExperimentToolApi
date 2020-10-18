using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ExperimentToolApi.Models
{
    [DataContract]
    public class TensileResult
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
        public TensileTest TensileTest {get;set;}
        [Required]
        [DataMember]
        public int AttemptNumber {get;set;}
        [Required]
        [DataMember]
        public decimal Elongation {get;set;}
        [Required]
        [DataMember]
        public decimal StandardForce {get;set;}
        [Required]
        [DataMember]
        public decimal TrueStress {get;set;}
        [Required]
        [DataMember]
        public decimal PlasticElongation {get;set;}
        [Required]
        [DataMember]
        public decimal XCorrectElongation {get;set;}

    }
}