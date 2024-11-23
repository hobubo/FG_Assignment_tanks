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

        public void Initialize() {
            // initialize input here
            inputActions = new InputActions();
            inputActions.Player.Enable();
            inputActions.Player.Move.performed += OnMove;
            inputActions.Player.Move.canceled += OnStop;
            inputActions.Player.Look.performed += OnLook;
        }

        void OnMove(InputAction.CallbackContext ctx) {
            MovementInput = ctx.ReadValue<Vector2>();
        }

        void OnStop(InputAction.CallbackContext ctx) {
            MovementInput = Vector2.zero;
        }

        void OnLook(InputAction.CallbackContext ctx) {
            MouseDelta = ctx.ReadValue<Vector2>();
        }

        public void SetCursorState(bool visibility, CursorLockMode lockMode) {
            Cursor.visible = visibility;
            Cursor.lockState = lockMode;
        }

        public void Dispose() {
            SetCursorState(true, CursorLockMode.None);
            inputActions.Disable();
        }
    }
}
