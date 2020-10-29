using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Text problemText;
    public Text[] answerTexts;

    public Image timeDial;
    private float timeDialRate;

    public Text endText;

    // instance
    public static UI instance;

    void Awake ()
    {
        // set the instance
        instance = this;
    }

    void Start ()
    {
        // calculate the time dial rate
        timeDialRate = 1.0f / GameManager.instance.timePerProblem;
    }

    void Update ()
    {
        // update the time dial
        timeDial.fillAmount = timeDialRate * GameManager.instance.remainingTime;
    }

    // sets the ship UI to display the new problem
    public void SetProblemText (Problem problem)
    {
        string operatorText = "";

        // get the operator and convert it to a string
        switch(problem.operation)
        {
            case MathsOperation.Addition:
                operatorText = " + ";
                break;
            case MathsOperation.Subtraction:
                operatorText = " - ";
                break;
            case MathsOperation.Multiplication:
                operatorText = " x ";
                break;
            case MathsOperation.Division:
                operatorText = " ÷ ";
                break;

        }

        // set the problem text to display the equation
        problemText.text = problem.firstNumber + operatorText + problem.secondNumber;

        // set the answers texts to display the correct and incorrect answers
        for(int i = 0; i < answerTexts.Length; i++)
        {
            answerTexts[i].text = problem.answers[i].ToString();
        }
    }

    // sets the win or lose text
    public void SetEndText (bool win)
    {
        // enable the text
        endText.gameObject.SetActive(true);

        // did the player win?
        if(win)
        {
            endText.text = "You Win!";
            endText.color = Color.green;
        }
        // did the player lose?
        else
        {
            endText.text = "Game Over!";
            endText.color = Color.red;
        }
    }
}