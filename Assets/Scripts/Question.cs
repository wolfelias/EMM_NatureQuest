using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question
{
    [TextArea(2, 7)]
    public string fact;
    public bool isTrue;
}
