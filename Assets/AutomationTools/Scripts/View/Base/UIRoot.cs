using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.View {
    public class UIRoot : MonoBehaviour {
        [SerializeField]
        private GameObject baseLayer;
        public GameObject BaseLayer {
            get { return baseLayer; }
        }
        [SerializeField]
        private GameObject loadingLayer;
        public GameObject LoadingLayer {
            get { return loadingLayer; }
        }
        [SerializeField]
        private GameObject tipLayer;
        public GameObject TipLayer {
            get { return tipLayer; }
        }
    }
}