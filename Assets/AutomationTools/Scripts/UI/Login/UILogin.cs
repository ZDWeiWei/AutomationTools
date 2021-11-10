using System;
using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.Util;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.UI {
    public class UILogin : UIBase {
        public const int Event_OnClose = 1;
        public const int Event_OnPlay = 2;
        public const int Event_OnEditor = 3;
        [SerializeField]
        private BtnComponent BtnPlay;
        [SerializeField]
        private BtnComponent BtnEditor;

        override protected void OnInit() {
            BtnPlay.AddOnClick(OnBtnPlayClickCallBack);
            BtnEditor.AddOnClick(OnBtnEditorClickCallBack);
        }

        override protected void OnClear() {
            BtnPlay.Clear();
            BtnEditor.Clear();
        }

        public void OnBtnPlayClickCallBack(bool isDown) {
            Dispatcher(Event_OnPlay, isDown);
        }

        public void OnBtnEditorClickCallBack(bool isDown) {
            Dispatcher(Event_OnEditor, isDown);
        }
    }
}