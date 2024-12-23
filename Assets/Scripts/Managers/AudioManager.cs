using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    private AudioSource bgmSource;

    [SerializeField] public AudioClip menuMusic;
    [SerializeField] public AudioClip gameMusic;
    [SerializeField] private float fadeDuration = 1f;

    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private float jumpSoundVolume = 0.5f;

    [SerializeField] private AudioClip deathSound;
    [SerializeField] private float deathSoundVolume = 0.5f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        bgmSource = GetComponent<AudioSource>();
    }

    public void PlayMenuMusic()
    {
        bgmSource.clip = menuMusic;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void PlayGameMusic()
    {
        bgmSource.clip = gameMusic;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void SwitchMusic(AudioClip newClip)
    {
        StartCoroutine(FadeMusic(newClip));
    }

    private IEnumerator FadeMusic(AudioClip newClip)
    {
        float startVolume = bgmSource.volume;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            bgmSource.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
            yield return null;
        }

        bgmSource.volume = 0;
        bgmSource.clip = newClip;
        bgmSource.Play();

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            bgmSource.volume = Mathf.Lerp(0, startVolume, t / fadeDuration);
            yield return null;
        }

        bgmSource.volume = startVolume;
    }

    public bool IsMenuMusicPlaying()
    {
        return bgmSource.clip == menuMusic && bgmSource.isPlaying;
    }

    public void PlayJumpSound()
    {
        PlayClip(jumpSound, jumpSoundVolume);
    }

    public void PlayDeathSound()
    {
        PlayClip(deathSound, deathSoundVolume);
    }

    private void PlayClip(AudioClip clip, float volume)
    {
        bgmSource.PlayOneShot(clip, volume);
    }
}