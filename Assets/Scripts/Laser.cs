using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float fireRate = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > 8f)
        {
            Destroy(this.gameObject);
        }
        transform.Translate(Vector3.up * fireRate * Time.deltaTime);
    }
}
