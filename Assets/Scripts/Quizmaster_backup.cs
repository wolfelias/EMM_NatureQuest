using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/**
*   DEPRECATED
*/
public class Quizmaster : MonoBehaviour
{
    public Signal signal;
    public Animator animator;
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    private bool playerInRange;

    private void Start()
    {
        dialogBox.SetActive(false);
        animator.SetBool("IsOpen", false);
        // dialogBox.SetActive(true);
        // animator.SetBool("IsOpen", true);
    }

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
                animator.SetBool("IsOpen", true);
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
            animator.SetBool("IsOpen", false);

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
}