using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class SignManager : MonoBehaviour
{
    public Signal signal;
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    private bool playerInRange;
    private float timer;
    private Text uiText;
    private string textToWrite;
    private float timePerCharacter;
    private int characterIndex;

    private void Start()
    {
        dialogBox.SetActive(false);
        // dialogBox.SetActive(true);
        // animator.SetBool("IsOpen", true);
    }

    void Update()
    {
        
        
        if (Input.GetKeyDown(KeyCode.E) && playerInRange)
        {
            if (dialogBox.activeInHierarchy)
            {

                StartCoroutine(WaitForDialogBox());
            }
            else
            {
                dialogBox.SetActive(true);
                if (uiText != null)
                {
                    timer -= Time.deltaTime;
                    if (timer <= 0f)
                    {
                     //Display next character   
                     timer += timePerCharacter;
                     characterIndex++;
                     uiText.text = textToWrite.Substring(0, characterIndex);
                    }
                }
                
                
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            signal.Raise();
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            signal.Raise();
            playerInRange = false;

            /*
            while (animator.IsInTransition(animator.GetLayerIndex("Base Layer")))
            {
                Debug.Log("wait");
                // wait for DialogBox to disappear
            }
            */
            dialogBox.SetActive(false);
        }
    }

    IEnumerator WaitForDialogBox()
    {
        yield return new WaitForSeconds(0.4f);
        dialogBox.SetActive(false);
    }

    public void AddWriter(Text uiText, string textToWrite, float timePerCharacter)
    {
        this.uiText = uiText;
        this.textToWrite = textToWrite;
        this.timePerCharacter = timePerCharacter;
        characterIndex = 0;
    }
}