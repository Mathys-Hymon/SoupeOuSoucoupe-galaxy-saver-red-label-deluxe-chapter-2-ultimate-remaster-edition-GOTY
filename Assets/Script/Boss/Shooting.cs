using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 2f;  // Bullets per second
    float duration = 5f;

    private float timeSinceLastFire = 0f;


    private void Start()
    {
        RandomShootingPattern();
    }



    private void Update()
    {

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
        duration = 5f;

        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        while (duration > 0)
        {
            

            Debug.Log("Straight");

            duration -= Time.deltaTime;
        }

        RandomShootingPattern();
    }

    void ShootMultipleLines()
    {
        duration = 5f;

        while (duration > 0)
        {
            

            Debug.Log("Mult");

            duration -= Time.deltaTime;
        }
        RandomShootingPattern();
    }

    void ShootSinusoidalWave()
    {
        duration = 5f;

        while (duration > 0)
        {

            
            Debug.Log("Wave");

            duration -= Time.deltaTime;
        }
        RandomShootingPattern();
    }
    // Add more pattern functions as needed
}
