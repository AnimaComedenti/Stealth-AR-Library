using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

namespace StealthDemo
{
    public class ReactionTask : UiTask
    {
        [Header("UI for Logic")]
        [SerializeField] private GameObject livesButtons;
        [SerializeField] private Text feedBackText;
        [SerializeField] private Text buttonToPressText;
        [SerializeField] private Image buttonToPressImage;

        private bool isButtonPressed = true;
        private bool isTimerStarted = false;
        private bool isLastAnswerWrong = false;

        private string buttonToPress;

        private float timeToClick = 10;
        private float timeTickMultiply = 1;
        private float currentTime = 10;

        private float timeForWrongSound = 5;
        private float wrongAnswerTimer = 0;

        private Image[] lives;
        private Image currentLive;
        private int liveCounter = 0;

        //Feedback
        private string[] dotArray = new string[3] { ".", "..", "..." };
        private float dotCount = 0;

        protected override void Start()
        {
            base.Start();
            lives = livesButtons.GetComponentsInChildren<Image>();
            currentLive = lives[0];
        }

        protected override void Update()
        {
            base.Update();
        }

        public override void DoingTask()
        {
            DeclareTime();

            if (isLastAnswerWrong)
            {
                wrongAnswerTimer += Time.deltaTime;
                if (wrongAnswerTimer <= timeForWrongSound)
                {
                    StopTimerAndSound(Color.yellow);
                    isLastAnswerWrong = false;
                }
            }

            if (isButtonPressed)
            {
                buttonToPress = RandomString();
                buttonToPressText.text = buttonToPress.ToUpper();
                isButtonPressed = false;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                isTimerStarted = true;
            }

            if (isTimerStarted)
            {
                if (Input.GetKeyDown(buttonToPress))
                {

                    isButtonPressed = true;
                    isTimerStarted = false;
                    HandelLives(currentTime);
                }
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

        private void HandelLives(float time)
        {
            timeTickMultiply = Random.Range(1, 10);
            timeToClick = Random.Range(5, 15);
            if (time <= 0 && time >= -1.25)
            {
                currentLive.color = Color.green;
                liveCounter++;
                if (liveCounter == lives.Length)
                {
                    isGameCompleted = true;
                    SetSoundAndLigth(Color.green);
                    CloseUI();
                    return;
                }
                currentLive = lives[liveCounter];
            }
            else
            {
                if (currentLive.color == Color.red && liveCounter > 0)
                {
                    currentLive.color = Color.white;
                    liveCounter--;
                }
                currentLive = lives[liveCounter];
                currentLive.color = Color.red;
                SetSoundAndLigth(Color.red);
                wrongAnswerTimer = 0;
                isLastAnswerWrong = true;
            }
        }

        private string RandomString()
        {
            string keysToPress = "abcdefghijklmnopqrstuvwxyz";
            int rndNumber = Random.Range(0, keysToPress.Length - 1);
            return keysToPress.Substring(rndNumber, 1);
        }
    }
}
