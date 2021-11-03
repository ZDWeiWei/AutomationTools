using System;
using Sofunny.Tools.AutomationTools.Util;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sofunny.Tools.AutomationTools.UI {
    public class BtnComponent : MonoBehaviour {
        private Action<bool> onClick;
        private Action<bool> onKeyCallBack;
        private KeyCode useKeyCode = KeyCode.None;
        private bool isInit = false;

        void Start() {
            isInit = true;
            ButtonUtil.AddTriggerListener(gameObject, EventTriggerType.PointerDown, OnDownCallBack);
            ButtonUtil.AddTriggerListener(gameObject, EventTriggerType.PointerUp, OnUpCallBack);
        }

        public void Clear() {
            if (isInit == false) {
                return;
            }
            if (useKeyCode != KeyCode.None) {
                ATUpdateRegister.RemoveUpdate(OnUpdateEvent);
            }
            onClick = null;
            onKeyCallBack = null;
            useKeyCode = KeyCode.None;
            ButtonUtil.RemoveTriggerListener(gameObject);
            isInit = false;
        }


        void OnUpdateEvent(float delta) {
            if (Input.GetKeyDown(useKeyCode)) {
                onKeyCallBack(true);
            }
            if (Input.GetKeyUp(useKeyCode)) {
                onKeyCallBack(false);
            }
        }

        public void AddOnClick(Action<bool> callBack) {
            AddOnClick(KeyCode.None, callBack);
        }

        public void AddOnClick(KeyCode key, Action<bool> callBack) {
            onClick += callBack;
            AddKeyCode(key, callBack);
        }

        private void OnDownCallBack(BaseEventData eventData) {
            if (onClick == null) {
                return;
            }
            onClick(true);
        }

        private void OnUpCallBack(BaseEventData eventData) {
            if (onClick == null) {
                return;
            }
            onClick(false);
        }

        private void AddKeyCode(KeyCode key, Action<bool> callBack) {
            if (callBack == null) {
                Debug.LogError("不能注册空的事件回调:" + name);
                return;
            }
            if (key == KeyCode.None) {
                if (useKeyCode != KeyCode.None) {
                    ATUpdateRegister.RemoveUpdate(OnUpdateEvent);
                }
            } else {
                if (useKeyCode == KeyCode.None) {
                    ATUpdateRegister.AddUpdate(OnUpdateEvent);
                }
            }
            useKeyCode = key;
            onKeyCallBack = callBack;
        }
    }
}