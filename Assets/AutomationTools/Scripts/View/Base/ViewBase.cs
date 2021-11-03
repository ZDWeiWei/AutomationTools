using System;
using Sofunny.Tools.AutomationTools.Asset;
using Sofunny.Tools.AutomationTools.UI;
using Sofunny.Tools.AutomationTools.Util;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.View {
    public class ViewBase {
        public enum OpenStateEnum {
            Open,
            Close,
            Opening,
            Closeing,
        }

        public OpenStateEnum OpenState {
            get { return openState; }
        }
        public string FunctionSign {
            get;
            private set;
        }
        private OpenStateEnum openState = OpenStateEnum.Close;
        private Action<OpenStateEnum> onOpenCallBack;
        private UIRoot.UIRootEnum myLayer = UIRoot.UIRootEnum.None;
        private Transform layerParent;
        private UIBase ui;

        public void Init(string sign) {
            Debug.Log("Open View:" + sign);
            FunctionSign = sign;
        }

        public void OpenASync(Action<OpenStateEnum> callBack) {
            Debug.Log("开始打开界面:" + FunctionSign);
            onOpenCallBack = callBack;
            openState = OpenStateEnum.Opening;
            var request = AssetManager.LoadUI(FunctionSign);
            request.completed += operation => {
                var prefab = (GameObject) request.asset;
                if (prefab != null) {
                    var uiGameObj = GameObject.Instantiate<GameObject>(prefab);
                    ui = uiGameObj.GetComponent<UIBase>();
                    ui.SetParent(layerParent);
                    RegisterUIEvent(ui);
                    ui.Init();
                    openState = OpenStateEnum.Open;
                    Debug.Log("打开界面成功:" + FunctionSign);
                    if (callBack != null) {
                        callBack(openState);
                    }
                } else {
                    Debug.LogError("OpenASync 失败:" + FunctionSign);
                }
            };
        }

        virtual protected void RegisterUIEvent(UIBase ui) {
        }

        public void SetLayer(Transform parent, UIRoot.UIRootEnum layer) {
            if (myLayer == layer) {
                return;
            }
            myLayer = layer;
            layerParent = parent;
            if (ui != null) {
                ui.SetParent(parent);
            }
        }

        public void Close() {
            if (ui == null) {
                return;
            }
            // 隔一帧执行，同步执行，部分按钮事件会出现按下的时候事件被清除，导致报错
            ATUpdateRegister.AddInvoke(InvokeClose, 0f);
        }

        private void InvokeClose() {
            if (ui == null) {
                return;
            }
            openState = OpenStateEnum.Closeing;
            var gameObj = ui.gameObject;
            ui.Clear();
            GameObject.Destroy(gameObj);
            openState = OpenStateEnum.Close;
            ui = null;
        }
    }
}