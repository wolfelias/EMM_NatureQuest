using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] Text dialogText;
    [SerializeField] int letterPerSecond;

    public static DialogManager Instance { get; private set; }
        private void Awake()
    {
        Instance = this;    
    }

    public void ShowDialog(Quiz_TextList quizText)
    {
        dialogBox.SetActive(true);
        StartCoroutine(TypeText(quizText.Lines[0]));
    }

    public IEnumerator TypeText(string quizText)
    {
        dialogText.text = "";
        foreach (var letter in quizText.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / letterPerSecond);
        }
    }

}
