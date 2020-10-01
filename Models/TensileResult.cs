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
        public double Elongation {get;set;}
        [Required]
        [DataMember]
        public double StandardForce {get;set;}
        [Required]
        [DataMember]
        public double TrueStress {get;set;}
        [Required]
        [DataMember]
        public double PlasticElongation {get;set;}
        [Required]
        [DataMember]
        public double XCorrectElongation {get;set;}

    }
}