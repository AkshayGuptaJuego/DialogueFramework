using System.Collections;
using UnityEngine;

namespace DialogueFramework
{
    /*
    SUMMARY
    DialogueHighlightEffect Has the whole string in text amd highlight each words induvidually
    */
    public class DialogueHighlightEffect
    {
        public static float interval;
        public static string color;
        public static float size;

        /*
        SUMMARY
        IntervalWiseNarration Highlights eachword for interval secounds
        */
        public static IEnumerator IntervalWiseNarration()
        {
            if(DialogueValues.isDebugLog) Debug.Log("Narration Started");
            
            DialogueValues.currentEffect = IntervalWiseNarration();

            interval = DialogueValues.interval;
            color = ToRGBHex(DialogueValues.highlightcolor);
            size = DialogueValues.highlightSize;

            DialogueValues.currentWordNumber = 0;

            string[] words = DialogueValues.currentLineText.Split(' ');
            while(DialogueValues.currentWordNumber < words.Length)
            {
                DialogueValues.narrationText.text = string.Empty;
                for (int i = 0; i < words.Length; i++)
                {
                    if (i == DialogueValues.currentWordNumber)
                        DialogueValues.narrationText.text += "<color=" + color + ">" + "<size=" + size.ToString() + ">" + words[i] + "</size></color> ";
                    else
                        DialogueValues.narrationText.text += words[i] + " ";
                }
                yield return new WaitForSeconds(interval);
                DialogueValues.currentWordNumber++;
            }
        }

        /*
        SUMMARY
        IntervalWiseNarration Highlights eachword for each word interval secounds
        */
        public static IEnumerator WordWiseNarration()
        {
            if(DialogueValues.isDebugLog) Debug.Log("Narration Started");
            
            DialogueValues.currentEffect = WordWiseNarration();

            color = ToRGBHex(DialogueValues.highlightcolor);
            size = DialogueValues.highlightSize;

            DialogueValues.currentWordNumber = 0;

            string[] words = DialogueValues.currentLineText.Split(' ','\n');

            if ( words.Length != DialogueValues.dialogueData.numberOfChapters[DialogueValues.currentChapterNumber].numberOfLines[DialogueValues.currentLineNumber].numberOfWords.Count )
            {
                Debug.Log("number of words dont match");
                yield break;
            }

            while(DialogueValues.currentWordNumber < words.Length)
            {
                interval = DialogueValues.dialogueData.numberOfChapters[DialogueValues.currentChapterNumber].numberOfLines[DialogueValues.currentLineNumber].numberOfWords[DialogueValues.currentWordNumber].stopTime - DialogueValues.dialogueData.numberOfChapters[DialogueValues.currentChapterNumber].numberOfLines[DialogueValues.currentLineNumber].numberOfWords[DialogueValues.currentWordNumber].startTime;
                DialogueValues.narrationText.text = string.Empty;
                for (int i = 0; i < words.Length; i++)
                {
                    if (i == DialogueValues.currentWordNumber)
                        DialogueValues.narrationText.text += "<color=" + color + ">" + "<size=" + size.ToString() + ">" + words[i] + "</size></color> ";
                    else
                        DialogueValues.narrationText.text += words[i] + " ";
                }
                yield return new WaitForSeconds(interval);
                DialogueValues.currentWordNumber++;
            }
        }


        private static string ToRGBHex(Color c)
        {
            return string.Format("#{0:X2}{1:X2}{2:X2}", ToByte(c.r), ToByte(c.g), ToByte(c.b));
        }
        private static byte ToByte(float f)
        {
            f = Mathf.Clamp01(f);
            return (byte)(f * 255);
        }
    }
}

