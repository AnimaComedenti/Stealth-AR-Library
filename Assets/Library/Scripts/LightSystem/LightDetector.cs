using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace StealthLib
{
    /*
     * Diese Klasse finden anhand eines RenderTextures das Lichtlevel des ObjeKtes, welche das Script implementiert.
     * Hierzu muss das Objekt welches den LigthRenderer besitzt über eine zweite Kamera verfügen, welche das Objekt sowie das genutzte RenderTexture beobachtet.
     * 
     * Code by Bospear Programming
     * https://www.youtube.com/watch?v=NYysvuyivc4
     */
    public class LightDetector : MonoBehaviourPun
    {
        [SerializeField] RenderTexture lightRenderTexture;
        private float lightLevel;

        // Lichtlevel das zurück gegeben wird
        public float CurrentLightLevel { get; private set; }

        private int width;
        private int height;
        private RenderTexture previous;
        private RenderTexture tempRenderTexture;
        private Texture2D texture2D;

        void Start()
        {
            height = lightRenderTexture.height;
            width = lightRenderTexture.height;
        }

        void Update()
        {

            if (!photonView.IsMine) return;
            /*
             * Erstellung einer Copie des RenderTextures.
             */
            tempRenderTexture = RenderTexture.GetTemporary(width, height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
            Graphics.Blit(lightRenderTexture, tempRenderTexture);

            /*
             * RenderTexturCopie setzen
             */
            previous = RenderTexture.active;
            RenderTexture.active = tempRenderTexture;


            /*
             * Erzeugung eines Texture2D um die Pixelunterschiede zu bekommen
             * Dient als "Snapshoot" für das aktuelle Lichtlevel
             */
            texture2D = new Texture2D(width, height);
            texture2D.ReadPixels(new Rect(0, 0, tempRenderTexture.width, tempRenderTexture.height), 0, 0);
            texture2D.Apply();


            RenderTexture.active = previous;
            RenderTexture.ReleaseTemporary(tempRenderTexture);


            Color32[] colors = texture2D.GetPixels32();

            lightLevel = 0;

            //Alle Pixel lessen und die Helligkeit aller Pixel zusammenrechnen
            foreach (Color32 color in colors)
            {
                /// Formel: https://en.wikipedia.org/wiki/Luma_(video)
                lightLevel += (0.2126f * color.r) + (0.7152f * color.g) + (0.0722f * color.b);
            }

            //Lichtlevel verkleinern für bessere Nutzung --> Werte sind dann XX,X Zahlen
            lightLevel = (float)Math.Round(lightLevel / 10000, 1);
            CurrentLightLevel = lightLevel;

            //Texture2D wieder löschen wegen Memory-Verlust
            Destroy(texture2D);
        }
    }
}

