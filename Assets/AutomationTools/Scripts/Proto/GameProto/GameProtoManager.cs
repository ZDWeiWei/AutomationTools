using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.UIGameProto {
    public delegate void ProtoCallBack(IGameProtoCallBackData message);
    public interface IGameProtoDoc {
        string GetID();
    }
    public interface IGameProtoCallBackData {
    }

    public static class GameProtoManager {
        private static Dictionary<string, List<Action<IGameProtoDoc>>> listenerList =
            new Dictionary<string, List<Action<IGameProtoDoc>>>();

        public static void AddListener(string ID, Action<IGameProtoDoc> callBack) {
            List<Action<IGameProtoDoc>> list;
            if (listenerList.TryGetValue(ID, out list) == false) {
                list = new List<Action<IGameProtoDoc>>();
                listenerList.Add(ID, list);
            }
            for (int i = 0; i < list.Count; i++) {
                var saveCallBack = list[i];
                if (saveCallBack == callBack) {
                    Debug.LogError("同一个回调不能重复注册:" + ID);
                    return;
                }
            }
            list.Add(callBack);
        }

        public static void RemoveListener(string ID, Action<IGameProtoDoc> callBack) {
            List<Action<IGameProtoDoc>> list;
            if (listenerList.TryGetValue(ID, out list) == false) {
                list = new List<Action<IGameProtoDoc>>();
                listenerList.Add(ID, list);
            }
            for (int i = 0; i < list.Count; i++) {
                var saveCallBack = list[i];
                if (saveCallBack == callBack) {
                    list.RemoveAt(i);
                    return;
                }
            }
        }

        public static void Send(IGameProtoDoc message) {
            var id = message.GetID();
            List<Action<IGameProtoDoc>> list;
            if (listenerList.TryGetValue(id, out list) == false) {
                list = new List<Action<IGameProtoDoc>>();
                listenerList.Add(id, list);
            }
            for (int i = list.Count - 1; i >= 0; i--) {
                var saveCallBack = list[i];
                saveCallBack.Invoke(message);
            }
        }
    }
}