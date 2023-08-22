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

        [Header("Анимация кружка")]
        [SerializeField] private Animator circleTime;

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
                case "answ":
                    dialogTypewrite.activeAnswers = true;

                    keyboardNavigation.canChoice = true;

                    textButtons[0].text = firstShort[numberAnswer];
                    textButtons[1].text = secondShort[numberAnswer];

                    for (int i = 0; i < buttons.Length; i++)
                        buttons[i].SetActive(true);

                    break;
            }
        }

        public void ButtonLeft()
        {
            dialogTypewrite.strings[dialogTypewrite.NumberString + 1] = firstAnswers[numberAnswer];

            buttonsAnim[1].SetTrigger("End");

            Invoke("AfterAnswer", 1);
        }

        public void ButtonRight()
        {
            dialogTypewrite.strings[dialogTypewrite.NumberString + 1] = secondAnswers[numberAnswer];

            buttonsAnim[0].SetTrigger("End");

            Invoke("AfterAnswer", 1);
        }

        void AfterAnswer()
        {
            for (int i = 0; i < buttons.Length; i++)
                buttons[i].SetActive(false);

            dialogTypewrite.activeAnswers = false;
            circleTime.SetTrigger("Idle");

            numberAnswer++;

            dialogTypewrite.NextString();
        }
    }
}
