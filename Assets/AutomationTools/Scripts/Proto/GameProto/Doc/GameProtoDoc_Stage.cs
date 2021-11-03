using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.UIGameProto {
    public class GameProtoDoc_Stage {
        public struct EnterGame : IGameProtoDoc {
            public const string ID = "2_1";
            public string GetID() {
                return ID;
            }
        }
        public struct EnterGameEnd : IGameProtoDoc {
            public const string ID = "2_2";
            public string GetID() {
                return ID;
            }
        }
    }
}
