using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 startPos = new Vector3(0, 0, 0);
    [SerializeField]
    private float _speed = 2.0f;
    [SerializeField]
    private GameObject _laser;
    [SerializeField]
    private int _lives = 3;
    private float _cooldownTimer = -1;
    private float fireRate = 0.25f;
   

    // Start is called before the first frame update
    void Start()
    {
        transform.position = startPos;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _cooldownTimer)
        {
            FireLaser();
        }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);

        if (transform.position.x < -12)
        {
            transform.position = new Vector3(12, transform.position.y, 0);
        }
        else if (transform.position.x > 12)
        {
            transform.position = new Vector3(-12, transform.position.y, 0);
        }

        // Boundaries for y position 
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4, 6), 0);
    }

    void FireLaser()
    {
        _cooldownTimer = Time.time + fireRate;
        Instantiate(_laser, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
    }

    public void Damage()
    {
        _lives--;
        Debug.Log("HIT!");
        if (_lives < 1)
        {
            Destroy(this.gameObject);
        }
    }
}