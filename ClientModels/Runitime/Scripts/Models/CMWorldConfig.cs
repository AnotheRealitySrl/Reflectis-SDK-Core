using UnityEngine;

namespace Reflectis.SDK.ClientModels
{
    public class CMWorldConfig : MonoBehaviour
    {
        [SerializeField] private string videoChatAppId;
        [SerializeField] private int maxShardCapacity = 20;
        [SerializeField] private int maxCCU;

        public string VideoChatAppId { get => videoChatAppId; set => videoChatAppId = value; }
        public int MaxShardCapacity { get => maxShardCapacity; set => maxShardCapacity = value; }
        public int MaxCCU { get => maxCCU; set => maxCCU = value; }
    }
}
