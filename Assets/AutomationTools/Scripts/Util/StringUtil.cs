using UnityEngine;
using System.Text;
using System;

namespace Sofunny.Tools.AutomationTools.Util {
    public static class StringUtil {
        private static StringBuilder stringBuilder = new StringBuilder();
        private static object lockObject = new object();

        public static string Concat(params object[] objects) {
            if (objects == null) {
                return string.Empty;
            }
            lock (lockObject) {
                stringBuilder.Length = 0;
                foreach (var item in objects) {
                    if (item != null) {
                        stringBuilder.Append(item);
                    }
                }
                return stringBuilder.ToString();
            }
        }
    }
}