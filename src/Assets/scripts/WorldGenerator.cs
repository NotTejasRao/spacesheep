using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour {

    public GameObject grassRockPrefab;
    public GameObject sheepPrefab;
    public GameObject uraniumPrefab;
    public GameObject grassRockHolder;
    public GameObject sheepHolder;
    public GameObject uraniumHolder;
    public int numberOfGrassRocks = 500;
    public int numberOfSheeps = 100;
    public int numberOfUraniums = 100;
    public SpaceshipHandler spaceshipHandler;

	void Start()
    {
        GenerateGrassRocks(numberOfGrassRocks);
        GenerateSheeps(numberOfSheeps);
        GenerateUraniums(numberOfUraniums);
	}

    private void Update()
    {
        if (numberOfSheeps < 20)
        {
            GenerateSheeps(30);
        }

        if (numberOfUraniums < spaceshipHandler.uraniumNeeded)
        {
            GenerateUraniums(100);
        }
    }

    void GenerateGrassRocks(int numberOfGrassRocks)
    {
        for (int i = 0; i < numberOfGrassRocks; i++)
        {
            GameObject newGrassRock = Object.Instantiate(grassRockPrefab, GetRandomPosition(), Quaternion.identity);
            newGrassRock.transform.SetParent(grassRockHolder.transform);
        }
    }

    void GenerateSheeps(int numberOfSheeps)
    {
        for (int i = 0; i < numberOfSheeps; i++)
        {
            GameObject newSheep = Object.Instantiate(sheepPrefab, GetRandomPosition(), Quaternion.identity);
            newSheep.transform.SetParent(sheepHolder.transform);
        }
    }
    
    void GenerateUraniums(int numberOfUraniums)
    {
        for (int i = 0; i < numberOfUraniums; i++)
        {
            GameObject newUranium = Object.Instantiate(uraniumPrefab, GetRandomPosition(), Quaternion.identity);
            newUranium.transform.SetParent(uraniumHolder.transform);
        }
    }

    Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(-250, 250), 2, Random.Range(-250, 250));
    }
	
}
