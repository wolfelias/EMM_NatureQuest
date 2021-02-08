using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! @file SpawnWaste.cs
 *
 *  @brief A script used for waste sorting minigame
 *
 *  @author Sunan Regi Maunakea
 *
 *  Waste sorting minigame is one of the minigames in Nature Quest, where
 *  player must sort the waste to its corresponding waste bin. If the waste
 *  falls to the correct waste bin, health increases by 2 points. Otherwise,
 *  health decreases. There are 5 different types of waste which are recyclable
 *  waste, paper waste, organic waste, household waste, and glass waste. In
 *  chill mode, there will be in total 17 waste to be spawned making it 34 points
 *  in total if player is able to correctly dispose all of the waste. Meanwhile,
 *  in survival mode there will only be 12 waste to be spawned. The waste will
 *  be spawned at random positions inside the trigger area.
 */
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

    /*! @brief Start method of the script
     *  
     *  Count the number of objects created. Set the amount how many prefabs
     *  should be instantiated. Set a random position for the first waste to
     *  spawn. Set the number of spawn limit based on game mode.
     */
    void Start()
    {
        count = 0;
        amount = 10;
        wasteList = new List<Transform>();
        spawnPosX = Random.Range(28.0f, 60.0f);
        spawnPosY = Random.Range(-8.0f, -44.0f);

        if (minigamesManager.isChill)
            spawnLimit = 17;
        else
            spawnLimit = 12;
        totalSpawned = 0;

        StartCoroutine(Spawn());
    }

    /*! @brief Update method of the script
     *  
     *  Check every frame if player is leaving the trigger area
     */
    void Update()
    {
        if (Physics2D.OverlapCircle(player.transform.position, 0.2f, triggerLayer) == null)
            {
                DropWaste();
            }
    }

    /*!
     *  IEnumerator for spawning waste for each a couple of seconds. Update random spawn positon.
     *  Instantiate copies of random Prefab and add to list.
     */
    private IEnumerator Spawn()
    {
        while(totalSpawned < spawnLimit)
        {
            spawnPosX = Random.Range(28.0f, 60.0f);
            spawnPosY = Random.Range(-8.0f, -44.0f);
            if (count != amount)
            {
                Vector2 spawnPos = new Vector2(spawnPosX, spawnPosY);
                whatToSpawn = Random.Range(1, 26);

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

    /*!
     *  Put back the equipped waste when player leaves the trigger area
     */
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

    /*!
     *  Decrease the amount of spawned waste
     */
    public void MinusCount()
    {
        count -= 1;
    }
}
