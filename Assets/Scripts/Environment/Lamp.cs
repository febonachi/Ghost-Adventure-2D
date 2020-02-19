using UnityEngine;
using System.Linq;
using System.Collections;

public class Lamp : InteractiveEntity {

    #region editor
    [SerializeField] private int startEnergy;
    [SerializeField] private int energyCost;
    [SerializeField] private int reloadEnergy;
    [SerializeField] private float reloadTime;
    [SerializeField] private float speed = 1f;
    [SerializeField] private Color energyColor;
    [SerializeField] private ParticleSystem interactEffect;
    [SerializeField] private SpawnZone[] spawnZones;
    #endregion

    private int reloadCount;
    private Lantern lantern;

    #region private
    private IEnumerator MoveToLantern(LampEnergy one) {
        float distance = Vector2.Distance(one.transform.position, lantern.transform.position);
        while (distance > .1f) {
            if (distance <= 1f) distance = 2f;
            one.transform.position = Vector2.MoveTowards(one.transform.position, lantern.transform.position, Time.deltaTime * speed * distance);
            distance = Vector2.Distance(one.transform.position, lantern.transform.position);
            yield return null;
        }
        player.AddEnergy(energyCost);
        lantern.LanternColor = energyColor;
        player.lightningArea.LightBackgroundColor = energyColor;
        Destroy(one.gameObject);
    }

    private IEnumerator ReloadSpawnZone(SpawnZone spawnZone) {
        reloadCount--;
        yield return new WaitForSeconds(reloadTime);
        spawnZone.ReloadOne();
    }
    #endregion

    #region protected
    protected override void Initialize() {
        base.Initialize();
        
        lantern = player?.lantern;

        reloadCount = reloadEnergy / energyCost;

        int elements = startEnergy / spawnZones.Length;

        foreach (SpawnZone spawnZone in spawnZones) {
            spawnZone.Initialize(elements / energyCost, energyColor);
        }

        ParticleSystem.MainModule interactEffectMain = interactEffect.main;
        Color interactEffectColor = energyColor;
        interactEffectColor.a /= 2f;
        interactEffectMain.startColor = interactEffectColor;
    }

    protected override void Interact() {
        if (!player.CanTakeEnergy(energyCost)) return;
        SpawnZone[] notEmptySpawnZones = spawnZones.Where(e => e.gameObject.activeSelf && e.CanTakeOne()).ToArray();
        if (notEmptySpawnZones.Length > 0) {
            SpawnZone spawnZone = notEmptySpawnZones[Random.Range(0, notEmptySpawnZones.Length)];
            LampEnergy one = spawnZone.TakeOne();
            if (one) {
                interactEffect.Play();
                if (reloadCount > 0) StartCoroutine(ReloadSpawnZone(spawnZone));
                StartCoroutine(MoveToLantern(one));
            }
        }
    }
    #endregion
}
