using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.Asset;
using Sofunny.Tools.AutomationTools.UIGameProto;
using Sofunny.Tools.AutomationTools.Util;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public class CharacterManager : IGameWorld {
        private List<CharacterEntity> roles = new List<CharacterEntity>();
        public void Init() {
            AddRole(true);
            ATUpdateRegister.AddUpdate(OnUpdate);
            GameProtoManager.Send(new GameProtoDoc_Character.OpenCharacterUI());
        }
        
        public void Clear() {
            ATUpdateRegister.RemoveUpdate(OnUpdate);
            RemoveAllRole();
        }

        public void OnUpdate(float delta) {
        }

        public void AddRole(bool isLocalRole) {
            var request = AssetManager.LoadGamePlayRole();
            request.completed += operation => {
                var prefab = (GameObject) request.asset;
                if (prefab != null) {
                    var roleGameObj = GameObject.Instantiate(prefab);
                    var entity = roleGameObj.GetComponent<CharacterEntity>();
                    entity.Init(isLocalRole);
                    roles.Add(entity);
                } else {
                    Debug.LogError("GamePlayRole 加载失败:");
                }
            };
        }

        public void RemoveAllRole() {
            for (int i = 0; i < roles.Count; i++) {
                var role = roles[i];
                role.Clear();
                GameObject.Destroy(role.gameObject);
            }
            roles.Clear();
        }
    }
}
