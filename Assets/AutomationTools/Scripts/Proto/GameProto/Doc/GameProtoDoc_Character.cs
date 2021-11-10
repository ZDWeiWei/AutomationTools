using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.UIGameProto {
    public class GameProtoDoc_Character {
        public struct OnMoveW : IGameProtoDoc {
            public const string ID = "1_1";
            public bool isDown;
            public string GetID() {
                return ID;
            }
        }
        public struct OnMoveS : IGameProtoDoc {
            public const string ID = "1_2";
            public bool isDown;
            public string GetID() {
                return ID;
            }
        }
        public struct OnMoveA : IGameProtoDoc {
            public const string ID = "1_3";
            public bool isDown;
            public string GetID() {
                return ID;
            }
        }
        public struct OnMoveD : IGameProtoDoc {
            public const string ID = "1_4";
            public bool isDown;
            public string GetID() {
                return ID;
            }
        }
        public struct OnJump : IGameProtoDoc {
            public const string ID = "1_5";
            public bool isDown;
            public string GetID() {
                return ID;
            }
        }
        public struct OnFire : IGameProtoDoc {
            public const string ID = "1_6";
            public bool isDown;
            public string GetID() {
                return ID;
            }
        }
        
        public struct OpenCharacterUI : IGameProtoDoc {
            public const string ID = "1_7";
            public string GetID() {
                return ID;
            }
        }
    }
}
