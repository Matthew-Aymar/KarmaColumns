using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherBehavior : MonoBehaviour
{
    GameObject[] boxes;
    Vector3 dir;
    float timer;
    float nexttime;
    /**
     * Wind Duration: 5 - 15 seconds
     * Rain Duration: 5 - 10 seconds
     * Hail Duration: 5 - 7  seconds
     * Lightning Duration: 3 - 10 strikes (10 seconds?)
     */
    float duration; 

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        nexttime = Time.time + 5.0f;
        dir = Vector3.forward;
        duration = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(duration == 0)
        {
            timer += (Time.time - timer);
            if (timer >= nexttime)
            {
                Rain();
                Wind();
            }
        }
        else if(duration <= Time.time)
        {
            stopRain();
            stopWind();
            duration = 0;
            nexttime = Time.time + 20.0f;
            timer = 0f;
        }
    }

    void Wind()
    {
        duration = Time.time + 5.0f;
        boxes = GameObject.FindGameObjectsWithTag("Box");
        for(int x = 0; x < boxes.Length; x++)
        {
            boxes[x].GetComponent<BoxBehavior>().setMotion(dir);
        }
    }

    void stopWind()
    {
        boxes = GameObject.FindGameObjectsWithTag("Box");
        for (int x = 0; x < boxes.Length; x++)
        {
            boxes[x].GetComponent<BoxBehavior>().stopMotion();
        }
    }

    void Rain()
    {
        duration = Time.time + 5.0f;
        boxes = GameObject.FindGameObjectsWithTag("Box");
        for (int x = 0; x < boxes.Length; x++)
        {
            boxes[x].GetComponent<BoxBehavior>().rainDamage();
        }
    }

    void stopRain()
    {
        boxes = GameObject.FindGameObjectsWithTag("Box");
        for (int x = 0; x < boxes.Length; x++)
        {
            boxes[x].GetComponent<BoxBehavior>().stopRainDamage();
        }
    }

    void Lightning()
    {

    }

    void stopLightning()
    {

    }

    void Hail()
    {
        duration = Time.time + 5.0f;
        boxes = GameObject.FindGameObjectsWithTag("Box");
        for (int x = 0; x < boxes.Length; x++)
        {
            boxes[x].GetComponent<BoxBehavior>().HailDamage();
        }
    }

    void stopHail()
    {
        boxes = GameObject.FindGameObjectsWithTag("Box");
        for (int x = 0; x < boxes.Length; x++)
        {
            boxes[x].GetComponent<BoxBehavior>().stopHailDamage();
        }
    }
}
