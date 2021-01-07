using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWaste : MonoBehaviour
{
    public Transform laundrySoapBottle, milkJug, plasticBottle, polystyreneFoamCup, pressurizedSprayCan, sodaCan, steelCan, tunaCan;
    public Transform brownBag, cardboardBox, greasyPizzaBox, newspaper;
    public Transform apple, banana, coffeeBeans, eggs, teaLeaf;
    public Transform ceramicMug;
    public Transform beerBottle, greenSodaBottle, masonJar, wineGlass;
    
    public Transform player;
    public LayerMask triggerLayer;

    private int count, amount, whatToSpawn;
    private List<Transform> wasteList;
    private WasteScript wasteScript;

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
                        wasteList.Add(Instantiate(laundrySoapBottle, spawnPos, Quaternion.identity));
                        break;
                    case 2:
                        wasteList.Add(Instantiate(milkJug, spawnPos, Quaternion.identity));
                        break;
                    case 3:
                        wasteList.Add(Instantiate(plasticBottle, spawnPos, Quaternion.identity));
                        break;
                    case 4:
                        wasteList.Add(Instantiate(polystyreneFoamCup, spawnPos, Quaternion.identity));
                        break;
                    case 5:
                        wasteList.Add(Instantiate(pressurizedSprayCan, spawnPos, Quaternion.identity));
                        break;
                    case 6:
                        wasteList.Add(Instantiate(sodaCan, spawnPos, Quaternion.identity));
                        break;
                    case 7:
                        wasteList.Add(Instantiate(steelCan, spawnPos, Quaternion.identity));
                        break;
                    case 8:
                        wasteList.Add(Instantiate(tunaCan, spawnPos, Quaternion.identity));
                        break;
                    case 9:
                        wasteList.Add(Instantiate(brownBag, spawnPos, Quaternion.identity));
                        break;
                    case 10:
                        wasteList.Add(Instantiate(cardboardBox, spawnPos, Quaternion.identity));
                        break;
                    case 11:
                        wasteList.Add(Instantiate(greasyPizzaBox, spawnPos, Quaternion.identity));
                        break;
                    case 12:
                        wasteList.Add(Instantiate(newspaper, spawnPos, Quaternion.identity));
                        break;
                    case 13:
                        wasteList.Add(Instantiate(apple, spawnPos, Quaternion.identity));
                        break;
                    case 14:
                        wasteList.Add(Instantiate(banana, spawnPos, Quaternion.identity));
                        break;
                    case 15:
                        wasteList.Add(Instantiate(coffeeBeans, spawnPos, Quaternion.identity));
                        break;
                    case 16:
                        wasteList.Add(Instantiate(eggs, spawnPos, Quaternion.identity));
                        break;
                    case 17:
                        wasteList.Add(Instantiate(teaLeaf, spawnPos, Quaternion.identity));
                        break;
                    case 18:
                        wasteList.Add(Instantiate(ceramicMug, spawnPos, Quaternion.identity));
                        break;
                    case 19:
                        wasteList.Add(Instantiate(beerBottle, spawnPos, Quaternion.identity));
                        break;
                    case 20:
                        wasteList.Add(Instantiate(greenSodaBottle, spawnPos, Quaternion.identity));
                        break;
                    case 21:
                        wasteList.Add(Instantiate(masonJar, spawnPos, Quaternion.identity));
                        break;
                    case 22:
                        wasteList.Add(Instantiate(wineGlass, spawnPos, Quaternion.identity));
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
                    wasteScript = wasteList[i].gameObject.GetComponent<WasteScript>();
                    if(wasteScript.equipped)
                    {
                        wasteScript.Detach();
                    }
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
