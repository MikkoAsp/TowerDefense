using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    PlayerStats playerStats;
    public float startSpeed = 10f;
    private float speed;
    public float starthealth;
    private float health;
    public float bounty;
    public GameObject DeathEffect;
    private Transform target;
    private int wavepointIndex = 0;
    public Image healthBar;


    private void Start()
    {
        speed = startSpeed;
        health = starthealth;
        target = Waypoints.points[0];
        playerStats = FindObjectOfType<PlayerStats>();
    }
    public void TakeDamage(float amount)
    {
        starthealth -= amount;
        healthBar.fillAmount = starthealth / health;
        if (starthealth <= 0)
            Die();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        //Moving towards target
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        //If target is less than 0.4 distance away
        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
            GetNextWaypoint();
        speed = startSpeed;
    }
    public void SlowMovement(float percentage)
    {
        speed = startSpeed * (1f - percentage);

    }
    private void GetNextWaypoint()
    {
        //If no more waypoints are available, else increase the index and change targets
        if(wavepointIndex >= Waypoints.points.Length - 1)
        {
            if (!PlayerStats.gameOver && !PlayerStats.inMainMenu)
            {
                playerStats.LoseLives(starthealth);
            }
            Destroy(gameObject);
            return;
        }
        else {
            wavepointIndex++;
            target = Waypoints.points[wavepointIndex];
        }
    }
    private void Die() {
        PlayerStats.money += bounty;
        GameObject effect = Instantiate(DeathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        Destroy(gameObject);
    }
}
