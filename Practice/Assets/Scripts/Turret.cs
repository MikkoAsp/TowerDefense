using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {
    protected Transform target;
    protected Enemy enemyTarget;
    [Header("General")]

    public float range = 15f;
    [Header("Use Bullets (default)")]
    public float fireRate = 1f;
    protected float fireCountdown = 0f;

    [Header("Unity setup fields")]

    public string enemyTag = "Enemy";
    public Transform rotationPoint;
    public float turretRotationSpeed = 10f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    protected void Start() {
        InvokeRepeating("UpdateTarget", 0f, 0.25f);
    }
    protected virtual void Update() {
        if(target == null)
        {
            return;
        }

        LockOnTarget();

        if (fireCountdown <= 0f)
         {
            Shoot();
            fireCountdown = 1f / fireRate;
            }

        fireCountdown -= Time.deltaTime;
    }
    protected virtual void UpdateTarget() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            enemyTarget = nearestEnemy.GetComponent<Enemy>();
        }
        else
            target = null;
    }
    protected void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(rotationPoint.rotation, lookRotation, Time.deltaTime * turretRotationSpeed).eulerAngles;
        rotationPoint.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    protected void Shoot() {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if (bullet != null)
            bullet.SeekTarget(target);
    }

    protected void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
