using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 25.0f;
    private Rigidbody rig;
    public GameObject holdingPosition;
    private Vector3 dir;
    public bool holding;
    Quaternion rotation;
    private GameObject o;
    public Animator anim;
    public GameObject cam;
    float hAxis;
    float vAxis;
    float angle;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");

        Debug.Log(Mathf.Abs(hAxis) + Mathf.Abs(vAxis));

        if(hAxis == 0 && vAxis == 0)
        {
            anim.SetInteger("State", 0);
        }
        else if((Mathf.Abs(hAxis) + Mathf.Abs(vAxis)) > 0.9f)
        {
            anim.SetInteger("State", 1);

            angle = Mathf.Acos(Vector3.Dot(Vector3.forward, new Vector3(hAxis, 0, vAxis)));
            if(hAxis < 0)
            {
                angle = -angle;
            }
            dir = cam.transform.position - transform.position;
            dir = Vector3.Normalize(dir);
            dir = new Vector3(dir.x, 0, dir.z);

            dir = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up) * dir;

            transform.position += -dir * Time.deltaTime * speed;

            transform.LookAt(transform.position - dir);
        }
        
        if(holding == true)
        {
            o.gameObject.transform.position = holdingPosition.transform.position;
            o.gameObject.transform.rotation = holdingPosition.transform.rotation;
            if (Input.GetKeyDown("joystick button 0"))
            {
                holding = false;
                o.GetComponent<Rigidbody>().useGravity = true;
                o.GetComponent<Rigidbody>().AddForce(o.transform.forward*10, ForceMode.Impulse);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if ((other.gameObject.tag.Equals("Resource") || other.gameObject.tag.Equals("Box")) && !holding)
        {
            o = other.gameObject;
            o.transform.position = holdingPosition.transform.position;
            o.GetComponent<Rigidbody>().useGravity = false;
            holding = true;
        }
    }
}
