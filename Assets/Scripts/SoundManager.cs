using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public bool isMute = false;
    private string PREF_SOUND = "pref_ismute";

    public AudioSource musicMenu;
    public AudioSource mainMusic;
    public AudioSource airplaneSound;
    public AudioSource punchSound;
    public AudioSource victorySound;

    public void SetIsMute()
    {
        isMute = true;
        PlayerPrefs.SetInt(PREF_SOUND, 1);
    }

    public void SetUnMute()
    {
        isMute = false;
        PlayerPrefs.SetInt(PREF_SOUND, 0);
    }

    private void PlaySound(AudioSource audioToPlay)
    {
        if (!isMute && audioToPlay != null)
            audioToPlay.Play();
    }

    private void StopSound(AudioSource audioToStop)
    {
        if (audioToStop != null)
            audioToStop.Stop();
    }

    public void StopMenuMusic()
    {
        StopSound(musicMenu);
    }

    public void StopMainMusic()
    {
        StopSound(mainMusic);
    }

    public void PlayMainMusic()
    {
        PlaySound(mainMusic);
    }

    public void PlayAirplaneSound()
    {
        PlaySound(airplaneSound);
    }

    public void PlayPunchSound()
    {
        PlaySound(punchSound);
    }

    public void PlayVictorySound()
    {
        PlaySound(victorySound);
    }
}
