using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.Asset;
using Sofunny.Tools.AutomationTools.UIGameProto;
using Sofunny.Tools.AutomationTools.Util;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public class CharacterManager : IGameWorld {
        private List<CharacterSystem> systems = new List<CharacterSystem>();

        public void Register() {
        }

        public void Init() {
            AddCharacterSystem(true);
            GameProtoManager.Send(new GameProtoDoc_Character.OpenCharacterUI());
        }

        public void Clear() {
            RemoveAllRole();
        }

        private void AddCharacterSystem(bool isLocalRole) {
            var system = new CharacterSystem();
            system.Init();
            system.SetIsLocalRole(isLocalRole);
            systems.Add(system);
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