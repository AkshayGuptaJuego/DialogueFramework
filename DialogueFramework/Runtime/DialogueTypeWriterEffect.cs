using System.Collections;
using UnityEngine;

namespace DialogueFramework
{
    /*
    SUMMARY
    Type writer effect starts from a blank line and adds strings accordingly
    */
    public class DialogueTypeWriterEffect
    {
        public static float interval;
        
        /*
        SUMMARY
        IntervalWiseCharacterNarration Adds characters to the Text every interval secounds
        */
        public static IEnumerator IntervalWiseCharacterNarration()
        {
            if(DialogueValues.isDebugLog) Debug.Log("Narration Started");

            DialogueValues.currentEffect = IntervalWiseCharacterNarration();

            interval = DialogueValues.interval;
            DialogueValues.currentWordNumber = 0;

            string[] words = DialogueValues.currentLineText.Split(' ');
            DialogueValues.narrationText.text = string.Empty;
            for (int i = 0; i < DialogueValues.currentLineText.Length; i++)
            {
                DialogueValues.narrationText.text += DialogueValues.currentLineText[i];
                yield return new WaitForSeconds(interval);
            }
        }

        /*
        SUMMARY
        IntervalWiseWordNarration Adds words to the Text every interval secounds
        */
        public static IEnumerator IntervalWiseWordNarration()
        {
            if(DialogueValues.isDebugLog) Debug.Log("Narration Started");

            DialogueValues.currentEffect = IntervalWiseWordNarration();

            interval = DialogueValues.interval;
            DialogueValues.currentWordNumber = 0;

            string[] words = DialogueValues.currentLineText.Split(' ');
            DialogueValues.narrationText.text = string.Empty;
            for (int i = 0; i < words.Length; i++)
            {
                DialogueValues.narrationText.text += words[i];
                yield return new WaitForSeconds(interval);
                DialogueValues.narrationText.text += ' ';
                DialogueValues.currentWordNumber++;
            }
        }

        /*
        SUMMARY
        WordWiseCharacterNarration Adds characters to the Text every Words interval in scriptable data secounds
        */
        public static IEnumerator WordWiseCharacterNarration()
        {
            if(DialogueValues.isDebugLog) Debug.Log("Narration Started");

            DialogueValues.currentEffect = WordWiseCharacterNarration();

            DialogueValues.currentWordNumber = 0;

            string[] words = DialogueValues.currentLineText.Split(' ');

            if ( words.Length != DialogueValues.dialogueData.numberOfChapters[DialogueValues.currentChapterNumber].numberOfLines[DialogueValues.currentLineNumber].numberOfWords.Count )
            {
                Debug.Log("number of words dont match");
                yield break;
            }

            DialogueValues.narrationText.text = string.Empty;
            for (int i = 0; i < words.Length; i++)
            {
                interval = DialogueValues.dialogueData.numberOfChapters[DialogueValues.currentChapterNumber].numberOfLines[DialogueValues.currentLineNumber].numberOfWords[DialogueValues.currentWordNumber].stopTime - DialogueValues.dialogueData.numberOfChapters[DialogueValues.currentChapterNumber].numberOfLines[DialogueValues.currentLineNumber].numberOfWords[DialogueValues.currentWordNumber].startTime;
                for(int j = 0; j < words[i].Length; j++)
                {
                    DialogueValues.narrationText.text += words[i][j];
                    yield return new WaitForSeconds(interval/words[i].Length);
                }
                if(DialogueValues.dialogueData.numberOfChapters[DialogueValues.currentChapterNumber].numberOfLines[DialogueValues.currentLineNumber].numberOfWords.Count - 1 != DialogueValues.currentWordNumber)
                {
                    interval = DialogueValues.dialogueData.numberOfChapters[DialogueValues.currentChapterNumber].numberOfLines[DialogueValues.currentLineNumber].numberOfWords[DialogueValues.currentWordNumber].stopTime - DialogueValues.dialogueData.numberOfChapters[DialogueValues.currentChapterNumber].numberOfLines[DialogueValues.currentLineNumber].numberOfWords[DialogueValues.currentWordNumber + 1].startTime;
                    if(interval>0)
                        yield return new WaitForSeconds(interval);
                }
                DialogueValues.narrationText.text += ' ';
                DialogueValues.currentWordNumber++;
            }
        }

        /*
        SUMMARY
        WordWiseWordNarration Adds words to the Text every Words interval in scriptable data secounds
        */
        public static IEnumerator WordWiseWordNarration()
        {
            if(DialogueValues.isDebugLog) Debug.Log("Narration Started");
            
            DialogueValues.currentEffect = WordWiseWordNarration();

            DialogueValues.currentWordNumber = 0;

            string[] words = DialogueValues.currentLineText.Split(' ');
            
            if ( words.Length != DialogueValues.dialogueData.numberOfChapters[DialogueValues.currentChapterNumber].numberOfLines[DialogueValues.currentLineNumber].numberOfWords.Count )
            {
                Debug.Log("number of words dont match");
                yield break;
            }

            DialogueValues.narrationText.text = string.Empty;
            for (int i = 0; i < words.Length; i++)
            {
                interval = DialogueValues.dialogueData.numberOfChapters[DialogueValues.currentChapterNumber].numberOfLines[DialogueValues.currentLineNumber].numberOfWords[DialogueValues.currentWordNumber].stopTime - DialogueValues.dialogueData.numberOfChapters[DialogueValues.currentChapterNumber].numberOfLines[DialogueValues.currentLineNumber].numberOfWords[DialogueValues.currentWordNumber].startTime;
                for(int j = 0; j < words[i].Length; j++)
                {
                    DialogueValues.narrationText.text += words[i];
                    yield return new WaitForSeconds(interval);
                }
                if(DialogueValues.dialogueData.numberOfChapters[DialogueValues.currentChapterNumber].numberOfLines[DialogueValues.currentLineNumber].numberOfWords.Count - 1 != DialogueValues.currentWordNumber)
                {
                    interval = DialogueValues.dialogueData.numberOfChapters[DialogueValues.currentChapterNumber].numberOfLines[DialogueValues.currentLineNumber].numberOfWords[DialogueValues.currentWordNumber].stopTime - DialogueValues.dialogueData.numberOfChapters[DialogueValues.currentChapterNumber].numberOfLines[DialogueValues.currentLineNumber].numberOfWords[DialogueValues.currentWordNumber + 1].startTime;
                    if(interval>0)
                        yield return new WaitForSeconds(interval);
                }
                DialogueValues.narrationText.text += ' ';
                DialogueValues.currentWordNumber++;
            }
        }
    }
}
