using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioManager : BaseManager<AudioManager>
{
    AudioSource bgm = null;
    float bgmVolume = 1;

    GameObject sound = null;
    List<AudioSource> soundList = new List<AudioSource>();

    float soundVolume = 1;

    public AudioManager()
    {
        MonoManager.Instance.AddUpdateEvent(Update);
    }

    public void Update()
    {
        for (int i = soundList.Count-1; i > 0; i--)
        {
            if (!soundList[i].isPlaying)
            {
                GameObject.Destroy(soundList[i]);
                soundList.RemoveAt(i);
            }
        }
    }

    public void PlayBGM(string name)
    {
        //如果场上没有BGM管理器，则创建一个；
        if (bgm == null)
        {
            GameObject obj = new GameObject("BGM");
            bgm = obj.AddComponent<AudioSource>();
        }
        //异步加载背景音乐，然后播放；
        ResourcesManager.Instance.LoadAsyn<AudioClip>($"Music/BGM/{name}", (clip) =>
        {
            bgm.clip = clip;
            bgm.loop = true;
            bgm.volume = bgmVolume;
            bgm.Play();
        });
    }

    public void PauseBGM()
    {
        if (bgm == null)
            return;
        bgm.Pause();
    }

    public void StopBGM()
    {
        if (bgm == null)
            return;
        
        bgm.Stop();
    }

    public void ChangeBGMVolume(float volume)
    {
        bgmVolume = volume;
        if (bgm == null)
            return;
        bgm.volume = bgmVolume;
    }

    public void PlaySound(string name,bool isLoop,UnityAction<AudioSource> callback = null)
    {
        if (sound == null)
            sound = new GameObject("Sound");

        ResourcesManager.Instance.LoadAsyn<AudioClip>($"Music/Sound/{name}", (clip) =>
        {
            AudioSource source = sound.AddComponent<AudioSource>();
            source.clip = clip;
            source.loop = isLoop;
            source.volume = soundVolume;
            source.Play();
            soundList.Add(source);
            if (callback != null)
                callback(source);
        });
    }

    public void StopSound(AudioSource source)
    {
        if (soundList.Contains(source))
        {
            soundList.Remove(source);
            source.Stop();
            GameObject.Destroy(source);
        }
    }

    public void ChangeSoundVolume(float volume)
    {
        soundVolume = volume;
        for (int i = 0; i < soundList.Count; i++)
            soundList[i].volume = soundVolume;
    }
}
