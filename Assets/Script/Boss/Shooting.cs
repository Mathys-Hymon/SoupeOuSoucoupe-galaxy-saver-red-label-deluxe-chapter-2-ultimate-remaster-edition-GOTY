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


    void ShootSinusoidalWave()
    {
        Debug.Log("Sinus");


        Invoke("RandomShootingPattern", 5f);
    }


}
