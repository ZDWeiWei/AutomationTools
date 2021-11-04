using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public partial class SystemBase {
        private Dictionary<int, List<Delegate>> handlers = new Dictionary<int, List<Delegate>>();

        public void Register(int id, Action handler) {
            RegisterDelegate(id, handler);
        }

        public void Register<T>(int id, Action<T> handler) {
            RegisterDelegate(id, handler);
        }

        public void Register<T1, T2>(int id, Action<T1, T2> handler) {
            RegisterDelegate(id, handler);
        }

        public void Register<T1, T2, T3>(int id, Action<T1, T2, T3> handler) {
            RegisterDelegate(id, handler);
        }

        public void Register<T1, T2, T3, T4>(int id, Action<T1, T2, T3, T4> handler) {
            RegisterDelegate(id, handler);
        }

        private void RegisterDelegate(int id, Delegate handler) {
            if (handler == null) {
                return;
            }
            List<Delegate> actionList;
            if (handlers.TryGetValue(id, out actionList) == false) {
                actionList = new List<Delegate>();
                handlers.Add(id, actionList);
            }
            actionList.Add(handler);
        }

        public void UnRegister(int id, Action handler) {
            UnRegisterDelegate(id, handler);
        }

        public void UnRegister<T>(int id, Action<T> handler) {
            UnRegisterDelegate(id, handler);
        }

        public void UnRegister<T1, T2>(int id, Action<T1, T2> handler) {
            UnRegisterDelegate(id, handler);
        }

        public void UnRegister<T1, T2, T3>(int id, Action<T1, T2, T3> handler) {
            UnRegisterDelegate(id, handler);
        }

        public void UnRegister<T1, T2, T3, T4>(int id, Action<T1, T2, T3, T4> handler) {
            UnRegisterDelegate(id, handler);
        }

        public void UnRegisterDelegate(int id, Delegate handler) {
            List<Delegate> actionList;
            if (handlers.TryGetValue(id, out actionList)) {
                for (int i = 0; i < actionList.Count; i++) {
                    var saveHandler = actionList[i];
                    if (handler == saveHandler) {
                        actionList.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        public void Dispatcher(int id) {
            if (handlers.TryGetValue(id, out List<Delegate> lists)) {
                for (int i = 0; i < lists.Count; i++) {
                    ((Action) lists[i]).Invoke();
                }
            }
        }

        public void Dispatcher<T>(int id, T data) {
            if (handlers.TryGetValue(id, out List<Delegate> lists)) {
                for (int i = 0; i < lists.Count; i++) {
                    ((Action<T>) lists[i]).Invoke(data);
                }
            }
        }

        public void Dispatcher<T1, T2>(int id, T1 data1, T2 data2) {
            if (handlers.TryGetValue(id, out List<Delegate> lists)) {
                for (int i = 0; i < lists.Count; i++) {
                    ((Action<T1, T2>) lists[i]).Invoke(data1, data2);
                }
            }
        }

        public void Dispatcher<T1, T2, T3>(int id, T1 data1, T2 data2, T3 data3) {
            if (handlers.TryGetValue(id, out List<Delegate> lists)) {
                for (int i = 0; i < lists.Count; i++) {
                    ((Action<T1, T2, T3>) lists[i]).Invoke(data1, data2, data3);
                }
            }
        }

        public void Dispatcher<T1, T2, T3, T4>(int id, T1 data1, T2 data2, T3 data3, T4 data4) {
            if (handlers.TryGetValue(id, out List<Delegate> lists)) {
                for (int i = 0; i < lists.Count; i++) {
                    ((Action<T1, T2, T3, T4>) lists[i]).Invoke(data1, data2, data3, data4);
                }
            }
        }
    }
}