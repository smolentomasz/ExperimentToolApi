using System.Collections.Generic;

namespace ExperimentToolApi.Models
{
    public class RecordTensileResponse
    {
        public int attemptNumber {get;set;}
        public List<TensileResult> testResult {get;set;}

        public RecordTensileResponse(){

        }
    }
}