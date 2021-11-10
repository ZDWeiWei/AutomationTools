using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.UIGameProto {
    public class GameProtoDoc_Weapon  {
        public struct CreateWeapon : IGameProtoDoc {
            public const string ID = "7_1";
            public string Sign;
            public ProtoCallBack CallBack;
            public string GetID() {
                return ID;
            }
        }
        public struct CreateWeaponCallBackData : IGameProtoCallBackData {
            public int WeaponId;
            public Transform WeaponTran;
        }
        
        public struct Fire : IGameProtoDoc {
            public const string ID = "7_2";
            public int WeaponId;
            public bool IsFire;
            public string GetID() {
                return ID;
            }
        }
    }
}
