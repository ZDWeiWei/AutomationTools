using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Sofunny.Tools.AutomationTools.Asset;
using Sofunny.Tools.AutomationTools.Util;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public partial class EntityBase : SystemBase.IEntity {
        public enum TranUpdateState {
            None,
            Update,
            LateUpdate,
            FixedUpdate,
        }

        protected SystemBase system;
        protected GameObject entityObj;
        protected Transform entityTran;
        private TranUpdateState updateState = TranUpdateState.None;

        public void Init(SystemBase system) {
            this.system = system;
            SetUpdateState(TranUpdateState.Update);
            OnInit();
        }

        public virtual void OnInit() {
        }

        public void Clear() {
            OnClear();
            RemoveUpdateState();
            RemoveEntityObj();
            objectDatas.Clear();
            this.system = null;
        }

        public virtual void OnClear() {
        }

        private void OnBaseUpdate(float delta) {
            if (objectDatas.Count <= 0) {
                return;
            }
            UpdateAttributes();
        }

        protected void SetUpdateState(TranUpdateState state) {
            if (updateState == state) {
                return;
            }
            RemoveUpdateState();
            updateState = state;
            switch (state) {
                case TranUpdateState.Update:
                    ATUpdateRegister.AddUpdate(OnBaseUpdate);
                    break;
                case TranUpdateState.FixedUpdate:
                    ATUpdateRegister.AddFixedUpdate(OnBaseUpdate);
                    break;
                case TranUpdateState.LateUpdate:
                    ATUpdateRegister.AddLateUpdate(OnBaseUpdate);
                    break;
            }
        }

        protected void RemoveUpdateState() {
            switch (updateState) {
                case TranUpdateState.Update:
                    ATUpdateRegister.RemoveUpdate(OnBaseUpdate);
                    break;
                case TranUpdateState.FixedUpdate:
                    ATUpdateRegister.RemoveFixedUpdate(OnBaseUpdate);
                    break;
                case TranUpdateState.LateUpdate:
                    ATUpdateRegister.RemoveLateUpdate(OnBaseUpdate);
                    break;
            }
            updateState = TranUpdateState.None;
        }

        private void OnCreateEntityCallBack() {
            GetComponentObjs();
            OnCreateEntityComponent();
        }

        protected virtual void OnCreateEntityComponent() {
        }

        protected void CreateEntityObj(string url) {
            var request = AssetManager.LoadGamePlayObjAsync(url);
            request.completed += operation => {
                var prefab = (GameObject) request.asset;
                if (prefab != null) {
                    entityObj = GameObject.Instantiate(prefab);
                    entityTran = entityObj.transform;
                    OnCreateEntityCallBack();
                    this.system.Dispatcher(SystemBase.Event_CreateEntityComplete, entityObj);
                } else {
                    Debug.LogError("加载失败:" + url);
                }
            };
        }

        private void RemoveEntityObj() {
            if (entityObj == null) {
                return;
            }
            GameObject.Destroy(entityObj);
            entityObj = null;
            entityTran = null;
        }
    }
}