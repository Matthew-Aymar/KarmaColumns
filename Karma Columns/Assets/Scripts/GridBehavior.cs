using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBehavior : MonoBehaviour
{
    Vector3[] positions;
    int[] height;
    // Start is called before the first frame update
    void Start()
    {
        positions = new Vector3[9];
        height = new int[9];
        Vector3 start = new Vector3(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y, gameObject.transform.position.z + 0.5f);
        for(int x = 0; x < positions.Length; x++)
        { 
            if(x < 3)
            {
                positions[x] = new Vector3(start.x + x * 1.0f, start.y, start.z);
            }
            else if(x < 6)
            {
                positions[x] = new Vector3(start.x + (x-3) * 1.0f, start.y, start.z - 1);
            }
            else if (x < 9)
            {
                positions[x] = new Vector3(start.x + (x-6) * 1.0f, start.y, start.z - 2);
            }
            height[x] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 snapPos = positions[0];
        int index = 0;
        if (other.gameObject.tag.Equals("Box"))
        {
            Vector3 boxPos = other.gameObject.transform.position;
            float dist = Vector3.Distance(boxPos, positions[0]);
            for(int x = 0; x < positions.Length; x++)
            {
                if(Vector3.Distance(boxPos, positions[x]) < dist)
                {
                    dist = Vector3.Distance(boxPos, positions[x]);
                    snapPos = positions[x];
                    index = x;
                }
            }

            if(((snapPos.y + (float)height[index]) - other.gameObject.transform.position.y) <= 0.2)
            {
                other.gameObject.transform.position = new Vector3(snapPos.x, snapPos.y + (float)height[index], snapPos.z);
                other.gameObject.transform.rotation = Quaternion.identity;
                height[index]++;
            }
        }
    }
}
