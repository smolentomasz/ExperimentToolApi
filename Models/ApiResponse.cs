namespace ExperimentToolApi.Models
{
    public class ApiResponse
    {
        public string responseMessage {get;set;}

        public ApiResponse(string responseMessage){
            this.responseMessage = responseMessage;
        }
    }
}