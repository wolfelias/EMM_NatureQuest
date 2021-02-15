using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /**
    *   Question Object which holds a String for question, 2 answer Strings and a boolean if answer = true
    */
[System.Serializable]
public class Question
{
    /**
    *   question String
    */
    [TextArea(2, 7)]
    public string question;
    /**
    *   answer String when Player choose correct answer
    */
    [TextArea(2, 8)]
    public string answerRight;
    /**
    *   answer String when Player choose wrong answer
    */
    [TextArea(2, 8)]
    public string answerWrong;
    /**
    *   if true then the correct answer to the question is TRUE
    */
    public bool isTrue;

    public string getQuestion { get { return question; } }
}
