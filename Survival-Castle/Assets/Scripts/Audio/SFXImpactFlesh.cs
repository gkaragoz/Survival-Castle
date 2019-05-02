using UnityEngine;

public class SFXImpactFlesh : MonoBehaviour {

    [SerializeField]
    private Sound[] _impactFlesh;

    private AudioSource _audioSource;

    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
    }

    private AudioClip GetRandomClip() {
        Sound sound = _impactFlesh[Random.Range(0, _impactFlesh.Length)];

        _audioSource.volume = sound.volume * (1f + UnityEngine.Random.Range(-sound.volumeVariance / 2f, sound.volumeVariance / 2f));
        _audioSource.pitch = sound.pitch * (1f + UnityEngine.Random.Range(-sound.pitchVariance / 2f, sound.pitchVariance / 2f));

        return sound.clip;
    }

    public void Play() {
        AudioClip clip = GetRandomClip();
        _audioSource.PlayOneShot(clip);
    }

}
