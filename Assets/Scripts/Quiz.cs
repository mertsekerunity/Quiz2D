using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    QuestionSO currentQuestion;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] GameObject[] answerButtons;
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    [SerializeField] Image timerImage;
    Timer timer;

    int correctAnswerIndex;

    // Start is called before the first frame update
    void Start()
    {
        timer = FindObjectOfType<Timer>();
        timer.loadNextQuestion = true;
    }

    // Update is called once per frame
    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        TimeIsUp();
        if (timer.loadNextQuestion)
        {
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
    }

    public void OnAnswerSelected(int index)
    {
        DisplayAnswer(index);

        SetButtonState(false);

        timer.isQuestionAnswered = true;
    }

    void DisplayAnswer(int index)
    {
        correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
        Image buttonImage;

        if (index == correctAnswerIndex)
        {
            questionText.text = "Correct answer!";
            buttonImage = answerButtons[index].GetComponent<Image>();

            buttonImage.sprite = correctAnswerSprite;
        }
        else
        {
            string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
            questionText.text = "Incorrect! The correct answer is: \n" + correctAnswer;
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    void TimeIsUp()
    {
        if (timer.isTimeUp)
        {
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            Image buttonImage;
            string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
            questionText.text = "Time is up! The correct answer is: \n" + correctAnswer;
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;

            SetButtonState(false);
        }
    }
    void GetNextQuestion()
    {
        if(questions.Count > 0)
        {
            GetRandomQuestion();
            DisplayQuestion();
            SetDefaultButtonSprite();
            SetButtonState(true);
        }
    }

    void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();

            buttonText.text = currentQuestion.GetAnswer(i);
        }

    }

    void SetDefaultButtonSprite()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }

    void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];
        if (questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
    }
}
