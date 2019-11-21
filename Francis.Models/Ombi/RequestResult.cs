namespace Francis.Models.Ombi
{
    public class RequestResult
    {
        public bool Result { get; set; }
        public string Message { get; set; }
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
        public long RequestId { get; set; }
    }
}
