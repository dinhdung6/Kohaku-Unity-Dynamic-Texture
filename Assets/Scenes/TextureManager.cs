using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureManager : MonoBehaviour
{
    public static TextureManager instance;

    public Texture exportedTexture;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keeps this object alive across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetTexture(Texture newTexture)
    {
        exportedTexture = newTexture;
    }

    public Texture GetTexture()
    {
        return exportedTexture;
    }
}
