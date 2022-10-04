using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int powerUpLevelRequireMent = 0;

    public BulletMover bullet;
    Vector3 direction;

    public bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        direction = (transform.localRotation * Vector3.forward).normalized;
    }

    public void Shoot()
    {
        GameObject go = Instantiate(bullet.gameObject, transform.position, Quaternion.identity);
        BulletMover goBullet = go.GetComponent<BulletMover>();
        goBullet.direction = direction;
    }
}
