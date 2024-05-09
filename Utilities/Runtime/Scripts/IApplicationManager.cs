namespace Reflectis.SDK.Utilities
{
    public interface IApplicationManager
    {
        public static IApplicationManager Instance { get; protected set; }

        public void QuitApplication();

        public void ErasePlayerSessionData();

        //public void HideEverything();
    }
}
