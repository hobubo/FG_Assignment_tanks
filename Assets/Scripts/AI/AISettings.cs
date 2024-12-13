using System;
using UnityEngine;

namespace Mechadroids {

    public enum EnemyType {
        None,
        Tank,
        Walker,
        Walker2,
        Wheeled,
        Fixed
    }

    [CreateAssetMenu(menuName = "Mechadroids/AISettings", fileName = "AISettings", order = 0)]
    public class AISettings : ScriptableObject {
        public EnemyGroup[] enemiesToSpawn;
    }

    [Serializable]
    public class EnemyGroup {
        public EnemyType enemyType;
        public EnemySettings enemySettings;
        public int enemyCount = 1;
        public Vector3 enemyTriggerZone;
    }
}
