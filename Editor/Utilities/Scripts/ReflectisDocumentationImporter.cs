using System.IO;
using System.Linq;
using System.Xml.Linq;

using UnityEditor;

using UnityEngine;

namespace Reflectis.SDK.Core.Utilities.Editor
{
    public static class ReflectisDocumentationImporter
    {
        private static readonly string ReflectisPrefix = "Reflectis";
        private static readonly string packagesPath = "Packages";
        private static readonly string documentationPath = "Documentation~";

        [MenuItem("Reflectis/Import Reflectis documentation")]
        public async static void CreateProjectItem()
        {
            EditorApplication.ExecuteMenuItem("Assets/Open C# Project");

            var csprojs = Directory.GetFiles(Directory.GetCurrentDirectory())
                          .Where(path => path.Contains($"\\{ReflectisPrefix}.") && path.EndsWith(".csproj") && !path.Contains("Player") && !path.Contains("Editor"))
                          .ToList();

            foreach (var csproj in csprojs)
            {
                var doc = XDocument.Load(csproj);

                string fileName = Path.GetFileName(csproj).Replace(".csproj", string.Empty);
                string fileNamePackage = fileName.Replace('.', '-');

                string suffix = fileNamePackage.Split('-')[^1];
                string prefix = fileNamePackage.Replace($"-{suffix}", string.Empty);

                string subdirectories;
                if (Directory.Exists(Path.Combine(packagesPath, fileNamePackage)))
                {
                    subdirectories = fileNamePackage;
                }
                else
                {
                    subdirectories = Path.Combine(prefix, suffix);
                }

                string directoryPath = Path.Combine(packagesPath, subdirectories, documentationPath);
                DirectoryInfo docDirectory = new(directoryPath);
                Debug.Log(directoryPath);
                if (docDirectory.Exists)
                {
                    FileInfo[] docFiles = docDirectory.GetFiles();

                    XElement itemGroup = new("ItemGroup");
                    foreach (var file in docFiles)
                    {
                        XElement none = new("None");
                        none.Add(new XAttribute("Include", Path.Combine(directoryPath, file.Name)));
                        itemGroup.Add(none);
                    }
                    doc.Root.Add(itemGroup);

                    var str = doc.ToString().Replace("<ItemGroup xmlns=\"\">", "<ItemGroup>");

                    using StreamWriter outputFile = new(csproj);
                    await outputFile.WriteAsync(str);
                }
            }
        }
    }
}

