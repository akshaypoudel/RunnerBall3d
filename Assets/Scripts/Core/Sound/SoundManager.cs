using UnityEngine;

public static class SoundManager
{
    public static float volume;
    public enum Sound
    {
        DonutCollect,
        DiamondCollect,
        Jump,
        GameOver,
        AliveAgain
    }

    public static void PlaySound(Sound sound)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(sound), volume);
        Object.Destroy(soundGameObject, 2);
    }
    public static void Mute()
    {
        AudioListener.volume = 0;
    }
    public static void AudioResume()
    {
        AudioListener.volume = 1;
    }
    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.soundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        return null;
    }

}
