using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundManager : SingletonComponent<SoundManager>
{
    [SerializeField] private List<SoundInfo> soundInfos = null;
    [System.Serializable]
    private class SoundInfo
    {
        public string id = "";
        public AudioClip audioClip = null;
        public AudioSource SoundAudioSource;
        public bool IsIndividual;
        [Range(0, 1)] public float clipVolume = 1;
    }
    AudioSource loopAudio;

    public void Play(string Id)
    {
        SoundInfo soundinfo = GetSoundInfo(Id);
        if (soundinfo.IsIndividual)
        {
            AudioSource a = gameObject.AddComponent<AudioSource>();
            a.clip = soundinfo.audioClip;
            a.volume = soundinfo.clipVolume;
            a.Play();
            return;
        }
        soundinfo.SoundAudioSource.clip = soundinfo.audioClip;
        soundinfo.SoundAudioSource.volume = soundinfo.clipVolume;
        soundinfo.SoundAudioSource.Play();
        if (soundinfo.SoundAudioSource.loop)
        {
            loopAudio = soundinfo.SoundAudioSource;
        }
    }

    public void LoopStop()
    {
        if (loopAudio != null)
            loopAudio.Stop();
    }
    private SoundInfo GetSoundInfo(string id)
    {

        for (int i = 0; i < soundInfos.Count; i++)
        {
            if (id == soundInfos[i].id)
            {
                return soundInfos[i];
            }
        }
        return null;
    }


}
