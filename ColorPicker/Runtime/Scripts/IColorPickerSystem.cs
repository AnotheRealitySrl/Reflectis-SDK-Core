using Reflectis.SDK.Core;

using System.Threading.Tasks;

using UnityEngine;

public interface IColorPickerSystem : ISystem
{
    Task AssignColorPicker(GameObject obj);

    void AssignColorToPicker(Color assignColor, GameObject obj);
}
