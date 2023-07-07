using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Events;

namespace Reflectis.SDK.Avatars
{
    /// <summary>
    /// Abstract class that represents an avatar loader
    /// </summary>
    public abstract class AvatarLoaderBase : ScriptableObject
    {

        #region Properties
        public abstract IAvatarConfig AvatarConfig { get; }
        #endregion

        #region Unity Callbacks
        /// <summary>
        /// Called when the avatar loading has been completed.
        /// The gameobject will be an avatar and AvatarData will be the avatar data necessary for the configuration
        /// </summary>
        public UnityEvent<GameObject,AvatarData> onLoadingAvatarComplete;
        #endregion

        #region Public methods
        /// <summary>
        /// </summary>
        /// <param name="avatarConfig"></param>
        /// <returns>wheter or not the avatarConfig gives enough data to load the avatar.</returns>
        public abstract bool CheckAvatarConfig(IAvatarConfig avatarConfig);

        /// <summary>
        /// If there is a loading process running returns the completion percentage. Returns 0.0 otherwise
        /// </summary>
        /// <returns></returns>
        public abstract float GetLoadingProgress();

        /// <summary>
        /// Start the loading avatar process
        /// </summary>
        public abstract Task LoadAvatar(IAvatarConfig avatarConfig);

        #endregion
    }
}
