using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.UIGameProto;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.View {
    public class PlayerOperrateViewFeature : IFeature {
        public void Init() {
            GameProtoManager.AddListener(GameProtoDoc_Character.OpenCharacterUI.ID, OnOpenCharacterUICallBack);
        }
        public void Clear() {
            GameProtoManager.RemoveListener(GameProtoDoc_Character.OpenCharacterUI.ID, OnOpenCharacterUICallBack);
        }

        private void OnOpenCharacterUICallBack(IGameProtoDoc message) {
            ViewManager.Instance.Open<PlayerOperateView>(null);
        }
    }
}
