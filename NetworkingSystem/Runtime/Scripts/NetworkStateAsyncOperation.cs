public class NetworkStateAsyncOperation
{
    public enum Status
    {
        Started,
        Aborted,
        Completed
    }

    public NetworkStateAsyncOperation()
    {
        OpStatus = Status.Started;
        Success = null;
        StatusCode = 0;
        ErrorMessage = string.Empty;
    }

    public Status OpStatus { get; set; }
    public bool? Success { get; set; }
    public int StatusCode { get; set; }
    public string ErrorMessage { get; set; }

    public void Abort()
    {
        OpStatus = Status.Aborted;
        Success = false;
    }

}