using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepHandler : MonoBehaviour
{

    private GameObject player;
    private float gravity;

    public GameObject muttonPrefab;

    public int health = 2;

    private bool moving = true;

    public int passiveMoveSpeed = 2;
    public int hostileMoveSpeed = 4;
    public int followDistance = 10;

    private bool isPassive = true;
    public Texture passiveTexture;
    public Texture hostileTexture;
    public Texture hostileHurtTexture;
    private Renderer renderer;

    public WorldGenerator worldGenerator;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.StartsWith("Player"))
        {
            if (isPassive)
            {
                SetSheepHostile();
                ReduceHealth();                
            }
        }
    }

    private void Awake()

    {
        worldGenerator = GameObject.Find("GameLord").GetComponent<WorldGenerator>();
        renderer = GetComponent<Renderer>();
    }

    public void ReduceHealth()
    {
        if (isPassive)
        {
            renderer.material.mainTexture = hostileHurtTexture;
            SetSheepHostile();
        }

        Debug.Log("HI11");
        health--;
        renderer.material.mainTexture = hostileHurtTexture;
        StartCoroutine(HurtAnimation());
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, 100);
        foreach (Collider col in colliders)
        {
            if (col.transform.name.StartsWith("Sheep"))
            {
                col.gameObject.GetComponent<SheepHandler>().SetSheepHostile();
            }
        }
    }

    IEnumerator HurtAnimation()
    {
        yield return new WaitForSeconds(0.1f);
        renderer.material.mainTexture = hostileTexture;
    }


    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    public void SetSheepHostile()
    {
        isPassive = false;
        renderer.material.mainTexture = hostileHurtTexture;
        HurtAnimation();
    }

    public void SetSheepPassive()
    {
        isPassive = true;
        renderer.material.mainTexture = passiveTexture;
    }

    public bool IsPassive()
    {
        return isPassive;
    }

    public void StopMoving()
    {
        moving = false;
    }

    void Update()
    {
        if (moving)
        {
            if (health <= 0)
            {
                for (int i = 0; i < Random.Range(2, 5); i++)
                {
                    GameObject.Instantiate(muttonPrefab, transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f)), transform.rotation);
                }
                worldGenerator.numberOfSheeps--;
                Destroy(this.gameObject);
            }

            float step;
            Vector3 locationFollow;

            if (isPassive)
            {
                step = passiveMoveSpeed * Time.deltaTime;
                locationFollow = transform.position;
            }
            else
            {
                step = hostileMoveSpeed * Time.deltaTime;
                locationFollow = Vector3.Distance(player.transform.position, transform.position) < 50 ? player.transform.position : transform.position;
            }
            transform.position = Vector3.MoveTowards(transform.position, locationFollow, step);
        }
    }

}
