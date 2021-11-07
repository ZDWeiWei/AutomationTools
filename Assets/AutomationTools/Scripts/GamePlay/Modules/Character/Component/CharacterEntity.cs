using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.Asset;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public class CharacterEntity : EntityBase {
        public const string Root = "Root";
        public const string Body = "Body";

        public bool IsLoadEntityObj {
            get;
            private set;
        }
        
        override public void OnInit() {
            var system = (CharacterSystem) this.system;
            system.Data.SetEntity(this);
            system.CreateEntity(URI.Role);
        }

        public void Clear() {
        }

        public Vector3 TransformDirection(string key, Vector3 dir) {
            var tran = GetTran(key);
            if (tran == null) {
                return Vector3.zero;
            }
            return tran.TransformDirection(dir);
        }

        public CharacterController AddCharacterController() {
            // if (this.entityObj == null) {
            //     return null;
            // }
            // return entityTran.gameObject.AddComponent<CharacterController>();
            return null;
        }
    }
}