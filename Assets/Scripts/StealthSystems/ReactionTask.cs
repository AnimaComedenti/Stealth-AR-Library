using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using StealthLib;
using Photon.Pun;

namespace StealthDemo
{
    public class ReactionTask : UITask
    {
        [Header("UI for Logic")]
        [SerializeField] private GameObject livesButtons;
        [SerializeField] private Text feedBackText;
        [SerializeField] private Text buttonToPressText;
        [SerializeField] private Image buttonToPressImage;

        [Header("Componets for Sound and Light")]
        [SerializeField] private NoiseAndSoundMaker noiseAndSoundMaker;
        [SerializeField] private string soundname;

        [Header("Others")]
        public bool isGameCompleted = false;

        private bool isButtonPressed = true;
        private bool isTimerStarted = false;
        private bool isLastAnswerWrong = false;

        //Gamebutton
        private string buttonToPress;

        //Timers
        private float timeToClick;
        private float timeTickMultiply;
        private float currentTime;
        private float timeForWrongSound = 5;
        private float wrongAnswerTimer = 0;

        //Lives
        private Image[] lives;
        private Image currentLive;
        private int liveCounter = 0;

        //Feedback
        private string[] dotArray = new string[3] { ".", "..", "..." };
        private float dotCount = 0;

        private  void Start()
        {
            lives = livesButtons.GetComponentsInChildren<Image>();
            currentLive = lives[0];
            Reset();
        }

        private  void FixedUpdate()
        {
            if (isGameCompleted || !isUIOpen)
            {
                CloseUI();
                return;
            }

            CloseUI();

            if (isUIOpen)
            {
                DoingTask();

                if (noiseAndSoundMaker.SoundAndLightTimer())
                {
                    photonView.RPC("SetLightAndColor", RpcTarget.AllBuffered, new Vector3(Color.red.r, Color.red.g, Color.red.b), true);
                    photonView.RPC("SetAudioRemoteSoundSO", RpcTarget.AllBuffered, soundname);
                }
                else
                {
                    photonView.RPC("SetLightAndColor", RpcTarget.AllBuffered, new Vector3(Color.yellow.r, Color.yellow.g, Color.yellow.b), false);
                    photonView.RPC("StopAudioRemote", RpcTarget.AllBuffered);
                }
            }
        }

        public override void DoingTask()
        {
            //Count Time
            DeclareTime();

            //Make Sound if wrong Answer
            if (isLastAnswerWrong)
            {
                wrongAnswerTimer += Time.deltaTime;
                if (wrongAnswerTimer >= timeForWrongSound)
                {
                    photonView.RPC("SetLightAndColor", RpcTarget.AllBuffered, new Vector3(Color.yellow.r, Color.yellow.g, Color.yellow.b), false);
                    photonView.RPC("StopAudioRemote", RpcTarget.AllBuffered);
                    isLastAnswerWrong = false;
                }
            }

            //Start Task
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isTimerStarted = true;
            }

            //Start after Space pressed
            if (isTimerStarted)
            {
                if (Input.GetKeyDown(buttonToPress))
                {

                    isButtonPressed = true;
                    isTimerStarted = false;
                    LivesHandler(currentTime);
                }
            }

            //Generate new Randome String after Button Press
            if (isButtonPressed)
            {
                buttonToPress = RandomString();
                buttonToPressText.text = buttonToPress.ToUpper();
                isButtonPressed = false;
            }
        }

        private void DeclareTime()
        {
            if (!isTimerStarted)
            {
                currentTime = timeToClick;
                buttonToPressImage.color = Color.red;
                feedBackText.text = "Space to start task";
            }
            else
            {
                currentTime -= Time.deltaTime * timeTickMultiply;
                dotCount += Time.deltaTime;
                if (dotCount > 3) dotCount = 0;
                feedBackText.text = "task in progress " + dotArray[(int)dotCount];
                if (currentTime <= 0) buttonToPressImage.color = Color.green;
            }
        }

        private void LivesHandler(float time)
        {

            if (time <= 0 && time >= -1.7)
            {
                CountHealthUp();
            }
            else
            {
                CountHealthDown();
            }

            Reset();
        }

        private string RandomString()
        {
            string keysToPress = "abcdefghijklmnopqrstuvwxyz";
            int rndNumber = Random.Range(0, keysToPress.Length - 1);
            return keysToPress.Substring(rndNumber, 1);
        }

        private void Reset()
        {
            timeTickMultiply = Random.Range(1, 10);
            timeToClick = Random.Range(5, 15);
        }

        private void CountHealthUp()
        {
            currentLive.color = Color.green;
            liveCounter++;
            if (liveCounter == lives.Length)
            {
                isGameCompleted = true;
                photonView.RPC("SetLightAndColor", RpcTarget.AllBuffered, new Vector3(Color.green.r, Color.green.g, Color.green.b), true);
                CloseUI();
                return;
            }
            currentLive = lives[liveCounter];
        }

        private void CountHealthDown()
        {
            if (currentLive.color == Color.red && liveCounter > 0)
            {
                currentLive.color = Color.white;
                liveCounter--;
            }

            currentLive = lives[liveCounter];
            currentLive.color = Color.red;
            photonView.RPC("SetLightAndColor", RpcTarget.AllBuffered, new Vector3(Color.red.r, Color.red.g, Color.red.b), true);
            photonView.RPC("SetAudioRemoteSoundSO",RpcTarget.AllBuffered, soundname);
            wrongAnswerTimer = 0;
            isLastAnswerWrong = true;
        }
    }
}
