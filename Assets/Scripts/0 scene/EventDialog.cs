using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Febucci.UI.Examples
{
    public class EventDialog : MonoBehaviour
    {
        public TextAnimatorPlayer textAnimatorPlayer;

        [SerializeField] private DialogTypewrite dialogTypewrite;
        [SerializeField] private KeyboardNavigation keyboardNavigation;

        [Header("Ответы НПС")]
        public string[] firstAnswers;
        public string[] secondAnswers;

        [Header("Текст на кнопках")]
        public string[] firstShort;
        public string[] secondShort;

        int numberAnswer = 0;

        [SerializeField] private GameObject[] buttons;
        [SerializeField] private TextMeshProUGUI[] textButtons;
        Animator[] buttonsAnim = new Animator[2];

        [Header("Питч вниз")]
        [SerializeField] private Animator pitchAudio;

        private void Awake()
        {
            textAnimatorPlayer.textAnimator.onEvent += OnEvent;


        }

        private void Start()
        {
            for (int i = 0; i < buttons.Length; i++)
                buttonsAnim[i] = buttons[i].GetComponent<Animator>();
        }

        void OnEvent(string message)
        {
            switch (message)
            {
                case "answr":
                    dialogTypewrite.activeAnswers = true;

                    keyboardNavigation.canChoice = true;

                    textButtons[0].text = firstShort[numberAnswer];
                    textButtons[1].text = secondShort[numberAnswer];

                    for (int i = 0; i < buttons.Length; i++)
                        buttons[i].SetActive(true);

                    break;
                case "stop":
                    CameraShake.Instance.ShakeCamera(3f, 0.1f, 1);
                    break;
                case "danger":
                    pitchAudio.SetTrigger("PitchDown");
                    break;
                case "backDanger":
                    pitchAudio.SetTrigger("PitchUp");
                    break;
            }
        }

        public void ButtonLeft()
        {
            dialogTypewrite.strings[dialogTypewrite.NumberString + 1] = firstAnswers[numberAnswer];

            buttonsAnim[1].SetTrigger("End");

            Invoke("ButtonLeftEnd", 1);

            Invoke("AfterAnswer", 1);
        }

        public void ButtonRight()
        {
            dialogTypewrite.strings[dialogTypewrite.NumberString + 1] = secondAnswers[numberAnswer];

            buttonsAnim[0].SetTrigger("End");

            Invoke("ButtonRightEnd", 1);

            Invoke("AfterAnswer", 1);
        }

        void ButtonLeftEnd()
        {
            buttonsAnim[0].SetTrigger("End");
        }


        void ButtonRightEnd()
        {
            buttonsAnim[1].SetTrigger("End");
        }

        void AfterAnswer()
        {
            for (int i = 0; i < buttons.Length; i++)
                buttons[i].SetActive(false);

            dialogTypewrite.activeAnswers = false;

            numberAnswer++;

            dialogTypewrite.EndLine();
        }
    }
}
