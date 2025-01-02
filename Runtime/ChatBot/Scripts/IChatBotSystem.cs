using Reflectis.SDK.Core;

using System.Threading.Tasks;

namespace Reflectis.SDK.ChatBot
{
    public interface IChatBotSystem : ISystem
    {
        string Url { get; set; }
        string ApiKey { get; set; }

        Task StartSessionAsync();
        Task CloseSessionAsync();
        Task UpdateSessionAsync(params object[] settings);
        Task SendTextMessageAsync(string text);
        Task SendAudioMessageAsync(string base64Audio);
    }
}
