using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [SerializeField] QuestionSO question;
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] GameObject[] answerButtons;
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    int correctAnswerIndex;

    // Start is called before the first frame update
    void Start()
    {
        GetNextQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAnswerSelected(int index)
    {
        correctAnswerIndex = question.GetCorrectAnswerIndex();
        Image buttonImage;

        if (index == correctAnswerIndex)
        {
            questionText.text = "Correct answer!";
            buttonImage = answerButtons[index].GetComponent<Image>();

            buttonImage.sprite = correctAnswerSprite;
        }
        else
        {
            string correctAnswer = question.GetAnswer(correctAnswerIndex);
            questionText.text = "Incorrect! The correct answer is: \n" + correctAnswer;
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    void GetNextQuestion()
    {
        DisplayQuestion();
        SetDefaultButtonSprite();
        SetButtonState(true);
    }

    void DisplayQuestion()
    {
        questionText.text = question.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();

            buttonText.text = question.GetAnswer(i);
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
}
