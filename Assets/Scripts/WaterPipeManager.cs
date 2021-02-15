using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! @file WaterPipeManager.cs
 *
 *  @brief A script used for water pipe minigame
 *
 *  @author Sunan Regi Maunakea
 *
 *  Water pipe minigame is one of the minigames in Nature Quest, where
 *  player must complete the water pipe puzzle before running out of
 *  health. When the player is in the minigame area, the health will
 *  decrease slowly. There are in total 4 different sets of water pipe
 *  puzzle and will be randomly selected at the start of the game. In
 *  chill mode, the puzzle will be reset 15 seconds after copmletion.
 *  Meanwhile in survival mode, puzzle will not be reset.
 */
[RequireComponent(typeof(AudioSource))]
public class WaterPipeManager : MonoBehaviour
{
    public GameObject[] PipeSet;
    public GameObject[] Pipes;

    [SerializeField]
    private int totalPipes = 0;
    [SerializeField]
    private int correctedPipes = 0;
    public bool isCompleted;

    private Health playerHealth;
    private bool isEntered;
    private GameObject PipesHolder;
    private GameObject Temp;
    private int selection;
    private int tempHealth;

    public MinigamesManager minigamesManager;

    AudioSource audio;

    /*! @brief Start method of the script
     *  
     *  Retrieve the AudioSource component and player health. Spawn the pipe set prefab
     */
    void Start()
    {
        audio = GetComponent<AudioSource>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        selection = Random.Range(0, 4);

        SpawnPipeSet();

        StartCoroutine(WaterAreaDamage());
        StartCoroutine(CheckCompleted());
    }

    /*!
     *  IEnumerator to check whether player is in the water minigame area
     */
    private IEnumerator WaterAreaDamage()
    {
        while (true)
        {
            if (isEntered && !isCompleted)
            {
                audio.Play();
                playerHealth.DecreaseHealth(1);
                tempHealth += 1;
            }
            yield return new WaitForSeconds(2);
        }
    }

    /*!
     *  IEnumerator to check whether the minigame is completed
     */
    private IEnumerator CheckCompleted()
    {
        while (true)
        {
            if (isCompleted && minigamesManager.isChill)
            {
                selection++;
                if (selection == PipeSet.Length)
                    selection = 0;
                yield return new WaitForSeconds(15);
                Destroy(Temp);
                SpawnPipeSet();
            }
            yield return new WaitForSeconds(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            isEntered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            isEntered = false;
        }
    }

    /*!
     *  If the pipe being rotated has the correct rotation, increase
     *  the number of the corrected pipes
     */
    public void CorrectMove()
    {
        correctedPipes += 1;

        if (correctedPipes == totalPipes)
        {
            isCompleted = true;
            playerHealth.IncreaseHealth(10 + tempHealth);
        }
    }

    /*!
     *  If the pipe being rotated doesn't have the correct rotation,
     *  decrease the number of the corrected pipes
     */
    public void WrongMove()
    {
        correctedPipes -= 1;
    }

    /*!
     *  Spawn a new pipe set
     */
    private void SpawnPipeSet()
    {
        ResetVariables();
        PipesHolder = PipeSet[selection];
        Temp = Instantiate(PipesHolder, new Vector3(35, 67, 0), Quaternion.identity);

        totalPipes = PipesHolder.transform.childCount;
        Pipes = new GameObject[totalPipes];

        for (int i = 0; i < Pipes.Length; i++)
        {
            Pipes[i] = PipesHolder.transform.GetChild(i).gameObject;
        }
    }

    /*!
     *  Reset the variables
     */
    private void ResetVariables()
    {
        correctedPipes = 0;
        isCompleted = false;
        tempHealth = 0;
    }
}
