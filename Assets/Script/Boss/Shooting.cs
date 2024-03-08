using CartoonFX;
using UnityEngine;
using UnityEngine.UI;

public class BossShooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab1;
    [SerializeField] private GameObject bulletPrefab2;
    [SerializeField] private Transform firePoint1;
    [SerializeField] private Transform firePoint2;
    [SerializeField] private Transform firePoint3;
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private GameObject lifeBar;

    [SerializeField] private int lifePoints;
    private bool canShoot = true;


    private void Start()
    {
        lifeBar.SetActive(true);

        RandomShootingPattern();
    }


    void RandomShootingPattern()
    {
        if(canShoot)
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

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<playerBulletScript>() != null)
        {
            lifePoints--;
            lifeBar.GetComponent<Slider>().value = lifePoints;
            if (lifePoints <= 0)
            {
                Destroy(collision.gameObject);
                GetComponent<Animator>().enabled = true;
            }
        }
        else if (collision.gameObject.GetComponent<playerScript>() != null)
        {
            collision.gameObject.GetComponent<playerScript>().Damaged();
        }
    }

    public void SmallExplosion1()
    {
        GameObject.Find("Explosion1").GetComponent<ParticleSystem>().Play();
        GameObject.Find("Explosion1").GetComponent<CFXR_Effect>().enabled = true;
    }

    public void SmallExplosion2()
    {
        GameObject.Find("Explosion2").GetComponent<ParticleSystem>().Play();
        GameObject.Find("Explosion2").GetComponent<CFXR_Effect>().enabled = true;
    }

    public void SmallExplosion3()
    {
        GameObject.Find("Explosion3").GetComponent<ParticleSystem>().Play();
        GameObject.Find("Explosion3").GetComponent<CFXR_Effect>().enabled = true;
    }

    public void FinalExplosion()
    {
        Invoke("HideMesh", 0.4f);

        GameObject.Find("FinalExplosion1").GetComponent<ParticleSystem>().Play();
        GameObject.Find("FinalExplosion1").GetComponent<CFXR_Effect>().enabled = true;
        GameObject.Find("FinalExplosion2").GetComponent<ParticleSystem>().Play();
        GameObject.Find("FinalExplosion2").GetComponent<CFXR_Effect>().enabled = true;

        Invoke("Die", 1);
    }

    private void HideMesh()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("BossDecoTurret1").SetActive(false);
        GameObject.Find("BossDecoTurret2").SetActive(false);
        GameObject.Find("BossMidTurret").SetActive(false);
        GameObject.Find("BossBotTurret").SetActive(false);
        GameObject.Find("BossTopTurret").SetActive(false);
        GameObject.Find("BossPipe").SetActive(false);
        GameObject.Find("BossAntenna").SetActive(false);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
