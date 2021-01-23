using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    [SerializeField] private GameObject line;

    public Transform DevicesHolder;
    public Transform[] Devices;
    public int totalDevices;

    public Transform PlugsHolder;
    public Transform[] Plugs;
    public int totalPlugs;

    private List<GameObject> LinesList;
    public int straightLines = 0;
    public bool isCompleted;
    public bool isEntered;

    private Health playerHealth;

    void Start()
    {
        // Get the health of the player
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();

        LinesList = new List<GameObject>();

        totalPlugs = PlugsHolder.transform.childCount;
        Plugs = new Transform[totalPlugs];

        totalDevices = DevicesHolder.transform.childCount;
        Devices = new Transform[totalDevices];

        // Create the lines between plugs and devices
        SpawnLines();

        // Coroutine to check if the game is completed
        StartCoroutine(CheckGameCompleted());
        StartCoroutine(CableAreaDamage());
    }

    private IEnumerator CheckGameCompleted()
    {
        while (true)
        {
            if(!isCompleted)
            {
                straightLines = 0;
                foreach (GameObject line in LinesList)
                {   
                    if (line.GetComponent<LineController>().CheckStraight())
                    {
                        straightLines++;
                    }
                }
                // If game is completed, destroy the lines and spawn new lines after 15 seconds
                if (straightLines == LinesList.Count)
                {
                    isCompleted = true;
                    playerHealth.IncreaseHealth(15);
                    yield return new WaitForSeconds(15);
                    DestroyLines();
                    SpawnLines();
                }
            }
            else
            {
                isCompleted = false;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator CableAreaDamage()
    {
        while (true)
        {
            if (isEntered && !isCompleted)
            {
                playerHealth.DecreaseHealth(1);
            }
            yield return new WaitForSeconds(3);
        }
    }

    private void SpawnLines()
    {
        // Set randomized lines
        int j = Random.Range(0, Devices.Length);
        int range = Random.Range(2, Devices.Length - 1);

        for (int i = 0; i < Plugs.Length; i++)
        {
            if (j >= Devices.Length) j %= Devices.Length;
            Plugs[i] = PlugsHolder.transform.GetChild(i).transform;
            Devices[j] = DevicesHolder.transform.GetChild(j).transform;

            Transform[] lines = { Plugs[i], Devices[j] };
            line.GetComponent<DrawLine>().SetPoints(lines);

            GameObject cable = Instantiate(line, Vector3.zero, Quaternion.identity);
            cable.transform.SetParent(GameObject.Find("LineManager").transform);
            LinesList.Add(cable);

            j += range;
        }
    }

    private void DestroyLines()
    {
        for (int i = 0; i < LinesList.Count; i++)
        {
            Destroy(LinesList[i]);
        }
        LinesList.Clear();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isEntered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isEntered = false;
        }
    }
}
