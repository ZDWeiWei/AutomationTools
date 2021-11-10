using System.Collections;
using System.Collections.Generic;
using Sofunny.Tools.AutomationTools.UIGameProto;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public class BulletManager : ManagerBase {
        private Dictionary<int, BulletSystem> bulletData = new Dictionary<int, BulletSystem>();
        private int bulletIndex = 0;

        override protected void OnInit() {
            GameProtoManager.AddListener(GameProtoDoc_Bullet.CreateBullet.ID, OnCreateBulletCallBack);
            GameProtoManager.AddListener(GameProtoDoc_Bullet.RemoveBullet.ID, OnRemoveBulletCallBack);
        }

        override protected void OnClear() {
            GameProtoManager.RemoveListener(GameProtoDoc_Bullet.CreateBullet.ID, OnCreateBulletCallBack);
            GameProtoManager.RemoveListener(GameProtoDoc_Bullet.RemoveBullet.ID, OnRemoveBulletCallBack);
            ClearAllBullet();
        }

        private void OnCreateBulletCallBack(IGameProtoDoc message) {
            var index = bulletIndex++;
            var bulletSystem = AddSystem<BulletSystem>();
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

        private void ClearAllBullet() {
            foreach (var value in bulletData.Values) {
                value.Clear();
            }
            bulletData.Clear();
        }
    }
}