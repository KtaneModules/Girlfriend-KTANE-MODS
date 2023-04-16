using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class Girlfriend : MonoBehaviour {

    //todo fix highlights not working
    //todo clip audio
    //todo level audio gain
    //todo get buttons workings
    //todo initialization of answer
    //todo get audio playing in unity
    //todo handle strike
    //todo handle pass
    //todo logging
    //todo manual
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

   static int ModuleIdCounter = 1;
   int ModuleId;
   private bool ModuleSolved;

   void Awake () {
      ModuleId = ModuleIdCounter++;
      /*
      foreach (KMSelectable object in keypad) {
          object.OnInteract += delegate () { keypadPress(object); return false; };
      }
      */

      //button.OnInteract += delegate () { buttonPress(); return false; };

   }

   void Start () {

   }

   void Update () {

   }

#pragma warning disable 414
   private readonly string TwitchHelpMessage = @"Use !{0} to do something.";
#pragma warning restore 414

   IEnumerator ProcessTwitchCommand (string Command) {
      yield return null;
   }

   IEnumerator TwitchHandleForcedSolve () {
      yield return null;
   }
}
