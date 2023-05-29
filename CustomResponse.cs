public class CustomResponse{
    public CustomResponse(int statusCode,string message,string details=null)
    {
        StatusCode=statusCode;
        Message=message;
        Details=details;
    }
    public string Message{get;set;}
    public string Details{get;set;}
    public int StatusCode{get;set;}

}