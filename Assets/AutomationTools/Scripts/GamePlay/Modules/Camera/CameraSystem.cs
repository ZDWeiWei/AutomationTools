using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.Asset;
using Sofunny.Tools.AutomationTools.UIGameProto;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public partial class CameraSystem : SystemBase {
        public CameraData Data;

        protected override void OnInit() {
            base.OnInit();
            Data = new CameraData();
            InitComponent();
        }

        private void InitComponent() {
            AddComponent<CameraEntity>();
            AddComponent<CameraMove>();
            AddComponent<CameraTarget>();
        }

        protected override void OnClear() {
            base.OnClear();
            Data = null;
        }
    }
}