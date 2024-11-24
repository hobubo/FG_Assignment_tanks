using UnityEngine;

namespace Mechadroids {
    public class PlayerIdleState: IEntityState {
        private readonly InputHandler inputHandler;
        private readonly PlayerReference playerReference;
        private readonly HitIndicator hitIndicatorInstance;
        private readonly IEntityHandler entityHandler;

        private float currentSpeed;
        private float turretAngle = 0f;
        private float barrelAngle = 0f;

        public PlayerIdleState(
            IEntityHandler entityHandler,
            InputHandler inputHandler,
            PlayerReference playerReference,
            HitIndicator hitIndicatorInstance) {
            this.entityHandler = entityHandler;
            this.inputHandler = inputHandler;
            this.playerReference = playerReference;
            this.hitIndicatorInstance = hitIndicatorInstance;
        }

        public void Enter() {
            // Any initialization when entering the state
            Debug.Log("enter Idle state");
        }

        public void LogicUpdate() {
            //handle player active state functionality

            // if(inputHandler.MovementInput != Vector2.zero || inputHandler.MouseDelta != Vector2.zero) {
            //     entityHandler.EntityState.Exit();
            //     entityHandler.EntityState = new PlayerActiveState(entityHandler, inputHandler, playerReference, hitIndicatorInstance);
            //     entityHandler.EntityState.Enter();
            // }

        }

        public void PhysicsUpdate() {
            // Implement physics update if needed
        }

        public void Exit() {
            // Clean up when exiting the state
            Debug.Log("exiting Idle state");
        }
    }
}
