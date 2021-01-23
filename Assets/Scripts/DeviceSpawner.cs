using System.Collections;
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
        Vector2 spawnPos = new Vector2(43, -9);

        int j = Random.Range(0, Devices.Length);
        int range = Random.Range(1, Devices.Length - 1);
        for (int i = 0; i < Devices.Length; i++)
        {
            if (j >= Devices.Length) j %= Devices.Length;
            Devices[j] = DevicesHolder.transform.GetChild(j).transform;
            (Instantiate(Devices[j], spawnPos, Quaternion.identity) as Transform).SetParent(GameObject.Find("DeviceSpawner").transform);
            spawnPos.y -= 4;
            j += range;
        }
    }
}
