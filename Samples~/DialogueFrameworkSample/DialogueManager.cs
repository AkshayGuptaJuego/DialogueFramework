using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

using DialogueFramework;

public class DialogueManager : MonoBehaviour
{
    [Header("           Presets")]
    [Space(10)]
    public bool isDebug;
    [Space(10)]
    public DialogueDataScriptable dialogueData;

    [Space(10)]

    public Button setLine;
    public Button nextLine;
    public Button prevLine;
    public Button nextChapter;
    public Button playNarration;
    public Button stopNarration;
    public Button skipNarration;
    public Button replayNarration;

    [Space(10)]
    public TMP_Text narratorName;
    public TMP_Text narrationText;



    public enum NarrationType {None, 
            TypeWriterIntervalWiseCharacterNarration, 
            TypeWriterIntervalWiseWordNarration, 
            TypeWriterWordWiseCharacterNarration, 
            TypeWriterWordWiseWordNarration, 
            HighlightIntervalWiseNarration, 
            HighlightWordWiseNarration}
            

    NarrationType narrationType;
    int chapterNumber;
    int lineNumber;
    float interval;
    float size;
    Color highlightColor;

    #region Editor
    #if(UNITY_EDITOR)
    [CustomEditor(typeof(DialogueManager))]
    public class DialogueManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            DialogueManager manager = (DialogueManager)target;

            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Ch no.",GUILayout.MaxWidth(50));
            manager.chapterNumber = EditorGUILayout.IntField(manager.chapterNumber);
            EditorGUILayout.LabelField("Line no.",GUILayout.MaxWidth(50));
            manager.lineNumber = EditorGUILayout.IntField(manager.lineNumber);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            manager.narrationType = (NarrationType)EditorGUILayout.EnumPopup("Narration Type ", manager.narrationType);
            
            switch (manager.narrationType)
            {
            case NarrationType.TypeWriterIntervalWiseCharacterNarration:
            case NarrationType.TypeWriterIntervalWiseWordNarration:
                TypeWriterIntervalWiseNarration(manager);
                break;
            case NarrationType.HighlightIntervalWiseNarration:
                HighlightIntervalWiseNarration(manager);
                break;
            case NarrationType.HighlightWordWiseNarration:
                HighlightWordWiseNarration(manager);
                break;
            }
        }
        void TypeWriterIntervalWiseNarration(DialogueManager manager)
        {
            EditorGUILayout.Space();
            manager.interval = EditorGUILayout.FloatField("Interval", manager.interval);
        }
        void HighlightIntervalWiseNarration(DialogueManager manager)
        {
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Interval", GUILayout.MaxWidth(50));
            manager.interval = EditorGUILayout.FloatField(manager.interval);
            EditorGUILayout.LabelField("Size", GUILayout.MaxWidth(50));
            manager.size = EditorGUILayout.FloatField(manager.size);
            EditorGUILayout.LabelField("Color", GUILayout.MaxWidth(50));
            manager.highlightColor = EditorGUILayout.ColorField(manager.highlightColor);
            EditorGUILayout.EndHorizontal();
        }
        void HighlightWordWiseNarration(DialogueManager manager)
        {
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Size", GUILayout.MaxWidth(50));
            manager.size = (float)EditorGUILayout.FloatField(manager.size);
            EditorGUILayout.LabelField("Color", GUILayout.MaxWidth(50));
            manager.highlightColor = (Color)EditorGUILayout.ColorField(manager.highlightColor);
            EditorGUILayout.EndHorizontal();
        }
    }
    #endif
    #endregion
    
    void OnEnable() 
    {
        setLine.onClick.AddListener(()=>{DialogueCallbacks.SetLine(this);});
        nextLine.onClick.AddListener(()=>{DialogueCallbacks.NextLine(this);});
        prevLine.onClick.AddListener(()=>{DialogueCallbacks.PrevLine(this);});
        nextChapter.onClick.AddListener(()=>{DialogueCallbacks.NextChapter(this);});
        playNarration.onClick.AddListener(()=>{DialogueCallbacks.PlayNarration(this);});
        stopNarration.onClick.AddListener(()=>{DialogueCallbacks.PauseNarration(this);});
        skipNarration.onClick.AddListener(()=>{DialogueCallbacks.SetLine(this);});
        replayNarration.onClick.AddListener(()=>{DialogueCallbacks.ReplayNarration(this);});



        DialogueValues.dialogueData=dialogueData;

        DialogueValues.narrationText = narrationText;

        DialogueValues.currentChapterNumber = chapterNumber;
        DialogueValues.currentLineNumber = lineNumber;

        DialogueValues.isDebugLog = isDebug;



        switch (narrationType)
        {
            case NarrationType.TypeWriterIntervalWiseCharacterNarration:
                DialogueValues.interval = interval;
                DialogueValues.currentEffect = DialogueTypeWriterEffect.IntervalWiseCharacterNarration();
                break;
            case NarrationType.TypeWriterIntervalWiseWordNarration:
                DialogueValues.interval = interval;
                DialogueValues.currentEffect = DialogueTypeWriterEffect.IntervalWiseWordNarration();
                break;
            case NarrationType.TypeWriterWordWiseCharacterNarration:
                DialogueValues.currentEffect = DialogueTypeWriterEffect.WordWiseCharacterNarration();
                break;
            case NarrationType.TypeWriterWordWiseWordNarration:
                DialogueValues.currentEffect = DialogueTypeWriterEffect.WordWiseWordNarration();
                break;
            case NarrationType.HighlightIntervalWiseNarration:
                DialogueValues.interval = interval;
                DialogueValues.highlightSize = size;
                DialogueValues.highlightcolor = highlightColor;
                DialogueValues.currentEffect = DialogueHighlightEffect.IntervalWiseNarration();
                break;
            case NarrationType.HighlightWordWiseNarration:
                DialogueValues.highlightSize = size;
                DialogueValues.highlightcolor = highlightColor;
                DialogueValues.currentEffect = DialogueHighlightEffect.WordWiseNarration();
                break;
        }

        DialogueCallbacks.SetValues(this);
    }
}
