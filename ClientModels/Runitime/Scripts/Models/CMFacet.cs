using System;

using UnityEngine;

namespace Reflectis.SDK.ClientModels
{
    public enum EFacetIdentifier
    {
        MuteOthers,
        KickOthers,
        WriteTool,
        LaserPointerTool,
        BoardTool,
        WebViewTool,
        Asset3DSpawn,
        VideoSpawn,
        ImageSpawn,
        DocSpawn,
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
