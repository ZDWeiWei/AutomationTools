using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.GamePlay;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public class ManagerBase : IGameWorld {
        public interface ISystem {
            void Init();
            void Clear();
        }
        public void Init() {
            OnInit();
        }

        virtual protected void OnInit() {
        }

        public void Clear() {
            OnClear();
        }

        virtual protected void OnClear() {
        }

        protected T AddSystem<T>() where T : ISystem, new() {
            ISystem system = new T();
            system.Init();
            return (T) system;
        }
    }
}