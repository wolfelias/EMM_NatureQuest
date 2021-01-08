using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] Text dialogText;
    [SerializeField] int letterPerSecond;

    private bool isTyping;
    private int currentLine = 0;
    Dialog dialog;

    public static DialogManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator ShowDialog(Dialog quizText)
    {
        yield return new WaitForEndOfFrame();

        this.dialog = quizText;
        dialogBox.SetActive(true);
        StartCoroutine(TypeText(quizText.Lines[0]));
    }

    public void UpdateText()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isTyping)
        {
            ++currentLine;
            if (currentLine < dialog.Lines.Count)
            {
                StartCoroutine(TypeText(dialog.Lines[currentLine]));
            }
            else
            {
                currentLine = 0;
                dialogBox.SetActive(false);
            }
        }
    }

    public IEnumerator TypeText(string Line)
    {
        isTyping = true;
        dialogText.text = "";
        foreach (var letter in Line.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / letterPerSecond);
        }
        isTyping = false;
    }

}
