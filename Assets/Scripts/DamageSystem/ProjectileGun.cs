using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGun : MonoBehaviour, IShootable
{
    [Header("Damage Settings")]
    public int playerNumber;
    public float fireRate;
    private float lastShotTime;

    [Header("Projectile Settings")]//accuracy settings
    public Projectile projectile;
    public float projectileForce = 0.8f;
    public int ammoNumber;
    private List<Projectile> bulletPool = new List<Projectile>();

    [SerializeField] Transform barrelEnd;

    [Header("Effects and Components")] //effects settings
    public float effectTime = 0.5f;
    public ParticleSystem muzzleFlash;
    LineRenderer line;

    [Space]
    public bool debugging = false;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        SetUpPool();
    }
    public void Shoot()
    {
        if (Time.time - lastShotTime < fireRate) return;
        muzzleFlash.transform.position = barrelEnd.position;
        muzzleFlash.transform.rotation = barrelEnd.rotation;
        muzzleFlash.Play();
        Projectile bullet = GetBulletFromPool();
        if (bullet == null) Debug.Log("Out of Ammo");

        bullet.transform.position = barrelEnd.position;
        bullet.transform.rotation = barrelEnd.rotation;

        bullet.gameObject.SetActive(true);

        bullet.rb.AddForce((barrelEnd.forward * projectileForce) + Vector3.up * 0.2f, ForceMode.Impulse);
        lastShotTime = Time.time;
    }

    void SetUpPool()
    {
        for (int i = 0; i < ammoNumber; i++)
        {
            Projectile spawnedAmmo = Instantiate(projectile, transform.position, transform.rotation);
            spawnedAmmo.playerNumber = playerNumber;
            spawnedAmmo.gun = this;
            bulletPool.Add(spawnedAmmo);
            spawnedAmmo.gameObject.SetActive(false);
        }
    }

    Projectile GetBulletFromPool()
    {
        for (int i = 0; i < bulletPool.Count; i++)
        {
            if (!bulletPool[i].gameObject.activeInHierarchy)
            {
                return bulletPool[i];
            }
        }
        return null;
    }
 
}
