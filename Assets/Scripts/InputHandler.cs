using UnityEngine;
using UnityEngine.InputSystem;

namespace Mechadroids {
    /// <summary>
    /// Handles input state from the Input System
    /// </summary>
    public class InputHandler {
        private InputActions inputActions;

        public Vector2 MovementInput { get; private set; }
        public Vector2 MouseDelta { get; private set; }
        public InputActions InputActions => inputActions;
        public bool cursorState;

        public void Initialize() {
            inputActions = new InputActions();
            inputActions.Enable();
            inputActions.Player.Move.performed += ctx => MovementInput = ctx.ReadValue<Vector2>();
            inputActions.Player.Move.canceled += ctx => MovementInput = Vector2.zero;
            inputActions.Player.Look.performed += ctx => MouseDelta = ctx.ReadValue<Vector2>();
            inputActions.Player.Look.canceled += ctx => MouseDelta = Vector2.zero;
        }

        public void SetCursorState(bool visibility, CursorLockMode lockMode) {
            Cursor.visible = visibility;
            Cursor.lockState = lockMode;
            cursorState = visibility;
        }


        public void Dispose() {
            SetCursorState(true, CursorLockMode.None);
            inputActions.Disable();
        }
    }
}
