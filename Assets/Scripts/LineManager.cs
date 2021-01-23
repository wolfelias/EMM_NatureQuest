using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    [SerializeField] private GameObject line;
    public Transform laptop, monitor, pc, smartphone, mixer, blender, toaster;
    public Transform plug1, plug2, plug3, plug4, plug5, plug6, plug7;

    void Awake()
    {
        for (int i = 0; i < 7; i++)
        {
            Instantiate(line, Vector3.zero, Quaternion.identity);
            if (i == 0)
            {
                Transform[] lines = { plug1, laptop };
                line.GetComponent<DrawLine>().SetPoints(lines);
            }
            if (i == 1)
            {
                Transform[] lines = { plug2, monitor };
                line.GetComponent<DrawLine>().SetPoints(lines);
            }
            if (i == 2)
            {
                Transform[] lines = { plug3, pc };
                line.GetComponent<DrawLine>().SetPoints(lines);
            }
            if (i == 3)
            {
                Transform[] lines = { plug4, smartphone };
                line.GetComponent<DrawLine>().SetPoints(lines);
            }
            if (i == 4)
            {
                Transform[] lines = { plug5, mixer };
                line.GetComponent<DrawLine>().SetPoints(lines);
            }
            if (i == 5)
            {
                Transform[] lines = { plug6, blender };
                line.GetComponent<DrawLine>().SetPoints(lines);
            }
            if (i == 6)
            {
                Transform[] lines = { plug7, toaster };
                line.GetComponent<DrawLine>().SetPoints(lines);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
