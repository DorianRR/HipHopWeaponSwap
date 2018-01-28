using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Audio Controller
/// To use:
///     Create an "AudioController" object in the scene and attach this script to it.
///     Drag audio clips to corresponding public AudioClips.
///     Add the code "public Sound soundScript;" into each PlayerController script (Drag the "AudioController" object into it in the Inspector).
///     Add an AudioSource component to the Player.
///     Access these methods "soundScript.FunctionName"
///     
///     Add two AudioSources to the "AudioController" object. Have one play the "HipHopSwap" song and the other play the "glassbreakambience".
///         They both need to loop and play on Awake
///         
///     Might need "soundScript = GameObject.Find("AudioController").GetComponent<Sound>();" in Start function.
///     
///     Need a "EnemySounds" empty game object with a AudioSourced attached. Put this in the scene somewhere.
///         Use for all enemy sounds. Drag it in th eSource Enemy public var.
/// </summary>
public class Sound : MonoBehaviour
{

    public AudioSource sourcePlayer1;
    public AudioSource sourcePlayer2;

    public AudioClip shootSoundLazer;
    public float shootVolumeLazer = 0.5f;
    public AudioClip shootSoundShotgun;
    public float shootVolumeShotgun = 0.5f;
    public AudioClip shootSoundEnemy;
    public float shootVolumeEnemy = 0.5f;

    public AudioClip throwGunOrBoozeSound;
    public float throwGunOrBoozeVolume = 0.5f;
    public AudioClip catchGunSound;
    public float catchGunVolume = 0.5f;
    public AudioClip catchBoozeSound;
    public float catchBoozeVolume = 0.5f;

    public AudioClip playerDeathSound;
    public float playerDeathVolume = 0.5f;
    public AudioClip enemyDeathSound;
    public float enemyDeathVolume = 0.5f;

    public AudioClip enemySoftHitSound;
    public float enemySoftHitVolume = 0.5f;
    public AudioClip enemyHardHitSound;
    public float enemyHardHitVolume = 0.5f;

    public AudioClip playerHitSound;
    public float playerHitVolume = 0.5f;

    public AudioClip drinkBoozeSound;
    public float drinkBoozeVolume = 0.5f;



    public void PlayLazerSoundP1()
    {
        float[] choices = new float[] { 0.5f, 1f };
        int numChoices = choices.Length;
        int randomIndex = Random.Range(0, numChoices);
        float result = choices[randomIndex];

        sourcePlayer1.pitch = result;
        sourcePlayer1.PlayOneShot(shootSoundLazer, shootVolumeLazer);
    }
    public void PlayLazerSoundP2()
    {
        float[] choices = new float[] { 0.5f, 1f };
        int numChoices = choices.Length;
        int randomIndex = Random.Range(0, numChoices);
        float result = choices[randomIndex];

        sourcePlayer2.pitch = result;
        sourcePlayer2.PlayOneShot(shootSoundLazer, shootVolumeLazer);
    }

    public void PlayShootSound2P1()
    {
        float[] choices = new float[] { 0.5f, 1f };
        int numChoices = choices.Length;
        int randomIndex = Random.Range(0, numChoices);
        float result = choices[randomIndex];

        sourcePlayer1.pitch = result;
        sourcePlayer1.PlayOneShot(shootSoundShotgun, shootVolumeShotgun);
    }
    public void PlayShootSound2P2()
    {
        float[] choices = new float[] { 0.5f, 1f };
        int numChoices = choices.Length;
        int randomIndex = Random.Range(0, numChoices);
        float result = choices[randomIndex];

        sourcePlayer2.pitch = result;
        sourcePlayer2.PlayOneShot(shootSoundShotgun, shootVolumeShotgun);
    }

    public void PlayEnemyShootSound()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(shootSoundEnemy, shootVolumeEnemy);
    }

    public void PlayThrowGunOrBoozeSoundP1()
    {
        sourcePlayer1.pitch = 1f;
        sourcePlayer1.PlayOneShot(throwGunOrBoozeSound, throwGunOrBoozeVolume);
    }
    public void PlayGunOrBoozeSoundP2()
    {
        sourcePlayer2.pitch = 1f;
        sourcePlayer2.PlayOneShot(throwGunOrBoozeSound, throwGunOrBoozeVolume);
    }

    public void PlayCatchGunSoundP1()
    {
        sourcePlayer1.pitch = 1f;
        sourcePlayer1.PlayOneShot(catchGunSound, catchGunVolume);
    }
    public void PlayCatchGunSoundP2()
    {
        sourcePlayer2.pitch = 1f;
        sourcePlayer2.PlayOneShot(catchGunSound, catchGunVolume);
    }

    public void PlayCatchBoozeSoundP1()
    {
        sourcePlayer1.pitch = 1f;
        sourcePlayer1.PlayOneShot(catchBoozeSound, catchBoozeVolume);
    }
    public void PlayCatchBoozeSoundP2()
    {
        sourcePlayer2.pitch = 1f;
        sourcePlayer2.PlayOneShot(catchBoozeSound, catchBoozeVolume);
    }

    public void PlayPlayerDeathSoundP1()
    {
        sourcePlayer1.pitch = 1f;
        sourcePlayer1.PlayOneShot(playerDeathSound, playerDeathVolume);
    }
    public void PlayPlayerDeathSoundP2()
    {
        sourcePlayer2.pitch = 1f;
        sourcePlayer2.PlayOneShot(playerDeathSound, playerDeathVolume);
    }

    public void PlayEnemyDeathSoundP1()
    {
        sourcePlayer1.pitch = 1f;
        sourcePlayer1.PlayOneShot(enemyDeathSound, enemyDeathVolume);
    }
    public void PlayEnemyDeathSoundP2()
    {
        sourcePlayer2.pitch = 1f;
        sourcePlayer2.PlayOneShot(enemyDeathSound, enemyDeathVolume);
    }

    public void PlayEnemySoftHitSoundP1()
    {
        sourcePlayer1.pitch = 1f;
        sourcePlayer1.PlayOneShot(enemySoftHitSound, enemySoftHitVolume);
    }
    public void PlayEnemySoftHitSoundP2()
    {
        sourcePlayer2.pitch = 1f;
        sourcePlayer2.PlayOneShot(enemySoftHitSound, enemySoftHitVolume);
    }

    public void PlayEnemyHardHitSoundP1()
    {
        sourcePlayer1.pitch = 1f;
        sourcePlayer1.PlayOneShot(enemyHardHitSound, enemyHardHitVolume);
    }
    public void PlayEnemyHardHitSoundP2()
    {
        sourcePlayer2.pitch = 1f;
        sourcePlayer2.PlayOneShot(enemyHardHitSound, enemyHardHitVolume);
    }

    public void PlayPlayerHitSoundP1()
    {
        sourcePlayer1.pitch = 1f;
        sourcePlayer1.PlayOneShot(playerHitSound, playerHitVolume);
    }
    public void PlayPlayerHitSoundP2()
    {
        sourcePlayer2.pitch = 1f;
        sourcePlayer2.PlayOneShot(playerHitSound, playerHitVolume);
    }

    public void PlayDrinkBoozeSoundP1()
    {
        sourcePlayer1.pitch = 1f;
        sourcePlayer1.PlayOneShot(drinkBoozeSound, drinkBoozeVolume);
    }
    public void PlayDrinkBoozeSoundP2()
    {
        sourcePlayer2.pitch = 1f;
        sourcePlayer2.PlayOneShot(drinkBoozeSound, drinkBoozeVolume);
    }
}