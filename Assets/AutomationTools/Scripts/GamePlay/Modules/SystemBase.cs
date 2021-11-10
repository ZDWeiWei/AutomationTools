using System;
using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.Asset;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public partial class SystemBase : ManagerBase.ISystem {
        public interface IComponent {
            void Init(SystemBase system);
            void Clear();
        }

        public interface IEntity {
            void Init(SystemBase system);
            void Clear();
        }

        public const int Event_CreateEntityComplete = -1;
        private List<IComponent> components = new List<IComponent>();
        private IEntity entity;
        private bool init = false;

        public void Init() {
            if (init) {
                Debug.LogError("重复初始化");
                return;
            }
            init = true;
            OnInit();
        }

        protected virtual void OnInit() {
        }

        public void Clear() {
            init = false;
            handlers.Clear();
            RemoveComponent();
            ClearEntity();
            OnClear();
        }

        protected virtual void OnClear() {
        }

        public void AddComponent<T>() where T : IComponent, new() {
            IComponent feature = new T();
            components.Add(feature);
            feature.Init(this);
        }

        public void RemoveComponent() {
            foreach (var component in components) {
                component.Clear();
            }
            components.Clear();
        }

        public T AddEntity<T>() where T : IEntity, new() {
            entity = new T();
            entity.Init(this);
            return (T) entity;
        }

        public void ClearEntity() {
            if (entity == null) {
                return;
            }
            entity.Clear();
            entity = null;
        }
    }
}