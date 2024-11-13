public class NetworkPlayerData
{
    protected int userId;

    protected int playerId;

    public int UserId => userId;

    public int PlayerId => playerId;

    public NetworkPlayerData(int userId, int playerId)
    {
        this.userId = userId;
        this.playerId = playerId;
    }
}
