using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public partial class EntityBase {
        public enum AttrState : int {
            Point = 0,
            Rotation = 1,
            LocalPoint = 2,
            LocalRotation = 3,
            LocalScale = 4,
        }

        public class ComponentData {
            public GameObject Obj = null;
            public Transform Tran = null;
            public Dictionary<int, AttrData> attrData;
        }

        public class AttrData {
            public AttrState AttrState;
            public bool IsChange = false;
            public Vector3 TargetPoint = Vector3.zero;
            public Quaternion TargetRota = Quaternion.identity;
        }

        private Dictionary<string, ComponentData> objectDatas = new Dictionary<string, ComponentData>();

        private void GetComponentObjs() {
            var objComponentKey = entityObj.GetComponentsInChildren<ObjComponentKey>(true);
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

        protected GameObject GetGameObj(string key) {
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

        public bool SetParent(string key, Transform tran) {
            if (tran == null) {
                return false;
            }
            ComponentData data;
            if (objectDatas.TryGetValue(key, out data)) {
                tran.SetParent(data.Tran);
                return true;
            }
            return false;
        }

        public bool SetActive(string key, bool isTrue) {
            ComponentData data;
            if (objectDatas.TryGetValue(key, out data)) {
                if (data.Obj.activeSelf != isTrue) {
                    data.Obj.SetActive(isTrue);
                    return true;
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

        public void SetLocalScale(string key, Vector3 value) {
            SetVector3(key, AttrState.LocalScale, value);
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
            data.TargetPoint = value;
            data.IsChange = true;
        }

        private void SetRotation(string key, AttrState attr, Quaternion value) {
            var data = GetAttrData(key, attr);
            data.TargetRota = value;
            data.IsChange = true;
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