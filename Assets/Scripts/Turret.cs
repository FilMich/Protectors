using System.Runtime.CompilerServices;
using UnityEngine;

public class Turret : MonoBehaviour
{
	[Header("Turret Costs")]
	public int cost;             // The cost to place this turret
	public int upgradeCost;     // The cost to upgrade this turret

	[Header("Upgrade")]
	public GameObject upgradedPrefab; // The prefab for the upgraded turret

	public Transform target;
    public float range = 15f;
    public float timeToShoot = 5.0f;
    private float shootingCountdown = 0;
    public string enemyTag = "enemy";
    public GameObject bullet;
    public Transform bulletStart;
    void Start()
    {
        InvokeRepeating("FindTarget", 0f, 0.5f);
        
    }

    void FindTarget ()
    {
        //Nastavi� prv� meranie mimo vzdialenosti ve�e
        float closestEnemyDistance = range + 1.0f;
        GameObject nearestEnemy = null;


        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        foreach (GameObject enemy in enemies)
        {
            float enemyDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (enemyDistance < closestEnemyDistance)
            {
                closestEnemyDistance = enemyDistance;
                nearestEnemy = enemy;
            }
            else
            {
                target = null;
            }
        }

        if (nearestEnemy != null && closestEnemyDistance <= range) {
            target = nearestEnemy.transform;     
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }


    void Update()
    {
        if (target == null) {
            return;
        }

        if (shootingCountdown <= 0)
        {
            Shoot();
            shootingCountdown = timeToShoot;
        }

        shootingCountdown -= Time.deltaTime;

    }
    void Shoot()
    {
        GameObject bulletObject = (GameObject)Instantiate(bullet, bulletStart.position, bulletStart.rotation);
        Bullet curBullet = bulletObject.GetComponent<Bullet>();

        if (curBullet != null)
        {
            curBullet.Seek(target);
        }

    }
}
