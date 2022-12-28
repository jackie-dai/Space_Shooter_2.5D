using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    /* Editable variables */
    [SerializeField]
    private float _speed = 4f;
    [SerializeField]
    private int powerID;
    /* PREFABS */
    private GameObject[] powerups;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        
        if (transform.position.y < -6f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player p = other.transform.GetComponent<Player>();
            if (p != null)
            {
                switch (powerID)
                {
                    case 0:
                        p.EnableTripleShot();
                        break;
                    case 1:
                        p.EnableSpeedBoost();
                        break;
                    default:
                        p.EnableShield();
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
