using UnityEngine;

public class SFXEarnGolds : MonoBehaviour {

    [SerializeField]
    private Sound _earnGolds = null;

    private AudioSource _audioSource;

    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Play() {
        _audioSource.volume = _earnGolds.volume * (1f + UnityEngine.Random.Range(-_earnGolds.volumeVariance / 2f, _earnGolds.volumeVariance / 2f));
        _audioSource.pitch = _earnGolds.pitch * (1f + UnityEngine.Random.Range(-_earnGolds.pitchVariance / 2f, _earnGolds.pitchVariance / 2f));

        _audioSource.PlayOneShot(_earnGolds.clip);
    }

}
