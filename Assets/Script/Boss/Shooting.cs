using UnityEngine;

public class BossShooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab1;
    [SerializeField] private GameObject bulletPrefab2;
    [SerializeField] private Transform firePoint1;
    [SerializeField] private Transform firePoint2;
    [SerializeField] private Transform firePoint3;
    [SerializeField] private float fireRate = 2f;



    private void Start()
    {
        RandomShootingPattern();
    }


    void RandomShootingPattern()
    {
        int patternSelector = Random.Range(1, 2);

        switch (patternSelector)
        {
            case 1:
                ShootMult();
                break;
            case 2:
                ShootMult();
                break;
        }
    }

    void ShootMult()
    {
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
}
