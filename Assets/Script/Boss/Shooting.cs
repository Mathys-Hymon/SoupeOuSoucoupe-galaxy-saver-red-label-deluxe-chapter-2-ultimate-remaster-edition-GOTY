using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 2f;  // Bullets per second
    float duration = 1f;

    private float timeSinceLastFire = 0f;


    private void Start()
    {
        RandomShootingPattern();
    }



    private void Update()
    {
        // Update the timer
        timeSinceLastFire += Time.deltaTime;

        // Check if it's time to shoot
        if (timeSinceLastFire >= 1f / fireRate)  // Inverse of fire rate to get time between shots
        {
            // Reset the timer
            timeSinceLastFire = 0f;

            // Call a random shooting pattern
            //RandomShootingPattern();
        }
    }

    void RandomShootingPattern()
    {

        int patternSelector = Random.Range(0, 3);  // Change the range based on the number of patterns

        switch (patternSelector)
        {
            case 0:
                duration = 5f;
                ShootStraight();
                break;
            case 1:
                duration = 10f;
                ShootMultipleLines();
                break;
            case 2:
                duration = 15f;
                ShootSinusoidalWave();
                break;
                // Add more cases for additional patterns
        }
    }

    void ShootStraight()
    {
        

        while (duration > 0)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            Debug.Log("Straight");

            duration -= Time.deltaTime;
        }

        RandomShootingPattern();
    }

    void ShootMultipleLines()
    {
        while (duration > 0)
        {
            

            Debug.Log("Mult");

            duration -= Time.deltaTime;
        }
        RandomShootingPattern();
    }

    void ShootSinusoidalWave()
    {
        while (duration > 0)
        {
            

            Debug.Log("Wave");

            duration -= Time.deltaTime;
        }
        RandomShootingPattern();
    }
    // Add more pattern functions as needed
}
