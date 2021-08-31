using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource source;

    [SerializeField]
    private AudioClip[] _voiceOverInstall;

    [SerializeField]
    private AudioClip[] _voiceOverRemove;

    public void PlayVoiceOver(ACTION_TYPE type, int index)
    {
        if (type == ACTION_TYPE.ASSEMBLY)
            source.clip = _voiceOverInstall[index];
        else
            source.clip = _voiceOverRemove[index];

        source.Play();
    }
}
