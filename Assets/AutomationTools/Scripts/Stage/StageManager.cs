using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.UIGameProto;
using Sofunny.Tools.AutomationTools.Util;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sofunny.Tools.AutomationTools.Stage {
    public class StageManager {
        private AsyncOperation async;
        private List<GameProtoDoc_Stage.LoadStage> loadList = new List<GameProtoDoc_Stage.LoadStage>();
        private string loadSign = "";

        public void Init() {
            GameProtoManager.AddListener(GameProtoDoc_Stage.LoadStage.ID, OnLoadStageCallBack);
            ATUpdateRegister.AddUpdate(OnUpdate);
        }

        public void Clear() {
            GameProtoManager.RemoveListener(GameProtoDoc_Stage.LoadStage.ID, OnLoadStageCallBack);
            ATUpdateRegister.RemoveUpdate(OnUpdate);
        }

        public void OnUpdate(float delta) {
            if (async == null) {
                return;
            }
            if (async.isDone) {
                async = null;
                Debug.Log("加载 Stage 完成" + loadSign);
                GameProtoManager.Send(new GameProtoDoc_Stage.LoadStageComponent {
                    sign = loadSign,
                });
                RenderLoadList();
            }
        }

        private void OnLoadStageCallBack(IGameProtoDoc message) {
            var data = (GameProtoDoc_Stage.LoadStage) message;
            loadList.Add(data);
            if (async == null) {
                RenderLoadList();
            }
        }

        private void RenderLoadList() {
            if (loadList.Count <= 0) {
                return;
            }
            var data = loadList[0];
            loadList.RemoveAt(0);
            LoadScene(data.sign, data.mode);
        }

        private void LoadScene(string sign, LoadSceneMode mode) {
            Debug.LogFormat("加载场景-->{0}", sign);
            loadSign = sign;
            async = SceneManager.LoadSceneAsync(sign, mode);
        }
    }
}