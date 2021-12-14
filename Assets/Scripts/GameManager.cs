using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    public ParticleSystem explosion;
    public int lives {get; private set;}
    public Text textLives;
    public int score {get; private set;}
    public Text textScore;
    public float respawnTime = 3.0f;
    public float invincibleTime = 3.0f;


    private void Start()
    {
        SetLives(3);
        SetScore(0);
        Respawn();
    }

    public void AsteroidDestroyed(Asteroid asteroid)
    {
        this.explosion.transform.position = asteroid.transform.position;
        this.explosion.Play();

        if (asteroid.size < 0.5f) {
            SetScore(score + 100);
        } else if (asteroid.size < 1.0f) {
            SetScore(score + 50);
        } else if (asteroid.size < 2.0f) {
            SetScore(score + 25);
        } 

    }

    public void PlayerDied()
    {
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();
        SetLives(this.lives - 1);

        if (this.lives <= 0) {
            GameOver();
        } else {
            Invoke(nameof(Respawn), this.respawnTime);
        }

    }

    private void Respawn()
    {
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.SetActive(true);
        this.player.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
        Invoke(nameof(TurnOnCollisions), this.invincibleTime);

    }

    private void TurnOnCollisions() 
    { 
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");  
    }

    private void SetScore(int score)
    {
        this.score = score;
        this.textScore.text = score.ToString();
    }

    private void SetLives(int lives) 
    {
        this.lives = lives;
        this.textLives.text = lives.ToString();
    }

    private void GameOver()
    {
        //TODO: menu options
    }
}
