using System;
using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.Util;
using Sofunny.Tools.AutomationTools.View;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.Main {
    public sealed class ATMain : MonoBehaviour {
        private readonly Queue<Task> tasks = new Queue<Task>();
        private ATUpdateRegister updateRegister;
        private ViewManager viewManager;

        void Start() {
            tasks.Enqueue(new Task("初始化 UpdateRegister", InitUpdateRegister));
            tasks.Enqueue(new Task("初始化 View", InitView));
        }
        
        void Update() {
            if (updateRegister != null) {
                updateRegister.OnUpdate(Time.deltaTime);
            }
            if (tasks.Count > 0) {
                var task = tasks.Dequeue();
                task.OnStart();
            }
        }

        void FixedUpdate() {
            if (updateRegister != null) {
                updateRegister.OnFixUpdate(Time.fixedTime);
            }
        }

        void LateUpdate() {
            if (updateRegister != null) {
                updateRegister.OnLateUpdate(Time.deltaTime);
            }
        }

        void InitUpdateRegister() {
            updateRegister = new ATUpdateRegister();
            updateRegister.Init();
        }

        void InitView() {
            viewManager = new ViewManager();
            viewManager.Init();
        }

        /// <summary>
        /// 创建启动任务
        /// </summary>
        private class Task {
            protected readonly string taskName;
            protected readonly Action action;

            public Task(string taskName, Action action) {
                this.taskName = taskName;
                this.action = action;
            }

            public virtual void OnStart() {
                Debug.Log(taskName);
                try {
                    action?.Invoke();
                } catch (Exception ex) {
                    Debug.LogErrorFormat("启动任务异常:{0} ,exception:{1}", taskName, ex);
                }
            }
        }
    }
}