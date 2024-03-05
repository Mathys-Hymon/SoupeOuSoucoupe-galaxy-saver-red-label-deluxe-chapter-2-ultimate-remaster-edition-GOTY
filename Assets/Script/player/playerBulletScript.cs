using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBulletScript : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    private void Update()
    {
        transform.position += transform.right * bulletSpeed * Time.deltaTime;

        if(transform.position.x > 14f)
        {
            Destroy(gameObject);
        }
    }
}
