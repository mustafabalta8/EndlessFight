using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 25f;
    [SerializeField] private float destructionDuration = 5f;

    private void Start()
    {
        transform.rotation = Player.instance.transform.rotation;
        Destroy(gameObject, destructionDuration);
    }
    void Update()
    {
        transform.localPosition += transform.forward * Time.deltaTime * speed;

    }

}
