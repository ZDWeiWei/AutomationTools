using System;
using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.Asset;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public partial class SystemBase {
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
        private GameObject entityObj;

        public void Init() {
            OnInit();
        }

        protected virtual void OnInit() {
        }

        public void Clear() {
            handlers.Clear();
            RemoveComponent();
            ClearEntity();
            RemoveEntityObj();
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

        public void CreateEntityObj(string url) {
            var request = AssetManager.LoadGamePlayObjAsync(url);
            request.completed += operation => {
                var prefab = (GameObject) request.asset;
                if (prefab != null) {
                    entityObj = GameObject.Instantiate(prefab);
                    Dispatcher(Event_CreateEntityComplete, entityObj);
                } else {
                    Debug.LogError("GamePlayRole 加载失败:");
                }
            };
        }

        private void RemoveEntityObj() {
            if (entityObj == null) {
                return;
            }
            GameObject.Destroy(entityObj);
            entityObj = null;
        }
    }
}