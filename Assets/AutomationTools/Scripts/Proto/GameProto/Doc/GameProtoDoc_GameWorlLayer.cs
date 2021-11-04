using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.UIGameProto {
    public class GameProtoDoc_GameWorlLayer {
        public struct SetLayer : IGameProtoDoc {
            public const string ID = "4_1";
            public Transform child;
            public byte layer;
            
            public string GetID() {
                return ID;
            }
        }
    }
}
