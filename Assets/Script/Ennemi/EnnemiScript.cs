using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.Windows;

public class EnnemiScript : MonoBehaviour
{
    [SerializeField] private GameObject bulletRef;
    [SerializeField] private float shootRate, speed, circleRad;
    [SerializeField] private int bulletPerShoot;
    [SerializeField] private int lifePoints;
    [SerializeField] private bulletTypeEnum bulletType;
    [SerializeField] private movementEnum movementType;

    private bool goUp;
    private Vector2 startPostion;
    float timeCounter = 0;
    private Rigidbody rb;

    private enum bulletTypeEnum
    {
        wavy,
        straight,
    }
    private enum movementEnum
    {
        line,
        round,
    }

    private void Start()
    {
        startPostion = transform.position;
        rb = GetComponent<Rigidbody>();
        InvokeRepeating("shoot", 0f, 5/shootRate);
    }

    private void Update()
    {
        Quaternion rotation = Quaternion.identity;

        transform.GetChild(0).localRotation = Quaternion.Slerp(transform.GetChild(0).localRotation, Quaternion.Euler(Mathf.Clamp(-rb.velocity.x * 12, -12, 12), -90, Mathf.Clamp(-rb.velocity.y * 10, -20, 20)), 20 * Time.deltaTime);
        if(transform.position.y >= 6.15f)
        {
            goUp = false;
        }
        else if (transform.position.y <= -6.15f)
        {
            goUp = true;
        }

        switch (movementType)
        {
            case movementEnum.line :
                if(goUp)
                {
                    rb.velocity = new Vector3(-speed/2, speed, 0);
                }
                else
                {
                    rb.velocity = new Vector3(-speed/2, -speed, 0);
                }
                print(rb.velocity);
                break;

            case movementEnum.round :

                timeCounter += Time.deltaTime;
                Vector2 offset = new Vector2(Mathf.Sin(timeCounter), Mathf.Cos(timeCounter)) * circleRad;
                rb.velocity = offset + (Vector2.right * -speed/4);
                break;
        }
    }

    private void shoot()
    {
        for(int i = 0; i < bulletPerShoot; i++)
        {
            var bullet = Instantiate(bulletRef, transform.position + -Vector3.right, Quaternion.identity);
            var bulletScript = bullet.GetComponent<EnnemiBulletScript>();

            Vector3 direction = Vector3.zero;

            if(i == 0)
            {
                direction = -Vector3.right;
            }
            else
            {
                if (i % 2 == 0)
                {
                    direction = -Vector3.right + new Vector3(0, -i* 0.3f, 0);
                }
                else
                {
                    direction = -Vector3.right + new Vector3(0, i * 0.3f, 0);
                }

            }

            bulletScript.InitializeBullet(((int)bulletType), direction);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("hello");
        if(collision.gameObject.GetComponent<playerBulletScript>() != null)
        {
            lifePoints--;
            if(lifePoints <= 0)
            {
                Destroy(collision.gameObject);
                Die();
            }
        }
    }



    private void Die()
    {
        Destroy(gameObject);
    }
}
