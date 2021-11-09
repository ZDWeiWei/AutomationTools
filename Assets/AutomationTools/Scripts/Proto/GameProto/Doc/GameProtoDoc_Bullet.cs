using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.UIGameProto {
    public class GameProtoDoc_Bullet  {
        public struct CreateBullet : IGameProtoDoc {
            public const string ID = "6_1";
            public string Sign;
            public byte BulletType;
            public Vector3 StartPoint;
            public Quaternion StartRota;
            public string GetID() {
                return ID;
            }
        }
        
        public struct RemoveBullet : IGameProtoDoc {
            public const string ID = "6_2";
            public int BulletId;
            public string GetID() {
                return ID;
            }
        }
    }
}
