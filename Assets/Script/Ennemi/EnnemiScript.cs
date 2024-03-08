using UnityEngine;


public class EnnemiScript : MonoBehaviour
{
    [SerializeField] private GameObject bulletRef;
    [SerializeField] private float shootRate, speed, circleRad;
    [SerializeField] private int bulletPerShoot;
    [SerializeField] private int lifePoints;
    [SerializeField] private bulletTypeEnum bulletType;
    [SerializeField] private movementEnum movementType;
    [SerializeField] private bool canMove = false;

    private float angle = 0f;
    private bool goUp;
    private Vector2 startPostion;
    private Rigidbody rb;
    private int formationPos;

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
        if(canMove)
        {
            rb = GetComponent<Rigidbody>();
        }
        InvokeRepeating("shoot", 0f, 5/shootRate);
    }


    public void InitializeEnnemie(int _bulletPerShoot, int _lifePoints, float _speed, float _shootRate, int formationPosition)
    {
        bulletPerShoot = _bulletPerShoot;
        lifePoints = _lifePoints;
        speed = _speed;
        shootRate = _shootRate;
        formationPos = formationPosition;
        angle -= formationPos/1.5f;
    }

    private void FixedUpdate()
    {
        Quaternion rotation = Quaternion.identity;

        if (canMove)
        {
            transform.GetChild(0).localRotation = Quaternion.Slerp(transform.GetChild(0).localRotation, Quaternion.Euler(Mathf.Clamp(-rb.velocity.x * 12, -12, 12), -90, Mathf.Clamp(-rb.velocity.y * 10, -20, 20)), 20 * Time.deltaTime);
            if (transform.position.y >= 6.15f)
            {
                goUp = false;
            }
            else if (transform.position.y <= -6.15f)
            {
                goUp = true;
            }



            switch (movementType)
            {
                case movementEnum.line:
                    if (goUp)
                    {
                        rb.velocity = new Vector3(-speed / 2, speed, 0);
                    }
                    else
                    {
                        rb.velocity = new Vector3(-speed / 2, -speed, 0);
                    }
                    break;

                case movementEnum.round:

                    angle += speed * Time.deltaTime;
                    float x = (startPostion.x -= (speed / 2) * Time.deltaTime) + circleRad * Mathf.Cos(angle);
                    float y = startPostion.y + circleRad * Mathf.Sin(angle);

                    rb.MovePosition(new Vector3(x, y, 0));
                    break;
            }

        }

        if(transform.position.x  < -20)
        {
            Destroy(gameObject);
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
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<playerBulletScript>() != null)
        {
            lifePoints--;
            if (lifePoints <= 0)
            {
                Destroy(collision.gameObject);
                Die();
            }
        }
        else if (collision.gameObject.GetComponent<playerScript>() != null)
        {
            collision.gameObject.GetComponent<playerScript>().Damaged();
        }
    }


    private void Die()
    {
        Destroy(gameObject);
    }
}
