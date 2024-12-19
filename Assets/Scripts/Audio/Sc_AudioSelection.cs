using System.Collections.Generic;
using UnityEngine;

public class Sc_AudioSelection : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _clip = new List<AudioClip>();
    public AudioSource _audioSource;
    public void PlaySound(int _sfxID)
    {
        _audioSource.clip = _clip[_sfxID];
        _audioSource.Play();
    }
}
