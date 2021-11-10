using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    [RequireComponent(typeof(ObjComponentKey))]
    public class ObjComponent : MonoBehaviour {
        public CameraEntity.AttrState[] Attrs;
    }
}
