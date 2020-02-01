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

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inMotion)
        {
            rb.AddForce(pushDir);
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

    void setOwner(GameObject g)
    {
        owner = g;
    }

    void setHolder(GameObject g)
    {
        holder = g;
    }

    void setMotion(Vector3 d)
    {
        pushDir = d;
        inMotion = true;
    }

    void toggleGrav()
    {
        rb.useGravity = !rb.useGravity;
    }
}