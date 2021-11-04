using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public class GameWorldLayerEntity : MonoBehaviour {
        [SerializeField]
        private Transform mapLayer;
        public Transform MapLayer {
            get { return mapLayer; }
        }
        [SerializeField]
        private Transform characterLayer;
        public Transform CharacterLayer {
            get { return characterLayer; }
        }
        [SerializeField]
        private Transform baseLayer;
        public Transform BaseLayer {
            get { return baseLayer; }
        }
    }
}