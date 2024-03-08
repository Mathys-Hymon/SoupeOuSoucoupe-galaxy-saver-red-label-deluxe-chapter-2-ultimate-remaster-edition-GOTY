using CartoonFX;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab1;
    [SerializeField] private GameObject bulletPrefab2;
    [SerializeField] private Transform firePoint1;
    [SerializeField] private Transform firePoint2;
    [SerializeField] private Transform firePoint3;
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private GameObject impactPrefab;

    [SerializeField] private int lifePoints;


    private bool canShoot = true;
    private float verticalRange = 6f;  // Adjust the vertical range as needed
    private float bulletSpeed = 5f;    // Adjust the speed of the bullets
    private float timeSinceLastFire = 0f;
    private int lastPattern = -1;
    private GameObject lifeBar;


    private void Start()
    {
        RandomShootingPattern();

        lifeBar = GameObject.FindGameObjectWithTag("PlayerUI").transform.GetChild(0).gameObject;
        if(lifeBar != null )
        {
            lifeBar.SetActive(true );

        }
    }


    void RandomShootingPattern()
    {
        if(canShoot) {
        CancelInvoke(nameof(RandomShootingPattern));
        int patternSelector = 0;

        do
        {
            patternSelector = Random.Range(1, 4);  // Change the range based on the number of patterns
        } while (patternSelector == lastPattern);

        lastPattern = patternSelector;

        // Cancel previous invokes to ensure no overlapping patterns
        CancelInvoke();

        switch (patternSelector)
        {
            case 1:
                ShootMult();
                break;
            case 2:
                ShootMult();
                ShootSinus();
                break;
            case 3:
                ShootSinus2();
                break;
            case 4:
                ShootUpDown();
                break;
        }
        }
    }

    void ShootMult()
    {
        InvokeRepeating("FireBulletMult", 0f, 1f / fireRate);

    }

    void ShootStraight()
    {
        Debug.Log("Straight");

        
        InvokeRepeating("FireBulletStraight", 0f, 1f / fireRate);

        
        Invoke("StopShootingStraight", 5f);
    }


    void FireBulletStraight()
    {
        Instantiate(bulletPrefab1, firePoint1.position, Quaternion.identity);
    }

    void StopShootingStraight()
    {
        
        CancelInvoke("FireBullet");

        
        RandomShootingPattern();
    }


    void FireBulletMult()
    {
        float[] angles = { -20f, -10f, 0f, 10f, 20f };
        foreach (float angle in angles)
        {
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            Instantiate(bulletPrefab1, firePoint1.position, rotation);
            Instantiate(bulletPrefab2, firePoint2.position, rotation);
            Instantiate(bulletPrefab2, firePoint3.position, rotation);
        }
        Invoke("StopShootingMult", 5f);
    }
    void StopShootingMult()
    {

        CancelInvoke("FireBulletMult");


        RandomShootingPattern();
    }

    void ShootSinus()
    {
        Debug.Log("Sinus");
        InvokeRepeating("FireBulletSinus", 0f, 1f / fireRate);       
        Invoke("StopShootingSinus", 5f);
    }



    void FireBulletSinus()
    {

        // smaller angle differences
        int numBullets = 15;  // Number of bullets in the wave
        float angleDifference = 4f;  // Angle difference between consecutive bullets

        for (int i = 1; i < numBullets; i++)
        {
            float angle = firePoint1.rotation.eulerAngles.z + i * angleDifference;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            Instantiate(bulletPrefab1, firePoint1.position, rotation);
        }

        for (int i = 1; i < numBullets; i++)
        {
            float angle = firePoint3.rotation.eulerAngles.z - i * angleDifference;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            Instantiate(bulletPrefab1, firePoint2.position, rotation);
        }


    }

    void StopShootingSinus()
    {
        
        CancelInvoke("FireBulletSinus");
        RandomShootingPattern();
    }



    void ShootSinus2()
    {
        Debug.Log("Sinus2");

        
        InvokeRepeating("FireBulletSinus2", 0f, 1f / fireRate);

        
        Invoke("StopShootingSinus2", 5f);
    }



    void FireBulletSinus2()
    {

        // smaller angle differences
        int numBullets = 15;  // Number of bullets in the wave
        float angleDifference = -4f;  // Angle difference between consecutive bullets

        for (int i = 1; i < numBullets; i++)
        {
            float angle = firePoint3.rotation.eulerAngles.z + i * angleDifference;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            Instantiate(bulletPrefab2, firePoint2.position, rotation);
        }

    }

    void StopShootingSinus2()
    {
        
        CancelInvoke("FireBulletSinus2");
        RandomShootingPattern();
    }



    void ShootUpDown()
    {
        Debug.Log("UpDown");
        InvokeRepeating("FireBulletUpDown", 0f, 1f / fireRate);
        InvokeRepeating("FireBulletUpDown2", 0f, 1f / fireRate);
        Invoke("StopShootingUpDown", 5f);
    }
    void FireBulletUpDown()
    {
        float verticalOffset = Mathf.Sin(Time.time * 2f) * verticalRange;  // Use Mathf.Sin for a smooth wave motion
        Vector3 spawnPosition = firePoint1.position + new Vector3(0f, verticalOffset, 0f);

        GameObject bullet = Instantiate(bulletPrefab2, spawnPosition, firePoint1.rotation);

        // Access the BulletMovement script and set the vertical speed
        BulletMovement bulletMovement = bullet.GetComponent<BulletMovement>();
        if (bulletMovement != null)
        {
            bulletMovement.SetVerticalSpeed(verticalOffset);
        }
    }
    void FireBulletUpDown2()
    {
        float verticalOffset = Mathf.Sin(Time.time * 2f) * verticalRange;  // Use Mathf.Sin for a smooth wave motion
        Vector3 spawnPosition = firePoint2.position - new Vector3(0f, verticalOffset, 0f);

        GameObject bullet = Instantiate(bulletPrefab2, spawnPosition, firePoint2.rotation);

        // Access the BulletMovement script and set the vertical speed
        BulletMovement bulletMovement = bullet.GetComponent<BulletMovement>();
        if (bulletMovement != null)
        {
            bulletMovement.SetVerticalSpeed(verticalOffset);
        }
    }
    void StopShootingUpDown()
    {
        CancelInvoke("FireBulletUpDown");
        RandomShootingPattern();
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<playerBulletScript>() != null)
        {
            Instantiate(impactPrefab, collision.transform.position, collision.transform.rotation);
            Destroy(collision.gameObject);
            lifePoints--;
            GameObject.Find("BossLife").GetComponent<Slider>().value = lifePoints;
            if (lifePoints <= 0)
            {
                playerScript.instance.AddScore(300);
                CancelInvoke();
                canShoot = false;
                GetComponent<Animator>().enabled = true;
            }
        }
        else if (collision.gameObject.GetComponent<playerScript>() != null)
        {
            collision.gameObject.GetComponent<playerScript>().Damaged();
        }
    }

    public void SmallExplosion1()
    {
        GameObject.Find("Explosion1").GetComponent<ParticleSystem>().Play();
        GameObject.Find("Explosion1").GetComponent<CFXR_Effect>().enabled = true;
    }

    public void SmallExplosion2()
    {
        GameObject.Find("Explosion2").GetComponent<ParticleSystem>().Play();
        GameObject.Find("Explosion2").GetComponent<CFXR_Effect>().enabled = true;
    }

    public void SmallExplosion3()
    {
        GameObject.Find("Explosion3").GetComponent<ParticleSystem>().Play();
        GameObject.Find("Explosion3").GetComponent<CFXR_Effect>().enabled = true;
    }

    public void FinalExplosion()
    {
        Invoke("HideMesh", 0.4f);

        GameObject.Find("FinalExplosion1").GetComponent<ParticleSystem>().Play();
        GameObject.Find("FinalExplosion1").GetComponent<CFXR_Effect>().enabled = true;
        GameObject.Find("FinalExplosion2").GetComponent<ParticleSystem>().Play();
        GameObject.Find("FinalExplosion2").GetComponent<CFXR_Effect>().enabled = true;

        Invoke("Die", 1);
    }

    private void HideMesh()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("BossDecoTurret1").SetActive(false);
        GameObject.Find("BossDecoTurret2").SetActive(false);
        GameObject.Find("BossMidTurret").SetActive(false);
        GameObject.Find("BossBotTurret").SetActive(false);
        GameObject.Find("BossTopTurret").SetActive(false);
        GameObject.Find("BossPipe").SetActive(false);
        GameObject.Find("BossAntenna").SetActive(false);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
