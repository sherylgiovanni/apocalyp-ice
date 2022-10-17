using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
     // Sheryl Tania
     
    private float speed = 35.0f;
    private float maximumZ;

    // Start is called before the first frame update
    void Start()
    {
        // The maximum distance player can shoot is up to 100 miles
        maximumZ = transform.position.z + 100;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
        if (transform.position.z > maximumZ)
        {
            Destroy(gameObject);
        }
    }
}
