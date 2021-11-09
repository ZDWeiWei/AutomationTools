using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Sofunny.Tools.AutomationTools.Util;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public class EntityBase : SystemBase.IEntity {
        public enum AttrState : int {
            Point = 0,
            Rotation = 1,
            LocalPoint = 2,
            LocalRotation = 3,
            LocalScale = 4,
        }

        public enum TranUpdateState {
            None,
            Update,
            LateUpdate,
            FixedUpdate,
        }

        public class ComponentData {
            public GameObject Obj = null;
            public Transform Tran = null;
            public string ObjKey;
            public Dictionary<int, AttrData> attrData;
        }

        public class AttrData {
            public AttrState AttrState;
            public bool IsChange = false;
            public Vector3 TargetPoint = Vector3.zero;
            public Quaternion TargetRota = Quaternion.identity;
        }

        protected SystemBase system;
        private Dictionary<string, ComponentData> objectDatas = new Dictionary<string, ComponentData>();
        private TranUpdateState updateState = TranUpdateState.None;

        public void Init(SystemBase system) {
            this.system = system;
            this.system.Register<GameObject>(SystemBase.Event_CreateEntityComplete, OnCreateEntityCallBack);
            SetUpdateState(TranUpdateState.Update);
            OnInit();
        }

        public virtual void OnInit() {
        }

        public void Clear() {
            OnClear();
            RemoveUpdateState();
            this.system.Register<GameObject>(SystemBase.Event_CreateEntityComplete, OnCreateEntityCallBack);
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

        private void UpdateAttributes() {
            foreach (var componentData in objectDatas.Values) {
                if (componentData.Tran == null) {
                    continue;
                }
                var tran = componentData.Tran;
                foreach (var value in componentData.attrData.Values) {
                    if (value.IsChange == false) {
                        continue;
                    }
                    value.IsChange = false;
                    switch (value.AttrState) {
                        case AttrState.Point:
                            tran.position = value.TargetPoint;
                            break;
                        case AttrState.Rotation:
                            tran.rotation = value.TargetRota;
                            break;
                        case AttrState.LocalPoint:
                            tran.localPosition = value.TargetPoint;
                            break;
                        case AttrState.LocalRotation:
                            tran.localRotation = value.TargetRota;
                            break;
                        case AttrState.LocalScale:
                            tran.localScale = value.TargetPoint;
                            break;
                    }
                }
            }
        }

        private void OnCreateEntityCallBack(GameObject gameObj) {
            GetComponentObjs(gameObj);
            OnCreateEntityComponent(gameObj);
        }

        private void GetComponentObjs(GameObject gameObj) {
            var objComponentKey = gameObj.GetComponentsInChildren<ObjComponentKey>(true);
            for (var i = 0; i < objComponentKey.Length; i++) {
                var cKey = objComponentKey[i];
                var cGameObj = cKey.gameObject;
                var key = cKey.Key == "" ? cGameObj.name : cKey.Key;
                var compData = GetComponentData(key);
                compData.Obj = cGameObj;
                compData.Tran = cGameObj.transform;
                var component = cKey.GetComponent<ObjComponent>();
                for (var j = 0; j < component.Attrs.Length; j++) {
                    var attrState = component.Attrs[j];
                    var attrKey = (int) attrState;
                    if (compData.attrData.ContainsKey(attrKey) == false) {
                        compData.attrData.Add(attrKey, GetNewAttrData(attrState));
                    }
                }
            }
        }

        protected virtual void OnCreateEntityComponent(GameObject gameObj) {
        }

        private ComponentData GetComponentData(string key) {
            ComponentData data;
            if (objectDatas.TryGetValue(key, out data) == false) {
                data = new ComponentData();
                data.attrData = new Dictionary<int, AttrData>();
                objectDatas.Add(key, data);
            }
            return data;
        }

        private AttrData GetNewAttrData(AttrState state) {
            var attrData = new AttrData();
            attrData.AttrState = state;
            attrData.IsChange = false;
            attrData.TargetPoint = Vector3.zero;
            attrData.TargetRota = Quaternion.identity;
            return attrData;
        }

        private AttrData GetAttrData(string key, AttrState state) {
            ComponentData compData = GetComponentData(key);
            var attrKey = (int) state;
            AttrData attrData;
            if (compData.attrData.TryGetValue(attrKey, out attrData) == false) {
                attrData = GetNewAttrData(state);
                compData.attrData.Add(attrKey, attrData);
            }
            return attrData;
        }

        protected GameObject GetObj(string key) {
            ComponentData data;
            if (objectDatas.TryGetValue(key, out data)) {
                return data.Obj;
            }
            return null;
        }

        protected Transform GetTran(string key) {
            ComponentData data;
            if (objectDatas.TryGetValue(key, out data)) {
                return data.Tran;
            }
            return null;
        }

        public void SetPoint(string key, Vector3 value) {
            SetVector3(key, AttrState.Point, value);
        }

        public void SetRota(string key, Quaternion value) {
            SetRotation(key, AttrState.Rotation, value);
        }

        public void SetLocalPoint(string key, Vector3 value) {
            SetVector3(key, AttrState.LocalPoint, value);
        }

        public void SetLocalRota(string key, Quaternion value) {
            SetRotation(key, AttrState.LocalRotation, value);
        }

        public void SetLocalScale(string key, Quaternion value) {
            SetRotation(key, AttrState.LocalScale, value);
        }

        public Vector3 GetPoint(string key) {
            return GetVector3(key, AttrState.Point);
        }

        public Quaternion GetRota(string key) {
            return GetRotation(key, AttrState.Rotation);
        }

        public Vector3 GetLocalPoint(string key) {
            return GetVector3(key, AttrState.LocalPoint);
        }

        public Quaternion GetLocalRota(string key) {
            return GetRotation(key, AttrState.LocalRotation);
        }

        public Vector3 GetLocalScale(string key) {
            return GetVector3(key, AttrState.LocalScale);
        }

        private void SetVector3(string key, AttrState attr, Vector3 value) {
            var data = GetAttrData(key, attr);
            if (data.TargetPoint != value) {
                data.TargetPoint = value;
                data.IsChange = true;
            }
        }

        private void SetRotation(string key, AttrState attr, Quaternion value) {
            var data = GetAttrData(key, attr);
            if (data.TargetRota.eulerAngles != value.eulerAngles) {
                data.TargetRota = value;
                data.IsChange = true;
            }
        }

        private Vector3 GetVector3(string key, AttrState attr) {
            var componentData = GetComponentData(key);
            var point = Vector3.zero;
            if (componentData.Tran != null) {
                switch (attr) {
                    case AttrState.Point:
                        point = componentData.Tran.position;
                        break;
                    case AttrState.LocalPoint:
                        point = componentData.Tran.localPosition;
                        break;
                    case AttrState.LocalScale:
                        point = componentData.Tran.localScale;
                        break;
                }
            }
            return point;
        }

        private Quaternion GetRotation(string key, AttrState attr) {
            var componentData = GetComponentData(key);
            var rota = Quaternion.identity;
            if (componentData.Tran != null) {
                switch (attr) {
                    case AttrState.Rotation:
                        rota = componentData.Tran.rotation;
                        break;
                    case AttrState.LocalRotation:
                        rota = componentData.Tran.localRotation;
                        break;
                }
            }
            return rota;
        }
    }
}