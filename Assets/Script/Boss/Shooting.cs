using Unity.VisualScripting;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.5f;  // Bullets per second

    private float timeSinceLastFire = 0f;

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
            RandomShootingPattern();
        }
    }

    void RandomShootingPattern()
    {
       
        int patternSelector = Random.Range(0, 3);  // Change the range based on the number of patterns

        switch (patternSelector)
        {
            case 0:
                StartCoroutine(ShootPattern(2f));  // Shoot pattern lasts 5 seconds
                break;
            case 1:
                StartCoroutine(ShootStraightPattern(10f));  // ShootStraight pattern lasts 10 seconds
                break;
            case 2:
                StartCoroutine(ShootSinusoidalPattern(10f));  // ShootSinusoidal pattern lasts 8 seconds
                break;
                // Add more cases for additional patterns
        }
    }

    System.Collections.IEnumerator ShootPattern(float duration)
    {
        while (duration > 0f)
        {
            // Shooting logic
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // Update duration
            duration -= Time.deltaTime;

            yield return null;
        }

        

        // After the pattern is finished, call the random shooting pattern again
        RandomShootingPattern();
    }

    System.Collections.IEnumerator ShootStraightPattern(float duration)
    {
        
        while (duration > 0f)
        {
            // Shooting logic for ShootStraight pattern
            //Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Debug.Log("straight");

            // Update duration
            duration -= Time.deltaTime;

            yield return null;
        }

        // After the pattern is finished, call the random shooting pattern again
        RandomShootingPattern();
    }

    System.Collections.IEnumerator ShootSinusoidalPattern(float duration)
    {
        while (duration > 0f)
        {
            // Shooting logic for ShootSinusoidal pattern
            //Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Debug.Log("sinus");

            // Update duration
            duration -= Time.deltaTime;

            yield return null;
        }

        // After the pattern is finished, call the random shooting pattern again
        RandomShootingPattern();
    }
    // Add more pattern functions as needed
}
