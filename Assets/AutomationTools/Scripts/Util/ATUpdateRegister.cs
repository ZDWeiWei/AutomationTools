using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.Util;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.Util {
    public delegate void UpdateDelegate(float delta);

    public partial class ATUpdateRegister {
        private List<WrapperUpdate> m_Updates = new List<WrapperUpdate>(64);
        private List<WrapperUpdate> m_RemoveUpdates = new List<WrapperUpdate>(16);
        private List<WrapperUpdate> m_FixedUpdates = new List<WrapperUpdate>(64);
        private List<WrapperUpdate> m_RemoveFixedUpdates = new List<WrapperUpdate>(16);
        private List<WrapperUpdate> m_LateUpdates = new List<WrapperUpdate>(64);
        private List<WrapperUpdate> m_RemoveLateUpdates = new List<WrapperUpdate>(16);

        public void Init() {
            instance = this;
        }

        public void Clear() {
            m_Updates.Clear();
            m_RemoveUpdates.Clear();
            m_FixedUpdates.Clear();
            m_RemoveFixedUpdates.Clear();
            m_LateUpdates.Clear();
            m_RemoveLateUpdates.Clear();
            instance = null;
        }

        public void OnUpdate(float delta) {
            TickRemoveUpdates(m_Updates, m_RemoveUpdates);
            TickUpdates(m_Updates, delta);
        }

        public void OnFixUpdate(float delta) {
            TickRemoveUpdates(m_FixedUpdates, m_RemoveFixedUpdates);
            TickUpdates(m_FixedUpdates, delta);
        }

        public void OnLateUpdate(float delta) {
            TickRemoveUpdates(m_LateUpdates, m_RemoveLateUpdates);
            TickUpdates(m_LateUpdates, delta);
        }

        public void RegisterUpdate(UpdateDelegate handler) {
            Register(m_Updates, handler);
        }

        public void UnregisterUpdate(UpdateDelegate handler) {
            Unregister(m_Updates, m_RemoveUpdates, handler);
        }

        public void RegisterFixedUpdate(UpdateDelegate handler) {
            Register(m_FixedUpdates, handler);
        }

        public void UnregisterFixedUpdate(UpdateDelegate handler) {
            Unregister(m_FixedUpdates, m_RemoveFixedUpdates, handler);
        }

        public void RegisterLateUpdate(UpdateDelegate handler) {
            Register(m_LateUpdates, handler);
        }

        public void UnregisterLateUpdate(UpdateDelegate handler) {
            Unregister(m_LateUpdates, m_RemoveUpdates, handler);
        }

        private void Register(List<WrapperUpdate> updates, UpdateDelegate handler) {
            if (SearchUpdateIndex(updates, handler) == -1) {
                updates.Add(new WrapperUpdate(handler));
            }
        }

        private void Unregister(
            List<WrapperUpdate> updates, 
            List<WrapperUpdate> removeUpdates,
            UpdateDelegate handler) {
            int index = SearchUpdateIndex(updates, handler);
            if (index >= 0) {
                var w = updates[index];
                w.IsRemove = true;
                updates[index] = w;
                removeUpdates.Add(w);
            }
        }

        private int SearchUpdateIndex(List<WrapperUpdate> updates, UpdateDelegate handler) {
            var index = -1;
            var length = updates.Count;
            for (var i = 0; i < length; ++i) {
                if (updates[i].IsEquals(handler)) {
                    index = i;
                    break;
                }
            }
            return index;
        }

        private void TickUpdates(List<WrapperUpdate> updates, float delta) {
            var length = updates.Count;
            for (var i = 0; i < length; ++i) {
                var handler = updates[i];
                handler.Invoke(delta);
            }
        }

        private void TickRemoveUpdates(List<WrapperUpdate> updates, List<WrapperUpdate> removeUpdates) {
            var length = removeUpdates.Count;
            if (length > 0) {
                for (var i = 0; i < length; ++i) {
                    var w = removeUpdates[i];
                    var index = SearchUpdateIndex(updates, w.Handler);
                    if (index >= 0) {
                        updates.RemoveAt(index);
                    }
                }
                removeUpdates.Clear();
            }
        }

        private struct WrapperUpdate {
            public string name;
            public bool IsRemove;
            public UpdateDelegate Handler;

            public WrapperUpdate(UpdateDelegate handler) {
                IsRemove = false;
                Handler = handler;
                name = StringUtil.Concat(Handler.Target.GetType(), ".", Handler.Method.Name);
            }

            public void Invoke(float delta) {
                if (!IsRemove) {
                    //可以在这里监控所有 update 耗时
                    Handler.Invoke(delta);
                }
            }

            public bool IsEquals(UpdateDelegate handler) {
                return Handler == handler;
            }
        }
    }
}