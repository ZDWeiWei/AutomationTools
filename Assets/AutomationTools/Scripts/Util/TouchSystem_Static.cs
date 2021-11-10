using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.Util {
    public partial class TouchSystem {
        private static TouchSystem instance;

        public static void Register(TouchDelegate handler) {
            if (instance == null) {
                return;
            }
            instance.OnRegister(handler);
        }

        public static void Unregister(TouchDelegate handler) {
            if (instance == null) {
                return;
            }
            instance.OnUnregister(handler);
        }
    }
}