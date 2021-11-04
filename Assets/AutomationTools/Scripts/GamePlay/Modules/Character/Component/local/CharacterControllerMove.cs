using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.Util;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public class CharacterControllerMove : SystemBase.IComponent {
        private CharacterSystem system;
        private CharacterController controller;
        private float moveSpeed = 10f;
        private bool isLoadEntityObj = false;

        public void Init(SystemBase system) {
            this.system = (CharacterSystem) system;
            isLoadEntityObj = false;
            ATUpdateRegister.AddUpdate(OnUpdate);
        }

        public void Clear() {
            ATUpdateRegister.RemoveUpdate(OnUpdate);
            RemoveController();
            this.system = null;
        }

        public void OnUpdate(float delta) {
            if (isLoadEntityObj == false && this.system.Data.Entity.IsLoadEntityObj == true) {
                this.system.Data.Entity.SetPoint(Vector3.zero);
                controller = this.system.Data.Entity.AddCharacterController();
                isLoadEntityObj = true;
            }
            if (isLoadEntityObj) {
                KeyUpdate(delta);
            }
        }
        
        private void KeyUpdate(float delta) {
            var data = this.system.Data;
            var moveDir = new Vector3(data.MoveH, 0, data.MoveV);
            moveDir = data.Entity.TransformDirection(moveDir);
            controller.Move(moveDir * moveSpeed * delta);
        }

        private void RemoveController() {
            if (controller == null) {
                return;
            }
            GameObject.Destroy(controller);
            controller = null;
        }
    }
}