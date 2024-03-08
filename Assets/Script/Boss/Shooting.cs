using UnityEngine;

public class BossShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 2f;  // Bullets per second


    float verticalRange = 6f;  // Adjust the vertical range as needed
    float bulletSpeed = 5f;    // Adjust the speed of the bullets

    private float timeSinceLastFire = 0f;



    private void Start()
    {
        RandomShootingPattern();
    }


    void RandomShootingPattern()
    {
        int patternSelector = Random.Range(4, 5);  


        CancelInvoke("FireBulletStraight");

        CancelInvoke("FireBulletMult");

        switch (patternSelector)
        {
            case 0:
                ShootStraight();
                
                break;
            case 1:
                ShootMult();
                break;
            case 2:
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

        // Instantiate five bullets with different initial directions
        float[] angles = { -20f, -10f, 0f, 10f, 20f }; 

        foreach (float angle in angles)
        {
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            Instantiate(bulletPrefab, firePoint.position, rotation);
        }

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
            float angle = firePoint.rotation.eulerAngles.z + i * angleDifference;
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

    void StopShootingUpDown()
    {
        CancelInvoke("FireBulletUpDown");
        RandomShootingPattern();
    }









}
