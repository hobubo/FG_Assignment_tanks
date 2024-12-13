using UnityEditor;
using UnityEngine;
using Mechadroids;

[CustomEditor(typeof(Route))]
public class RouteEditor : Editor
{
    void OnEnable() {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    void OnDisable() {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    void OnSceneGUI(SceneView sceneView) {
        Route route = (Route)target;
        if(!route.displayRoutePoints) return;
        if(route.routePoints == null) return;

        for (int i = 0; i < route.routePoints.Length; i++) {
            Vector3 point = route.routePoints[i];
            EditorGUI.BeginChangeCheck();
            Vector3 newPoint = Handles.PositionHandle(point, Quaternion.identity);
            if (EditorGUI.EndChangeCheck()) {
                route.routePoints[i] = newPoint;
                EditorUtility.SetDirty(route);
            }
            Handles.Label(point, $"Point {i}");
        }

        SceneView.RepaintAll();
    }
}

