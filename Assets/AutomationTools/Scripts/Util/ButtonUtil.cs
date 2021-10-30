using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sofunny.Tools.AutomationTools.Util {
    public static class ButtonUtil {
        public static void AddTriggerListener(
            this GameObject gameObject,
            EventTriggerType eventID,
            UnityAction<BaseEventData> action) {
            if (!gameObject) {
                return;
            }
            var trigger = gameObject.GetComponent<EventTrigger>();
            if (trigger == null) {
                trigger = gameObject.AddComponent<EventTrigger>();
            }
            if (trigger.triggers == null) {
                trigger.triggers = new List<EventTrigger.Entry>();
            }
            var graphics = gameObject.GetComponent<Graphic>();
            if (graphics != null) {
                graphics.raycastTarget = true;
            }
            var callback = new UnityAction<BaseEventData>(action);
            var entry = new EventTrigger.Entry {
                eventID = eventID
            };
            entry.callback.AddListener(callback);
            trigger.triggers.Add(entry);
        }

        public static void RemoveTriggerListener(this GameObject gameObject) {
            if (!gameObject) {
                return;
            }
            var trigger = gameObject.GetComponent<EventTrigger>();
            if (trigger != null) {
                trigger.triggers.Clear();
            }
            var graphics = gameObject.GetComponent<Graphic>();
            if (graphics != null) {
                graphics.raycastTarget = false;
            }
        }
    }
}