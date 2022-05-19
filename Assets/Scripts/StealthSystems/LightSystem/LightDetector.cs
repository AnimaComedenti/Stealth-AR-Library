using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetector : MonoBehaviour
{
    [SerializeField] RenderTexture ligthRenderTexture;
    private float lightLevel;
    public float currentLightLevel { get; private set; }
    private int width;
    private int height;
    private RenderTexture previous;
    private RenderTexture tempRenderTexture;
    private Texture2D texture2D;

    void Start()
    {
        height = ligthRenderTexture.height;
        width = ligthRenderTexture.height;
    }

    void FixedUpdate()
    {
        if (SystemInfo.deviceType == DeviceType.Handheld) return;

        tempRenderTexture = RenderTexture.GetTemporary(width, height,  0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
        Graphics.Blit(ligthRenderTexture, tempRenderTexture);
        previous = RenderTexture.active;
        RenderTexture.active = tempRenderTexture;

        texture2D = new Texture2D(width,height);
        texture2D.ReadPixels(new Rect(0,0,tempRenderTexture.width,tempRenderTexture.height),0,0);
        texture2D.Apply();

        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(tempRenderTexture);

        Color32[] colors = texture2D.GetPixels32();

        lightLevel = 0;
        foreach(Color32 color in colors)
        {
            lightLevel += (0.2126f * color.r) + (0.7152f * color.g) + (0.0722f * color.b);
        }
        lightLevel = (float)Math.Round(lightLevel / 10000, 1);
        currentLightLevel = lightLevel;
    }
}
