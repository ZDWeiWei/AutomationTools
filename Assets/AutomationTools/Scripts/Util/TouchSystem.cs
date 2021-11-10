using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.Util {
    public delegate void TouchDelegate(float h, float v, bool isDown);

    public partial class TouchSystem {
        private List<TouchDelegate> m_TouchList = new List<TouchDelegate>(64);
        private bool isDown = false;
        private Vector3 prevPoint = Vector3.zero;

        public void Init() {
            instance = this;
            ATUpdateRegister.AddUpdate(OnUpdate);
        }

        public void OnUpdate(float delta) {
            if (m_TouchList.Count <= 0) {
                return;
            }
            if (Input.GetMouseButtonDown(0)) {
                prevPoint = Input.mousePosition;
                this.isDown = true;
            }
            if (Input.GetMouseButtonUp(0)) {
                this.isDown = false;
            }
            if (isDown) {
                var diffPoint = Input.mousePosition - prevPoint;
                prevPoint = Input.mousePosition;
                UpdateTouchList(diffPoint.x, -diffPoint.y, true);
            } else {
                UpdateTouchList(0, 0, false);
            }
        }

        private void UpdateTouchList(float x, float y, bool isDown) {
            for (int i = 0; i < m_TouchList.Count; i++) {
                var callBack = m_TouchList[i];
                if (callBack != null) {
                    callBack(x, y, isDown);
                }
            }
        }

        public void OnRegister(TouchDelegate handler) {
            if (SearchTouchIndex(handler) == -1) {
                m_TouchList.Add(handler);
            }
        }

        public void OnUnregister(TouchDelegate handler) {
            var length = m_TouchList.Count;
            for (var i = 0; i < length; ++i) {
                if (m_TouchList[i] == handler) {
                    m_TouchList.RemoveAt(i);
                    return;
                }
            }
        }

        private int SearchTouchIndex(TouchDelegate handler) {
            var index = -1;
            var length = m_TouchList.Count;
            for (var i = 0; i < length; ++i) {
                if (m_TouchList[i] == handler) {
                    index = i;
                    break;
                }
            }
            return index;
        }
    }
}