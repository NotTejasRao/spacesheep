using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldDayNightCycleHandler : MonoBehaviour {

    public bool isDay = true;
    public SpaceshipHandler spaceshipHandler;
    public GameObject sheepHolder;
    public float timeTillCycleChange = 15.0f;
    private float timeInCycle;
    public int daysSurvived = 0;

    void Start()
    {
        timeInCycle = timeTillCycleChange;
    }

    void Update()
    {                
        timeInCycle -= Time.deltaTime;
        if (timeInCycle <= 0)
        {
            isDay = !isDay;
            if (!isDay)
            {                
                foreach (Transform sheep in sheepHolder.GetComponentInChildren<Transform>())
                {
                    sheep.GetComponent<SheepHandler>().SetSheepHostile();
                }
            }
            else
            {
                spaceshipHandler.UpdatePower(daysSurvived);
                foreach (Transform sheep in sheepHolder.GetComponentInChildren<Transform>())
                {
                    sheep.GetComponent<SheepHandler>().SetSheepPassive();
                }
            }
            timeInCycle = (daysSurvived+1)*timeTillCycleChange;
            daysSurvived++;
        }
    }

    void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 150, 25), "Time Till " + (isDay ? "Night: " : "Day: ") + ((int) timeInCycle)/60 + ":" + ((int)timeInCycle) % 60);
        GUI.Box(new Rect(Screen.width-180, 10, 150, 25), "Days Survived: " + daysSurvived);
    }

}
