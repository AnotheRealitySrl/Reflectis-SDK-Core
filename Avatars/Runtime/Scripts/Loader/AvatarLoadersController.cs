using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SPACS.SDK.Avatars
{
    /// <summary>
    /// Class that handles all the possible ways of loading the avatars.
    /// Checks in list order which avatar loader can istantiate the avatar with a given IAvatarConfig and returns that avatar loader
    /// </summary>
    [CreateAssetMenu(fileName = "AvatarLoadersController", menuName = "Avatar/AvatarLoadersController", order = 1)]
    public class AvatarLoadersController : ScriptableObject
    {
        #region Public variables
        [Tooltip("ORDERED List of avatar loaders used to load the avatar")]
        public List<AvatarLoaderBase> avatarLoaders = new List<AvatarLoaderBase>();
        #endregion

        #region Public methods
        public AvatarLoaderBase GetAvatarLoader(IAvatarConfig avatarConfig)
        {
            foreach (AvatarLoaderBase avatarLoader in avatarLoaders)
            {
                if (avatarLoader.CheckAvatarConfig(avatarConfig))
                {
                    return avatarLoader;
                }
            }
            Debug.LogError("No compatible avatar loader found! Probably something is missing in the avatar configuration");
            return null;
        }
        #endregion
    }
}
