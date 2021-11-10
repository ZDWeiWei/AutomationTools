using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.UIGameProto;
using Sofunny.Tools.AutomationTools.Util;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public class CharacterWeapon : SystemBase.IComponent {
        private CharacterSystem system;
        private List<int> weapons = new List<int>();
        private bool isDown = false;
        private bool isRotaDown = false;
        private bool isFire = false;

        public void Init(SystemBase systemBase) {
            system = (CharacterSystem) systemBase;
            system.Register<bool>(CharacterSystem.Event_Fire, OnFireCallBack);
            system.Register<GameObject>(SystemBase.Event_CreateEntityComplete, OnCreateEntityCompleteCallBack);
            TouchSystem.Register(OnRota);
            ATUpdateRegister.AddUpdate(OnUpdate);
        }

        public void Clear() {
            weapons.Clear();
            ATUpdateRegister.RemoveUpdate(OnUpdate);
            system.UnRegister<GameObject>(SystemBase.Event_CreateEntityComplete, OnCreateEntityCompleteCallBack);
            system.UnRegister<bool>(CharacterSystem.Event_Fire, OnFireCallBack);
            TouchSystem.Unregister(OnRota);
        }

        private void OnCreateEntityCompleteCallBack(GameObject gameObj) {
            GameProtoManager.Send(new GameProtoDoc_Weapon.CreateWeapon {
                Sign = "Wp1",
                CallBack = OnCreateWeapon1CallBack,
            });
            GameProtoManager.Send(new GameProtoDoc_Weapon.CreateWeapon {
                Sign = "Wp1",
                CallBack = OnCreateWeapon2CallBack,
            });
        }

        public void OnCreateWeapon1CallBack(IGameProtoCallBackData message) {
            var data = (GameProtoDoc_Weapon.CreateWeaponCallBackData) message;
            weapons.Add(data.WeaponId);
            this.system.Entity.SetParent(CharacterEntity.WP1, data.WeaponTran);
        }

        public void OnCreateWeapon2CallBack(IGameProtoCallBackData message) {
            var data = (GameProtoDoc_Weapon.CreateWeaponCallBackData) message;
            weapons.Add(data.WeaponId);
            this.system.Entity.SetParent(CharacterEntity.WP2, data.WeaponTran);
        }

        private void OnUpdate(float delta) {
            var isFire = isDown || isRotaDown;
            if (isFire != this.isFire) {
                this.isFire = isFire;
                for (int i = 0; i < weapons.Count; i++) {
                    GameProtoManager.Send(new GameProtoDoc_Weapon.Fire {
                        WeaponId = weapons[i],
                        IsFire = isFire,
                    });
                }
            }
        }

        private void OnRota(float h, float v, bool isDown) {
            this.isRotaDown = isDown;
        }

        private void OnFireCallBack(bool isDown) {
            this.isDown = isDown;
        }
    }
}