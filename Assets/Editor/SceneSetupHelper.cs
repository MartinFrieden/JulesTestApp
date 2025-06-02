using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class SceneSetupHelper
{
    [MenuItem("Jules/Setup Scene")]
    public static void SetupScene()
    {
        // Open the SampleScene
        EditorSceneManager.OpenScene("Assets/Scenes/SampleScene.unity");

        // Find the Main Camera
        GameObject mainCameraObj = GameObject.Find("Main Camera");
        if (mainCameraObj != null)
        {
            Camera camera = mainCameraObj.GetComponent<Camera>();
            if (camera != null)
            {
                // Set to Orthographic
                camera.orthographic = true;
                // Set Orthographic Size (e.g., 5 for a 10-unit vertical view)
                camera.orthographicSize = 5;
                // Set Clear Flags and Background Color
                camera.clearFlags = CameraClearFlags.SolidColor;
                camera.backgroundColor = new Color(0.53f, 0.81f, 0.92f); // Light blue
                // Set Camera Z position
                mainCameraObj.transform.position = new Vector3(mainCameraObj.transform.position.x, mainCameraObj.transform.position.y, -10);
                Debug.Log("Main Camera configured for 2D.");
            }
            else
            {
                Debug.LogError("Main Camera component not found on 'Main Camera' GameObject.");
            }
        }
        else
        {
            Debug.LogError("'Main Camera' GameObject not found in the scene.");
        }

        // Regarding aspect ratio:
        // This is typically set in the Game View dropdown in the Unity Editor.
        // We'll log a message about it.
        Debug.Log("Scene setup for camera complete. Please ensure the Game View is set to a 16:9 aspect ratio (e.g., 1920x1080).");

        // Save the scene
        EditorSceneManager.SaveOpenScenes();
    }
}
