using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.UIGameProto;
using Sofunny.Tools.AutomationTools.Util;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public class CharacterLocalInput : SystemBase.IComponent {
        private CharacterSystem system;
        private bool isWDown = false;
        private bool isSDown = false;
        private bool isADown = false;
        private bool isDDown = false;
        private float moveH = 0.0f;
        private float moveV = 0.0f;

        public void Init(SystemBase system) {
            this.system = (CharacterSystem) system;
            AddListenerInput();
        }

        private void AddListenerInput() {
            GameProtoManager.AddListener(GameProtoDoc_Character.OnMoveW.ID, OnWCallBack);
            GameProtoManager.AddListener(GameProtoDoc_Character.OnMoveS.ID, OnSCallBack);
            GameProtoManager.AddListener(GameProtoDoc_Character.OnMoveA.ID, OnACallBack);
            GameProtoManager.AddListener(GameProtoDoc_Character.OnMoveD.ID, OnDCallBack);
            GameProtoManager.AddListener(GameProtoDoc_Character.OnJump.ID, OnJumpCallBack);
            GameProtoManager.AddListener(GameProtoDoc_Character.OnFire.ID, OnFireCallBack);
        }

        public void Clear() {
            GameProtoManager.RemoveListener(GameProtoDoc_Character.OnMoveW.ID, OnWCallBack);
            GameProtoManager.RemoveListener(GameProtoDoc_Character.OnMoveS.ID, OnSCallBack);
            GameProtoManager.RemoveListener(GameProtoDoc_Character.OnMoveA.ID, OnACallBack);
            GameProtoManager.RemoveListener(GameProtoDoc_Character.OnMoveD.ID, OnDCallBack);
            GameProtoManager.RemoveListener(GameProtoDoc_Character.OnJump.ID, OnJumpCallBack);
            GameProtoManager.RemoveListener(GameProtoDoc_Character.OnFire.ID, OnFireCallBack);
            this.system = null;
        }

        private void OnWCallBack(IGameProtoDoc message) {
            var data = (GameProtoDoc_Character.OnMoveW) message;
            isWDown = data.isDown;
            UpdateMoveVKey();
        }

        private void OnSCallBack(IGameProtoDoc message) {
            var data = (GameProtoDoc_Character.OnMoveS) message;
            isSDown = data.isDown;
            UpdateMoveVKey();
        }

        private void OnACallBack(IGameProtoDoc message) {
            var data = (GameProtoDoc_Character.OnMoveA) message;
            isADown = data.isDown;
            UpdateMoveHKey();
        }

        private void OnDCallBack(IGameProtoDoc message) {
            var data = (GameProtoDoc_Character.OnMoveD) message;
            isDDown = data.isDown;
            UpdateMoveHKey();
        }

        private void OnJumpCallBack(IGameProtoDoc message) {
            var data = (GameProtoDoc_Character.OnJump) message;
            this.system.Dispatcher(CharacterSystem.Event_Jump, data.isDown);
        }

        private void OnFireCallBack(IGameProtoDoc message) {
            var data = (GameProtoDoc_Character.OnFire) message;
            this.system.Dispatcher(CharacterSystem.Event_Fire, data.isDown);
        }

        private void UpdateMoveHKey() {
            if (isADown == isDDown) {
                moveH = 0;
            } else {
                if (isADown) {
                    moveH = -1;
                }
                if (isDDown) {
                    moveH = 1;
                }
            }
            this.system.Dispatcher(CharacterSystem.Event_MoveH, moveH);
        }

        private void UpdateMoveVKey() {
            if (isWDown == isSDown) {
                moveV = 0;
            } else {
                if (isWDown) {
                    moveV = 1;
                }
                if (isSDown) {
                    moveV = -1;
                }
            }
            this.system.Dispatcher(CharacterSystem.Event_MoveV, moveV);
        }
    }
}