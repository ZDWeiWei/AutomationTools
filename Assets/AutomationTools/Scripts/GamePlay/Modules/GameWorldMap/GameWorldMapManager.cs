using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.UIGameProto;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public class GameWorldMapManager : IGameWorld {
        private string loadSign = "TestMap";

        public void Register() {
            GameProtoManager.AddListener(GameProtoDoc_Stage.LoadStageComponent.ID, OnLoadStageComponentCallBack);
        }

        public void Init() {
            GameProtoManager.Send(new GameProtoDoc_Stage.LoadStage {
                sign = loadSign, mode = LoadSceneMode.Additive
            });
        }

        public void Clear() {
            GameProtoManager.RemoveListener(GameProtoDoc_Stage.LoadStageComponent.ID, OnLoadStageComponentCallBack);
        }

        private void OnLoadStageComponentCallBack(IGameProtoDoc message) {
            var data = (GameProtoDoc_Stage.LoadStageComponent) message;
            if (data.sign != loadSign) {
                return;
            }
            Debug.Log("子场景加载成功:" + data.sign);
            var scene = SceneManager.GetSceneByName(loadSign);
            SceneManager.SetActiveScene(scene);
        }
    }
}