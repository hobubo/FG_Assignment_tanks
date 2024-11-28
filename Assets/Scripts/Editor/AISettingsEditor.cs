using UnityEditor;
using UnityEngine;
using Mechadroids;

[CustomEditor(typeof(AISettings))]
public class AISettingsEditor : Editor
{
    void OnEnable() {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    void OnDisable() {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    void OnSceneGUI(SceneView sceneView) {
        AISettings aiSettings = (AISettings)target;

        for (int i = 0; i < aiSettings.enemiesToSpawn.Length; i++) {
            Vector3 handlePosition = aiSettings.enemiesToSpawn[i].enemyTriggerZone;
            EditorGUI.BeginChangeCheck();
            Handles.color = new Color(1, 1, 1, .2f);

            var fmh_26_17_638683847771056119 = Quaternion.identity; handlePosition = Handles.FreeMoveHandle(
                handlePosition,
                50f,
                Vector3.zero,
                Handles.SphereHandleCap
            );

            if (EditorGUI.EndChangeCheck()) {
                aiSettings.enemiesToSpawn[i].enemyTriggerZone = handlePosition;
            }
            Handles.Label(handlePosition, $"Enemy Trigger Zone {i}");
        }

        SceneView.RepaintAll();
    }
}

