using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   //config Params
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float xPadding = .3f;
    [SerializeField] float yPadding = .25f;
    [SerializeField] GameObject LaserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 1f;

    bool isFiring;
    //cache
    float xMin;
    float xMax;
    float yMin;
    float yMax;
    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }


    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1") && !isFiring)
        {
            StartCoroutine(FireContinuously());
        }      
    }

    //So When the button for fire is down and isFiring is not true,
    //it will start the coroutine. The below method sets it to true and while the fire button
   private IEnumerator FireContinuously()
    {
        isFiring = true;
        while (Input.GetButton("Fire1"))
        {
            GameObject laser = Instantiate(
            LaserPrefab,
            transform.position,
            Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity =
                new Vector2(0, projectileSpeed);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
        isFiring = false;
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
        
    }
    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + xPadding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - xPadding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + yPadding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - yPadding;

    }

    
}

