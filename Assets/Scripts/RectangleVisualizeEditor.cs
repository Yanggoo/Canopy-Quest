#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

[CustomEditor(typeof(RectangleVisualizer))]
public class RectangleVisualizerEditor : Editor
{
    private Texture2D rectangleTexture;

    private void Awake()
    {
        RectangleVisualizer visualizer = (RectangleVisualizer)target;
        if (!visualizer.hasVisualizered)
        {
            rectangleTexture = new Texture2D(1, 1);
            rectangleTexture.SetPixel(0, 0, Color.green);
            rectangleTexture.Apply();

            // ×¢²á SceneView.duringSceneGui ÊÂ¼þ
            SceneView.duringSceneGui += OnSceneGUI;
            visualizer.hasVisualizered = true;
        }
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        RectangleVisualizer visualizer = (RectangleVisualizer)target;

        // Draw solid rectangle with outline
        Handles.DrawSolidRectangleWithOutline(
            new Vector3[] {
                new Vector3(visualizer.Rectangle.x, visualizer.Rectangle.y, 0),
                new Vector3(visualizer.Rectangle.x + visualizer.Rectangle.width, visualizer.Rectangle.y, 0),
                new Vector3(visualizer.Rectangle.x + visualizer.Rectangle.width, visualizer.Rectangle.y + visualizer.Rectangle.height, 0),
                new Vector3(visualizer.Rectangle.x, visualizer.Rectangle.y + visualizer.Rectangle.height, 0),
            },
            new Color(0, 1, 0, 0.2f),
            Color.clear // Use clear color for the outline
        );

        // Draw size label
        Handles.Label(visualizer.Rectangle.center, "Size: " + visualizer.Rectangle.size);

        // Draw texture in Scene view
        GUI.DrawTexture(new Rect(HandleUtility.WorldToGUIPoint(visualizer.Rectangle.position), new Vector2(visualizer.Rectangle.width * HandleUtility.GetHandleSize(Vector3.zero), visualizer.Rectangle.height * HandleUtility.GetHandleSize(Vector3.zero))), rectangleTexture);
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        RectangleVisualizer visualizer = (RectangleVisualizer)target;

        EditorGUILayout.LabelField("Size: ", visualizer.Rectangle.size.ToString());
        EditorGUILayout.LabelField("Center: ", visualizer.Rectangle.center.ToString());
    }
}

