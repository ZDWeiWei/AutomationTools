using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sofunny.Tools.AutomationTools.GamePlay {
    public class CharacterEntity : EntityBase {
        public interface IComponent {
            void Init(CharacterEntity entity);
            void Clear();
        }

        private List<IComponent> components = new List<IComponent>();
        public const int Event_W = 1;
        public const int Event_S = 2;
        public const int Event_A = 3;
        public const int Event_D = 4;
        public const int Event_Jump = 5;
        public const int Event_Fire = 6;

        public void Init(bool isLocalRole) {
            if (isLocalRole) {
                AddComponent<CharacterControllerMove>();
                AddComponent<CharacterLocalInput>();
            }
        }

        public void Clear() {
            RemoveComponent();
        }

        public void RemoveComponent() {
            foreach (var component in components) {
                component.Clear();
            }
            components.Clear();
        }

        public void AddComponent<T>() where T : IComponent, new() {
            IComponent feature = new T();
            components.Add(feature);
            feature.Init(this);
        }
    }
}