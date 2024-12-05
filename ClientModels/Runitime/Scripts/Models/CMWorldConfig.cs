using UnityEngine;

namespace Reflectis.SDK.ClientModels
{
    public class CMWorldConfig : MonoBehaviour
    {

        [SerializeField] private string networkAppId;
        [SerializeField] private string voiceAppId;
        [SerializeField] private string textChatAppId;
        [SerializeField] private string videoChatAppId;
        [SerializeField] private int maxShardCapacity = 20;

        public string NetworkAppId { get => networkAppId; set => networkAppId = value; }
        public string VoiceAppId { get => voiceAppId; set => voiceAppId = value; }
        public string TextChatAppId { get => textChatAppId; set => textChatAppId = value; }
        public string VideoChatAppId { get => videoChatAppId; set => videoChatAppId = value; }
        public int MaxShardCapacity { get => maxShardCapacity; set => maxShardCapacity = value; }
    }
}
