using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.Util {
    public partial class ATUpdateRegister {
        private static ATUpdateRegister instance;

        public static void AddUpdate(UpdateDelegate update) {
            if (instance == null) {
                return;
            }
            instance.RegisterUpdate(update);
        }

        public static void RemoveUpdate(UpdateDelegate update) {
            if (instance == null) {
                return;
            }
            instance.UnregisterUpdate(update);
        }

        public static void AddFixedUpdate(UpdateDelegate update) {
            if (instance == null) {
                return;
            }
            instance.RegisterFixedUpdate(update);
        }

        public static void RemoveFixedUpdate(UpdateDelegate update) {
            if (instance == null) {
                return;
            }
            instance.UnregisterFixedUpdate(update);
        }

        public static void AddLateUpdate(UpdateDelegate update) {
            if (instance == null) {
                return;
            }
            instance.RegisterLateUpdate(update);
        }

        public static void RemoveLateUpdate(UpdateDelegate update) {
            if (instance == null) {
                return;
            }
            instance.UnregisterLateUpdate(update);
        }

        public static void AddInvoke(InvokeDelegate handler, float invokeTime) {
            if (instance == null) {
                return;
            }
            instance.RegisterInvoke(handler, invokeTime);
        }
    }
}