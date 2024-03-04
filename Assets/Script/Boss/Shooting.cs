using UnityEngine;

public class BossShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 2f;  // Bullets per second

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
                ShootStraight();
                break;
            case 1:
                ShootMultipleLines();
                break;
            case 2:
                ShootSinusoidalWave();
                break;
                // Add more cases for additional patterns
        }
    }

    void ShootStraight()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    void ShootMultipleLines()
    {
        // Implement logic to shoot multiple lines with different angles
        // You can use loops and change the rotation of bullets
    }

    void ShootSinusoidalWave()
    {
        // Implement logic to shoot bullets in a sinusoidal wave pattern
        // You can use Mathf.Sin to achieve this
    }
    // Add more pattern functions as needed
}
