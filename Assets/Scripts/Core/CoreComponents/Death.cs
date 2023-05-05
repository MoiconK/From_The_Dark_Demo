using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : CoreComponent
{
    [SerializeField] private GameObject[] deathParticles;
    [SerializeField] private GameObject healthPack;
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private bool isCoffer;
    [SerializeField] private bool isPlayer;
    [SerializeField] private bool isBoss;
    public AudioSource deathSound;

    private ParticleManager ParticleManager => particleManager ? particleManager : core.GetCoreComponent<ParticleManager>();
    private ParticleManager particleManager;

    private Stats Stats => stats ? stats : core.GetCoreComponent<Stats>();
    private Stats stats;


    private void Start()
    {
        deathMenu.SetActive(false);
        
    }

   public void Die()
    {      

        foreach (var particle in deathParticles)
        {
            ParticleManager.StartParticles(particle);
        }

        if (isCoffer) { 
            Instantiate(healthPack, transform.position, Quaternion.identity); 
        }
        
        

        if (isPlayer)
        {
            deathMenu.SetActive(true);
            Time.timeScale = 0f;
        }

        if (isBoss)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        deathSound.Play();
        Destroy(core.transform.parent.gameObject);      

    }

    public void OnEnable()
    {
        Stats.OnHealthZero += Die;
        
    }

    public void OnDisable()
    {
        Stats.OnHealthZero -= Die;
    }
}
