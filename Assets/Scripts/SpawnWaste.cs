using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWaste : MonoBehaviour
{
    [SerializeField]
    private Transform plasticWaste, paperWaste,
    organicWaste, hazardousWaste, glassWaste;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private LayerMask triggerLayer;

    private int count, amount, whatToSpawn;
    private List<Transform> wasteList;

    // Start is called before the first frame update
    void Start()
    {
        // Count the number of objects created
        count = 0;

        // The amount how many objects should be created
        amount = Random.Range(5, 10);

        // Initiate List
        wasteList = new List<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.OverlapCircle(player.transform.position, 0.2f, triggerLayer) != null)
        {
            // Set a random spawn position
            float spawnPosX = Random.Range(15.0f, 45.0f);
            float spawnPosY = Random.Range(-25.0f, 25.0f);
            if (count != amount)
            {
                // Set the vector 3 position
                Vector2 spawnPos = new Vector2(spawnPosX, spawnPosY);
                whatToSpawn = Random.Range(1, 6);

                // Instantiate copies of random Prefab and add to list
                switch (whatToSpawn)
                {
                    case 1:
                        wasteList.Add(Instantiate(plasticWaste, spawnPos, Quaternion.identity));
                        break;
                    case 2:
                        wasteList.Add(Instantiate(paperWaste, spawnPos, Quaternion.identity));
                        break;
                    case 3:
                        wasteList.Add(Instantiate(organicWaste, spawnPos, Quaternion.identity));
                        break;
                    case 4:
                        wasteList.Add(Instantiate(hazardousWaste, spawnPos, Quaternion.identity));
                        break;
                    case 5:
                        wasteList.Add(Instantiate(glassWaste, spawnPos, Quaternion.identity));
                        break;
                }
                count++;
            }
        }
        else
        {
            ClearWaste();
        }
    }

    private void ClearWaste()
    {
        if (wasteList != null)
        {
            for (int i = 0; i < wasteList.Count; i++)
            {
                Destroy(wasteList[i].gameObject);
            }
            wasteList.Clear();

            // Reset the counter and set a new amount
            count = 0;
            amount = Random.Range(5, 10);
        }
    }
}
