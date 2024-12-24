using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO; // For file handling

using UnityEngine.SceneManagement;  // This is necessary to manage scenes

public class Main : MonoBehaviour
{

    [SerializeField] private RawImage img = default;
    private WebCamTexture webCam;
    private WebCamDevice externalCam; // To store the external camera details
    private string externalCamName = "HD Web Camera"; // Update this with the exact name of your DroidCam

    public Renderer objectRenderer;
    // Assuming you have a RenderTexture already created and assigned in your inspector
    
    public Camera renderCamera; // Assign in Inspector
    public RenderTexture renderTexture; // Assign in Inspector
    public Material displayMaterial; // Assign in Inspector

    void Start()
    {
        // Get a list of available cameras
        WebCamDevice[] devices = WebCamTexture.devices;

        // Loop through all available devices
        for (int i = 0; i < devices.Length; i++)
        {
            Debug.Log("Camera: " + devices[i].name);

            // Find the external camera (e.g., DroidCam or any specific camera)
            if (devices[i].name.Contains(externalCamName))
            {
                externalCam = devices[i];
                break;
            }
        }

        // If the external camera is found, use it; otherwise, fallback to default
        if (!string.IsNullOrEmpty(externalCam.name))
        {
            webCam = new WebCamTexture(externalCam.name);
        }
        else
        {
            Debug.LogWarning("External camera not found, using default camera.");
            webCam = new WebCamTexture(); // Fallback to the default camera
        }

        // Start the webcam if it's not already playing
        if (!webCam.isPlaying)
        {
            webCam.Play();
        }

        // Assign the webcam texture to the UI image
        img.texture = webCam;
    }

    void Update()
    {
        // Set the material's texture to the Render Texture
        displayMaterial.mainTexture = img.texture;
    }

    public void toStopped()
    {
        if (webCam.isPlaying)
        {
            // Capture the frame and save it as a file before stopping the webcam
            SaveCapturedFrame();

            // Now stop the webcam
            webCam.Stop();
        }
    }

    public void changeScene(int  scene)
    {

        SaveRenderTexture(); // Optionally save the texture content
        SceneManager.LoadScene(scene);  // Loads the scene with build index 1
    }

    public void toStart()
    {
        if (!webCam.isPlaying)
        {
            webCam.Play();
        }

        img.texture = webCam;
    }

    private void SaveCapturedFrame()
    {
        // Ensure that the webcam is playing before capturing
        if (webCam.isPlaying)
        {
            // Create a new Texture2D to store the webcam frame
            Texture2D capturedTexture = new Texture2D(webCam.width, webCam.height);
            capturedTexture.SetPixels(webCam.GetPixels());
            capturedTexture.Apply();

            //add texture to material
            objectRenderer.material.mainTexture = capturedTexture;
            // Save the texture to be used in the next scene// Set the target texture for the camera
            renderCamera.targetTexture = renderTexture;

            // Change the camera's clear flags
            renderCamera.clearFlags = CameraClearFlags.Nothing; // Or Don't Clear

            // Set the material's texture to the Render Texture
            displayMaterial.mainTexture = renderTexture;
            SaveRenderTexture();
        }
        else
        {
            Debug.LogWarning("Cannot capture frame: webcam is not playing.");
        }
    }


    void SaveRenderTexture()
    {
        RenderTexture.active = renderTexture; // Set the Render Texture as active
        Texture2D tempTexture = new Texture2D(renderTexture.width, renderTexture.height);
        tempTexture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        tempTexture.Apply();
        // Save or use the Texture2D as needed
        RenderTexture.active = null; // Clear the active Render Texture
    }

}
