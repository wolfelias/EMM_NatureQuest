using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    // Start is called before the first frame update
    void Start()
    {
        // Get the health of the player
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        selection = Random.Range(0, 4);

        // Spawn the pipe set prefab
        SpawnPipeSet();

        StartCoroutine(WaterAreaDamage());
        StartCoroutine(CheckCompleted());
    }

    // If Player enters the water minigame, the health bar decreases slowly
    private IEnumerator WaterAreaDamage()
    {
        while (true)
        {
            if (isEntered && !isCompleted)
            {
                playerHealth.DecreaseHealth(1);
                tempHealth += 1;
            }
            yield return new WaitForSeconds(2);
        }
    }

    // If the pipe set is completed, spawn a new pipe set after 15 seconds
    // in chill gamemode. In survival mode, the pipe set won't be reseted
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

    // If the pipe being rotated has the correct rotation, increase
    // the number of the corrected pipes
    public void CorrectMove()
    {
        correctedPipes += 1;

        if (correctedPipes == totalPipes)
        {
            isCompleted = true;
            playerHealth.IncreaseHealth(10 + tempHealth);
        }
    }

    // If the pipe being rotated doesn't have the correct rotation,
    // decrease the number of the corrected pipes
    public void WrongMove()
    {
        correctedPipes -= 1;
    }

    // Spawn a new pipe set
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

    private void ResetVariables()
    {
        correctedPipes = 0;
        isCompleted = false;
        tempHealth = 0;
    }
}
