using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.UIGameProto;
using Sofunny.Tools.AutomationTools.Util;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public class CharacterLocalInput : CharacterEntity.IComponent {
        private CharacterEntity entity;

        public void Init(CharacterEntity entity) {
            this.entity = entity;
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
            this.entity = null;
        }

        private void OnWCallBack(IGameProtoDoc message) {
            var data = (GameProtoDoc_Character.OnMoveW) message;
            this.entity.Dispatcher(CharacterEntity.Event_W, data.isDown);
        }

        private void OnSCallBack(IGameProtoDoc message) {
            var data = (GameProtoDoc_Character.OnMoveS) message;
            this.entity.Dispatcher(CharacterEntity.Event_S, data.isDown);
        }

        private void OnACallBack(IGameProtoDoc message) {
            var data = (GameProtoDoc_Character.OnMoveA) message;
            this.entity.Dispatcher(CharacterEntity.Event_A, data.isDown);
        }

        private void OnDCallBack(IGameProtoDoc message) {
            var data = (GameProtoDoc_Character.OnMoveD) message;
            this.entity.Dispatcher(CharacterEntity.Event_D, data.isDown);
        }

        private void OnJumpCallBack(IGameProtoDoc message) {
            var data = (GameProtoDoc_Character.OnJump) message;
            this.entity.Dispatcher(CharacterEntity.Event_Jump, data.isDown);
        }

        private void OnFireCallBack(IGameProtoDoc message) {
            var data = (GameProtoDoc_Character.OnFire) message;
            this.entity.Dispatcher(CharacterEntity.Event_Fire, data.isDown);
        }
    }
}