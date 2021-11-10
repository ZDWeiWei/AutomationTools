using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.Asset;
using Sofunny.Tools.AutomationTools.UIGameProto;
using Sofunny.Tools.AutomationTools.Util;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public class CharacterManager : ManagerBase {
        private List<CharacterSystem> systems = new List<CharacterSystem>();

        override protected void OnInit() {
            AddCharacterSystem(true);
            GameProtoManager.Send(new GameProtoDoc_Character.OpenCharacterUI());
        }

        override protected void OnClear() {
            RemoveAllRole();
        }

        private void AddCharacterSystem(bool isLocalRole) {
            var system = AddSystem<CharacterSystem>();
            systems.Add(system);
            system.SetIsLocalRole(isLocalRole);
        }

        private void RemoveAllRole() {
            for (int i = 0; i < systems.Count; i++) {
                var role = systems[i];
                role.Clear();
            }
            systems.Clear();
        }
    }
}