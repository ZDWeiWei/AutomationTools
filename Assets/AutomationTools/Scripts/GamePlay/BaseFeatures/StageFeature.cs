using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.UIGameProto;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public class StageFeature : IFeature {
        public void Init() {
            GameProtoManager.AddListener(GameProtoDoc_Stage.EnterGameEnd.ID, OnEnterGameEndCallBack);
            GameProtoManager.Send(new GameProtoDoc_Stage.EnterGame());
        }

        public void Clear() {
            GameProtoManager.RemoveListener(GameProtoDoc_Stage.EnterGameEnd.ID, OnEnterGameEndCallBack);
        }

        public void OnEnterGameEndCallBack(IGameProtoDoc message) {
            
        }
    }
}