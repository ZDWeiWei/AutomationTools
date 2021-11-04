using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.Asset;
using Sofunny.Tools.AutomationTools.UIGameProto;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public partial class CharacterSystem : SystemBase {
        public ChatacterData Data;
        public TransformComponentData TranData;

        protected override void OnInit() {
            base.OnInit();
            Data = new ChatacterData();
            AddComponent<CharacterEntity>();
        }

        protected override void OnClear() {
            base.OnClear();
            Data = null;
            TranData = null;
        }

        public void SetIsLocalRole(bool isLocalRole) {
            Data.SetIsLocalRole(isLocalRole);
            if (isLocalRole) {
                AddComponent<CharacterCamera>();
                AddComponent<CharacterLocalInput>();
                AddComponent<CharacterControllerMove>();
            }
        }
    }
}