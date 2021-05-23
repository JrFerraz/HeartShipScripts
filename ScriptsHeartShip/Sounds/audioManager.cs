using UnityEngine.Audio;
using UnityEngine;
using System;

public class audioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static audioManager instance;
    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        //if (instance == null)
        //{
        //    instance = this;
        //}
        //else
        //{
        //    Destroy(gameObject);
        //    return;
        //}
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.vol;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    private void Start()
    {

    }
    public void Play(string name)
    {
        Sound s =  Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Pause();
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }

    public void SetPitch(string name, float numPitch)
    {
        float naturalPitch = 0;
        Sound s = Array.Find(sounds, sound => sound.name == name);
        naturalPitch = s.source.pitch;
        s.source.pitch = numPitch;
    }
    private void agregarSonido(Sound unSonido)
    {
        unSonido.source = gameObject.AddComponent<AudioSource>();
        unSonido.source.clip = unSonido.clip;
        unSonido.source.volume = unSonido.vol;
        unSonido.source.pitch = unSonido.pitch;
        unSonido.source.loop = unSonido.loop;
    }
    public void SetPitchPro(float numPitch, float vol, Sound unSonido)
    {
        agregarSonido(unSonido);
        unSonido.source.pitch = numPitch;
        unSonido.source.volume = vol;
        unSonido.source.Play();
    }
    public void allMuted()
    {
        foreach (Sound s in sounds)
        {
            s.source.volume = 0f;
        }
    }
    public void allVolModified(float x)
    {
        foreach (Sound s in sounds)
        {
            s.source.volume = x;
        }
    }
    public void allSounding()
    {
        foreach (Sound s in sounds)
        {
            s.source.volume = 1f;
        }
    }
    public void slowSounding()
    {
        foreach (Sound s in sounds)
        {
            s.source.pitch *= 0.7f;
        }
    }
    public void normalSounding()
    {
        foreach (Sound s in sounds)
        {
            s.source.pitch /= 0.7f;
        }
    }
    public void allPaused()
    {
        foreach (Sound s in sounds)
        {
            s.source.Pause();
        }
    }
    public void allPlaying()
    {
        foreach (Sound s in sounds)
        {
            s.source.Play();
        }
    }
    public float getVolume(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        return s.source.volume;
    }
}
