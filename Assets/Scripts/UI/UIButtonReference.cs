using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;

namespace Mechadroids.UI {
    public class UIButtonReference: UIElementReference {
        [SerializeField] private TextMeshProUGUI textField;
        private Button button;
        public bool clicked;

        void Awake() {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);
            // button.clicked += clicked;
        }

        void OnClick() {
            clicked = true;
        }

        public void SetText(string text) {
            textField.text = text;
        }
    }
}
