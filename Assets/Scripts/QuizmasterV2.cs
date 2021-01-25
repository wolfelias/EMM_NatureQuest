using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class QuizmasterV2 : MonoBehaviour
{
    private Health playerHealth;
    private int playerOnEnterHealth;
    public Signal signal;
    public Animator animator;
    public GameObject dialogBox;
    public Text dialogText;
    private bool lastText = true;
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
        // activates the 2nd time the player talks to the Quizmaster --> starts the game
        if (Input.GetKeyDown(KeyCode.E) && playerInRange && countPressE == 1 && !gameFinished)
        {
            countPressE++;
            isInGame = true;
            playerOnEnterHealth = playerHealth.CurHealth;
            //dialogText.text = currentQuestion.question;
            StopAllCoroutines();
            StartCoroutine(TypeText(currentQuestion.question));
            // StartQuestGame();
            // StartQuestGameTwo();
        }
        // activates the first time the player talks to the Quizmaster --> starts dialog
        if (Input.GetKeyDown(KeyCode.E) && playerInRange && countPressE == 0)
        {
            dialogBox.SetActive(true);
            //dialogText.text = openingText;
            StopAllCoroutines();
            StartCoroutine(TypeText(openingText));
            animator.SetBool("IsOpen", true);
            countPressE++;
        }
        // every time the player talks to the quizmaster, after game started
        if (Input.GetKeyDown(KeyCode.E) && playerInRange && countPressE > 1 && !isInGame)
        {
            if (answeredQuestions == questionsToAnswer)
            {
                openingText = "Ok so you have finished my Quiz. Now go on. " +
                "The world is large and you may be needed at another place! Goodbye.";
                gameFinished = true;
                isInGame = false;
                //dialogText.text = openingText;
                if (lastText)
                {
                    lastText = false;
                    StopAllCoroutines();
                    StartCoroutine(TypeText(openingText));
                }
                else
                {
                    dialogText.text = openingText;
                }
            }
            else
            {
                isInGame = true;
                //dialogText.text = currentQuestion.question;
                StopAllCoroutines();
                StartCoroutine(TypeText(currentQuestion.question));
            }
        }
        if (isInGame && !gameFinished)
        {
            if (Input.GetKeyDown(KeyCode.T) && playerInRange)
            {
                checkPlayerAnswer(KeyCode.T);
            }
            if (Input.GetKeyDown(KeyCode.F) && playerInRange)
            {
                checkPlayerAnswer(KeyCode.F);
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

            if (!gameFinished)
            {
                unansweredQuestions = questions.ToList<Question>();
                for (int i = 0; i < answeredQuestions; i++)
                {
                    if (playerHealth.CurHealth > playerOnEnterHealth)
                    {
                        playerHealth.DecreaseHealth(rewardHealthPoints);
                    }
                    else if (playerHealth.CurHealth < playerOnEnterHealth)
                    {
                        playerHealth.IncreaseHealth(rewardHealthPoints);
                    }
                }
                answeredQuestions = 0;
                isInGame = false;
                countPressE = 0;
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

    /*
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
    */

    private void checkPlayerAnswer(KeyCode k)
    {
        answeredQuestions++;
        if ((k == KeyCode.T && currentQuestion.isTrue) || (k == KeyCode.F && !currentQuestion.isTrue))
        {
            //dialogText.text = "Yeaaaah! This was correct.";
            StopAllCoroutines();
            StartCoroutine(TypeText(currentQuestion.answerRight));
            playerHealth.IncreaseHealth(rewardHealthPoints);
            isInGame = false;
        }
        else
        {
            //dialogText.text = "Sorry, but that answer wasn't correct.";
            StopAllCoroutines();
            StartCoroutine(TypeText(currentQuestion.answerWrong));
            playerHealth.DecreaseHealth(rewardHealthPoints);
            isInGame = false;
        }
        if (answeredQuestions < questionsToAnswer)
        {
            GetRandomQuestion();
        }
    }

    IEnumerator TypeText(string text)
    {
        dialogText.text = "";
        foreach (char letter in text.ToCharArray())
        {
            dialogText.text += letter;
            yield return null;
        }
    }

    IEnumerator WaitForDialogBox()
    {
        yield return new WaitForSeconds(0.4f);
        if (!gameFinished)
        {
            dialogText.text = "";
        }
        countPressE = 0;
        dialogBox.SetActive(false);
    }
}
