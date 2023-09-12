using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Febucci.UI.Examples
{
    public class RadioDlgEvent : MonoBehaviour
    {
        public TextAnimatorPlayer textAnimatorPlayer;

        [SerializeField] private DialogTypewrite dialogTypewrite;

        [Space(10)]
        [SerializeField] TextMeshProUGUI textDialog;
        [SerializeField] private AudioSource dialogAudioSource;
        [SerializeField] private Animator circleTime;

        [Header("Второй собеседник")]
        [SerializeField] private TMP_FontAsset firstFont;
        [SerializeField] private AudioClip firstAudio;

        [Header("Второй собеседник")]
        [SerializeField] private TMP_FontAsset secondFont;
        [SerializeField] private AudioClip secondAudio;


        private void Awake()
        {
            textAnimatorPlayer.textAnimator.onEvent += OnEvent;
        }

        void OnEvent(string message)
        {
            switch (message)
            {
                case "skip":
                    //Invoke("SkipString", 1);
                    dialogTypewrite.NextString();
                    circleTime.SetTrigger("Start");
                    break;
                case "changeFont":
                    textDialog.font = secondFont;
                    dialogAudioSource.clip = secondAudio;
                    break;
                case "changeFontBack":
                    textDialog.font = firstFont;
                    dialogAudioSource.clip = firstAudio;
                    break;
            }
        }

        void SkipString()
        {
            dialogTypewrite.NextString();
        }
    }
}
