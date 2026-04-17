using System.Collections;
using UnityEngine;

public class AudioManager : PersistentSingleton<AudioController>
{
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip jumpSFX;
    [SerializeField] private AudioClip deathSFX;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private float backgroundMusicDelay = 2f;

    private Coroutine _playBackgroundMusicCo;

    private void Start()
    {
        if (_playBackgroundMusicCo != null)
        {
            StopCoroutine(_playBackgroundMusicCo);
            _playBackgroundMusicCo = null;
        }
        _playBackgroundMusicCo = StartCoroutine(PlayMusicAfterDelay());
    }

    public void PlayJumpSFX()
    {
        sfxSource.clip = jumpSFX;
        sfxSource.Play();
    }

    public void PlayDeathSFX()
    {
        sfxSource.clip = deathSFX;
        sfxSource.Play();
    }

    public void PlayBackgroundMusic()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    private IEnumerator PlayMusicAfterDelay()
    {
        yield return new WaitForSeconds(backgroundMusicDelay);
        Debug.Log("Background music has started...");
        PlayBackgroundMusic();
    }
}
