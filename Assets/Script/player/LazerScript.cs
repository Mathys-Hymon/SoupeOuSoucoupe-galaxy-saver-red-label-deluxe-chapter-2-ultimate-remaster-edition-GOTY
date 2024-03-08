using UnityEngine;

public class LazerScript : MonoBehaviour
{
    private bool lazer;
    public void setLazer()
    {
        lazer = true;
        GetComponent<ParticleSystem>().Play();
        Invoke(nameof(StopLazer), 5);
    }

    private void StopLazer()
    {
        lazer = false;
        GetComponent<ParticleSystem>().Stop();
    }
    void Update()
    {
        if(lazer)
        {
            Collider[] overlapped = Physics.OverlapBox(gameObject.transform.position, new Vector3(100, 1.4f, 5), Quaternion.identity);
            for(int i = 0; i < overlapped.Length; i++)
            {
                if (overlapped[i].gameObject.GetComponent<EnnemiBulletScript>() != null || overlapped[i].gameObject.GetComponent<EnnemiScript>() != null)
                {
                    Destroy(overlapped[i].gameObject);
                }
            }
        }
    }
}
