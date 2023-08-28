
using Reflectis.SDK.Utilities.Extensions;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Reflectis.SDK.ClientModels
{
    [Serializable]
    public class CMResource
    {
        [SerializeField] private int id;
        [SerializeField] private string name;
        [SerializeField] private int sizeBytes;
        [SerializeField] private string path;
        [SerializeField] private string thumbnailPath;
        [SerializeField] private int type;
        [SerializeField] private DateTime creationDate;

        public int ID { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int SizeBytes => sizeBytes;

        public string SizeBytesString
        {
            get
            {
                const int gigabyte = 1024 * 1024 * 1024;
                const int megabyte = 1024 * 1024;
                const int kilobyte = 1024;

                if (SizeBytes >= gigabyte)
                {
                    int value = SizeBytes / gigabyte;
                    return $"{value:F2} GB";
                }
                else if (sizeBytes >= megabyte)
                {
                    double value = SizeBytes / megabyte;
                    return $"{value:F2} MB";
                }
                else if (SizeBytes >= kilobyte)
                {
                    double value = SizeBytes / kilobyte;
                    return $"{value:F2} KB";
                }
                else
                {
                    return $"{SizeBytes} bytes";
                }
            }
        }
        public string Path { get => path; set => path = value; }
        public string ThumbnailPath => thumbnailPath?.Replace(" ", "%20");
        public int Type { get => type; set => type = value; }
        public DateTime CreationDate { get => creationDate; set => creationDate = value; }

        public async Task<Sprite> GetThumbnail()
        {
            string thumbnailUri = ThumbnailPath;
            if (!string.IsNullOrEmpty(thumbnailUri))
            {
                using var dh = new DownloadHandlerTexture();
                using var www = new UnityWebRequest(thumbnailUri, UnityWebRequest.kHttpVerbGET, dh, null);
                //www.certificateHandler = new AcceptAllCertificates();
                await www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.Success)
                {
                    var rect = new Rect(0, 0, dh.texture.width, dh.texture.height);
                    return Sprite.Create(dh.texture, rect, new Vector2(0.5f, 0.5f));
                }
            }
            return null;
        }
        public CMResource(int id, string name, int sizeBytes, string path, string thumbnailPath, int type, DateTime creationDate)
        {
            ID = id;
            Name = name;
            this.sizeBytes = sizeBytes;
            Path = path;
            this.thumbnailPath = thumbnailPath;
            Type = type;
            CreationDate = creationDate;
        }

    }
}
