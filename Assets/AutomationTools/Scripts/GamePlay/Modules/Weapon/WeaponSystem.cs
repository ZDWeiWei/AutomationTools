using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.UIGameProto;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public class WeaponSystem : SystemBase {
        public const int Event_Fire = 1;
        
        public WeaponData Data;
        public WeaponEntity Entity;
        protected override void OnInit() {
            base.OnInit();
            Data = new WeaponData();
            Entity = AddEntity<WeaponEntity>();
        }

        protected override void OnClear() {
            base.OnClear();
            Data = null;
            Entity = null;
        }

        public void SetWeaponData(GameProtoDoc_Weapon.CreateWeapon message, int weaponId) {
            Data.WeaponID = weaponId;
            Entity.SetWeaponData(message);
            InitComponends();
        }

        private void InitComponends() {
            AddComponent<WeaponFire>();
        }
    }
}
