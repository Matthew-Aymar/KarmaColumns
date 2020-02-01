using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherBehavior : MonoBehaviour
{
    GameObject[] boxes;
    Vector3 dir;
    float timer;
    float nexttime;
    float duration;

    bool windActive;
    bool rainActive;
    bool hailActive;
    bool lightningActive;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        nexttime = Time.time + 5.0f;
        dir = Vector3.forward;
        duration = 0;
        windActive = false;
        rainActive = false;
        hailActive = false;
        lightningActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(duration == 0)
        {
            timer += (Time.time - timer);
            if (timer >= nexttime)
            {
                duration = Time.time + 6.0f;
                RandomWeather();
            }
        }
        else if(duration <= Time.time)
        {
            StopWeather();
            duration = 0;
            nexttime = Time.time + 10.0f;
            timer = 0f;
        }
    }

    void Wind()
    {
        Debug.Log("Wind Start!");
        windActive = true;
        boxes = GameObject.FindGameObjectsWithTag("Box");
        for(int x = 0; x < boxes.Length; x++)
        {
            boxes[x].GetComponent<BoxBehavior>().setMotion(dir);
        }
    }

    void stopWind()
    {
        Debug.Log("Wind End!");
        windActive = false;
        boxes = GameObject.FindGameObjectsWithTag("Box");
        for (int x = 0; x < boxes.Length; x++)
        {
            boxes[x].GetComponent<BoxBehavior>().stopMotion();
        }
    }

    void Rain()
    {
        Debug.Log("Rain Start!");
        rainActive = true;
        boxes = GameObject.FindGameObjectsWithTag("Box");
        for (int x = 0; x < boxes.Length; x++)
        {
            boxes[x].GetComponent<BoxBehavior>().rainDamage();
        }
    }

    void stopRain()
    {
        Debug.Log("Rain End!");
        rainActive = false;
        boxes = GameObject.FindGameObjectsWithTag("Box");
        for (int x = 0; x < boxes.Length; x++)
        {
            boxes[x].GetComponent<BoxBehavior>().stopRainDamage();
        }
    }

    void Lightning()
    {
        Debug.Log("Lightning Start!");
        lightningActive = true;
        boxes = GameObject.FindGameObjectsWithTag("Box");
        for (int x = 0; x < boxes.Length; x++)
        {
            boxes[x].GetComponent<BoxBehavior>().lightningStrikes();
        }
    }

    void stopLightning()
    {
        lightningActive = false;
        Debug.Log("Lightning End!");
        boxes = GameObject.FindGameObjectsWithTag("Box");
        for (int x = 0; x < boxes.Length; x++)
        {
            boxes[x].GetComponent<BoxBehavior>().stopStrikes();
        }
        duration = 0;
    }

    void Hail()
    {
        Debug.Log("Hail Start!");
        hailActive = true;
        boxes = GameObject.FindGameObjectsWithTag("Box");
        for (int x = 0; x < boxes.Length; x++)
        {
            boxes[x].GetComponent<BoxBehavior>().HailDamage();
        }
    }

    void stopHail()
    {
        Debug.Log("Hail End!");
        hailActive = false;
        boxes = GameObject.FindGameObjectsWithTag("Box");
        for (int x = 0; x < boxes.Length; x++)
        {
            boxes[x].GetComponent<BoxBehavior>().stopHailDamage();
        }
    }

    void RandomWeather()
    {
        /**
         * 0-20 Nothing this pass
         * 20-40 Wind
         * 40-60 Rain
         * 60-80 Lightning
         * 80-100 Hail
         */
        float r = Random.value * 100;
        if(r < 20)
        {
            Debug.Log("Nothing Happened!");
            duration = Time.time + 10.0f;
        }
        else if(r < 40)
        {
            Wind();
        }
        else if(r < 60)
        {
            Rain();
        }
        else if(r < 80)
        {
            Lightning();
        }
        else if(r < 100)
        {
            Hail();
        }
    }

    void StopWeather()
    {
        if(windActive)
        {
            stopWind();
        }
        if(rainActive)
        {
            stopRain();
        }
        if (lightningActive)
        {
            stopLightning();
        }
        if (hailActive)
        {
            stopHail();
        }
    }
}
