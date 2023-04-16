using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;
using static UnityEngine.Debug;

public class Girlfriend : MonoBehaviour {

    //todo fix highlights not working X
    //todo clip audio X
    //todo level audio gain X
    //todo get buttons workings X
    //todo initialization of answer X
    //todo handle strike X
    //todo handle pass X
    //todo logging X
    //todo manual X
    //todo twitch plays
    //todo twitch auto solves


   public KMBombInfo Bomb;
   public KMAudio Audio;

    public AudioClip English;
    public AudioClip French;
    public AudioClip German;
    public AudioClip Italian;
    public AudioClip Japanese;
    public AudioClip Mandarin;
    public AudioClip Portuguese;
    public AudioClip Spanish;


    public KMSelectable PlayButton;
    public KMSelectable DisplayButton;
    public KMSelectable LeftButton;
    public KMSelectable RightButton;

    public TextMesh DisplayText;


    private string[] languages;
    private AudioClip[] clips;

    private int answerIndex;
    private int currentIndex;
    private bool soundPlaying;


    static int ModuleIdCounter = 1;
   int ModuleId;
   private bool ModuleSolved;

    private void SetUpModule()
    {
        soundPlaying = false;

        clips = new AudioClip[]
        {
            English,
            French,
            German,
            Italian,
            Japanese,
            Mandarin,
            Portuguese,
            Spanish
        };

        languages = new string[]
        {
            "English",
            "French",
            "German",
            "Italian",
            "Japanese",
            "Mandarin",
            "Portuguese",
            "Spanish"
        };

        answerIndex = Rnd.Range(0, clips.Length);

        Log($"Answer Index: {answerIndex}");

        currentIndex = Rnd.Range(0, languages.Length);

        DisplayText.text = languages[currentIndex];

        LogFormat($"[Girlfriend #{ModuleId}] Language is {languages[answerIndex]}");

        LeftButton.OnInteract += delegate () { LeftButtonPressed(); return false; };
        RightButton.OnInteract += delegate () { RightButtonPressed(); return false; };

        PlayButton.OnInteract += delegate () { PlayButtonPressed(); return false; };
        DisplayButton.OnInteract += delegate () { DisplayButtonPressed(); return false; };
    }

    void Awake () {
        ModuleId = ModuleIdCounter++;
        SetUpModule();
   }

    private void PlayButtonPressed()
    {
        RightButton.AddInteractionPunch(0.1f);

        if (ModuleSolved || soundPlaying)
            return;

        soundPlaying = true;

        StartCoroutine(PlaySounds());
    }

    private void DisplayButtonPressed()
    {
        RightButton.AddInteractionPunch(0.1f);
        LogFormat($"[Girlfriend #{ModuleId}] Submitted {languages[currentIndex]}");

        Log($"Subbmited index {currentIndex}");
        if (currentIndex != answerIndex)
        {
            GetComponent<KMBombModule>().HandleStrike();
        }

        else
        {
            DisplayText.text = "";
            GetComponent<KMBombModule>().HandlePass();
        }
    }



    private void LeftButtonPressed()
    {
        LeftButton.AddInteractionPunch(0.1f);

        currentIndex--;


        if (currentIndex < 0)
        {
            currentIndex = languages.Length - 1;
        }

        Log($"Current index: {currentIndex}");

        DisplayText.text = languages[currentIndex];
    }

    private void RightButtonPressed()
    {
        RightButton.AddInteractionPunch(0.1f);

        currentIndex = (currentIndex + 1) % languages.Length;

        Log($"Current index: {currentIndex}");

        DisplayText.text = languages[currentIndex];
    }

    IEnumerator PlaySounds()
    {
        Audio.PlaySoundAtTransform(clips[answerIndex].name, transform);
        yield return new WaitForSeconds(clips[answerIndex].length);
        soundPlaying = false;
    }

#pragma warning disable 414
    private readonly string TwitchHelpMessage = @"Use `!{0} play` to play the song. Use `!{0} [language]` to select the language. Use `!{0} submit` to submit.";
#pragma warning restore 414

   IEnumerator ProcessTwitchCommand (string Command) {

        Command = Command.ToUpper();
        yield return null;

        switch (Command)
        {
            case "PLAY":
                PlayButton.OnInteract();
                break;

            case "SUBMIT":
                DisplayButton.OnInteract();
                break;

            default:
                if (!languages.Any(w => w.ToUpper() == Command))
                {
                    yield return string.Format("sendtochaterror Invalid command");
                    yield break;
                }

                else
                {
                    while (DisplayText.text.ToUpper() != Command)
                    {
                        RightButton.OnInteract();
                    }
                    break;
                }
        }
   }

   IEnumerator TwitchHandleForcedSolve () {
      yield return null;
   }
}
