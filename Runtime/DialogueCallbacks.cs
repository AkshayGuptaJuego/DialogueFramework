using UnityEngine;

namespace DialogueFramework
{
    /*
    SUMMARY
    Callbacks has all the functions To Manuplate the text box content
    */

    public class DialogueCallbacks
    {
        /*
        SUMMARY
        SetValues Sets DialogueValues current line and clip
        */
        public static void SetValues(MonoBehaviour monoBehaviour)
        {
            DialogueValues.currentLineText = DialogueValues.dialogueData.
                                                numberOfChapters[DialogueValues.currentChapterNumber].
                                                numberOfLines[DialogueValues.currentLineNumber].
                                                narrationTextString;
            DialogueValues.currentAudioClip = DialogueValues.dialogueData.
                                                numberOfChapters[DialogueValues.currentChapterNumber].
                                                numberOfLines[DialogueValues.currentLineNumber].
                                                narrationAudioClip;
            
            if(DialogueValues.isDebugLog) Debug.Log("Values Set");
        }

        /*
        SUMMARY
        SetLine Set the Text line while stoping all effects
        */
        public static void SetLine(MonoBehaviour monoBehaviour)
        {
            PauseNarration(monoBehaviour);

            SetValues(monoBehaviour);

            DialogueValues.currentWordNumber = 0;
            DialogueValues.narrationText.text = DialogueValues.currentLineText;
            
            if(DialogueValues.isDebugLog) Debug.Log("Line Set");
        }

        /*
        SUMMARY
        Goes to next line if possible and sets the line
        */
        public static void NextLine(MonoBehaviour monoBehaviour)
        {
            if(DialogueValues.dialogueData.numberOfChapters[DialogueValues.currentChapterNumber].numberOfLines.Count - 1 == DialogueValues.currentLineNumber)
            {
                if(DialogueValues.isDebugLog) Debug.Log("Last Line of the Chapter reached");
                return;
            }

            if(DialogueValues.isDebugLog) Debug.Log("Next Line");

            DialogueValues.currentLineNumber++;
            SetLine(monoBehaviour);
        }

        /*
        SUMMARY
        Goes to Previous line if possible and sets the line
        */
        public static void PrevLine(MonoBehaviour monoBehaviour)
        {
            if(DialogueValues.currentLineNumber == 0)
            {
                if(DialogueValues.isDebugLog) Debug.Log("First Line of the Chapter reached");
                return;
            }

            if(DialogueValues.isDebugLog) Debug.Log("Previous Line");

            DialogueValues.currentLineNumber--;
            SetLine(monoBehaviour);
        }

        /*
        SUMMARY
        Goes to next Chapter if possible and sets the line
        */
        public static void NextChapter(MonoBehaviour monoBehaviour)
        {
            if(DialogueValues.dialogueData.numberOfChapters.Count - 1 == DialogueValues.currentChapterNumber)
            {
                if(DialogueValues.isDebugLog) Debug.Log("Last Chapter reached");
                return;
            }

            if(DialogueValues.isDebugLog) Debug.Log("Next Chapter");

            DialogueValues.currentLineNumber = 0;
            DialogueValues.currentChapterNumber++;

            SetLine(monoBehaviour);
        }

        
        /*
        SUMMARY
        Replays the Current narration effect
        */
        public static void ReplayNarration(MonoBehaviour monoBehaviour)
        {
            if(DialogueValues.isDebugLog) Debug.Log("Narration Replayed");

            PauseNarration(monoBehaviour);

            PlayNarration(monoBehaviour);
        }

        /*
        SUMMARY
        Stops the Current narration effect
        */
        public static void PauseNarration(MonoBehaviour monoBehaviour)
        {
            if(DialogueValues.isDebugLog) Debug.Log("Narration Paused");

            if(DialogueValues.runningEffect != null)    monoBehaviour.StopCoroutine(DialogueValues.runningEffect);
        }


        /*
        SUMMARY
        plays the Current narration effect
        */
        public static void PlayNarration(MonoBehaviour monoBehaviour)
        {
            if(DialogueValues.isDebugLog)
            {
                if(DialogueValues.currentEffect != null)    Debug.Log("Narration Played");
                else    Debug.Log("No narrated effect selected");
            }

            if(DialogueValues.currentEffect != null)    DialogueValues.runningEffect = monoBehaviour.StartCoroutine(DialogueValues.currentEffect);
        }
    }
}
