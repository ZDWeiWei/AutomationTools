using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.Asset;
using Sofunny.Tools.AutomationTools.UIGameProto;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public partial class CameraSystem : SystemBase {
        public CameraData Data;
        public CameraEntity Entity;

        protected override void OnInit() {
            base.OnInit();
            Data = new CameraData();
            Entity = AddEntity<CameraEntity>();
            InitComponent();
        }

        private void InitComponent() {
            AddComponent<CameraMove>();
            AddComponent<CameraRota>();
        }

        protected override void OnClear() {
            base.OnClear();
            Data = null;
        }
    }
}