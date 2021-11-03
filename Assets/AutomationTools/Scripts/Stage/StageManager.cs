using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.UIGameProto;
using Sofunny.Tools.AutomationTools.Util;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sofunny.Tools.AutomationTools.Stage {
    public class StageManager {
        private AsyncOperation async;

        public void Init() {
            GameProtoManager.AddListener(GameProtoDoc_Stage.EnterGame.ID, OnEnterGameCallBack);
            ATUpdateRegister.AddUpdate(OnUpdate);
        }

        public void Clear() {
            GameProtoManager.RemoveListener(GameProtoDoc_Stage.EnterGame.ID, OnEnterGameCallBack);
            ATUpdateRegister.RemoveUpdate(OnUpdate);
        }

        public void OnUpdate(float delta) {
            if (async == null) {
                return;
            }
            if (async.isDone) {
                async = null;
                GameProtoManager.Send(new GameProtoDoc_Stage.EnterGameEnd());
            } else {
                Debug.Log("加载：" + async.progress);
            }
        }

        private void OnEnterGameCallBack(IGameProtoDoc message) {
            LoadScene("Game");
        }

        private void LoadScene(string url) {
            Debug.LogFormat("加载场景-->{0}", url);
            async = SceneManager.LoadSceneAsync(url, LoadSceneMode.Additive);
        }
    }
}