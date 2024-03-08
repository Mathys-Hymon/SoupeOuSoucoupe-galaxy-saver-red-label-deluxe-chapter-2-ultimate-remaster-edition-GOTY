using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab1;
    [SerializeField] private GameObject bulletPrefab2;
    [SerializeField] private Transform firePoint1;
    [SerializeField] private Transform firePoint2;
    [SerializeField] private Transform firePoint3;
    public float fireRate = 2f;
    float verticalRange = 6f;  // Adjust the vertical range as needed
    float bulletSpeed = 5f;    // Adjust the speed of the bullets
    private float timeSinceLastFire = 0f;
    private int lastPattern = -1;


    private void Start()
    {
        RandomShootingPattern();
    }


    void RandomShootingPattern()
    {
        int patternSelector = Random.Range(1, 2);

        switch (patternSelector)
        {
        int patternSelector;

        do
        {
            patternSelector = Random.Range(1, 5);  // Change the range based on the number of patterns
        } while (patternSelector == lastPattern);

        lastPattern = patternSelector;

        // Cancel previous invokes to ensure no overlapping patterns
        CancelInvoke();

        switch (patternSelector)
        {
            case 0:
                ShootStraight();
                break;
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

    void ShootMult()
    {
        InvokeRepeating("FireBulletMult", 0f, 1f / fireRate);

    void ShootStraight()
    {
        Debug.Log("Straight");

        
        InvokeRepeating("FireBulletStraight", 0f, 1f / fireRate);

        
        Invoke("StopShootingStraight", 5f);
    }


    void FireBulletStraight()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    void StopShootingStraight()
    {
        
        CancelInvoke("FireBullet");

        
        RandomShootingPattern();
    }


    void ShootMult()
    {
        Debug.Log("Mult");
        InvokeRepeating("FireBulletMult", 0f, 1f / fireRate);
        Invoke("StopShootingMult", 5f);
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
    }

    void StopShootingMult()
    {
        CancelInvoke("FireBulletMult");
        Invoke("RandomShootingPattern", 3);
    }
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
            float angle = firePoint.rotation.eulerAngles.z + i * angleDifference;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            Instantiate(bulletPrefab, firePoint.position, rotation);
        }

        for (int i = 1; i < numBullets; i++)
        {
            float angle = firePoint.rotation.eulerAngles.z - i * angleDifference;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            Instantiate(bulletPrefab, firePoint.position, rotation);
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
            float angle = firePoint.rotation.eulerAngles.z + i * angleDifference;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            Instantiate(bulletPrefab, firePoint.position, rotation);
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
        Vector3 spawnPosition = firePoint.position + new Vector3(0f, verticalOffset, 0f);

        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, firePoint.rotation);

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
        Vector3 spawnPosition = firePoint.position - new Vector3(0f, verticalOffset, 0f);

        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, firePoint.rotation);

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
}
