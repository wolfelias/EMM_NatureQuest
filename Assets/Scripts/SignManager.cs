using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;


/**
*   manages the minigame-signs in game
*/
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


    /**
    *   deactivate dialog box
    */
    private void Start()
    {
        dialogBox.SetActive(false);
        // dialogBox.SetActive(true);
        // animator.SetBool("IsOpen", true);
    }

    /**
    *   activate and show the sign when player near it and presses E
    */
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

    /**
    *   when player near the sign activate the Indicator on top of player
    */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            signal.Raise();
            playerInRange = true;
        }
    }

    /**
    *   when player leaves the trigger then deactivate the Indicator on top of player and close dialog box  
    */
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

    /**
    *   wait for dialog box to close (animation) before deactivating it
    */
    IEnumerator WaitForDialogBox()
    {
        yield return new WaitForSeconds(0.4f);
        dialogBox.SetActive(false);
    }

    /**
    *   deprecated
    */
    public void AddWriter(Text uiText, string textToWrite, float timePerCharacter)
    {
        this.uiText = uiText;
        this.textToWrite = textToWrite;
        this.timePerCharacter = timePerCharacter;
        characterIndex = 0;
    }
}