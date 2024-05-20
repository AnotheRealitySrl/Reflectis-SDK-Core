using Reflectis.SDK.ClientModels;
using Reflectis.SDK.Utilities;

using System.Collections.Generic;

namespace Reflectis.SDK.ApplicationManagement
{
    public interface IReflectisApplicationManager : IApplicationManager
    {
        public static new IReflectisApplicationManager Instance;

        #region Events
        public CMEvent CurrentEvent { get; }
        #endregion

        #region Shards
        public CMShard CurrentShard { get; }
        #endregion

        #region Worlds

        List<CMWorld> Worlds { get; }
        CMWorld CurrentWorld { get; }

        #endregion

        #region Permissions
        List<CMPermission> CurrentEventPermissions { get; }
        List<CMPermission> WorldPermissions { get; }
        #endregion

        #region Facets
        public List<CMFacet> WorldFacets { get; }
        #endregion

        public bool ShowFullNickname { get; }

        #region Users
        CMUser UserData { get; }
        #endregion

        #region OnlinePresence

        float UsersRefreshRateSeconds { get; }

        float PlayerPingRateSeconds { get; }
        #endregion
    }

}
