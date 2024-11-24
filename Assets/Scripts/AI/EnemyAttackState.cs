using UnityEngine;

namespace Mechadroids {
    public class EnemyAttackState: IEntityState {
        private readonly IEntityHandler entityHandler;
        private readonly EnemyReference enemyReference;
        private readonly Transform attackTarget;
        private float timerStartTime;

        public EnemyAttackState(IEntityHandler entityHandler, EnemyReference enemyReference, Transform attackTarget) {
            this.entityHandler = entityHandler;
            this.enemyReference = enemyReference;
            this.attackTarget = attackTarget;
        }

        public void Enter() {
            // Optionally set idle animation
            Debug.Log("enter Attack state");
        }

        public void LogicUpdate() {
            MoveIntoAttackRange();
            if(IsPlayerOutOfRange()) TransitionToPatrolState();
        }

        public void PhysicsUpdate() {
        }

        public void Exit() {
            // Cleanup if necessary
        }

        private void TransitionToPatrolState() {
            Exit();
            entityHandler.EntityState = new EnemyPatrolState(entityHandler, enemyReference, attackTarget);
            entityHandler.EntityState.Enter();
        }

        private bool IsPlayerOutOfRange() {
            if(!attackTarget) return false;
            float distance = Vector3.Distance(enemyReference.transform.position, attackTarget.position);
            return distance >= enemyReference.enemySettings.enemy.detectionRadius;
        }


        private void Attack() {
            if(Time.time - timerStartTime > enemyReference.enemySettings.enemy.attackSpeed) {
                Debug.Log("Attack");
                timerStartTime = Time.time;
            }
        }

        private void MoveIntoAttackRange() {
            var direction = (attackTarget.position - enemyReference.transform.position).normalized;
            RotateTowards(direction);
            if(Vector3.Distance(enemyReference.transform.position, attackTarget.position) <= enemyReference.enemySettings.enemy.attackRange) {
                Attack();
            } else {
                enemyReference.transform.position += direction * enemyReference.enemySettings.enemy.attackSpeed * Time.deltaTime;
            }
        }

        private void RotateTowards(Vector3 direction) {
            if(direction.magnitude == 0) return;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemyReference.transform.rotation = Quaternion.Slerp(
                enemyReference.transform.rotation,
                targetRotation,
                enemyReference.enemySettings.enemy.attackRotationSpeed * Time.deltaTime
            );
        }

    }
}
