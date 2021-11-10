using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sofunny.Tools.AutomationTools.UI;
using Sofunny.Tools.AutomationTools.UIGameProto;

namespace Sofunny.Tools.AutomationTools.View {
    public class LoginView : ViewBase {
        protected override void RegisterUIEvent(UI.UIBase ui) {
            ui.Register(UILogin.Event_OnClose, OnCloseCallBack);
            ui.Register<bool>(UILogin.Event_OnPlay, OnPlayCallBack);
            ui.Register<bool>(UILogin.Event_OnEditor, OnEditorCallBack);
        }

        private void OnCloseCallBack() {
            Close();
        }

        private void OnPlayCallBack(bool isDown) {
        }

        private void OnEditorCallBack(bool isDown) {
            GameProtoManager.Send(new GameProtoDoc_StartGame.EnterGame {});
            Close();
        }
    }
}
