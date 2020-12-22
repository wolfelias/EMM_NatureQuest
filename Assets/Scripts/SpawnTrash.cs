using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrash : MonoBehaviour
{
    public Transform myTrash;
    private int count, amount;

    // Start is called before the first frame update
    void Start()
    {
        // Count the number of objects created
        count = 0;

        // The amount how many objects should be created
        amount = Random.Range(0, 10);
    }

    // Update is called once per frame
    void Update()
    {
        // Set a random spawn position
        float spawnPosX = Random.Range(-10.0f, 10.0f);
        float spawnPosY = Random.Range(-20.0f, 20.0f);
        if (count != amount)
        {
            // Set the vector 3 position
            Vector2 spawnPos = new Vector2(spawnPosX, spawnPosY);

            // Instantiate copies of Prefab each with different rotations
            Instantiate(myTrash, spawnPos, Quaternion.identity);
            count++;
        }
    }
}
