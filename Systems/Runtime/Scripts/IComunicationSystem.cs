using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SPACS.Core;

namespace SPACS.SDK.Systems
{
    public interface IComunicationSystem : ISystem
    {
        void ConnectToRoom(Room _room);
    }

    public struct Room
    {
        public string Name;
    }
}

