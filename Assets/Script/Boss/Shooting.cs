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

        Invoke("RandomShootingPattern", 5f);

            
    }

    void ShootMultipleLines()
    {
        // Implement logic to shoot multiple lines with different angles
        // You can use loops and change the rotation of bullets
        Debug.Log("Mult");


        Invoke("RandomShootingPattern", 5f);



    }

    void ShootSinusoidalWave()
    {
        // Implement logic to shoot bullets in a sinusoidal wave pattern
        // You can use Mathf.Sin to achieve this
        Debug.Log("Wave");

        Invoke("RandomShootingPattern", 5f);




    }
    
}
