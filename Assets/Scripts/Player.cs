using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private string horizontal = "Horizontal", vertical = "Vertical", fire1 = "Fire1";

    // GAME DESIGN
    [SerializeField] float moveSpeed = 7f;
    [SerializeField] float smoothMoveTime = .1f;
    [SerializeField] float turnSpeed = 8;
    [SerializeField] float shootingInterval = 0.3f;

    private float angle;
    private float smoothInputMagnitude;
    private float smoothMoveVelocity;
    private Vector3 velocity;
    private Rigidbody myRigidbody;
    private Vector3 inputDirection;

    public static Player instance;

    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject projectileInitialPosition;
    [SerializeField] private GameObject projectileParent;
    [SerializeField] private GameObject winUI;
    private bool shooting = true;
    private float timeSineceLastShooting;

    public static bool gameOn = false;
    private void Awake()
    {
        Time.timeScale = 1;
        if (instance == null)
        {
            instance = this;
        }

    }
    private void Start()
    {        
        myRigidbody = GetComponent<Rigidbody>();
        Debug.Log(Time.timeScale);
        
    }
    private void Update()
    {
        //if (gameOn)
        {
            GetMovementInput();
            Shoot();
            if (transform.position.y < -3 && transform.position.y > -5)
                Die();
        }
        
    }

    private void Die()
    {
        Score.instance.ShowScore();
        winUI.SetActive(true);
        Time.timeScale = 0;
        gameOn = false;
    }

    //Rigidbody should be updated in FixedUpdate method 
    private void FixedUpdate()
    {
        Move();
    }

    private void GetMovementInput()
    {
        inputDirection = Vector3.zero;
        inputDirection = new Vector3(Input.GetAxisRaw(horizontal), 0, Input.GetAxisRaw(vertical)).normalized;
        float inputMagnitude = inputDirection.magnitude;
        smoothInputMagnitude = Mathf.SmoothDamp(smoothInputMagnitude, inputMagnitude, ref smoothMoveVelocity, smoothMoveTime);

        float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
        angle = Mathf.LerpAngle(angle, targetAngle, Time.deltaTime * turnSpeed * inputMagnitude);

        velocity = transform.forward * moveSpeed * smoothInputMagnitude;
        //transform.eulerAngles = Vector3.up * angle;        
        //transform.Translate(transform.forward * smoothInputMagnitude * moveSpeed * Time.deltaTime, Space.World);
    }

    private void Move()
    {
        myRigidbody.MoveRotation(Quaternion.Euler(Vector3.up * angle));
        myRigidbody.MovePosition(myRigidbody.position + velocity * Time.fixedDeltaTime);
    }
    
    private void Shoot()
    {
        if (shooting)
        {
            if (Input.GetButton(fire1))
            {
                GameObject projectileUnit = Instantiate(projectile, projectileInitialPosition.transform.position,
                    Quaternion.identity, projectileParent.transform);
                shooting = false;

            }
        }
        else
        {
            timeSineceLastShooting += Time.deltaTime;
            if (timeSineceLastShooting > shootingInterval)
            {
                shooting = true;
                timeSineceLastShooting = 0;
            }
        }
        
    }
}
