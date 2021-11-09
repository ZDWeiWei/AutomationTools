using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.UIGameProto;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public class BulletManager : IGameWorld {
        private Dictionary<int, BulletSystem> bulletData = new Dictionary<int, BulletSystem>();
        private int bulletIndex = 0;

        public void Init() {
            GameProtoManager.AddListener(GameProtoDoc_Bullet.CreateBullet.ID, OnCreateBulletCallBack);
            GameProtoManager.AddListener(GameProtoDoc_Bullet.RemoveBullet.ID, OnRemoveBulletCallBack);
        }

        public void Clear() {
            GameProtoManager.RemoveListener(GameProtoDoc_Bullet.CreateBullet.ID, OnCreateBulletCallBack);
            GameProtoManager.RemoveListener(GameProtoDoc_Bullet.RemoveBullet.ID, OnRemoveBulletCallBack);
        }

        private void OnCreateBulletCallBack(IGameProtoDoc message) {
            var index = bulletIndex++;
            var bulletSystem = new BulletSystem();
            bulletSystem.Init();
            bulletSystem.SetData(index, (GameProtoDoc_Bullet.CreateBullet) message);
            bulletData.Add(index, bulletSystem);
        }

        private void OnRemoveBulletCallBack(IGameProtoDoc message) {
            var data = (GameProtoDoc_Bullet.RemoveBullet) message;
            BulletSystem bulletSystem;
            if (bulletData.TryGetValue(data.BulletId, out bulletSystem)) {
                bulletSystem.Clear();
                bulletData.Remove(data.BulletId);
            }
        }
    }
}