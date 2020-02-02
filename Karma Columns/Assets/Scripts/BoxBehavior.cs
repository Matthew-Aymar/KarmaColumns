using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBehavior : MonoBehaviour
{
    public string type;         //What type of resource the block is made of
    public float durability;    //How much effective durability it has/has left
    public float weight;        //Multiplier on the objects mass

    public GameObject owner;    //Which player created the object
    public GameObject holder;   //Which player is using the block

    private Rigidbody rb;       //RigidBody of the cube
    private Vector3 pushDir;    //Direction wind is pushing the box
    private bool inMotion;      //Whether or not it should be pushed

    private bool rainDmg;
    private bool hailDmg;

    private float timer;
    private float starttime;
    private float scale;

    public GameObject lightning;
    private bool lightningCheck;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        timer = 1.0f;
        durability = 100;
        scale = 1.0f;
        lightning = (GameObject)Resources.Load("Prefabs/Lightning");
    }

    // Update is called once per frame
    void Update()
    {
        if (inMotion)
        {
            rb.AddForce(pushDir * 3.5f);
        }

        if (rainDmg)
        {
            if(Time.time >= starttime + timer)
            {
                durability -= 2;
                scale = durability / 100;
                if(scale <= 0)
                {
                    Destroy(gameObject);
                }
                gameObject.transform.localScale = new Vector3(scale, gameObject.transform.localScale.y, scale);
                timer += timer;
            }
        }

        if (hailDmg)
        {
            if (Time.time >= starttime + timer)
            {
                durability -= durability*0.2f;
                scale = durability / 100;
                if (scale <= 0)
                {
                    Destroy(gameObject);
                }
                gameObject.transform.localScale = new Vector3(scale, gameObject.transform.localScale.y, scale);
                timer += timer;
            }
        }

        if (lightningCheck)
        {
            if (Time.time >= starttime + timer)
            {
                float r = Random.value * 100;
                if (r > 90)
                {
                    lightning = Instantiate(lightning, gameObject.transform);
                    lightning.GetComponent<LightningBehavior>().box = gameObject;
                }
                timer += timer;
            }
        }
    }

    void setType(string t)
    {
        type = t;
        if(type.Equals("Straw"))
        {
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            durability = 50;
            weight = 0.75f;
            rb.mass *= weight;
        }
        else if (type.Equals("Wood"))
        {
            gameObject.GetComponent<Renderer>().material.color = Color.green;
            durability = 100;
            weight = 1.0f;
            rb.mass *= weight;
        }
        else if (type.Equals("Stone"))
        {
            gameObject.GetComponent<Renderer>().material.color = Color.gray;
            durability = 150;
            weight = 1.25f;
            rb.mass *= weight;
        }
        else if (type.Equals("Iron"))
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
            durability = 200;
            weight = 1.5f;
            rb.mass *= weight;
        }
    }

    public void setOwner(GameObject g)
    {
        owner = g;
    }

    public void setHolder(GameObject g)
    {
        holder = g;
    }

    public void setMotion(Vector3 d)
    {
        pushDir = d;
        inMotion = true;
    }

    public void stopMotion()
    {
        pushDir = Vector3.zero;
        inMotion = false;
    }

    public void rainDamage()
    {
        starttime = Time.time;
        rainDmg = true;
        timer = 0.5f;
    }

    public void stopRainDamage()
    {
        rainDmg = false;
        timer = 0;
        starttime = 0;
    }

    public void HailDamage()
    {
        starttime = Time.time;
        hailDmg = true;
        timer = 1.0f;
    }

    public void stopHailDamage()
    {
        hailDmg = false;
        timer = 0;
        starttime = 0;
    }

    public void lightningStrikes()
    {
        lightningCheck = true;
        starttime = Time.time;
        timer = 1.0f;
    }

    public void stopStrikes()
    {
        lightningCheck = false;
        starttime = 0;
        timer = 0;
    }
}