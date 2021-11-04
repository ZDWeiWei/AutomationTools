using System;
using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.Asset;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public partial class SystemBase {
        public const int Event_CreateEntityComplete = -1;
        private List<IComponent> components = new List<IComponent>();
        private EntityBase entityBase;

        public interface IComponent {
            void Init(SystemBase entity);
            void Clear();
        }

        public void Init() {
            OnInit();
        }

        protected virtual void OnInit() {
        }

        public void Clear() {
            handlers.Clear();
            RemoveEntity();
            RemoveComponent();
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

        private void RemoveEntity() {
            if (entityBase == null) {
                return;
            }
            GameObject.Destroy(entityBase.gameObject);
            entityBase = null;
        }

        public void CreateEntity(string url) {
            var request = AssetManager.LoadGamePlayObjAsync(url);
            request.completed += operation => {
                var prefab = (GameObject) request.asset;
                if (prefab != null) {
                    var gameObj = GameObject.Instantiate(prefab);
                    Dispatcher(Event_CreateEntityComplete, gameObj);
                } else {
                    Debug.LogError("GamePlayRole 加载失败:");
                }
            };
        }
    }
}