using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBase : MonoBehaviour {
    private Dictionary<int, Delegate> handlers = new Dictionary<int, Delegate>();

    void OnDestroy() {
        handlers.Clear();
    }

    public void Register(int id, Action handler) {
        RegisterDelegate(id, handler);
    }

    public void Register<T>(int id, Action<T> handler) {
        RegisterDelegate(id, handler);
    }

    public void Register<T1, T2>(int id, Action<T1, T2> handler) {
        RegisterDelegate(id, handler);
    }

    public void Register<T1, T2, T3>(int id, Action<T1, T2, T3> handler) {
        RegisterDelegate(id, handler);
    }

    public void Register<T1, T2, T3, T4>(int id, Action<T1, T2, T3, T4> handler) {
        RegisterDelegate(id, handler);
    }

    public void UnRegister(int id) {
        if (handlers.ContainsKey(id) == false) {
            return;
        }
        handlers.Remove(id);
    }

    private void RegisterDelegate(int id, Delegate handler) {
        if (handler == null) {
            return;
        }
        if (handlers.ContainsKey(id)) {
            return;
        }
        handlers.Add(id, handler);
    }

    public void Dispatcher(int id) {
        if (handlers.TryGetValue(id, out Delegate handler)) {
            ((Action) handler).Invoke();
        }
    }

    public void Dispatcher<T>(int id, T data) {
        if (handlers.TryGetValue(id, out Delegate handler)) {
            ((Action<T>) handler).Invoke(data);
        }
    }

    public void Dispatcher<T1, T2>(int id, T1 data1, T2 data2) {
        if (handlers.TryGetValue(id, out Delegate handler)) {
            ((Action<T1, T2>) handler).Invoke(data1, data2);
        }
    }

    public void Dispatcher<T1, T2, T3>(int id, T1 data1, T2 data2, T3 data3) {
        if (handlers.TryGetValue(id, out Delegate handler)) {
            ((Action<T1, T2, T3>) handler).Invoke(data1, data2, data3);
        }
    }

    public void Dispatcher<T1, T2, T3, T4>(int id, T1 data1, T2 data2, T3 data3, T4 data4) {
        if (handlers.TryGetValue(id, out Delegate handler)) {
            ((Action<T1, T2, T3, T4>) handler).Invoke(data1, data2, data3, data4);
        }
    }
}