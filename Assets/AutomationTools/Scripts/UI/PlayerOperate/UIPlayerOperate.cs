using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.UI {
    public class UIPlayerOperate : UIBase {
        public const int Event_W = 1;
        public const int Event_S = 2;
        public const int Event_A = 3;
        public const int Event_D = 4;
        public const int Event_Jump = 5;
        public const int Event_Fire = 6;
        
        [SerializeField]
        private BtnComponent BtnW;
        [SerializeField]
        private BtnComponent BtnS;
        [SerializeField]
        private BtnComponent BtnA;
        [SerializeField]
        private BtnComponent BtnD;
        [SerializeField]
        private BtnComponent BtnFire;
        [SerializeField]
        private BtnComponent BtnJump;

        override protected void OnInit() {
            BtnW.AddOnClick(KeyCode.W, OnBtnWCallBack);
            BtnS.AddOnClick(KeyCode.S, OnBtnSCallBack);
            BtnA.AddOnClick(KeyCode.A, OnBtnACallBack);
            BtnD.AddOnClick(KeyCode.D, OnBtnDCallBack);
            BtnJump.AddOnClick(KeyCode.Space,OnBtnJumpCallBack);
            BtnFire.AddOnClick(KeyCode.K, OnBtnFireCallBack);
        }

        override protected void OnClear() {
            BtnW.Clear();
            BtnS.Clear();
            BtnA.Clear();
            BtnD.Clear();
            BtnJump.Clear();
            BtnFire.Clear();
        }

        private void OnBtnWCallBack(bool isDown) {
            Dispatcher(Event_W, isDown);
        }

        private void OnBtnSCallBack(bool isDown) {
            Dispatcher(Event_S, isDown);
        }

        private void OnBtnACallBack(bool isDown) {
            Dispatcher(Event_A, isDown);
        }

        private void OnBtnDCallBack(bool isDown) {
            Dispatcher(Event_D, isDown);
        }

        private void OnBtnJumpCallBack(bool isDown) {
            Dispatcher(Event_Jump, isDown);
        }

        private void OnBtnFireCallBack(bool isDown) {
            Dispatcher(Event_Fire, isDown);
        }
    }
}