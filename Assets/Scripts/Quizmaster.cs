using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Quizmaster : MonoBehaviour
{
    public Signal signal;
    public Animator animator;
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    private bool playerInRange;

    public Question[] questions;
    private static List<Question> unansweredQuestions;

    private Question currentQuestion;

    private void Start()
    {
        dialogBox.SetActive(false);
        animator.SetBool("IsOpen", false);
        dialogText.text = "Hello Adventurer! I am the Quizmaster";

        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList<Question>();
        }
        GetRandomQuestion();
    }
    IEnumerator WaitForDialogBox()
    {
        yield return new WaitForSeconds(0.4f);
        dialogBox.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange)
        {
            if(dialogBox.activeInHierarchy)
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
            } else
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
        if(collision.CompareTag("Player"))
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

    void GetRandomQuestion()
    {
        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];

        unansweredQuestions.RemoveAt(randomQuestionIndex);
    }


}
