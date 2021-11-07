using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.UIGameProto;
using Sofunny.Tools.AutomationTools.Util;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public class CharacterControllerMove : SystemBase.IComponent {
        private CharacterSystem system;
        private CharacterController controller;
        private float moveSpeed = 10f;
        private bool isLoadEntityObj = false;
        private Quaternion hRota;

        public void Init(SystemBase system) {
            this.system = (CharacterSystem) system;
            isLoadEntityObj = false;
            ATUpdateRegister.AddUpdate(OnUpdate);
            GameProtoManager.AddListener(GameProtoDoc_Camera.SendHVRotation.ID, OnSendHVRotationCallBack);
        }

        public void Clear() {
            ATUpdateRegister.RemoveUpdate(OnUpdate);
            GameProtoManager.RemoveListener(GameProtoDoc_Camera.SendHVRotation.ID, OnSendHVRotationCallBack);
            RemoveController();
            this.system = null;
        }

        public void OnUpdate(float delta) {
            if (isLoadEntityObj == false && this.system.Data.Entity.IsLoadEntityObj == true) {
                this.system.Data.Entity.SetPoint(CharacterEntity.Root, Vector3.zero);
                controller = this.system.Data.Entity.AddCharacterController();
                isLoadEntityObj = true;
            }
            
            if (isLoadEntityObj) {
                UpdateRota();
                KeyUpdate(delta);
            }
        }
        
        private void KeyUpdate(float delta) {
            var data = this.system.Data;
            var moveDir = new Vector3(data.MoveH, 0, data.MoveV);
            moveDir = data.Entity.TransformDirection(CharacterEntity.Root, moveDir);
            controller.Move(moveDir * moveSpeed * delta);
        }

        private void RemoveController() {
            if (controller == null) {
                return;
            }
            GameObject.Destroy(controller);
            controller = null;
        }

        private void OnSendHVRotationCallBack(IGameProtoDoc message) {
            var data = (GameProtoDoc_Camera.SendHVRotation) message;
            hRota = data.hRota;
        }

        private void UpdateRota() {
            var data = this.system.Data;
            if (data.MoveH == 0f && data.MoveV == 0f) {
                return;
            }
            data.Entity.SetRota(CharacterEntity.Root, hRota);
        }
    }
}