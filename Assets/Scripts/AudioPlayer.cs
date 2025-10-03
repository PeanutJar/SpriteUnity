using Unity.VisualScripting;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audiosource;
    public AudioClip mainaudiosound; //optional/backup/error audio (or whatever you want to use it for I guess)
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAudio()
    {
        audiosource.PlayOneShot(mainaudiosound, .7f);
    }
    public void PlayAudio(AudioClip audio, float volume)
    {
        AudioSource.PlayClipAtPoint(audio, transform.position, volume); //creates a temporary GameObject at a specified position, attaches an AudioSource to it, plays the given AudioClip, and automatically
        //destroys itself once the clip has finished (alternative to PlayOneShot, since this will continue/not be disrupted upon connected gameobject's destruction
        //however, this instead plays the sound in the world space at that position at that moment, so it will not move with the object as the sound plays
        //audiosource.PlayOneShot(audio, volume);
    }
}
