using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sofunny.Tools.AutomationTools.UIGameProto {
    public class GameProtoDoc_Stage {
        public struct LoadStage : IGameProtoDoc {
            public const string ID = "2_1";
            public string sign;
            public LoadSceneMode mode;
            public string GetID() {
                return ID;
            }
        }
        
        public struct LoadStageComponent : IGameProtoDoc {
            public const string ID = "2_3";
            public string sign;
            public string GetID() {
                return ID;
            }
        }
    }
}
