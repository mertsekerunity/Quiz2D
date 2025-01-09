using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Timer : MonoBehaviour
{
    [Header("Time to")]
    [Tooltip("This field is for assigning the time to answer question")]
    [SerializeField] float timeToCompleteQuestion = 30f;
    [Tooltip("This field is for assigning the time to show correct answer")]
    [SerializeField] float timeToShowCorrectAnswer = 5f;
    float timerValue;

    [HideInInspector] public bool isQuestionAnswered = false;
    [HideInInspector] public bool isTimeUp = false;
    [HideInInspector] public bool loadNextQuestion = true;
    [HideInInspector] public float fillFraction;

    // Start is called before the first frame update
    void Start()
    {
        timerValue = timeToCompleteQuestion;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;

        if (!(isQuestionAnswered))
        {
            if(timerValue > 0)
            {
                if (isTimeUp)
                {
                    fillFraction = timerValue / timeToShowCorrectAnswer;
                }
                else
                {
                    fillFraction = timerValue / timeToCompleteQuestion;
                }
            }
            else
            {
                if (isTimeUp)
                {
                    loadNextQuestion = true;
                }
                isTimeUp = true;
                timerValue = timeToShowCorrectAnswer;
            }
        }
        else
        {
            if (timerValue > 0 && timeToShowCorrectAnswer >=timerValue)
            {
                fillFraction = timerValue / timeToShowCorrectAnswer;
            }
            else if(timerValue <= 0)
            {
                isQuestionAnswered = false;
                timerValue = timeToCompleteQuestion;
                loadNextQuestion = true;
            }
            else
            {
                timerValue = timeToShowCorrectAnswer;
            }
        }
            
    }
}
