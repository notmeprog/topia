using UnityEngine;

public class LightningManager : MonoBehaviour
{
    public ParticleSystem[] lightningParticleSystems; // Массив систем частиц молнии
    public AudioSource[] lightningSounds; // Массив источников звука молнии

    public float minTimeBetweenLightning = 5f; // Минимальное время между молниями
    public float maxTimeBetweenLightning = 10f; // Максимальное время между молниями

    private void Start()
    {
        // Запускаем первую молнию
        Invoke("PlayRandomLightning", Random.Range(minTimeBetweenLightning, maxTimeBetweenLightning));
    }

    private void PlayRandomLightning()
    {
        // Отключаем все системы частиц и звуки молнии
        foreach (var ps in lightningParticleSystems)
        {
            ps.gameObject.SetActive(false);
        }

        foreach (var audio in lightningSounds)
        {
            audio.gameObject.SetActive(false);
        }

        // Выбираем случайную систему частиц и звук молнии
        int randomIndex = Random.Range(0, lightningParticleSystems.Length);

        // Запускаем систему частиц молнии
        lightningParticleSystems[randomIndex].gameObject.SetActive(true);
        //lightningParticleSystems[randomIndex].Play();

        // Запускаем звук молнии
        lightningSounds[randomIndex].gameObject.SetActive(true);
        //lightningSounds[randomIndex].Play();

        // Запускаем таймер для следующей молнии
        Invoke("PlayRandomLightning", Random.Range(minTimeBetweenLightning, maxTimeBetweenLightning));
    }
}
