namespace ExperimentToolApi.Models
{
    public class CreateCompResultRequest
    {
        public int CompressionTestId {get;set;}
        public int AttemptNumber {get;set;}
        public decimal RelativeReduction {get;set;}
        public decimal StandardForce {get;set;}
        public decimal PlasticRelativeReduction {get;set;}
        public decimal XCorrectRelativeReduction {get;set;}
        public decimal D0 {get;set;}
        public decimal H0 {get;set;}
        public decimal S0 {get;set;}

        public CreateCompResultRequest(){}

        public CompressionResult returnResult(){
            var compressionResult = new CompressionResult{
                CompressionTestId = this.CompressionTestId,
                AttemptNumber = this.AttemptNumber,
                RelativeReduction = this.RelativeReduction,
                StandardForce = this.StandardForce,
                PlasticRelativeReduction = this.PlasticRelativeReduction,
                XCorrectRelativeReduction = this.XCorrectRelativeReduction,
                D0 = this.D0,
                H0 = this.H0,
                S0 = this.S0
            };
            return compressionResult;
        }
    }
}