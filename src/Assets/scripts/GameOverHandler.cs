using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverHandler : MonoBehaviour {

    public GameObject sheepHolder;
    private WorldDayNightCycleHandler worldDayNightCycleHandler;
    public bool gameOver = false;

    public void Awake()
    {
        worldDayNightCycleHandler = GetComponent<WorldDayNightCycleHandler>();
    }

    // Update is called once per frame
    public void GameOver()
    {
        gameOver = true;
        foreach (Transform sheep in sheepHolder.transform)
        {
            sheep.gameObject.GetComponent<SheepHandler>().StopMoving();            
        }
	}

    private void OnGUI()
    {
        if (gameOver)
        {
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Game Over! You Survived " + worldDayNightCycleHandler.daysSurvived + " Days...");
        }
    }

}
