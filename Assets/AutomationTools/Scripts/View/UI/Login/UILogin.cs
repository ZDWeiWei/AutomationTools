using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.Util;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.View {
    public class UILogin : UIBase {
        [SerializeField]
        private BtnComponent BtnPlay;
        [SerializeField]
        private BtnComponent BtnEditor;

        override protected void OnInit() {
            BtnPlay.OnClick = OnBtnPlayClickCallBack;
            BtnEditor.OnClick = OnBtnEditorClickCallBack;
            ATUpdateRegister.AddUpdate(OnUpdate);
        }

        override protected void OnClear() {
            ATUpdateRegister.RemoveUpdate(OnUpdate);
            BtnPlay.OnClick = null;
            BtnEditor.OnClick = null;
        }

        public void OnBtnPlayClickCallBack(bool isDown) {
            Debug.Log("Play:" + isDown);
        }

        public void OnBtnEditorClickCallBack(bool isDown) {
            Debug.Log("Editor:" + isDown);
        }

        private void OnUpdate(float delta) {
        }
    }
}