using UnityEngine;
using System;

namespace Mechadroids.UI {

    public enum UIElementType {
        Single = 0,
        Multiple = 1,
        Button = 2
    }

    [CreateAssetMenu(menuName = "Mechadroids/UIPrefabs", fileName = "UIPrefabs", order = 0)]
    public class UIPrefabs : ScriptableObject {
        public MenuReference mainMenuReferencePrefab;
        public MenuReference debugMenuReferencePrefab;

        [Tooltip("UIElementReference prefabs for each UIElementType")]
        [SerializeField] private UIElementReference [] uiElementReferencePrefabs;

        // add menu ui prefabs here as well

        public T GetUIElementReference<T>(UIElementType type) where T: UIElementReference {
            return (T)uiElementReferencePrefabs[(int)type];
        }
    }
}
