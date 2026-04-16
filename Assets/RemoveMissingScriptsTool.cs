using UnityEngine;
using UnityEditor;
// test03
public class RemoveMissingScriptsTool
{
    [MenuItem("Tools/Remove Missing Scripts In Scene")]
    static void RemoveMissingScriptsInScene()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        int totalRemoved = 0;

        foreach (GameObject go in allObjects)
        {
            int removed = GameObjectUtility.RemoveMonoBehavioursWithMissingScript(go);
            if (removed > 0)
            {
                totalRemoved += removed;
                Debug.Log($"Removed {removed} missing scripts from: {go.name}", go);
            }
        }

        Debug.Log($"DONE! Total missing scripts removed: {totalRemoved}");
    }

    [MenuItem("Tools/Remove Missing Scripts In Selected")]
    static void RemoveMissingScriptsInSelected()
    {
        int totalRemoved = 0;

        foreach (GameObject go in Selection.gameObjects)
        {
            int removed = GameObjectUtility.RemoveMonoBehavioursWithMissingScript(go);
            if (removed > 0)
            {
                totalRemoved += removed;
                Debug.Log($"Removed {removed} missing scripts from: {go.name}", go);
            }
        }

        Debug.Log($"DONE! Total missing scripts removed: {totalRemoved}");
    }
}