using UnityEngine;

namespace Reflectis.SDK.ClientModels
{
    public class CMWorldConfig : MonoBehaviour
    {
        [SerializeField] private string videoChatAppId;
        [SerializeField] private int maxShardCapacity = 20;

        public string VideoChatAppId { get => videoChatAppId; set => videoChatAppId = value; }
        public int MaxShardCapacity { get => maxShardCapacity; set => maxShardCapacity = value; }

    }
}
