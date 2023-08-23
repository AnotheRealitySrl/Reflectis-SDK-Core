using System;

using UnityEngine;

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

        public int Id { get => id; set => id = value; }
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
        public FileTypeExt Type { get => (FileTypeExt) type; set => type = (int) value; }
        public DateTime CreationDate { get => creationDate; set => creationDate = value; }

        public CMResource() { }

        public CMResource(int id, string name, int sizeBytes, string path, string thumbnailPath, int type, DateTime creationDate)
        {
            Id = id;
            Name = name;
            this.sizeBytes = sizeBytes;
            Path = path;
            this.thumbnailPath = thumbnailPath;
            this.type = type;
            CreationDate = creationDate;
        }

        public override string ToString()
        {
            return $"ID: {id}\n" +
               $"Name: {name}\n" +
               $"Size (Bytes): {sizeBytes}\n" +
               $"Path: {path}\n" +
               $"Thumbnail Path: {thumbnailPath}\n" +
               $"Type: {type}\n" +
               $"Creation Date: {creationDate}";
        }
    }
}
