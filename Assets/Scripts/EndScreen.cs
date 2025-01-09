using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;

    ScoreTracker scoreTracker;

    // Start is called before the first frame update
    void Awake()
    {
        scoreTracker = FindObjectOfType<ScoreTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayFinalScore()
    {
        finalScoreText.gameObject.SetActive(true);
        finalScoreText.text = "Congratulations! \r\nYou gained " + scoreTracker.CalculateScore() + "%";
    }
}
