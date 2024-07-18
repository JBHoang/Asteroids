using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Useful for overall state of the game,
    // keeping track of score, lives, state of player(dead or alive)

    public Player player;
    public float respawnTime = 3.0f;
    public float respawnInvulnerabilityTime = 3.0f;
    public ParticleSystem explosion;
    public int lives = 3;
    public Text liveText;
    public int score = 0;
    public Text scoreText;
    public GameObject gameOver;
    public GameObject startButton;
    
    public void AsteriodDestroyed(Asteriods asteriods)
    {
        this.explosion.transform.position = asteriods.transform.position;
        this.explosion.Play();

        if (asteriods.size < 0.75f)
        {
            this.score += 100;
        }
        else if (asteriods.size < 1.2f)
        {
            this.score += 50;
        }
        else
        {
            this.score += 25;
        }

        this.scoreText.text = score.ToString();
    }

    public void PlayerDied()
    {
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();
        this.lives--;
        this.liveText.text = lives.ToString();

        if(this.lives <= 0)
        {
            GameOver();
        }
        else
        {
            Invoke(nameof(Respawn), this.respawnTime);
        }

        
    }
    
    private void Respawn()
    {
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
        this.player.gameObject.SetActive(true);
        Invoke(nameof(TurnOnCollission), this.respawnInvulnerabilityTime);
    }

    private void TurnOnCollission()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void Start()
    {
        Pause();
    }
    
    public void Play()
    {
        startButton.SetActive(false);
        gameOver.SetActive(false);

        Time.timeScale = 1f;
        player.enabled = true;

        this.lives = 3;
        this.score = 0;
        this.scoreText.text = score.ToString();
        this.liveText.text = lives.ToString();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        this.player.enabled = false;
    }

    private void GameOver()
    {
        startButton.SetActive(true);
        gameOver.SetActive(true);

        Pause();

        Invoke(nameof(Respawn), this.respawnTime);
    }
}
