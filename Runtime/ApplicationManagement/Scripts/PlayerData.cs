namespace Reflectis.SDK.Core
{
    public class PlayerData
    {
        protected int userId;

        protected string sessionId;

        public int UserId => userId;

        public string SessionId => sessionId;

        public PlayerData(int userId, string sessionId)
        {
            this.userId = userId;
            this.sessionId = sessionId;
        }
    }
}
