﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceSpawner : MonoBehaviour
{
    [SerializeField] private Transform DevicesHolder;
    [SerializeField] private Transform[] Devices;
    [SerializeField] private int totalDevices;

    // Start is called before the first frame update
    void Awake()
    {
        totalDevices = DevicesHolder.transform.childCount;
        Devices = new Transform[totalDevices];
        Vector2 spawnPos = new Vector2(0, 12);

        // int j = Random.Range(0, Devices.Length);
        // int range = Random.Range(1, Devices.Length - 1);
        for (int i = 0; i < Devices.Length; i++)
        {
            // if (j >= Devices.Length) j %= Devices.Length;
            Devices[i] = DevicesHolder.transform.GetChild(i).transform;
            Transform device = Instantiate(Devices[i], spawnPos, Quaternion.identity) as Transform;
            device.SetParent(GameObject.Find("DeviceSpawner").transform);
            device.localPosition = spawnPos;
            spawnPos.y -= 4;
            // j += range;
        }
    }
}