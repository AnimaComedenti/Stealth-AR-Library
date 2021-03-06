using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StealthLib;

namespace StealthDemo
{
    public class SeeTimerUI : MonoBehaviour
    {
        public float objectSeenTimer = 30f;
        public float objectNotSeenTimer = 15f;

        [SerializeField] private Text timerText;
        [SerializeField] private ObjectDetection objectDetection;

        private float maxTimer;
        private float maxObjectNotSeenTimer;
        private GameManager gameManager;

        // Start is called before the first frame update
        void Start()
        {
            maxTimer = objectNotSeenTimer;
            maxObjectNotSeenTimer = objectSeenTimer;
            gameManager = GameManager.Instance;
        }

        // Update is called once per frame
        void Update()
        {
            if (!objectDetection.IsObjectSeen)
            {
                objectNotSeenTimer -= Time.deltaTime;

                if (objectNotSeenTimer <= 0)
                {
                    objectNotSeenTimer = maxTimer;
                    objectSeenTimer = maxObjectNotSeenTimer;
                    timerText.text = objectSeenTimer + "s";
                }

            }
            else
            {
                if (objectSeenTimer <= 0)
                {
                    objectSeenTimer = 0;
                    gameManager.hasSeekerWon = true;
                    return;
                }

                objectNotSeenTimer = maxTimer;
                objectSeenTimer -= Time.deltaTime;

                int timer = (int)objectSeenTimer;
                timerText.text = timer + "s";
            }
        }
    }
}
