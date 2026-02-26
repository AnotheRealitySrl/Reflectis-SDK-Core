using UnityEngine;

namespace Reflectis.SDK.Core
{
    public enum ECameraModes
    {
        DefaultCamera, //default camera, rotation and movement
        StaticCamera, //Give nothing to the user, First person. Camera can't move. 
        RotationCamera, //Can only rotate camera with booleans for rotation -> senti Lucas
        CinemaCamera //Camera movement via cinemachine -> Gestito da utente in first person. Not needed since I can simply enable/disable cameras
    }
}
