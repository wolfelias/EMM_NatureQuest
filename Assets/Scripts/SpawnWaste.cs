using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWaste : MonoBehaviour
{
    public Transform laundrySoapBottle, milkJug, plasticBottle, polystyreneFoamCup, pressurizedSprayCan, sodaCan, steelCan, tunaCan;
    public Transform brownBag, cardboardBox, newspaper;
    public Transform apple, driedFlower, eggs, fishbone, tea;
    public Transform brokenCup, brokenMirror, cigarette, mask, pizzaCarton;
    public Transform beerBottle, greenSodaBottle, masonJar, wineGlass;
    
    public Transform player;
    public LayerMask triggerLayer;

    private int count, amount, whatToSpawn;
    private List<Transform> wasteList;
    private WasteScript wasteScript;

    private float spawnPosX, spawnPosY;

    public int timeTilNext = 30;
    public MinigamesManager minigamesManager;
    private int spawnLimit, totalSpawned;

    // Start is called before the first frame update
    void Start()
    {
        // Count the number of objects created
        count = 0;

        // The amount how many objects should be created
        amount = 10;

        // Initiate List
        wasteList = new List<Transform>();

        // Set a random spawn position
        spawnPosX = Random.Range(28.0f, 60.0f);
        spawnPosY = Random.Range(-8.0f, -44.0f);

        // Set the number of spawn limit based on game mode
        if (minigamesManager.isChill)
            spawnLimit = 100;
        else
            spawnLimit = 20;
        totalSpawned = 0;

        // Start the coroutine
        StartCoroutine(Spawn());
    }

    void Update()
    {
        if (Physics2D.OverlapCircle(player.transform.position, 0.2f, triggerLayer) == null)
            {
                DropWaste();
            }
    }

    // Spawn waste for each 30 seconds
    private IEnumerator Spawn()
    {
        while(totalSpawned < spawnLimit)
        {
            // Update random spawn position
            spawnPosX = Random.Range(28.0f, 60.0f);
            spawnPosY = Random.Range(-8.0f, -44.0f);
            if (count != amount)
            {
                // Set the vector 2 position
                Vector2 spawnPos = new Vector2(spawnPosX, spawnPosY);
                whatToSpawn = Random.Range(1, 26);

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
                        wasteList.Add(Instantiate(newspaper, spawnPos, Quaternion.identity));
                        break;
                    case 12:
                        wasteList.Add(Instantiate(apple, spawnPos, Quaternion.identity));
                        break;
                    case 13:
                        wasteList.Add(Instantiate(driedFlower, spawnPos, Quaternion.identity));
                        break;
                    case 14:
                        wasteList.Add(Instantiate(eggs, spawnPos, Quaternion.identity));
                        break;
                    case 15:
                        wasteList.Add(Instantiate(fishbone, spawnPos, Quaternion.identity));
                        break;
                    case 16:
                        wasteList.Add(Instantiate(tea, spawnPos, Quaternion.identity));
                        break;
                    case 17:
                        wasteList.Add(Instantiate(brokenCup, spawnPos, Quaternion.identity));
                        break;
                    case 18:
                        wasteList.Add(Instantiate(brokenMirror, spawnPos, Quaternion.identity));
                        break;
                    case 19:
                        wasteList.Add(Instantiate(cigarette, spawnPos, Quaternion.identity));
                        break;
                    case 20:
                        wasteList.Add(Instantiate(mask, spawnPos, Quaternion.identity));
                        break;
                    case 21:
                        wasteList.Add(Instantiate(pizzaCarton, spawnPos, Quaternion.identity));
                        break;
                    case 22:
                        wasteList.Add(Instantiate(beerBottle, spawnPos, Quaternion.identity));
                        break;
                    case 23:
                        wasteList.Add(Instantiate(greenSodaBottle, spawnPos, Quaternion.identity));
                        break;
                    case 24:
                        wasteList.Add(Instantiate(masonJar, spawnPos, Quaternion.identity));
                        break;
                    case 25:
                        wasteList.Add(Instantiate(wineGlass, spawnPos, Quaternion.identity));
                        break;
                }
                count++;
                totalSpawned++;
            }
            yield return new WaitForSeconds(timeTilNext);
        }
    }

    private void DropWaste()
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
                        wasteScript.PutBack();
                    }
                }
            }
        }
    }

    public void MinusCount()
    {
        count -= 1;
    }
}
