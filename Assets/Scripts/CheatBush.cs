using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CheatBush : MonoBehaviour
{
    private Health playerHealth;
    private int playerOnEnterHealth;
    public Signal signal;
    public Animator animator;
    public GameObject dialogBox;
    public Text dialogText;
    private bool lastText = true;
    private bool playerInRange;
    private string openingText = "Hello Adventurer! My name is Mr. Bush!";
    private bool wantsToCheat = false;

    /**
    *   Array of all texts the CheatBush is holding
    *   questions must be >= questionsToAnswer
    */
    
    public Question[] questions;
    private static List<Question> unansweredQuestions;
    private int questionsToAnswer = 1;

    private Question currentQuestion;
    private int countPressE = 0;
    private int answeredQuestions = 0;
    private bool gameFinished = false;
    private bool isInGame = false;


    /**
    *   initializes CheatBush GameObject
    */
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

    /**
    *   checks if player is in trigger zone of the cheat bush and counts the times E was pressed
    *   depending on the countPressE and isInGame Variable it will react different to the user input
    */
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
                openingText = "You've made your decision. Now go on. Hush hush.";
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
            if (Input.GetKeyDown(KeyCode.Y) && playerInRange)
            {
                checkPlayerAnswer(KeyCode.Y);
            }
            if (Input.GetKeyDown(KeyCode.N) && playerInRange)
            {
                checkPlayerAnswer(KeyCode.N);
            }
        }
    }

    
    /**
    * on player entering the trigger it raises the signal above players head and sets playerInRange = true  
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
    *   when player leaves the field and wants to cheat set his health to 100 so he wins automatically
    *   else set playerInRange to false deactivate the signal above players head and close dialog
    */
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
                answeredQuestions = 0;
                isInGame = false;
                countPressE = 0;
            }
            if(wantsToCheat)
            {
                playerHealth.SetHealth(100);
            }
            StartCoroutine(WaitForDialogBox());
        }
    }

    /**
    *   set a random text from the questions Array as the current text
    *   it just has one text to show so it just choses that one text every time
    */
    void GetRandomQuestion()
    {
        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];

        unansweredQuestions.RemoveAt(randomQuestionIndex);
    }

    /**
    * controls the answer of the player and sets wantsToCheat to either true or false  
    */
    private void checkPlayerAnswer(KeyCode k)
    {
        answeredQuestions++;
        if (k == KeyCode.Y)
        {
            //dialogText.text = "Yeaaaah! This was correct.";
            StopAllCoroutines();
            StartCoroutine(TypeText(currentQuestion.answerRight));
            //playerHealth.IncreaseHealth(rewardHealthPoints);
            wantsToCheat = true;
            isInGame = false;
        }
        else
        {
            //dialogText.text = "Sorry, but that answer wasn't correct.";
            StopAllCoroutines();
            StartCoroutine(TypeText(currentQuestion.answerWrong));
            //playerHealth.DecreaseHealth(rewardHealthPoints);
            wantsToCheat = false;
            isInGame = false;
        }
        if (answeredQuestions < questionsToAnswer)
        {
            GetRandomQuestion();
        }
    }

    /**
    *   creates an animation effect of all the letters in a text
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
    * Wait for the dialogbox to disappear  
    */
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
