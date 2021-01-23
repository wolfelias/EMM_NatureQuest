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
    [SerializeField] private int straightLines = 0;
    [SerializeField] private bool isCompleted;

    void Start()
    {
        LinesList = new List<GameObject>();

        totalPlugs = PlugsHolder.transform.childCount;
        Plugs = new Transform[totalPlugs];

        totalDevices = DevicesHolder.transform.childCount;
        Devices = new Transform[totalDevices];

        int j = Random.Range(0, Devices.Length);
        int range = Random.Range(2, Devices.Length - 1);

        for (int i = 0; i < Plugs.Length; i++)
        {
            if (j >= Devices.Length) j %= Devices.Length;
            Plugs[i] = PlugsHolder.transform.GetChild(i).transform;
            Devices[j] = DevicesHolder.transform.GetChild(j).transform;

            Transform[] lines = { Plugs[i], Devices[j] };
            line.GetComponent<DrawLine>().SetPoints(lines);

            LinesList.Add(Instantiate(line, Vector3.zero, Quaternion.identity));

            j += range;
        }

        StartCoroutine(CheckGameCompleted());
    }

    private IEnumerator CheckGameCompleted()
    {
        while (true)
        {
            straightLines = 0;
            foreach (GameObject line in LinesList)
            {   
                if (line.GetComponent<LineController>().CheckStraight())
                {
                    straightLines++;
                }
            }
            if (straightLines == LinesList.Count)
            {
                isCompleted = true;
            }
            yield return new WaitForSeconds(1);
        }
    }
}
