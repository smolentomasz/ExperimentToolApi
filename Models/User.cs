using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ExperimentToolApi.Models
{
    [DataContract]
    public class User
    {
        [Key]
        [Required]
        [DataMember]
        public int Id {get;set;}
        [Required]
        [DataMember]
        public string Username {get;set;}
        [Required]
        public string Password {get;set;}
        public string RefreshToken { get; set; }
    }
}