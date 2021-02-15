using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
*   DEPRECATED
*/
public class Sign : MonoBehaviour
{
    public Signal signal;
    public Animator animator;
    public GameObject dialogBox;
    public Text dialogText;
    public string signText;
    private bool playerInRange;

    /**
    *   deactivate dialogbox and animator at the start
    */
    void Start()
    {
        dialogBox.SetActive(false);
        animator.SetBool("IsOpen", false);
    }

    /**
    *   activate dialogbox and animator when player in range and presses E
    */
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange)
        {
            if (dialogBox.activeInHierarchy)
            {
                animator.SetBool("IsOpen", false);
                /*
                while (animator.IsInTransition(animator.GetLayerIndex("Base Layer")))
                {
                    Debug.Log("wait");
                    // wait for DialogBox to disappear
                }
                */
                //   dialogBox.SetActive(false);
                StartCoroutine(WaitForDialogBox());
            }
            else
            {
                dialogBox.SetActive(true);
                //dialogText.text = signText;
                animator.SetBool("IsOpen", true);
                StopAllCoroutines();
                StartCoroutine(TypeText(signText));
            }
        }
    }

    /**
    *   activate checkmark above players head and set playerInRange = true  
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
    *   deactivate checkmark above player and set playerInRange = false
    *   deactivte dialogbox and animator  
    */
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            signal.Raise();
            playerInRange = false;
            animator.SetBool("IsOpen", false);

            /*
            while (animator.IsInTransition(animator.GetLayerIndex("Base Layer")))
            {
                Debug.Log("wait");
                // wait for DialogBox to disappear
            }
            */
            StartCoroutine(WaitForDialogBox());
        }
    }

    /**
    *   creates Animation Effect of typing text
    */
    IEnumerator TypeText(string text)
    {
        dialogText.text = "";
        foreach (char letter in text.ToCharArray())
        {
            dialogText.text += letter;
            yield return null;
        }
    }

    /**
    *   waits for dialogbox to close (in screen) before going on with deactivating it
    */
    IEnumerator WaitForDialogBox()
    {
        yield return new WaitForSeconds(0.4f);
        dialogText.text = "";
        dialogBox.SetActive(false);
    }

}
