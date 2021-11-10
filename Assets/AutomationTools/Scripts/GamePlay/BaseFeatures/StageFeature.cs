using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.UIGameProto;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public class StageFeature : IFeature {
        public void Init() {
            GameProtoManager.AddListener(GameProtoDoc_Stage.LoadStageComponent.ID, OnLoadStageComponentCallBack);
            GameProtoManager.Send(new GameProtoDoc_Stage.LoadStage {
                sign = "Game",
                mode = LoadSceneMode.Single
            });
        }

        public void Clear() {
            GameProtoManager.RemoveListener(GameProtoDoc_Stage.LoadStageComponent.ID, OnLoadStageComponentCallBack);
        }

        public void OnLoadStageComponentCallBack(IGameProtoDoc message) {
            
        }
    }
}