using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question
{
    [TextArea(2, 7)]
    public string question;
    [TextArea(2, 8)]
    public string answerRight;
    [TextArea(2, 8)]
    public string answerWrong;
    public bool isTrue;

    public string getQuestion { get { return question; } }
}
