using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Turret
{
    [Header("Use Laser")]
    public bool useLaser = false;
    public float damageOverTime = 0.5f;
    [SerializeField] [Range(0, 1)] float slowdownPercent;
    public LineRenderer lineRenderer;
    public ParticleSystem laserShootEffect;
    public Light impactLight;

    protected override void Update()
    {
        if (target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    laserShootEffect.Stop();
                    impactLight.enabled = false;
                }
            }
            return;
        }
        LockOnTarget();
        if (useLaser)
        {
            Lasering();
        }
    }
    void Lasering()
    {
        enemyTarget.TakeDamage(damageOverTime * Time.deltaTime);
        enemyTarget.SlowMovement(slowdownPercent);
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            laserShootEffect.Play();
            impactLight.enabled = true;
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;

        laserShootEffect.transform.rotation = Quaternion.LookRotation(dir);
        laserShootEffect.transform.position = target.position + dir.normalized;
    }
}
