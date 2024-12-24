using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Required for UI components like RawImage

public class ApplyTextureInNextScene : MonoBehaviour
{
    public Renderer targetObjectRenderer;  // For the 3D object's material
    public RawImage rawImage;              // For the UI RawImage element

    void Start()
    {
        // Ensure the TextureManager instance and Renderer are available
        if (TextureManager.instance != null)
        {
            // Get the saved texture from TextureManager
            Texture savedTexture = TextureManager.instance.GetTexture();

            // Apply the texture to the 3D object's material "_BaseMap"
            if (savedTexture != null)
            {
                if (targetObjectRenderer != null)
                {
                    targetObjectRenderer.material.SetTexture("_Diffuse", savedTexture);
                }

                // Apply the same texture to the RawImage UI element
                if (rawImage != null)
                {
                    rawImage.texture = savedTexture;
                }
                else
                {
                    Debug.LogWarning("RawImage component is not assigned.");
                }
            }
            else
            {
                Debug.LogWarning("No texture found in TextureManager.");
            }
        }
        else
        {
            Debug.LogError("TextureManager instance is missing.");
        }
    }
}
