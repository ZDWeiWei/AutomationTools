using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.Asset;
using Sofunny.Tools.AutomationTools.Util;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public class CharacterEntity : EntityBase {
        public const string Root = "Root";
        public const string Body = "Body";
        public const string WP1 = "WP1Box";
        public const string WP2 = "WP2Box";
        public bool IsLoadEntityObj {
            get { return entityTran != null; }
        }

        public override void OnInit() {
            SetPoint(CharacterEntity.Root, new Vector3(0f, 10f, 0f));
            CreateEntityObj(URI.Role);
        }

        public override void OnClear() {
            base.OnClear();
        }

        protected override void OnCreateEntityComponent() {
            base.OnCreateEntityComponent();
        }

        public Vector3 TransformDirection(string key, Vector3 dir) {
            var tran = GetTran(key);
            if (tran == null) {
                return Vector3.zero;
            }
            return tran.TransformDirection(dir);
        }

        public CharacterController AddCharacterController() {
            if (this.entityObj == null) {
                return null;
            }
            return entityTran.gameObject.AddComponent<CharacterController>();
        }
    }
}