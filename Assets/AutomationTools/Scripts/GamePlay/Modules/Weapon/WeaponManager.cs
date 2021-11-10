using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.UIGameProto;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public class WeaponManager : ManagerBase {
        private int weaponId = 0;
        private List<WeaponSystem> systems = new List<WeaponSystem>();

        override protected void OnInit() {
            GameProtoManager.AddListener(GameProtoDoc_Weapon.CreateWeapon.ID, CreateWeaponCallBack);
            GameProtoManager.AddListener(GameProtoDoc_Weapon.Fire.ID, OnFireCallBack);
        }

        override protected void OnClear() {
            GameProtoManager.RemoveListener(GameProtoDoc_Weapon.CreateWeapon.ID, CreateWeaponCallBack);
            GameProtoManager.RemoveListener(GameProtoDoc_Weapon.Fire.ID, OnFireCallBack);
            ClearAllWeapon();
        }

        public void CreateWeaponCallBack(IGameProtoDoc message) {
            weaponId++;
            var data = (GameProtoDoc_Weapon.CreateWeapon) message;
            var weaponSystem = AddSystem<WeaponSystem>();
            systems.Add(weaponSystem);
            weaponSystem.SetWeaponData(data, weaponId);
        }

        private void ClearAllWeapon() {
            for (int i = 0; i < systems.Count; i++) {
                var system = systems[i];
                system.Clear();
            }
            systems.Clear();
        }

        private void OnFireCallBack(IGameProtoDoc message) {
            var data = (GameProtoDoc_Weapon.Fire) message;
            for (int i = 0; i < systems.Count; i++) {
                var system = systems[i];
                if (system.Data.WeaponID == data.WeaponId) {
                    system.Dispatcher(WeaponSystem.Event_Fire, data.IsFire);
                }
            }
        }
    }
}