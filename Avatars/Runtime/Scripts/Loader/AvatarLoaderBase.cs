using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Events;

namespace SPACS.SDK.Avatars
{
    public enum AvatarBodyType
    {
        HalfBody = 0,
        FullBody = 1,
    }

    /// <summary>
    /// Abstract class that represents an avatar loader
    /// </summary>
    public abstract class AvatarLoaderBase : ScriptableObject
    {

        #region Properties
        public abstract IAvatarConfig AvatarConfig { get; protected set; }

        public abstract AvatarBodyType AvatarBody { get; protected set; }
        #endregion

        #region Unity Callbacks
        /// <summary>
        /// Called when the avatar loading has been completed.
        /// The gameobject will be an avatar with attached an Animatar with the avatar rig in the avatar variable
        /// </summary>
        public UnityEvent<GameObject> onLoadingAvatarComplete;
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
        public abstract void LoadAvatar(IAvatarConfig avatarConfig);
        #endregion
    }
}
