﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWasteBins : MonoBehaviour
{
    [SerializeField]
    private Transform plasticBin, paperBin,
    organicBin, hazardousBin, glassBin;
    private List<Transform> binList;

    // Start is called before the first frame update
    void Start()
    {
        binList = new List<Transform>();

        // Spawn waste bins
        SpawnBins();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnBins()
    {
        binList.Add(Instantiate(plasticBin, new Vector2(22, 0), Quaternion.identity));
        binList.Add(Instantiate(paperBin, new Vector2(26, 0), Quaternion.identity));
        binList.Add(Instantiate(organicBin, new Vector2(30, 0), Quaternion.identity));
        binList.Add(Instantiate(hazardousBin, new Vector2(34, 0), Quaternion.identity));
        binList.Add(Instantiate(glassBin, new Vector2(38, 0), Quaternion.identity));
    }

    private void ClearBin()
    {
        if (binList != null)
        {
            for (int i = 0; i < binList.Count; i++)
            {
                Destroy(binList[i].gameObject);
            }
            binList.Clear();
        }
    }
}
