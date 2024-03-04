using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class EnnemiBulletScript : MonoBehaviour
{
    public float bulletLife = 10f;  // Defines how long before the bullet is destroyed
    public float rotation = 0f;
    public float speed = 1f;

    public AnimationCurve test;


    private Vector2 spawnPoint;
    private float timer = 0f;


    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = new Vector2(transform.position.x, transform.position.y);
    }


    // Update is called once per frame
    void Update()
    {
        if (timer > bulletLife) Destroy(this.gameObject);
        timer += Time.deltaTime;
        //transform.position = Movement(timer);

        transform.localPosition += new Vector3(-Time.deltaTime, test.Evaluate(timer/5)*Time.deltaTime, 0);
    }


    private Vector2 Movement(float timer)
    {
        float x = timer * speed * -transform.right.x;
        float y = timer * speed * -transform.right.y;
        return new Vector2(x + spawnPoint.x, y + spawnPoint.y);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }

    }

}
