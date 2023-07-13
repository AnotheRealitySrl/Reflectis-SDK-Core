namespace Reflectis.SDK.Avatars
{
    /// <summary>
    /// Each avatar configuration object must implement this class
    /// </summary>
    public interface IAvatarConfig
    {
        #region Properties
        public float? PlayerHeight { get; }
        #endregion
    }
}
