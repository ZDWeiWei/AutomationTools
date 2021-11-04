using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.Asset;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public class CharacterEntity : CharacterSystem.IComponent {
        private CharacterSystem system;
        private GameObject entityObj;
        private Transform entityTran;
        public Vector3 Point {
            get { return this.entityTran.position; }
        }
        public Quaternion Rotation {
            get { return this.entityTran.rotation; }
        }
        public Vector3 Scale {
            get { return this.entityTran.localScale; }
        }
        public bool IsLoadEntityObj {
            get { return this.entityObj != null; }
        }

        public void Init(SystemBase system) {
            this.system = (CharacterSystem) system;
            this.system.Data.SetEntity(this);
            this.system.Register<GameObject>(SystemBase.Event_CreateEntityComplete, OnCreateEntityCallBack);
            this.system.CreateEntity(URI.Role);
        }

        public void Clear() {
            this.system.UnRegister<GameObject>(SystemBase.Event_CreateEntityComplete, OnCreateEntityCallBack);
            this.system = null;
            this.entityObj = null;
            this.entityTran = null;
        }

        private void OnCreateEntityCallBack(GameObject gameObj) {
            this.entityObj = gameObj;
            this.entityTran = gameObj.transform;
        }

        public void SetPoint(Vector3 point) {
            if (this.entityObj == null) {
                return;
            }
            this.entityTran.position = point;
        }

        public void SetRotation(Quaternion rota) {
            if (this.entityObj == null) {
                return;
            }
            this.entityTran.rotation = rota;
        }

        public void SetScale(Vector3 scale) {
            if (this.entityObj == null) {
                return;
            }
            this.entityTran.localScale = scale;
        }

        public Vector3 TransformDirection(Vector3 dir) {
            if (this.entityObj == null) {
                return Vector3.zero;
            }
            return this.entityTran.TransformDirection(dir);
        }

        public CharacterController AddCharacterController() {
            if (this.entityObj == null) {
                return null;
            }
            return entityTran.gameObject.AddComponent<CharacterController>();
        }
    }
}