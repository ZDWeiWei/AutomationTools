using System;
using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.Asset;
using Sofunny.Tools.AutomationTools.UIGameProto;
using Sofunny.Tools.AutomationTools.Util;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public class WeaponEntity : EntityBase {
        public const string Root = "Root";
        private ProtoCallBack createCallBack;
        private WeaponSystem weaponSystem;

        public override void OnInit() {
            this.weaponSystem = (WeaponSystem) system;
        }

        public override void OnClear() {
            this.weaponSystem = null;
        }

        public void SetWeaponData(GameProtoDoc_Weapon.CreateWeapon message) {
            createCallBack = message.CallBack;
            CreateEntityObj(StringUtil.Concat(URI.Weapon, message.Sign));
        }

        protected override void OnCreateEntityComponent() {
            base.OnCreateEntityComponent();
            createCallBack(new GameProtoDoc_Weapon.CreateWeaponCallBackData {
                WeaponId = this.weaponSystem.Data.WeaponID, 
                WeaponTran = entityTran
            });
            this.SetLocalPoint(Root, Vector3.zero);
            this.SetLocalRota(Root, Quaternion.identity);
            this.SetLocalScale(Root, Vector3.one);
        }
    }
}