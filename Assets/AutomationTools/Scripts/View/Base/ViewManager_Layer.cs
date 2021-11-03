using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.Asset;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.View {
    public partial class ViewManager {
        private readonly string[] layerInfos = {
            "Login","0", 
            "PlayerOperate","0"
        };
        private Dictionary<string, UIRoot.UIRootEnum> layerData = new Dictionary<string, UIRoot.UIRootEnum>();

        private void InitLayer() {
            AddUIRoot();
            InitLayerInfo();
        }

        private void AddUIRoot() {
            var uiRootGameObj = AssetManager.LoadUIRoot();
            Debug.Log(uiRootGameObj);
            uiRootGameObj = GameObject.Instantiate(uiRootGameObj);
            uiRoot = uiRootGameObj.GetComponent<UIRoot>();
        }

        private void InitLayerInfo() {
            for (int i = 0; i < layerInfos.Length; i += 2) {
                var sign = layerInfos[i];
                var rootEnum = (UIRoot.UIRootEnum) int.Parse(layerInfos[i + 1]);
                layerData.Add(sign, rootEnum);
            }
        }

        private GameObject GetUILayer(UIRoot.UIRootEnum type) {
            return uiRoot.Get(type);
        }

        private void UpdateLayer(ViewBase viewBase) {
            UIRoot.UIRootEnum rootEnum;
            if (layerData.TryGetValue(viewBase.FunctionSign, out rootEnum)) {
                var layer = uiRoot.Get(rootEnum);
                viewBase.SetLayer(layer.transform, rootEnum);
            } else {
                Debug.LogError("没有设置对应 UI 层级：" + viewBase.FunctionSign);
            }
        }
    }
}