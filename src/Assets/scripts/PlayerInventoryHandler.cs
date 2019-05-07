using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryHandler : MonoBehaviour {

    public int uraniumCount = 0;
    private Transform cameraTransform;
    public WorldGenerator worldGenerator;

    private void Start()
    {
        cameraTransform = Camera.main.transform;

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.StartsWith("Uranium"))
        {
            Destroy(collision.gameObject);
            uraniumCount++;
            worldGenerator.numberOfUraniums--;            
        }
    }

    private void Update()
    {
 


    }


    void OnGUI()
    {
       GUI.Box(new Rect(10, 50, 150, 25), "Uranium Ores: " + uraniumCount);
    }

}
