using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipHandler : MonoBehaviour {

    public int uraniumNeeded = 1;
    public PlayerInventoryHandler playerInventoryHandler;
    public GameOverHandler gameOverHandler;


    public void UpdatePower(int days)
    {
        if (uraniumNeeded == 0)
        {
            uraniumNeeded = days += Random.Range(days, days*2);
        }
        else
        {
            // Game over
            gameOverHandler.GameOver();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (playerInventoryHandler.uraniumCount >= uraniumNeeded)
            {
                playerInventoryHandler.uraniumCount -= uraniumNeeded;
                uraniumNeeded = 0;                
            }
            else
            {
                uraniumNeeded -= playerInventoryHandler.uraniumCount;
                playerInventoryHandler.uraniumCount = 0;
            }
        }
    }

    void OnGUI()
    {
        GUI.Box(new Rect(10, 30, 150, 20), "Uranium Needed: " + uraniumNeeded);
    }

}
