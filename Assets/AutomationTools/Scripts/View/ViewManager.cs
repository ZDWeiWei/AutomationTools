using System;
using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.Asset;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.View {
    public partial class ViewManager {
        public static ViewManager Instance {
            get;
            private set;
        }
        private List<ViewBase> views = new List<ViewBase>();
        private UIRoot uiRoot;

        public void Init() {
            Instance = this;
            InitLayer();
            Open<UILoginView>(null);
        }

        public void Open<T>(Action<ViewBase.OpenStateEnum> callBack) where T : ViewBase, new() {
            var viewBase = Get<T>();
            if (viewBase == null) {
                var typeName = typeof(T).Name;
                viewBase = new T();
                var functionName = typeName.Replace("UI", "");
                functionName = functionName.Replace("View", "");
                viewBase.Init(functionName);
                UpdateLayer(viewBase);
                views.Add(viewBase);
            }
            viewBase.OpenASync(callBack);
        }

        public void Close<T>() where T : ViewBase, new() {
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
    }
}