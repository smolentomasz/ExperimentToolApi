using System.Runtime.CompilerServices;
namespace ExperimentToolApi.Models
{
    public class CreateTensResultRequest
    {
        public int TensileTestId {get;set;}
        public int AttemptNumber {get;set;}
        public decimal Elongation {get;set;}
        public decimal StandardForce {get;set;}
        public decimal TrueStress {get;set;}
        public decimal PlasticElongation {get;set;}
        public decimal XCorrectElongation {get;set;}

        public CreateTensResultRequest(){

        }

        public TensileResult returnResult(){
            var tensileResult = new TensileResult{
                TensileTestId = this.TensileTestId,
                AttemptNumber = this.AttemptNumber,
                Elongation = this.Elongation,
                StandardForce = this.StandardForce,
                TrueStress = this.TrueStress,
                PlasticElongation = this.PlasticElongation,
                XCorrectElongation = this.XCorrectElongation
            };
            return tensileResult;
        }

    }
}