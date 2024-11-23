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
            inputActions.Player.Move.performed += OnMovePerformed;
            inputActions.Player.Move.canceled += OnMoveCanceled;
            inputActions.Player.Look.performed += OnAimPerformed;
            inputActions.Player.Look.canceled += OnAimCanceled;
        }

        void OnMovePerformed(InputAction.CallbackContext ctx) {
            MovementInput = ctx.ReadValue<Vector2>();
        }

        void OnMoveCanceled(InputAction.CallbackContext ctx) {
            MovementInput = Vector2.zero;
        }

        void OnAimPerformed(InputAction.CallbackContext ctx) {
            MouseDelta = ctx.ReadValue<Vector2>();
        }

        void OnAimCanceled(InputAction.CallbackContext ctx) {
            MouseDelta = Vector2.zero;
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
