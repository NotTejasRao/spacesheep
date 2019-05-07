using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthHandler : MonoBehaviour {

    public int health = 10;
    public int hunger = 10;
    private Vector3 lastLocation;
    private float distanceTravelled = 0;
    private Collider collider;
    private bool inHurtTouch = false;
    public GameOverHandler gameOverHandler;
    

    void Start()
    {
        lastLocation = transform.position;
        collider = GetComponent<Collider>();
        InvokeRepeating("InitiateRegenerateHealthLoop", 0, 4.0f);
        InvokeRepeating("InitiateDepleteHealthLoop", 4.0f, 4.0f);
        InvokeRepeating("InitiateTouchDepleteHealthLoop", 0.0f, 0.5f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.StartsWith("Mutton"))
        {
            if (hunger != 10)
            {
                hunger++;
            }
            Destroy(collision.gameObject);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name.StartsWith("Sheep"))
        {
            if (!collision.gameObject.GetComponent<SheepHandler>().IsPassive())
            {
                inHurtTouch = true;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name.StartsWith("Sheep"))
        {
            if (!collision.gameObject.GetComponent<SheepHandler>().IsPassive())
            {
                inHurtTouch = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        distanceTravelled += Vector3.Distance(transform.position, lastLocation);
        lastLocation = transform.position;

        if (distanceTravelled > 150)
        {
            hunger--;
            distanceTravelled = 0;
        }

        if (health == 0)
        {
            gameOverHandler.GameOver();
        }
    }

    void InitiateRegenerateHealthLoop()
    {
        // 4 seconds
        if (hunger == 10)
        {
            if (health < 10)
            {
                health++;
            }
        }
    }

    void InitiateDepleteHealthLoop()
    {
        // 4 seconds
        if (hunger <= 5)

        {
            ReduceHealth();
        }
        
    }

    void InitiateTouchDepleteHealthLoop()
    {
        // 0.5 seconds
        if (inHurtTouch)
        {
            ReduceHealth();
        }        
    }

    void ReduceHealth()
    {
        if (health > 0)
        {
            health--;
        }
    }

    void OnGUI()
    {
        GUI.Box(new Rect(10, 90, 150, 25), "Food Level: " + hunger);
        GUI.Box(new Rect(10, 110, 150, 25), "Health: " + health);
    }

}

