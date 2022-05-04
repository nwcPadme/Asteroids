using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;

    public float respawnTime = 3.0f;
    public float respawnInvulnerabilityTime = 3.0f;

    public int lives = 3;
    public int score = 0;

    public ParticleSystem explosion;


    public void AsteroidDestroyed(Asteroid asteroid)
    {
        this.explosion.transform.position = asteroid.transform.position;
        this.explosion.Play();

        if (asteroid.size < 0.75f)
        {
            score += 100;
        }
        else if (asteroid.size < 1.2f)
        {
            score += 50;
        }
        else
        {
            score += 25;
        }

        //TODO best score, GUI
    }



    public void PlayerDie()
    {
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();
        this.lives--;

        if (this.lives <= 0)
        {
            GameOver();
        }
        else
        {
            Invoke(nameof(Respawn), respawnTime);
        }

        
    }

    private void Respawn()
    {

        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("IgnoreCollision");
        this.player.gameObject.SetActive(true);

        Invoke(nameof(TurnOnCollisions), respawnInvulnerabilityTime);
        //TODO this.player.GetComponent<SpriteRenderer>().color.gamma();
    }

    private void TurnOnCollisions()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }


    private void GameOver()
    {
        //TODO GUI

        this.lives = 3;
        this.score = 0;

        Invoke(nameof(Respawn), respawnTime);
    }


}
