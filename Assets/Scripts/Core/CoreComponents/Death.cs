using UnityEngine;

public class Death : CoreComponent
{
    [SerializeField] private GameObject[] deathParticles;
    [SerializeField] private GameObject healthPack;
    [SerializeField] private bool isCoffer;

    private ParticleManager ParticleManager => particleManager ? particleManager : core.GetCoreComponent<ParticleManager>();
    private ParticleManager particleManager;

    private Stats Stats => stats ? stats : core.GetCoreComponent<Stats>();
    private Stats stats;
   public void Die()
    {
        foreach (var particle in deathParticles)
        {
            ParticleManager.StartParticles(particle);
        }

        if (isCoffer) { 
            Instantiate(healthPack, transform.position, Quaternion.identity); 
        }
        
        core.transform.parent.gameObject.SetActive(false);

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
