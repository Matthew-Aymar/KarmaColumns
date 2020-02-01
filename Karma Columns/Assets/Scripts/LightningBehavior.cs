using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBehavior : MonoBehaviour
{
    private float duration;
    public GameObject box;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.identity;
        duration = Time.time + 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if(duration < Time.time)
        {
            Destroy(gameObject);
            Destroy(box);
        }
    }
}
