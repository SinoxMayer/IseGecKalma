using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public GameObject[] rockPrefab;
    public GameObject[] treePrefab;
    public GameObject[] smallThingsPrefab;

    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private Vector3 spawntreePos = new Vector3(25, 0, -5);
    private Vector3 spawnrockPos = new Vector3(25, 0, -5);

    private float startDelay = 3f;
    private float startDelay2 = 0f;
    private float repeatRate = 2.5f;

    private float mmintDelay = 0.0f;
    private float mmintDelay2 = 0.45f;
    private float mmintDelay3 = 1.0f;
    private float mmintDelay4 = 0.6f;

    private float maxtDelay = 5.0f;
    private float maxtDelay2 = 1.1f;
    private float maxtDelay3 = 2.3f;
    private float maxtDelay4 = 1.5f;

    private float spawnInterval;
    private float spawnInterval2;
    private float spawnInterval3;
    private float spawnInterval4;


    private PlayerController playerControllerScript;


    // Start is called before the first frame update
    void Start()
    {

        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

        //InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        StartCoroutine(RndmTimeSpawner());
        StartCoroutine(RndmTimeObsticalSpawner());
        StartCoroutine(RndmTimeRockSpawner());
        StartCoroutine(RndmTimeSmallThingsSpawner());
    }

    // Update is called once per frame
    void Update()
    {
        spawnInterval = Random.Range(mmintDelay, maxtDelay);
        spawnInterval2 = Random.Range(mmintDelay2, maxtDelay2);
        spawnInterval3 = Random.Range(mmintDelay3, maxtDelay3);
        spawnInterval4 = Random.Range(mmintDelay4, maxtDelay4);
      
    }

    void SpawnObstacle()
    {

        if (playerControllerScript.gameOver == false)
        {

            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
     
  

        }

    
    }

    void SpawnTree()
    {
        if (playerControllerScript.gameOver == false)
        {
            int treeIndex = Random.Range(0, treePrefab.Length);

            Instantiate(treePrefab[treeIndex], spawntreePos, treePrefab[treeIndex].transform.rotation);
        }
    }

    void SpawnRock()
    {


        if (playerControllerScript.gameOver == false)
        {
            int rockIndex = Random.Range(0,rockPrefab.Length);

            Instantiate(rockPrefab[rockIndex], spawnrockPos, rockPrefab[rockIndex].transform.rotation);
        }
    
    }

    void SpawnSmallThings()
    {

        Vector3 spawnSmallThngsPos = new Vector3(25,0,Random.Range(-4, 4));
        if (playerControllerScript.gameOver == false)
        {
            int treeIndex = Random.Range(0, smallThingsPrefab.Length);

            Instantiate(smallThingsPrefab[treeIndex], spawnSmallThngsPos, smallThingsPrefab[treeIndex].transform.rotation);
        }
    }

    private IEnumerator RndmTimeObsticalSpawner()
    {
        yield return new WaitForSeconds(startDelay);
        while (playerControllerScript.gameObject)
        {

            SpawnObstacle();
            yield return new WaitForSeconds(spawnInterval3);
        }

    }

    private IEnumerator RndmTimeSpawner()
    {
        yield return new WaitForSeconds(startDelay2);
        while (playerControllerScript.gameObject)
        {

            SpawnTree();
            yield return new WaitForSeconds(spawnInterval);
        }
        
    }

    private IEnumerator RndmTimeSmallThingsSpawner()
    {
        yield return new WaitForSeconds(startDelay2);
        while (playerControllerScript.gameObject)
        {

            SpawnSmallThings();
            yield return new WaitForSeconds(spawnInterval4);
        }

    }
    private IEnumerator RndmTimeRockSpawner()
    {
        yield return new WaitForSeconds(startDelay2);

        while (playerControllerScript.gameObject)
        {
            SpawnRock();
            yield return new WaitForSeconds(spawnInterval2);

        }

    }
}
