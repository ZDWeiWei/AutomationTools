using System;
using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.Util;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.Asset {
    public class AssetManager {

        public static ResourceRequest LoadUI(string functionSign) {
            var url = StringUtil.Concat(URI.UI, "/UI", functionSign);
            Debug.Log("LoadUI:" + url);
            return LoadAssetAsync<GameObject>(url);
        }

        public static GameObject LoadUIRoot() {
            var url = StringUtil.Concat(URI.Root, "/ATUIRoot");
            return LoadAsset<GameObject>(url);
        }
        
        public static T LoadAsset<T>(string url) where T : UnityEngine.Object {
            return Resources.Load<T>(url);
        }
        
        public static ResourceRequest LoadAssetAsync<T>(string url) where T : UnityEngine.Object {
            return Resources.LoadAsync<T>(url);
        }
    }
}
