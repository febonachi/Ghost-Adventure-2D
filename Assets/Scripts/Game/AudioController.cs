using System.Linq;
using UnityEngine;

[System.Serializable]
public class Sound {
    public string tag;
    public AudioClip clip;

    [Range(0, 1)]
    public float volume = 1f;
    public bool loop = false;

    [HideInInspector]
    public AudioSource Source;
}

public class AudioController : MonoBehaviour {
    public static AudioController instance;

    #region editor
    [SerializeField] private Sound[] sounds;
    #endregion

    #region private
    private void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }

        foreach (Sound s in sounds) {
            GameObject o = new GameObject(s.tag);
            o.transform.SetParent(transform);
            s.Source = o.AddComponent<AudioSource>();
            s.Source.clip = s.clip;
            s.Source.loop = s.loop;
            s.Source.volume = s.volume;
        }
    }

    private void Start() {

    }
    #endregion

    #region public
    public void Play(string tag) {
        Sound sound = sounds.First(s => s.tag == tag);
        if (sound != null) sound.Source.Play();
    }

    public void Stop(string tag) {
        Sound sound = sounds.First(s => s.tag == tag);
        if (sound != null) sound.Source.Stop();
    }
    #endregion
}
