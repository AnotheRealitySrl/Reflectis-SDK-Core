public class NetworkStateAsyncOperation
{
    public enum EStatusCode
    {
        InProgress = -1,
        Success = 0,
        Disconnected = 1,
        JoinShardFailed = 2,
        ShardFull = 3 // Pun error code
    }

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
        StatusCode = EStatusCode.InProgress;
        ErrorMessage = string.Empty;
    }

    public Status OpStatus { get; set; }
    public bool? Success { get; set; }
    public EStatusCode StatusCode { get; set; }
    public string ErrorMessage { get; set; }

    public void Abort()
    {
        OpStatus = Status.Aborted;
        Success = false;
    }

}