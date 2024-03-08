using UnityEngine;

public class BossShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint1;
    public Transform firePoint2;
    public Transform firePoint3;
    public float fireRate = 2f;  // Bullets per second



    private void Start()
    {
        RandomShootingPattern();
    }


    void RandomShootingPattern()
    {
        int patternSelector = Random.Range(0, 3);  // Change the range based on the number of patterns


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
                ShootSinusoidalWave();
                break;
                // Add more cases for additional patterns
        }
    }

    void ShootStraight()
    {
        // Shoot multiple bullets with a certain fire rate until canceled
        InvokeRepeating("FireBulletStraight", 0f, 1f / fireRate);

        // Schedule the end of continuous shooting
        Invoke("StopShootingStraight", 5f);
    }


    void FireBulletStraight()
    {
        Instantiate(bulletPrefab, firePoint1.position, firePoint1.rotation);
        Instantiate(bulletPrefab, firePoint2.position, firePoint2.rotation);
        Instantiate(bulletPrefab, firePoint3.position, firePoint3.rotation);
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
            Instantiate(bulletPrefab, firePoint1.position, rotation);
            Instantiate(bulletPrefab, firePoint2.position, rotation);
            Instantiate(bulletPrefab, firePoint3.position, rotation);
        }

    }

    void StopShootingMult()
    {
        // Cancel the repeating invocation to stop shooting
        CancelInvoke("FireBulletMult");

        // Call the random shooting pattern function to choose a new pattern
        RandomShootingPattern();
    }


    void ShootSinusoidalWave()
    {
        Invoke("RandomShootingPattern", 5f);
    }


}
