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
        public decimal L0 {get;set;}
        public decimal Lu {get;set;}
        public decimal Lc {get;set;}

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
                XCorrectElongation = this.XCorrectElongation,
                L0 = this.L0,
                Lu = this.Lu,
                Lc = this.Lc
            };
            return tensileResult;
        }

    }
}