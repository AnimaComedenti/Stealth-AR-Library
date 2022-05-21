using UnityEngine;
using Photon.Pun;

namespace StealthDemo
{
    public abstract class UiTask : MonoBehaviour, IInteractable
    {
        [SerializeField] protected PhotonView pv;
        [SerializeField] protected GameObject ui;
        [SerializeField] protected Light ligthResource;
        [Header("Timer to tick while doing task")]
        [SerializeField] protected float secondsWithSound = 20;
        [SerializeField] protected float soundAndLigthDuration = 2;


        public bool isGameCompleted = false;
        protected bool isTimerActive = false;
        protected float timerToMakeSound = 0;

        private HiderPlayerController hiderplayer;

        public abstract void DoingTask();

        protected virtual void Start()
        {
            ligthResource.gameObject.SetActive(false);
        }

        protected virtual void FixedUpdate()
        {
            if (SystemInfo.deviceType == DeviceType.Handheld) return;

            if (isGameCompleted)
            {
                CloseUI();
                return;
            }

            if (ui.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    CloseUI();
                }
            }

            if (isTimerActive)
            {
                DoingTask();

                timerToMakeSound += Time.deltaTime;
                if (timerToMakeSound >= secondsWithSound)
                {
                    SetSoundAndLigth(Color.red);
                }
                if (timerToMakeSound >= (secondsWithSound + soundAndLigthDuration))
                {
                    StopTimerAndSound(Color.yellow);
                }
            }
        }

        public void SetSoundAndLigth(Color color)
        {
            pv.RPC("SetAudioRemote", RpcTarget.AllBuffered, "Running");
            pv.RPC("SetLigthAndColor", RpcTarget.AllBuffered, new Vector3(color.r, color.g, color.b), true);

        }

        public void StopTimerAndSound(Color color)
        {
            timerToMakeSound = 0;
            pv.RPC("StopAudio", RpcTarget.AllBuffered);
            pv.RPC("SetLigthAndColor", RpcTarget.AllBuffered, new Vector3(color.r, color.g, color.b), false);
        }

        public void OnInteract(GameObject player)
        {
            if (isGameCompleted) return;
            hiderplayer = player.GetComponent<HiderPlayerController>();
            OpenUI();
            isTimerActive = true;
        }

        public void OpenUI()
        {
            if (ui.activeSelf) return;
            hiderplayer.isMovementDisabled = true;
            ui.SetActive(true);
        }
        public void CloseUI()
        {
            ui.SetActive(false);
            hiderplayer.isMovementDisabled = false;
            isTimerActive = false;
            timerToMakeSound = 0;
        }

        [PunRPC]
        public void SetLigthAndColor(Vector3 color, bool activeState)
        {
            ligthResource.color = new Color(color.x, color.y, color.z, 255);
            ligthResource.gameObject.SetActive(activeState);
        }
    }
}