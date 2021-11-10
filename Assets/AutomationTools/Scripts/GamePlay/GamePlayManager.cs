using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.UIGameProto;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public interface IFeature {
        void Init();
        void Clear();
    }

    public class GamePlayManager {
        private List<IFeature> features = new List<IFeature>();

        public void Init() {
            GameProtoManager.AddListener(GameProtoDoc_StartGame.EnterGame.ID, OnEnterGameCallBack);
            GameProtoManager.AddListener(GameProtoDoc_StartGame.QuitGame.ID, OnQuitGameCallBack);
        }

        public void Clear() {
            GameProtoManager.RemoveListener(GameProtoDoc_StartGame.EnterGame.ID, OnEnterGameCallBack);
            GameProtoManager.RemoveListener(GameProtoDoc_StartGame.QuitGame.ID, OnQuitGameCallBack);
        }

        private void OnEnterGameCallBack(IGameProtoDoc message) {
            AddFeature<StageFeature>();
        }

        private void OnQuitGameCallBack(IGameProtoDoc message) {
            RemoveFeature();
        }

        public void RemoveFeature() {
            foreach (var feature in features) {
                feature.Clear();
            }
            features.Clear();
        }

        public void AddFeature<T>() where T : IFeature, new() {
            IFeature feature = new T();
            features.Add(feature);
            feature.Init();
        }
    }
}