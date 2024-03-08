using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] EnnemieFormation;

    private int wave;
    private int bulletPerShoot;
    private int lifePoint;
    private float speed;
    private float shootRate;

    private void Update()
    {
        if (GameObject.FindObjectsOfType<EnnemiScript>().Length == 0)
        {
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
            shootRate = 5;
        }
        else if (wave > 3 && wave < 7)
        {
            bulletPerShoot = 2;
            lifePoint = 2;
            speed = 1;
            shootRate = 7;
        }
        else if (wave >= 7 && wave < 12)
        {
            bulletPerShoot = 3;
            lifePoint = 2;
            speed = 2;
            shootRate = 8;
        }
        else if (wave >= 12 && wave < 15)
        {
            bulletPerShoot = 3;
            lifePoint = 3;
            speed = 2.5f;
            shootRate = 10;
        }
        else if (wave >= 15 && wave < 20)
        {
            bulletPerShoot = 4;
            lifePoint = 3;
            speed = 2.5f;
            shootRate = 7;
        }

        int Formation = Random.Range(0, EnnemieFormation.Length - 1);
        Instantiate(EnnemieFormation[Formation], gameObject.transform);
        EnnemiScript[] ennemies = GameObject.FindObjectsOfType<EnnemiScript>();
        int i = 0;
        foreach (EnnemiScript ennemie in ennemies)
        {
            ennemie.InitializeEnnemie(bulletPerShoot, lifePoint, speed, shootRate, i);
            i++;
        }

        wave++;
        print(wave);
    }
}