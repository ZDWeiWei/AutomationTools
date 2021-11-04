using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.UIGameProto {
    public class GameProtoDoc_Camera {
        public struct SetPoint : IGameProtoDoc {
            public const string ID = "5_1";
            public Vector3 point;
            public string GetID() {
                return ID;
            }
        }
        public struct SetRotation : IGameProtoDoc {
            public const string ID = "5_2";
            public Quaternion rota;
            public string GetID() {
                return ID;
            }
        }
    }
}
