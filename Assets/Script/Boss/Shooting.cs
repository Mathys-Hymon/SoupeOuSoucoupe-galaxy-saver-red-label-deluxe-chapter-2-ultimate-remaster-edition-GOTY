using UnityEngine;

public class BossShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 2f;  // Bullets per second

    private float timeSinceLastFire = 0f;



    private void Start()
    {
        RandomShootingPattern();
    }


    void RandomShootingPattern()
    {
        int patternSelector = Random.Range(0, 4);  // Change the range based on the number of patterns


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
                // Add more cases for additional patterns
        }
    }

    void ShootStraight()
    {
        Debug.Log("Straight");

        // Shoot multiple bullets with a certain fire rate until canceled
        InvokeRepeating("FireBulletStraight", 0f, 1f / fireRate);

        // Schedule the end of continuous shooting
        Invoke("StopShootingStraight", 5f);
    }


    void FireBulletStraight()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    void StopShootingStraight()
    {
        // Cancel the repeating invocation to stop shooting
        CancelInvoke("FireBullet");

        // Call the random shooting pattern function to choose a new pattern
        RandomShootingPattern();
    }


    void ShootMult()
    {
        Debug.Log("Mult");

        // Shoot multiple bullets with a certain fire rate until canceled
        InvokeRepeating("FireBulletMult", 0f, 1f / fireRate);

        // Schedule the end of continuous shooting
        Invoke("StopShootingMult", 5f);
    }



    void FireBulletMult()
    {

        // Instantiate five bullets with different initial directions
        float[] angles = { -20f, -10f, 0f, 10f, 20f }; // Example angles, adjust as needed

        foreach (float angle in angles)
        {
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            Instantiate(bulletPrefab, firePoint.position, rotation);
        }

    }

    void StopShootingMult()
    {
        // Cancel the repeating invocation to stop shooting
        CancelInvoke("FireBulletMult");

        // Call the random shooting pattern function to choose a new pattern
        RandomShootingPattern();
    }


    void ShootSinus()
    {
        Debug.Log("Sinus");

        // Shoot multiple bullets with a certain fire rate until canceled
        InvokeRepeating("FireBulletSinus", 0f, 1f / fireRate);

        // Schedule the end of continuous shooting
        Invoke("StopShootingSinus", 5f);
    }



    void FireBulletSinus()
    {

        // Instantiate bullets in a sinusoidal wave pattern with smaller angle differences
        int numBullets = 15;  // Number of bullets in the wave
        float angleDifference = 4f;  // Angle difference between consecutive bullets

        for (int i = 0; i < numBullets; i++)
        {
            float angle = firePoint.rotation.eulerAngles.z + i * angleDifference;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            Instantiate(bulletPrefab, firePoint.position, rotation);
        }

    }

    void StopShootingSinus()
    {
        // Cancel the repeating invocation to stop shooting
        CancelInvoke("FireBulletSinus");

        // Call the random shooting pattern function to choose a new pattern
        RandomShootingPattern();
    }



    void ShootSinus2()
    {
        Debug.Log("Sinus2");

        // Shoot multiple bullets with a certain fire rate until canceled
        InvokeRepeating("FireBulletSinus2", 0f, 1f / fireRate);

        // Schedule the end of continuous shooting
        Invoke("StopShootingSinus2", 5f);
    }



    void FireBulletSinus2()
    {

        // Instantiate bullets in a sinusoidal wave pattern with smaller angle differences
        int numBullets = 15;  // Number of bullets in the wave
        float angleDifference = -4f;  // Angle difference between consecutive bullets

        for (int i = 0; i < numBullets; i++)
        {
            float angle = firePoint.rotation.eulerAngles.z + i * angleDifference;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            Instantiate(bulletPrefab, firePoint.position, rotation);
        }

    }

    void StopShootingSinus2()
    {
        // Cancel the repeating invocation to stop shooting
        CancelInvoke("FireBulletSinus2");

        // Call the random shooting pattern function to choose a new pattern
        RandomShootingPattern();
    }











}
