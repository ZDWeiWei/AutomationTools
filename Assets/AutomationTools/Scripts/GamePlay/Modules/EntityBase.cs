using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.Util;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public class EntityBase : SystemBase.IComponent {
        public enum AttrState : int {
            Point = 0,
            Rotation = 1,
            LocalPoint = 2,
            LocalRotation = 3,
            LocalScale = 4,
        }

        public class MonoData {
            public GameObject Obj = null;
            public Transform Tran = null;
            public AttrState AttrState;
            public bool IsChange = false;
            public string ObjKey;
            public Vector3 TargetPoint = Vector3.zero;
            public Quaternion TargetRota = Quaternion.identity;
        }

        private Dictionary<string, List<MonoData>> objectDatas = new Dictionary<string, List<MonoData>>();
        protected SystemBase system;

        public void Init(SystemBase system) {
            this.system = system;
            this.system.Register<GameObject>(SystemBase.Event_CreateEntityComplete, OnCreateEntityCallBack);
            OnInit();
        }

        public virtual void OnInit() {
        }

        public void Clear() {
            OnClear();
            this.system.Register<GameObject>(SystemBase.Event_CreateEntityComplete, OnCreateEntityCallBack);
            objectDatas.Clear();
            this.system = null;
        }

        public virtual void OnClear() {
        }

        public void OnUpdate(float delta) {
            if (objectDatas.Count <= 0) {
                return;
            }
            UpdateAttributes();
        }

        private void UpdateAttributes() {
            foreach (var values in objectDatas.Values) {
                for (int i = 0; i < values.Count; i++) {
                    var value = values[i];
                    if (value.IsChange == false || value.Tran == null) {
                        continue;
                    }
                    value.IsChange = false;
                    switch (value.AttrState) {
                        case AttrState.Point:
                            value.Tran.position = value.TargetPoint;
                            break;
                        case AttrState.Rotation:
                            value.Tran.rotation = value.TargetRota;
                            break;
                        case AttrState.LocalPoint:
                            value.Tran.localPosition = value.TargetPoint;
                            break;
                        case AttrState.LocalRotation:
                            value.Tran.localRotation = value.TargetRota;
                            break;
                        case AttrState.LocalScale:
                            value.Tran.localScale = value.TargetPoint;
                            break;
                    }
                }
            }
        }

        private void OnCreateEntityCallBack(GameObject gameObj) {
            var cameraMonoList = gameObj.GetComponentsInChildren<ObjComponent>(true);
            for (int i = 0; i < cameraMonoList.Length; i++) {
                var cameraMono = cameraMonoList[i];
                var key = cameraMono.Key;
                if (HasMonoDataObj(key, cameraMono.AttrState)) {
                    Debug.LogErrorFormat("obj 重复添加 key:{0}, objState: {1}", key, cameraMono.AttrState);
                    return;
                }
                var monoData = GetMonoData(key, cameraMono.AttrState);
                monoData.Obj = cameraMono.gameObject;
                monoData.Tran = cameraMono.transform;
            }
        }

        private MonoData GetMonoData(string key, AttrState attrState) {
            List<MonoData> lists;
            if (objectDatas.TryGetValue(key, out lists) == false) {
                lists = new List<MonoData>();
                objectDatas.Add(key, lists);
            }
            MonoData monoData;
            for (int i = 0; i < lists.Count; i++) {
                monoData = lists[i];
                if (monoData.AttrState == attrState) {
                    return monoData;
                }
            }
            monoData = new MonoData();
            monoData.Obj = null;
            monoData.Tran = null;
            monoData.ObjKey = key;
            monoData.AttrState = attrState;
            lists.Add(monoData);
            return monoData;
        }

        protected GameObject GetObj(string key) {
            List<MonoData> lists;
            if (objectDatas.TryGetValue(key, out lists)) {
                if (lists.Count <= 0) {
                    return null;
                }
                return lists[0].Obj;
            }
            return null;
        }

        protected Transform GetTran(string key) {
            List<MonoData> lists;
            if (objectDatas.TryGetValue(key, out lists)) {
                if (lists.Count <= 0) {
                    return null;
                }
                return lists[0].Tran;
            }
            return null;
        }

        private bool HasMonoDataObj(string key, AttrState attrState) {
            List<MonoData> lists;
            if (objectDatas.TryGetValue(key, out lists)) {
                MonoData monoData;
                for (int i = 0; i < lists.Count; i++) {
                    monoData = lists[i];
                    if (monoData.AttrState == attrState && monoData.Obj != null) {
                        return true;
                    }
                }
            }
            return false;
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
            var monoData = GetMonoData( key, attr);
            if (monoData.TargetPoint != value) {
                monoData.TargetPoint = value;
                monoData.IsChange = true;
            }
        }

        private void SetRotation(string key, AttrState attr, Quaternion value) {
            var monoData = GetMonoData(key, attr);
            if (monoData.TargetRota.eulerAngles != value.eulerAngles) {
                monoData.TargetRota = value;
                monoData.IsChange = true;
            }
        }

        private Vector3 GetVector3(string key, AttrState attr) {
            var monoData = GetMonoData(key, attr);
            return new Vector3(monoData.TargetPoint.x, monoData.TargetPoint.y, monoData.TargetPoint.z);
        }

        private Quaternion GetRotation(string key, AttrState attr) {
            var monoData = GetMonoData(key, attr);
            return new Quaternion(monoData.TargetRota.x, monoData.TargetRota.y, monoData.TargetRota.z,
                monoData.TargetRota.w);
        }
    }
}