using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    public Signal signal;
    public Animator animator;
    public GameObject dialogBox;
    public Text dialogText;
    public string signText;
    private bool playerInRange;


    void Start()
    {
        dialogBox.SetActive(false);
        animator.SetBool("IsOpen", false);
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
                dialogText.text = signText;
                animator.SetBool("IsOpen", true);
            }
        }
    }
    IEnumerator WaitForDialogBox()
    {
        yield return new WaitForSeconds(0.4f);
        dialogText.text = "";
        dialogBox.SetActive(false);
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
            StartCoroutine(WaitForDialogBox());
        }
    }


}
