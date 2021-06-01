using UnityEngine;

// https://answers.unity.com/questions/1260393/make-music-continue-playing-through-scenes.html
public class AudioPlayer : MonoBehaviour
{

    [SerializeField]
    public AudioClip[] musics;

    private AudioSource _audioSource;
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        _audioSource = GetComponent<AudioSource>();
    }

    public void SetClip(string name)
    {
        StopMusic();
        foreach(AudioClip clip in musics)
        {
            if(clip.name == name)
            {
                Debug.Log("oui");
                _audioSource.clip = clip;
                PlayMusic();
            }
        }
    }

    private void PlayMusic()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }
}