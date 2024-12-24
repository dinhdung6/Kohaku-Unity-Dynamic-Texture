using UnityEngine;

public class RenderTextureHolder : MonoBehaviour
{
    public static RenderTextureHolder Instance { get; private set; }
    public RenderTexture renderTexture;

    private void Awake()
    {
        // Ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Prevent destruction on scene change
            CreateRenderTexture(); // Initialize Render Texture
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instance
        }
    }

    private void CreateRenderTexture()
    {
        renderTexture = new RenderTexture(1024, 1024, 16); // Set your desired size and depth
    }
}
