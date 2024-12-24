using UnityEngine;

public class HideEditorOnlyObjects : MonoBehaviour
{
    void Start()
    {
        // Find all GameObjects with the tag "EditorOnly"
        GameObject[] editorOnlyObjects = GameObject.FindGameObjectsWithTag("EditorOnly");

        // Loop through each GameObject and hide it
        foreach (GameObject obj in editorOnlyObjects)
        {
            obj.SetActive(false);
        }
    }
}
