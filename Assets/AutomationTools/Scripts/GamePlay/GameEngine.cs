using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public class GameEngine : MonoBehaviour {
        void Start() {
            var gameWorldManager = new GameWorldManager();
            gameWorldManager.Init();
        }
    }
}