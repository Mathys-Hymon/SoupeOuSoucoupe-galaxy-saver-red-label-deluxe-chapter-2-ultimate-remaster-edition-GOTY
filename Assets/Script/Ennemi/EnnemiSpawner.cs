using System.Collections;
using UnityEngine;

public class EnnemiSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] ennemieFormation;
    [SerializeField] private GameObject boss;

    [SerializeField]  private int wave;
    private int bulletPerShoot;
    private int lifePoint;
    private float speed;
    private float shootRate;
    private bool canSpawn = true;

    private void Update()
    {
        if (GameObject.FindObjectsOfType<EnnemiScript>().Length == 0 && GameObject.FindObjectsOfType<Shooting>().Length == 0 && canSpawn)
        {
            canSpawn = false;
            SpawnWave();
        }


    }


    private void SpawnWave()
    {
        if (wave == 0)
        {
            bulletPerShoot = 1;
            lifePoint = 1;
            speed = 1;
            shootRate = 3;
        }
        else if (wave > 3 && wave < 7)
        {
            bulletPerShoot = 2;
            lifePoint = 2;
            speed = 1;
            shootRate = 4;
        }
        else if (wave >= 7 && wave < 12)
        {
            bulletPerShoot = 3;
            lifePoint = 2;
            speed = 2;
            shootRate = 5;
        }
        else if (wave >= 12 && wave < 15)
        {
            bulletPerShoot = 3;
            lifePoint = 3;
            speed = 2.5f;
            shootRate = 7;
        }
        else if (wave >= 15 && wave < 20)
        {
            bulletPerShoot = 4;
            lifePoint = 3;
            speed = 2.5f;
            shootRate = 8;
        }


        if(wave % 5 == 0 && wave != 0)
        {
            GameObject.Find("WavesMusic").GetComponent<AudioSource>().Stop();
            GameObject.Find("BossMusic").GetComponent<AudioSource>().Play();

            GameObject bossRef  = Instantiate(boss, gameObject.transform);
            bossRef.transform.localPosition = new Vector3(-6.5f, bossRef.transform.position.y, 1.87f);
            bossRef.transform.parent = null;
        }
        else
        {
            int Formation = Random.Range(0, ennemieFormation.Length);
            Instantiate(ennemieFormation[Formation], gameObject.transform);
            EnnemiScript[] ennemies = GameObject.FindObjectsOfType<EnnemiScript>();
            int i = 0;
            foreach (EnnemiScript ennemie in ennemies)
            {
                ennemie.InitializeEnnemie(bulletPerShoot, lifePoint, speed, shootRate, i);
                i++;
            }
        }
        wave++;
        canSpawn = true;
    }
}
