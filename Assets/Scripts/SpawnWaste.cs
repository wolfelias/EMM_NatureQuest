using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWaste : MonoBehaviour
{
    public Transform cutlery, drinkCan, film, plasticBottle, styrofoam;
    public Transform brochure, magazine, newspaper, paperBag, tissue;
    public Transform apple, banana, coffeeGround, eggShell, teaBag;
    public Transform backingPaper, cigaretteButt, diaper, paint, vacuumCleanerBag;
    public Transform beer, jamJar, wineBottle;
    
    public Transform player;
    public LayerMask triggerLayer;

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
            float spawnPosY = Random.Range(-20.0f, 20.0f);
            if (count != amount)
            {
                // Set the vector 3 position
                Vector2 spawnPos = new Vector2(spawnPosX, spawnPosY);
                whatToSpawn = Random.Range(1, 24);

                // Instantiate copies of random Prefab and add to list
                switch (whatToSpawn)
                {
                    case 1:
                        wasteList.Add(Instantiate(cutlery, spawnPos, Quaternion.identity));
                        break;
                    case 2:
                        wasteList.Add(Instantiate(drinkCan, spawnPos, Quaternion.identity));
                        break;
                    case 3:
                        wasteList.Add(Instantiate(film, spawnPos, Quaternion.identity));
                        break;
                    case 4:
                        wasteList.Add(Instantiate(plasticBottle, spawnPos, Quaternion.identity));
                        break;
                    case 5:
                        wasteList.Add(Instantiate(styrofoam, spawnPos, Quaternion.identity));
                        break;
                    case 6:
                        wasteList.Add(Instantiate(brochure, spawnPos, Quaternion.identity));
                        break;
                    case 7:
                        wasteList.Add(Instantiate(magazine, spawnPos, Quaternion.identity));
                        break;
                    case 8:
                        wasteList.Add(Instantiate(newspaper, spawnPos, Quaternion.identity));
                        break;
                    case 9:
                        wasteList.Add(Instantiate(paperBag, spawnPos, Quaternion.identity));
                        break;
                    case 10:
                        wasteList.Add(Instantiate(tissue, spawnPos, Quaternion.identity));
                        break;
                    case 11:
                        wasteList.Add(Instantiate(apple, spawnPos, Quaternion.identity));
                        break;
                    case 12:
                        wasteList.Add(Instantiate(banana, spawnPos, Quaternion.identity));
                        break;
                    case 13:
                        wasteList.Add(Instantiate(coffeeGround, spawnPos, Quaternion.identity));
                        break;
                    case 14:
                        wasteList.Add(Instantiate(eggShell, spawnPos, Quaternion.identity));
                        break;
                    case 15:
                        wasteList.Add(Instantiate(teaBag, spawnPos, Quaternion.identity));
                        break;
                    case 16:
                        wasteList.Add(Instantiate(backingPaper, spawnPos, Quaternion.identity));
                        break;
                    case 17:
                        wasteList.Add(Instantiate(cigaretteButt, spawnPos, Quaternion.identity));
                        break;
                    case 18:
                        wasteList.Add(Instantiate(diaper, spawnPos, Quaternion.identity));
                        break;
                    case 19:
                        wasteList.Add(Instantiate(paint, spawnPos, Quaternion.identity));
                        break;
                    case 20:
                        wasteList.Add(Instantiate(vacuumCleanerBag, spawnPos, Quaternion.identity));
                        break;
                    case 21:
                        wasteList.Add(Instantiate(beer, spawnPos, Quaternion.identity));
                        break;
                    case 22:
                        wasteList.Add(Instantiate(jamJar, spawnPos, Quaternion.identity));
                        break;
                    case 23:
                        wasteList.Add(Instantiate(wineBottle, spawnPos, Quaternion.identity));
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
                if (wasteList[i] != null)
                {
                    Destroy(wasteList[i].gameObject);
                }
            }
            wasteList.Clear();

            // Reset the counter and set a new amount
            count = 0;
            amount = Random.Range(5, 10);
        }
    }
}
