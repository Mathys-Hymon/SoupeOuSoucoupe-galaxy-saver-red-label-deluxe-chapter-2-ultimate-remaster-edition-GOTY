using UnityEngine;


public class EnnemiScript : MonoBehaviour
{
    [SerializeField] private GameObject bulletRef;
    [SerializeField] private GameObject[] powerUpRef;
    [SerializeField] private float shootRate, speed, circleRad;
    [SerializeField] private int bulletPerShoot;
    [SerializeField] private int lifePoints;
    [SerializeField] private bulletTypeEnum bulletType;
    [SerializeField] private movementEnum movementType;

    [SerializeField] private GameObject impactPrefab;
    [SerializeField] private GameObject explosionPrefab;

    [SerializeField] private AudioSource impactSound;
    [SerializeField] private AudioSource explosionSound;
    [SerializeField] private AudioSource shootSound;

    private float angle = 0f;
    private bool goUp, canDie;
    private Vector2 startPostion;
    private Rigidbody rb;
    private float formationPos;

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

    public void InitializeEnnemie(int _bulletPerShoot, int _lifePoints, float _speed, float _shootRate, int formationPosition)
    {
        bulletPerShoot = _bulletPerShoot;
        lifePoints = _lifePoints;
        speed = _speed;
        shootRate = _shootRate;
        formationPos = formationPosition;
        angle -= formationPos/1.5f;
        Invoke(nameof(InvokeShoot), formationPos/5);
        Invoke(nameof(SetDie), 0.6f);
    }
    private void SetDie()
    {
        canDie = true;
    }
    private void InvokeShoot()
    {
        InvokeRepeating("shoot", 0, 5 / shootRate);
    }

    private void FixedUpdate()
    {
        Quaternion rotation = Quaternion.identity;

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
                    rb.MovePosition(transform.position + new Vector3(-speed * 1.5f, speed, 0)*Time.deltaTime);
                    }
                    else
                    {
                    rb.MovePosition(transform.position + new Vector3(-speed * 1.5f, -speed, 0)*Time.deltaTime);
                }
                    break;

                case movementEnum.round:

                    angle += speed * Time.deltaTime;
                    float x = (startPostion.x -= (speed / 2) * Time.deltaTime) + circleRad * Mathf.Cos(angle);
                    float y = startPostion.y + circleRad * Mathf.Sin(angle);

                    rb.MovePosition(new Vector3(x, y, 0));
                    break;
            }

        if(transform.position.x  < -20)
        {
            Destroy(gameObject);
        }
    }

    private void shoot()
    {
        if (transform.position.x < 14f)
        {
            for (int i = 0; i < bulletPerShoot; i++)
            {
                shootSound.pitch = (Random.Range(0.8f, 1.2f));
                shootSound.Play();

                var bullet = Instantiate(bulletRef, transform.position + -Vector3.right, Quaternion.identity);
                var bulletScript = bullet.GetComponent<EnnemiBulletScript>();

                Vector3 direction = Vector3.zero;

                if (i == 0)
                {
                    direction = -Vector3.right;
                }
                else
                {
                    if (i % 2 == 0)
                    {
                        direction = -Vector3.right + new Vector3(0, -i * 0.3f, 0);
                    }
                    else
                    {
                        direction = -Vector3.right + new Vector3(0, i * 0.3f, 0);
                    }

                }

                bulletScript.InitializeBullet(((int)bulletType), direction);

            }
        }
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<playerBulletScript>() != null)
        {
            lifePoints--;

            impactSound.pitch = (Random.Range(0.8f, 1.2f));
            impactSound.Play();
            Instantiate(impactPrefab, collision.transform.position, collision.transform.rotation);
            Destroy(collision.gameObject);

            if (lifePoints <= 0)
            {
                explosionSound.pitch = (Random.Range(0.8f, 1.2f));
                explosionSound.Play();
                Instantiate(explosionPrefab, transform.position, transform.rotation);
                
                Invoke("Die", 0.2f);
            }
        }
        else if (collision.gameObject.GetComponent<playerScript>() != null)
        {
            collision.gameObject.GetComponent<playerScript>().Damaged();
        }
    }


    private void Die()
    {
        if(canDie)
        {
            int spawnCollectible = Random.Range(0, 100);

            if (spawnCollectible < 6)
            {
                var PowerUp = Instantiate(powerUpRef[0], transform);
                PowerUp.transform.parent = null;
            }
            playerScript.instance.AddScore(10);
            Destroy(gameObject);
        }

    }

    public void DieLaser()
    {
        if(canDie)
        {
            playerScript.instance.AddScore(10);
            explosionSound.pitch = (Random.Range(0.8f, 1.2f));
            explosionSound.Play();
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

}
