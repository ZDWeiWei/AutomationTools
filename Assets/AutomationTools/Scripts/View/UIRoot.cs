using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.View {
    public class UIRoot : MonoBehaviour {
        public enum UIRootEnum {
            Base,
            Loading,
            Tip,
            
            None,
        }

        [SerializeField]
        private GameObject BaseLayer;
        [SerializeField]
        private GameObject LoadingLayer;
        [SerializeField]
        private GameObject TipLayer;

        public GameObject Get(UIRootEnum type) {
            switch (type) {
                case UIRootEnum.Base:
                    return BaseLayer;
                case UIRootEnum.Loading:
                    return LoadingLayer;
                case UIRootEnum.Tip:
                    return TipLayer;
            }
            return null;
        }
    }
}