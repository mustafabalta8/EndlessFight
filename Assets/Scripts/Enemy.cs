using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //GAME DESIGN
    [SerializeField] float speed = 2f;
    [SerializeField] float pullbackSpeed = 4f;
    [SerializeField] float waitingTimeAfterShot = 1f;
    
    GameObject target;    
    Rigidbody rg;

    private bool updateOn = true;
    private Vector3 direction;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        rg = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        //transform.eulerAngles = Vector3.RotateTowards(transform.eulerAngles, target.transform.eulerAngles, maxRadiansDelta, maxMagniruteDelta * Time.deltaTime);
        Vector3 direction = target.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
    }
    private void FixedUpdate()
    {
        //if (Player.gameOn)
        {
            if (updateOn)
            {
                direction = (target.transform.position - transform.position).normalized;
                rg.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);
                if (transform.position.y < -3)
                {
                    Destroy(gameObject);
                    Score.instance.ChangeScore();
                }
            }
            else
            {
                rg.MovePosition(transform.position - direction * pullbackSpeed * Time.fixedDeltaTime);
            }
        }
        

        
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            StartCoroutine(updateOff());
        }
    }
    IEnumerator updateOff()
    {
        updateOn = false;
        yield return new WaitForSeconds(waitingTimeAfterShot);
        updateOn = true;
    }
}
