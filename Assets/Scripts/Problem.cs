using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MathsOperation
{
    Addition,
    Subtraction,
    Multiplication,
    Division
}

[System.Serializable]
public class Problem
{
    // problem: 5 + 10
    // answers: 20, 8, 15, 30
    // correct tube: 2 

    public float firstNumber;
    public float secondNumber;
    public MathsOperation operation;
    public float[] answers;

    public int correctTube;
}