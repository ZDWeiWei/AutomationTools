using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sofunny.Tools.AutomationTools.UI;
using Sofunny.Tools.AutomationTools.UIGameProto;

namespace Sofunny.Tools.AutomationTools.View {
    public class PlayerOperateView : ViewBase {
        protected override void RegisterUIEvent(UI.UIBase ui) {
            ui.Register<bool>(UIPlayerOperate.Event_W, OnWCallBack);
            ui.Register<bool>(UIPlayerOperate.Event_S, OnSCallBack);
            ui.Register<bool>(UIPlayerOperate.Event_A, OnACallBack);
            ui.Register<bool>(UIPlayerOperate.Event_D, OnDCallBack);
            ui.Register<bool>(UIPlayerOperate.Event_Fire, OnFireCallBack);
            ui.Register<bool>(UIPlayerOperate.Event_Jump, OnJumpCallBack);
        }

        private void OnWCallBack(bool isDown) {
            GameProtoManager.Send(new GameProtoDoc_Character.OnMoveW() {
                isDown = isDown,
            });
        }

        private void OnSCallBack(bool isDown) {
            GameProtoManager.Send(new GameProtoDoc_Character.OnMoveS() {
                isDown = isDown,
            });
        }

        private void OnACallBack(bool isDown) {
            GameProtoManager.Send(new GameProtoDoc_Character.OnMoveA() {
                isDown = isDown,
            });
        }

        private void OnDCallBack(bool isDown) {
            GameProtoManager.Send(new GameProtoDoc_Character.OnMoveD() {
                isDown = isDown,
            });
        }

        private void OnJumpCallBack(bool isDown) {
            GameProtoManager.Send(new GameProtoDoc_Character.OnJump() {
                isDown = isDown,
            });
        }

        private void OnFireCallBack(bool isDown) {
            GameProtoManager.Send(new GameProtoDoc_Character.OnFire() {
                isDown = isDown,
            });
        }
    }
}