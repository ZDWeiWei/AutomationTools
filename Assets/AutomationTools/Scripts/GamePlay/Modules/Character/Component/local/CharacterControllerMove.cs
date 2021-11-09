using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.UIGameProto;
using Sofunny.Tools.AutomationTools.Util;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public class CharacterControllerMove : SystemBase.IComponent {
        private CharacterSystem system;
        private CharacterController controller;
        private Quaternion hRota;
        private Vector3 point = Vector3.zero;
        private bool isLoadEntityObj = false;
        private float moveSpeed = 10f;
        private float gravitey = 40f;
        private float jumpSpeed = 80f;
        private float jumpSpeedValue = 0.0f;

        private float moveH = 0.0f;
        private float moveV = 0.0f;

        public void Init(SystemBase system) {
            this.system = (CharacterSystem) system;
            isLoadEntityObj = false;
            GameProtoManager.AddListener(GameProtoDoc_Camera.SendHVRotation.ID, OnSendHVRotationCallBack);
            system.Register<bool>(CharacterSystem.Event_Jump, OnJumpCallBack);
            this.system.Register<float>(CharacterSystem.Event_MoveH, OnMoveHCallBack);
            this.system.Register<float>(CharacterSystem.Event_MoveV, OnMoveVCallBack);
            ATUpdateRegister.AddUpdate(OnUpdate);
            jumpSpeedValue = gravitey;
        }

        public void Clear() {
            ATUpdateRegister.RemoveUpdate(OnUpdate);
            system.UnRegister<bool>(CharacterSystem.Event_Jump, OnJumpCallBack);
            GameProtoManager.RemoveListener(GameProtoDoc_Camera.SendHVRotation.ID, OnSendHVRotationCallBack);
            RemoveController();
            this.system = null;
        }

        public void OnUpdate(float delta) {
            if (isLoadEntityObj == false && this.system.Entity.IsLoadEntityObj == true) {
                this.system.Entity.SetPoint(CharacterEntity.Root, Vector3.zero);
                controller = this.system.Entity.AddCharacterController();
                isLoadEntityObj = true;
            }
            if (isLoadEntityObj) {
                UpdateRota();
                KeyUpdate(delta);
            }
        }

        private void KeyUpdate(float delta) {
            var entity = this.system.Entity;
            var moveDir = new Vector3(moveH, 0, moveV);
            moveDir = entity.TransformDirection(CharacterEntity.Root, moveDir);
            if (jumpSpeedValue > 0f) {
                jumpSpeedValue -= Time.deltaTime * jumpSpeed;
            }
            moveDir.y += (jumpSpeedValue - gravitey) * delta;
            controller.Move(moveDir * moveSpeed * delta);
            point = controller.transform.position;
        }

        private void OnJumpCallBack(bool isDown) {
            if (isDown) {
                jumpSpeedValue = jumpSpeed;
            }
        }

        private void OnMoveHCallBack(float value) {
            Debug.Log("OnMoveHCallBack:" + value);
            moveH = value;
        }

        private void OnMoveVCallBack(float value) {
            Debug.Log("OnMoveVCallBack:" + value);
            moveV = value;
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
            var entity = this.system.Entity;
            if (moveH == 0f && moveV == 0f) {
                return;
            }
            entity.SetRota(CharacterEntity.Root,
                Quaternion.Lerp(entity.GetRota(CharacterEntity.Root), hRota, Time.deltaTime * 5f));
        }
    }
}