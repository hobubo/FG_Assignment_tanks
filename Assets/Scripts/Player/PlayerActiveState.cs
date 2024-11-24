using UnityEngine;

namespace Mechadroids {
    public class PlayerActiveState : IEntityState {
        private readonly InputHandler inputHandler;
        private readonly PlayerReference playerReference;
        private readonly HitIndicator hitIndicatorInstance;
        private IEntityHandler entityHandler;

        private float currentSpeed;
        private float turretAngle = 0f;
        private float barrelAngle = 0f;

        public PlayerActiveState(
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
            // Debug.Log("enter Active state");
        }

        public void LogicUpdate() {
            //handle player active state functionality
            // HandleMovement();
            Move();
            HandleTurretAiming();

            // if(Mathf.Approximately(currentSpeed, 0) && inputHandler.MouseDelta == Vector2.zero) {
            //     entityHandler.EntityState.Exit();
            //     entityHandler.EntityState = new PlayerIdleState(entityHandler, inputHandler, playerReference, hitIndicatorInstance);
            //     entityHandler.EntityState.Enter();
            // }

        }

        public void PhysicsUpdate() {
            // Implement physics update if needed
        }

        public void Exit() {
            // Clean up when exiting the state
            // Debug.Log("exiting Active state");
        }


        private void HandleMovement() {
            // create accelerate and decelerate logic
            var accelerate = inputHandler.MovementInput.y * playerReference.playerSettings.acceleration * Time.deltaTime;
            var decelerate = - Mathf.Sign(currentSpeed) * playerReference.playerSettings.deceleration * Time.deltaTime;
            // conditionally sets accelerate and decelerate based on Input
            currentSpeed += inputHandler.MovementInput.y != 0 ? accelerate : decelerate;
            // adjust speed based on slopes
            currentSpeed = EntityHelper.HandleSlope(playerReference.tankBody, playerReference.playerSettings.maxSlopeAngle, currentSpeed);
            // limits speed to min and max
            currentSpeed = Mathf.Clamp(currentSpeed, - playerReference.playerSettings.moveSpeed, + playerReference.playerSettings.moveSpeed);
            // update position
            playerReference.tankBody.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
            // create rotation logic
            var rotation = inputHandler.MovementInput.x * playerReference.playerSettings.rotationSpeed * Time.deltaTime;
            // conditionally sets rotation to inverse if moving backwards
            rotation = currentSpeed > 0 ? rotation : -rotation;
            // update rotation
            playerReference.tankBody.Rotate(Vector3.up, rotation);
        }

        private void HandleTurretAiming() {
            Vector2 mouseInput = inputHandler.MouseDelta;

            // Update turret horizontal angle
            turretAngle += mouseInput.x * playerReference.playerSettings.turretRotationSpeed * Time.deltaTime;
            turretAngle = Mathf.Clamp(turretAngle, playerReference.playerSettings.minTurretAngle, playerReference.playerSettings.maxTurretAngle);

            // Update barrel elevation angle
            barrelAngle -= mouseInput.y * playerReference.playerSettings.barrelRotationSpeed * Time.deltaTime; // Inverted because moving mouse up should raise the barrel
            barrelAngle = Mathf.Clamp(barrelAngle, playerReference.playerSettings.minBarrelElevation, playerReference.playerSettings.maxBarrelElevation);

            // Apply turret rotation relative to tank body
            Quaternion turretRotation = playerReference.tankBody.rotation * Quaternion.Euler(0f, turretAngle, 0f);
            playerReference.turretBase.rotation = turretRotation;

            // Apply barrel rotation
            Quaternion barrelRotation = Quaternion.Euler(barrelAngle, 0f, 0f);
            playerReference.barrel.localRotation = barrelRotation;
        }

        // private void UpdateHitIndicator() {
        //     var ray = new Ray(playerReference.barrelEnd.position, playerReference.barrelEnd.forward);
        //     if(Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, playerReference.aimLayerMask)) {
        //         hitIndicatorInstance.gameObject.SetActive(true);
        //         hitIndicatorInstance.transform.position = hitInfo.point + hitInfo.normal * 0.01f;
        //         hitIndicatorInstance.transform.rotation = Quaternion.LookRotation(hitInfo.normal);
        //     }
        //     else {
        //         if(hitIndicatorInstance != null) {
        //             hitIndicatorInstance.gameObject.SetActive(false);
        //         }
        //     }
        // }

    }
}
