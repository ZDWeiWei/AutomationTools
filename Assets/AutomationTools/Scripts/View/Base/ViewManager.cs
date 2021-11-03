using System;
using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.Asset;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.View {
    public interface IFeature {
        void Init();
        void Clear();
    }
    
    public partial class ViewManager {
        public static ViewManager Instance {
            get;
            private set;
        }

        private List<ViewBase> views = new List<ViewBase>();
        private List<IFeature> features = new List<IFeature>();
        private UIRoot uiRoot;

        public void Init() {
            Instance = this;
            InitLayer();
            InitFeature();
            Open<LoginView>(null);
        }

        public void Clear() {
            RemoveRegisterFeature();
        }

        public void Open<T>(Action<ViewBase.OpenStateEnum> callBack) where T : ViewBase, new() {
            var viewBase = Get<T>();
            if (viewBase == null) {
                var typeName = typeof(T).Name;
                viewBase = new T();
                var functionName = typeName.Replace("View", "");
                viewBase.Init(functionName);
                UpdateLayer(viewBase);
                views.Add(viewBase);
            }
            viewBase.OpenASync(callBack);
        }

        public void Close<T>() where T : ViewBase {
            for (var i = 0; i < views.Count; i++) {
                var controller = views[i];
                if (controller is T) {
                    var viewBase = controller as T;
                    viewBase.Close();
                    views.RemoveAt(i);
                    break;
                }
            }
        }

        public T Get<T>() where T : ViewBase {
            for (var i = 0; i < views.Count; i++) {
                var controller = views[i];
                if (controller is T) {
                    return controller as T;
                }
            }
            return null;
        }

        public void InitFeature() {
            AddRegisterFeature<PlayerOperrateViewFeature>();
        }
        
        public void AddRegisterFeature<T>() where T : IFeature, new() {
            IFeature feature = new T();
            features.Add(feature);
            feature.Init();
        }
        public void RemoveRegisterFeature() {
            foreach (var feature in features) {
                feature.Clear();
            }
            features.Clear();
        }
    }
}