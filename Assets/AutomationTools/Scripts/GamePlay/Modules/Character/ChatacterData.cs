using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public class ChatacterData {
        public bool IsLocalRole {
            get;
            private set;
        }
        public CharacterEntity Entity {
            get;
            private set;
        }
        public float MoveH {
            get;
            private set;
        }
        public float MoveV {
            get;
            private set;
        }

        public void SetIsLocalRole(bool isLocalRole) {
            IsLocalRole = isLocalRole;
        }

        public void SetEntity(CharacterEntity entity) {
            Entity = entity;
        }

        public void SetMoveH(float moveH) {
            MoveH = moveH;
        }

        public void SetMoveV(float moveV) {
            MoveV = moveV;
        }
    }
}