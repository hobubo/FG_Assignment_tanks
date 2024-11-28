using UnityEngine;

namespace Mechadroids {

    /// <summary>
    /// Class that handles the states for each enemy behaviour. Each enemy will have its own entity handler
    /// </summary>
    public class EnemyEntityHandler : IEntityHandler {
        private readonly EnemySettings enemySettings;
        private readonly Vector3 triggerZone;
        private readonly Transform parentHolder;
        private EnemyReference enemyReference;

        public IEntityState EntityState { get; set; }

        public EnemyEntityHandler(EnemySettings enemySettings, Vector3 triggerZone, Transform parentHolder) {
            this.enemySettings = enemySettings;
            this.triggerZone = triggerZone;
            this.parentHolder = parentHolder;
        }

        public void Initialize() {
            // enemyReference = Object.Instantiate(enemySettings.enemy.enemyReferencePrefab, parentHolder);
            // enemyReference.transform.position = enemySettings.routeSettings.routePoints[0];

            // GameObject triggerVolume = new GameObject("TriggerVolume");
            // SphereCollider collider = triggerVolume.AddComponent<SphereCollider>();
            // triggerVolume.transform.position = triggerZone;
            // collider.radius = 25f;
            // collider.isTrigger = true;
            // collider.OnTriggerEnter += OnTriggerEnter;

            // Initialize the default state (Idle State)
        }

        public void CheckTriggerZone() {
            Debug.Log(Physics.CheckSphere(triggerZone, 25f, LayerMask.GetMask("Player")));
            if(Physics.CheckSphere(triggerZone, 25f, LayerMask.GetMask("Player"))) {
                enemyReference = Object.Instantiate(enemySettings.enemy.enemyReferencePrefab, parentHolder);
                enemyReference.transform.position = enemySettings.routeSettings.routePoints[0];
                EntityState = new EnemyIdleState(this, enemyReference);
                EntityState.Enter();
            }
        }

        public void Tick() {
            if(enemyReference != null) {
                EntityState.HandleInput();
                EntityState.LogicUpdate();
            } else CheckTriggerZone();
        }

        public void PhysicsTick() {
            if(enemyReference != null) EntityState.PhysicsUpdate();
        }

        public void LateTick() {
            // Implement if needed
        }

        public void Dispose() {
            if (enemyReference != null) {
                Object.Destroy(enemyReference.gameObject);
                enemyReference = null;
            }
        }
    }

    // functionality for patrolling

        // private void SetNextPatrolDestination() {
        // if(enemyReference.enemySettings.routeSettings.routePoints.Length == 0) return;
        // currentPatrolIndex %= enemyReference.enemySettings.routeSettings.routePoints.Length;
        // }
        //
        // private void MoveTowardsPatrolPoint() {
        //     if(enemyReference.enemySettings.routeSettings.routePoints.Length == 0) return;
        //
        //     Vector3 targetPoint = enemyReference.enemySettings.routeSettings.routePoints[currentPatrolIndex];
        //     targetPoint.y = enemyReference.transform.position.y;
        //     Vector3 direction = (targetPoint - enemyReference.transform.position).normalized;
        //
        //     // Move towards the target point
        //     enemyReference.transform.position += direction * enemyReference.enemySettings.enemy.patrolSpeed * Time.deltaTime;
        //
        //     // Rotate towards the target point
        //     RotateTowards(direction);
        //
        //     // Check if the enemy has reached the patrol point
        //     if(Vector3.Distance(enemyReference.transform.position, targetPoint) <= 0.1f) {
        //         currentPatrolIndex = (currentPatrolIndex + 1) % enemyReference.enemySettings.routeSettings.routePoints.Length;
        //         SetNextPatrolDestination();
        //     }
        // }
        //
        // private void RotateTowards(Vector3 direction) {
        //     if(direction.magnitude == 0) return;
        //     Quaternion targetRotation = Quaternion.LookRotation(direction);
        //     enemyReference.transform.rotation = Quaternion.Slerp(
        //         enemyReference.transform.rotation,
        //         targetRotation,
        //         enemyReference.enemySettings.enemy.patrolRotationSpeed * Time.deltaTime
        //     );
        // }
        //
        // private bool IsPlayerInDetectionRange() {
        //     if(playerTransform == null) return false;
        //     float distance = Vector3.Distance(enemyReference.transform.position, playerTransform.position);
        //     return distance <= enemyReference.enemySettings.enemy.detectionRadius;
        // }

}
