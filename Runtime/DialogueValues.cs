using System.Collections;
using UnityEngine;
using TMPro;

namespace DialogueFramework
{
    /*
    SUMMARY
    Has all the values needed to manuplate the text box
    */
    public class DialogueValues
    {
        public static DialogueDataScriptable dialogueData;

        public static TMP_Text narratorName;
        public static TMP_Text narrationText;

        public static int currentChapterNumber = 0;
        public static int currentLineNumber = 0;
        public static int currentWordNumber = 0;

        public static string currentLineText;
        public static AudioClip currentAudioClip;

        public static IEnumerator currentEffect;
        public static Coroutine runningEffect;

        public static float interval;
        public static Color highlightcolor;
        public static float highlightSize;
        public static bool isDebugLog;
    }
}
