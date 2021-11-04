using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.Asset;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.View {
    public partial class ViewManager {
        public enum UIRootEnum {
            Base,
            Loading,
            Tip,
            None,
        }
        private readonly string[] layerInfos = {
            "Login","0", 
            "PlayerOperate","0"
        };
        private Dictionary<string, UIRootEnum> layerData = new Dictionary<string, UIRootEnum>();

        private void InitLayer() {
            AddUIRoot();
            InitLayerInfo();
        }

        private void AddUIRoot() {
            var uiRootGameObj = AssetManager.LoadUIRoot();
            uiRootGameObj = GameObject.Instantiate(uiRootGameObj);
            uiRoot = uiRootGameObj.GetComponent<UIRoot>();
            GameObject.DontDestroyOnLoad(uiRootGameObj);
        }

        private void InitLayerInfo() {
            for (int i = 0; i < layerInfos.Length; i += 2) {
                var sign = layerInfos[i];
                var rootEnum = (UIRootEnum) int.Parse(layerInfos[i + 1]);
                layerData.Add(sign, rootEnum);
            }
        }

        private GameObject GetUILayer(UIRootEnum type) {
            return Get(type);
        }

        private void UpdateLayer(ViewBase viewBase) {
            UIRootEnum rootEnum;
            if (layerData.TryGetValue(viewBase.FunctionSign, out rootEnum)) {
                var layer = Get(rootEnum);
                viewBase.SetLayer(layer.transform, rootEnum);
            } else {
                Debug.LogError("没有设置对应 UI 层级：" + viewBase.FunctionSign);
            }
        }
        
        public GameObject Get(UIRootEnum type) {
            switch (type) {
                case UIRootEnum.Base:
                    return uiRoot.BaseLayer;
                case UIRootEnum.Loading:
                    return uiRoot.LoadingLayer;
                case UIRootEnum.Tip:
                    return uiRoot.TipLayer;
            }
            return null;
        }
    }
}