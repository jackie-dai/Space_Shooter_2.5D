using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpawnManager _spawnManager;
    private Vector3 startPos = new Vector3(0, 0, 0);
    private bool tripleShotEnabled = false;
    private bool shieldEnabled = false;
    /* PRFEFABS */
    [SerializeField]
    private GameObject _laser;
    [SerializeField]
    private GameObject _triple_shot;
    [SerializeField]
    private GameObject _shieldPrefab;
    /* Editable variables */
    [SerializeField]
    private float _speed = 2.0f;
    [SerializeField]
    private int _lives = 3;
    private float _cooldownTimer = -1;
    private float fireRate = 0.25f;
    private float tripleShotDuration = 5f;
    private int speedMultiplier = 1;
    private float speedBoostDuration = 5f; 


    // Start is called before the first frame update
    void Start()
    {
        transform.position = startPos;
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("SpawnManager does not exist");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _cooldownTimer)
        {
            if (tripleShotEnabled)
            {
                FireTripleShot();
            } else
            {
                FireLaser();
            }
        }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * (_speed * speedMultiplier) * Time.deltaTime);

        if (transform.position.x < -12)
        {
            transform.position = new Vector3(12, transform.position.y, 0);
        }
        else if (transform.position.x > 12)
        {
            transform.position = new Vector3(-12, transform.position.y, 0);
        }

        // Boundaries for y position 
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.6f, 5.7f), 0);
    }

    void FireLaser()
    {
        _cooldownTimer = Time.time + fireRate;
        GameObject newLaser = Instantiate(_laser, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
    }

    void FireTripleShot()
    {
        Instantiate(_triple_shot, transform.position, Quaternion.identity);
    }

    public void Damage()
    {
        if (shieldEnabled) {
            shieldEnabled = false;
            _shieldPrefab.SetActive(false);
        } 
        else
        {
            _lives--;
            if (_lives < 1)
            {
                _spawnManager.OnPlayerDeath();
                Destroy(this.gameObject);
            }
        }

        Debug.Log("HIT!");
    }

    public void EnableTripleShot()
    {
        tripleShotEnabled = true;
        StartCoroutine(TripleShotCooldown());
     
    }
    IEnumerator TripleShotCooldown()
    {
        yield return new WaitForSeconds(tripleShotDuration);
        tripleShotEnabled = false;
    }

    public void EnableSpeedBoost()
    {
        speedMultiplier = 2;
        StartCoroutine(SpeedBoostCooldown());
    }
    
    IEnumerator SpeedBoostCooldown()
    {
        yield return new WaitForSeconds(speedBoostDuration);
        speedMultiplier = 1;
    }

    public void EnableShield()
    {
        if (!shieldEnabled)
        {
            shieldEnabled = true;
            _shieldPrefab.SetActive(true);
        }
    }
}