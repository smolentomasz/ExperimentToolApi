using System.Collections.Generic;

namespace ExperimentToolApi.Models
{
    public class RecordCompressionResponse
    {
          public int attemptNumber {get;set;}
        public List<CompressionResult> testResult {get;set;}

        public RecordCompressionResponse(){

        }
    }
}