using System;

using UnityEngine;

namespace Reflectis.SDK.ClientModels
{
    public enum EFacetIdentifier
    {
        Unknown = 0,
        MuteOthers = 1,
        KickOthers = 2,
        WriteTool = 3,
        LaserPointerTool = 4,
        BoardTool = 5,
        WebViewTool = 6,
        Asset3DSpawn = 7,
        VideoSpawn = 8,
        ImageSpawn = 9,
        DocSpawn = 10,
        MuteAll = 11,
    }

    [Serializable]
    public class CMFacet
    {
        [SerializeField] private int id;
        [SerializeField] private EFacetIdentifier identifier;
        [SerializeField] private string label;

        public int Id { get => id; set => id = value; }
        public EFacetIdentifier Identifier { get => identifier; set => identifier = value; }
        public string Label { get => label; set => label = value; }
    }
}
