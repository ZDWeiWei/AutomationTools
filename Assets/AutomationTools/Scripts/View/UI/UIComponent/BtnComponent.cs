using System;
using Sofunny.Tools.AutomationTools.Util;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sofunny.Tools.AutomationTools.View {
    public class BtnComponent : MonoBehaviour {
        public Action<bool> OnClick;

        void Start() {
            ButtonUtil.AddTriggerListener(gameObject, EventTriggerType.PointerDown, OnDownCallBack);
            ButtonUtil.AddTriggerListener(gameObject, EventTriggerType.PointerUp, OnUpCallBack);
        }

        void OnDestroy() {
            ButtonUtil.RemoveTriggerListener(gameObject);
        }

        private void OnDownCallBack(BaseEventData eventData) {
            if (OnClick == null) {
                return;
            }
            OnClick(true);
        }

        private void OnUpCallBack(BaseEventData eventData) {
            if (OnClick == null) {
                return;
            }
            OnClick(false);
        }
    }
}