using UnityEngine;

public class SFXArrowProjectile : MonoBehaviour {

    [SerializeField]
    private Projectile _projectile = null;
    [SerializeField]
    private Sound[] _arrowRelease = null;
    [SerializeField]
    private Sound _xBowArrowRelease = null;

    private AudioSource _audioSource;

    private void Awake() {
        _audioSource = GetComponent<AudioSource>();

        switch (_projectile.Owner) {
            case Projectile.OwnerEnum.Base:
                PlayArrowRelease();
                break;
            case Projectile.OwnerEnum.Enemy:
                PlayXBowArrowRelease();
                break;
        }
    }

    private void PlayArrowRelease() {
        AudioClip clip = GetRandomClip();
        _audioSource.PlayOneShot(clip);
    }

    private void PlayXBowArrowRelease() {
        _audioSource.volume = _xBowArrowRelease.volume * (1f + UnityEngine.Random.Range(-_xBowArrowRelease.volumeVariance / 2f, _xBowArrowRelease.volumeVariance / 2f));
        _audioSource.pitch = _xBowArrowRelease.pitch * (1f + UnityEngine.Random.Range(-_xBowArrowRelease.pitchVariance / 2f, _xBowArrowRelease.pitchVariance / 2f));

        _audioSource.PlayOneShot(_xBowArrowRelease.clip);
    }

    private AudioClip GetRandomClip() {
        Sound sound = _arrowRelease[Random.Range(0, _arrowRelease.Length)];

        _audioSource.volume = sound.volume * (1f + UnityEngine.Random.Range(-sound.volumeVariance / 2f, sound.volumeVariance / 2f));
        _audioSource.pitch = sound.pitch * (1f + UnityEngine.Random.Range(-sound.pitchVariance / 2f, sound.pitchVariance / 2f));

        return sound.clip;
    }

}
