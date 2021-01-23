using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Quizmaster : MonoBehaviour
{
    private Health playerHealth;
    private int playerOnEnterHealth;
    public Signal signal;
    public Animator animator;
    public GameObject dialogBox;
    public Text dialogText;
    private bool playerInRange;
    private string openingText = "Hello Adventurer! I am the Quizmaster";

    // questions must be >= questionsToAnswer!!!
    public Question[] questions;
    private static List<Question> unansweredQuestions;
    public int questionsToAnswer = 3;

    private Question currentQuestion;
    private int countPressE = 0;
    private int answeredQuestions = 0;
    private bool gameFinished = false;
    private bool isInGame = false;

    private int rewardHealthPoints = 5;

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();

        dialogBox.SetActive(false);
        animator.SetBool("IsOpen", false);

        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList<Question>();
        }
        GetRandomQuestion();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange && countPressE == 1)
        {
            countPressE++;
            isInGame = true;
            playerOnEnterHealth = playerHealth.CurHealth;
            dialogText.text = currentQuestion.question;
            //    StartQuestGame();
            // StartQuestGameTwo();
        }
        if (Input.GetKeyDown(KeyCode.E) && playerInRange && countPressE == 0)
        {
                Debug.Log("First Text");
                dialogBox.SetActive(true);
                dialogText.text = openingText;
                animator.SetBool("IsOpen", true);
                countPressE++;
                Debug.Log("Update DownPart " + countPressE);
        }
        if (Input.GetKeyDown(KeyCode.T) && playerInRange && isInGame && !gameFinished)
        {
            checkPlayerAnswer(KeyCode.T);
        }
        if (Input.GetKeyDown(KeyCode.F) && playerInRange && isInGame && !gameFinished)
        {
            checkPlayerAnswer(KeyCode.F);
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

            if (!gameFinished)
            {
                unansweredQuestions = questions.ToList<Question>();
                for(int i = 0; i < answeredQuestions; i++)
                {
                    Debug.Log("health");
                    if (playerHealth.CurHealth > playerOnEnterHealth)
                    {
                        playerHealth.DecreaseHealth(rewardHealthPoints);
                    } else if (playerHealth.CurHealth < playerOnEnterHealth)
                    {
                        playerHealth.IncreaseHealth(rewardHealthPoints);
                    }
                }
                answeredQuestions = 0;
            }

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

    void GetRandomQuestion()
    {
        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];

        unansweredQuestions.RemoveAt(randomQuestionIndex);
    }
    private void StartQuestGame()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // do nothing
        }
        for (int i = 0; i < questionsToAnswer; i++)
        {
            dialogText.text = currentQuestion.question;
            KeyCode[] kcArray = { KeyCode.F, KeyCode.T };
            StartCoroutine(WaitPlayerChoice(kcArray)); 
            answeredQuestions++;
            GetRandomQuestion();
        }
        gameFinished = true;
        openingText = "Ok so you have finished my Quiz. Now go on. " +
            "The world is large and you may be needed at another place! Goodbye.";
        dialogText.text = openingText;
    }

    private void StartQuestGameTwo()
    {
        Debug.Log("StartTwo");
        /*
        for (int i = 0; i < questionsToAnswer; i++)
        {
            dialogText.text = currentQuestion.question;
            answeredQuestions++;
            GetRandomQuestion();
        }
        gameFinished = true;
        openingText = "Ok so you have finished my Quiz. Now go on. " +
            "The world is large and you may be needed at another place! Goodbye.";
        dialogText.text = openingText;
        */
    }

    IEnumerator WaitPlayerChoice(KeyCode[] codes)
    {
        bool pressed = false;
        while (!pressed)
        {
            foreach (KeyCode k in codes)
            {
                if (Input.GetKeyDown(k))
                {
                    pressed = true;
                    Debug.Log("In Courotine");
                    checkPlayerAnswer(k);
                    break;
                }
            }
        }
        yield return new WaitForSeconds(1);
    }
    private void checkPlayerAnswer(KeyCode k)
    {
        answeredQuestions++;
        if((k == KeyCode.T && currentQuestion.isTrue) || (k == KeyCode.F && !currentQuestion.isTrue))
        {
            dialogText.text = "Yeaaaah! This was correct.";
            playerHealth.IncreaseHealth(rewardHealthPoints);
        } else
        {
            dialogText.text = "Sorry, but that answer wasn't correct.";
            playerHealth.DecreaseHealth(rewardHealthPoints);
        }
        GetRandomQuestion();
        if(answeredQuestions == questionsToAnswer)
        {
            openingText = "Ok so you have finished my Quiz. Now go on. " +
            "The world is large and you may be needed at another place! Goodbye.";
            gameFinished = true;
            dialogText.text = openingText;
        }

    }

    IEnumerator WaitForDialogBox()
    {
        yield return new WaitForSeconds(0.4f);
        dialogText.text = "";
        countPressE = 0;
        dialogBox.SetActive(false);
    }
}
