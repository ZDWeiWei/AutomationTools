using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.Asset;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public class BulletEntity : EntityBase {
        public const string Root = "Root";
        public const string Body = "Body";

        public override void OnInit() {
            CreateEntityObj(URI.Bullet);
            SetUpdateState(TranUpdateState.LateUpdate);
        }

        public override void OnClear() {
        }
    }
}