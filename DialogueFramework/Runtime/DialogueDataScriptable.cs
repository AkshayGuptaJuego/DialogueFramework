using System.Collections.Generic;
using UnityEngine;

namespace DialogueFramework 
{
    [CreateAssetMenu(fileName = "Scriptable/DialogueDataScriptable")]
    public class DialogueDataScriptable : ScriptableObject
    {
        [Header("           Story Dialogue Data")]
        [Space(20)]
        public List<ChapterWiseDialogueData> numberOfChapters;
    }

    [System.Serializable]
    public class ChapterWiseDialogueData
    {
        // public int chapterNumber;
        [Header("           Line-Wise Dialogue Data")]
        [Space(20)]
        public List<LineWiseDialogueData> numberOfLines;
    }

    [System.Serializable]
    public class LineWiseDialogueData
    {
        [Space(10)]
        // public int lineNumber;
        [Tooltip("Use if showing Narrator Name")]
        public string narratorName;
        [Space(10)]

        [TextArea()]
        public string narrationTextString;
        [Space(10)]
        
        public AudioClip narrationAudioClip;
        [Space(10)]

        [Header("           Word-Wise Dialogue Data")]
        [Space(20)]
        [Tooltip("Required for Word wise Narration effects")]
        public List<wordWiseDialogueData> numberOfWords;
    }

    [System.Serializable]
    public class wordWiseDialogueData
    {
        public float startTime;
        public float stopTime;
    }
}

