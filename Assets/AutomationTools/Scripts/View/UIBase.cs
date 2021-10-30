using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.View {
    public class UIBase : MonoBehaviour {
        protected GameObject MyGameObj;
        protected Transform MyTransform;

        public void Init() {
            MyGameObj = gameObject;
            MyTransform = transform;
            SetActive(true);
            OnInit();
        }

        protected virtual void OnInit() {
        }

        public void Clear() {
            OnClear();
            MyGameObj = null;
            MyTransform = null;
        }

        protected virtual void OnClear() {
        }

        public void SetParent(Transform parent) {
            var mTran = transform;
            mTran.SetParent(parent);
            mTran.localPosition = Vector3.zero;
            mTran.localRotation = Quaternion.identity;
            mTran.localScale = Vector3.one;
        }

        public void SetActive(bool isTrue) {
            MyGameObj.SetActive(isTrue);
        }
    }
}