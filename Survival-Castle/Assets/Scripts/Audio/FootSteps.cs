using UnityEngine;

public class FootSteps : MonoBehaviour {

    [SerializeField]
    private AudioClip[] _clips;

    private AudioSource _audioSource;

    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Step() {
        AudioClip clip = GetRandomClip();
        _audioSource.PlayOneShot(clip);
    }
    
    private AudioClip GetRandomClip() {
        return _clips[Random.Range(0, _clips.Length)];
    }

}
