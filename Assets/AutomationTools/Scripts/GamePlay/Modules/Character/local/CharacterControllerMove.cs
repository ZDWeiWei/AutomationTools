using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.Util;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public class CharacterControllerMove : CharacterEntity.IComponent {
        private CharacterEntity entity;
        private CharacterController controller;
        private Transform entityTran;
        private bool isWDown = false;
        private bool isSDown = false;
        private bool isADown = false;
        private bool isDDown = false;
        private float moveH;
        private float moveV;

        private float moveSpeed = 10f;

        public void Init(CharacterEntity entity) {
            ATUpdateRegister.AddUpdate(OnUpdate);  
            this.entity = entity;
            entityTran = entity.transform;
            entityTran.position = Vector3.zero;
            AddController();
            Register();
        }

        public void Clear() {
            ATUpdateRegister.RemoveUpdate(OnUpdate);
            UnRegister();
            RemoveController();
        }

        public void OnUpdate(float delta) {
            UpdateMoveVKey();
            UpdateMoveHKey();
            var moveDir = new Vector3(moveH, 0, moveV);
            moveDir = entityTran.TransformDirection(moveDir);
            controller.Move(moveDir * moveSpeed * delta);
        }

        private void Register() {
            this.entity.Register<bool>(CharacterEntity.Event_W, OnWCallBack);
            this.entity.Register<bool>(CharacterEntity.Event_S, OnSCallBack);
            this.entity.Register<bool>(CharacterEntity.Event_A, OnACallBack);
            this.entity.Register<bool>(CharacterEntity.Event_D, OnDCallBack);
        }

        private void UnRegister() {
            this.entity.UnRegister(CharacterEntity.Event_W);
            this.entity.UnRegister(CharacterEntity.Event_S);
            this.entity.UnRegister(CharacterEntity.Event_A);
            this.entity.UnRegister(CharacterEntity.Event_D);
        }

        private void AddController() {
            if (controller != null) {
                Debug.LogError("重复添加控制器");
                return;
            }
            controller = this.entity.gameObject.AddComponent<CharacterController>();
        }

        private void RemoveController() {
            if (controller == null) {
                return;
            }
            GameObject.Destroy(controller);
            controller = null;
        }

        private void OnWCallBack(bool isDown) {
            isWDown = isDown;
        }

        private void OnSCallBack(bool isDown) {
            isSDown = isDown;
        }

        private void OnACallBack(bool isDown) {
            isADown = isDown;
        }

        private void OnDCallBack(bool isDown) {
            isDDown = isDown;
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
        }
    }
}