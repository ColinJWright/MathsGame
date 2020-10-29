using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // list of all the problems
    public Problem[] problems;

    // current problem
    public int curProblem;

    // max time for each problem
    public float timePerProblem;

    // time remaining for the problem
    public float remainingTime;

    // player object
    public PlayerController player;

    // instance
    public static GameManager instance;

    void Awake ()
    {
        // set the instance
        instance = this;
    }

    void Start ()
    {
        // set the initial problem
        SetProblem(0);
    }

    void Update ()
    {
        remainingTime -= Time.deltaTime;
        
        // has the remaining time ran out?
        if(remainingTime <= 0)
        {
            Lose();
        }
    }

    // called when the player enters a tube
    public void OnPlayerEnterTube (int tube)
    {
        // did the player enter the right tube?
        if(tube == problems[curProblem].correctTube)
        {
            CorrectAnswer();
        }
        else
        {
            IncorrectAnswer();
        }
    }

    void CorrectAnswer ()
    {
        // is this the last problem?
        if(problems.Length - 1 == curProblem)
        {
            Win();
        }
        else
        {
            SetProblem(curProblem + 1);
        }
    }

    void IncorrectAnswer ()
    {
        player.Stun();
    }

    // sets the current problem
    void SetProblem (int problem)
    {
        curProblem = problem;
        UI.instance.SetProblemText(problems[curProblem]);
        remainingTime = timePerProblem;
    }

    // called when the player answers all the questions
    void Win ()
    {
        Time.timeScale = 0.0f;
        UI.instance.SetEndText(true);
    }

    // called when the player loses
    void Lose ()
    {
        Time.timeScale = 0.0f;
        UI.instance.SetEndText(false);
    }
}