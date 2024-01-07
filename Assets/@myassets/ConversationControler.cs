using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class ConversationEvent : UnityEvent<int>
{
}



// So, have you found a reson to code yet, buddy?
// - Javier Gomez
public class ConversationControler : MonoBehaviour
{
    public ConversationEvent m_ConvEvent = new ConversationEvent();

    [SerializeField]
    string levelTextFile;
    [SerializeField]
    bool eventAutoStart;

    [SerializeField]
    GameObject SubtitleCanvas = null;
    TextMeshProUGUI NameText;
    TextMeshProUGUI Subtitle;

    [SerializeField]
    Material textMaterial;

    GameObject BGMLibrary;
    GameObject VoicelineLibrary;

    int iteratorBGM = 0;
    int maxBGM = 0;
    BackgroundMusicClass currentBgm;
    Dictionary<int, BackgroundMusicClass> BGMList;
    float bgmTimer;
    bool goToNext;
    int BGMCounter;
    Coroutine currentBGMCoroutine = null;

    float voicelineTimer;
    LinkedList<int> voicelineBuffer;
    Voiceline currentVoiceline = null;

    int maxVoiceline = 0;
    Dictionary<int, Voiceline> voicelineList;
    int maxConversation = 0;
    Dictionary<int, Conversation> converList;
    Conversation definingConversation;

    float bgmStartTime;

    [SerializeField]
    Color[] colorList;


    // Start is called before the first frame update
    void Start()
    {
        //Set up of subtitle Canvas
        if (SubtitleCanvas == null)
        {
            SubtitleCanvas = new GameObject();
            SubtitleCanvas.name = "SubtitleCanvas";
            SubtitleCanvas.AddComponent<RectTransform>();
            SubtitleCanvas.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            CanvasScaler canvScal = SubtitleCanvas.AddComponent<CanvasScaler>();
            canvScal.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvScal.referenceResolution = new Vector2(1920, 1080);
            canvScal.matchWidthOrHeight = 1;
            SubtitleCanvas.AddComponent<GraphicRaycaster>();
        }
        

        //Set up name text
        GameObject nameGM = new GameObject();
        nameGM.transform.parent = SubtitleCanvas.transform;
        nameGM.transform.localScale = new Vector3(1, 1, 1);
        nameGM.transform.localRotation = Quaternion.Euler(0, 0, 0);
        nameGM.layer = LayerMask.NameToLayer("UI");
        nameGM.name = "NameText";
        RectTransform NamRecTra = nameGM.AddComponent<RectTransform>();
        NamRecTra.anchorMin = new Vector2(0.5f, 1f);
        NamRecTra.anchorMax = new Vector2(0.5f, 1f);
        NamRecTra.pivot = new Vector2(0.5f, 1f);
        NamRecTra.sizeDelta = new Vector2(1920, 100);
        NamRecTra.anchoredPosition3D = new Vector3(0, 0, 0);
        nameGM.AddComponent<CanvasRenderer>();
        NameText = nameGM.AddComponent<TextMeshProUGUI>();
        NameText.fontMaterial = textMaterial;
        NameText.fontStyle = FontStyles.Bold;
        NameText.fontSize = 30;
        NameText.alignment = TextAlignmentOptions.Center;

        //Set up subtitle text
        GameObject subtitleGm = new GameObject();
        subtitleGm.transform.parent = SubtitleCanvas.transform;
        subtitleGm.transform.localScale = new Vector3(1, 1, 1);
        subtitleGm.name = "SubtitleText";
        subtitleGm.transform.localRotation = Quaternion.Euler(0, 0, 0);
        subtitleGm.layer = LayerMask.NameToLayer("UI");
        RectTransform SubRecTra = subtitleGm.AddComponent<RectTransform>();
        SubRecTra.anchorMin = new Vector2(0.5f, 1f);
        SubRecTra.anchorMax = new Vector2(0.5f, 1f);
        SubRecTra.pivot = new Vector2(0.5f, 1f);
        SubRecTra.sizeDelta = new Vector2(1920, 200);
        SubRecTra.anchoredPosition3D = new Vector3(0, -68, 0);
        subtitleGm.AddComponent<CanvasRenderer>();
        Subtitle = subtitleGm.AddComponent<TextMeshProUGUI>();
        Subtitle.fontMaterial = textMaterial;
        Subtitle.fontStyle = FontStyles.Bold;
        Subtitle.fontSize = 30;
        Subtitle.horizontalAlignment = HorizontalAlignmentOptions.Center;
        Subtitle.verticalAlignment = VerticalAlignmentOptions.Top;

        BGMLibrary = new GameObject();
        BGMLibrary.name = "BGMLibrary";
        BGMLibrary.transform.parent = GameObject.Find("Main Camera").transform;
        BGMLibrary.transform.localPosition = new Vector3(0, 0, 0);

        VoicelineLibrary = new GameObject();
        VoicelineLibrary.name = "VoicelineLibrary";
        VoicelineLibrary.transform.parent = GameObject.Find("Main Camera").transform;
        VoicelineLibrary.transform.localPosition = new Vector3(0, 0, 0);


        BGMList = new Dictionary<int, BackgroundMusicClass>();
        voicelineList = new Dictionary<int, Voiceline>();
        voicelineBuffer = new LinkedList<int>();
        converList = new Dictionary<int, Conversation>();

        TextAsset soundData = Resources.Load(levelTextFile) as TextAsset;
        string[] fLines = Regex.Split(soundData.text, "\r\n|\n|\r");

        bool isDefining = false;
        bool bgmDefining = false;
        bool bgmDefined = false;
        bool conversationDefining = false;

        for (int i = 0; i < fLines.Length; i++)
        {
            string cLine = fLines[i];
            // doble barra será comentarios y saltaremos la linea
            // Si la linea está vacia también la saltamos
            if (cLine.Length == 0 || (cLine[0] == '/' && cLine[1] == '/'))
                continue;
            if (!isDefining)
            {
                if (string.Compare(cLine.ToUpper(), "DEFINE_BGM") == 0)
                {
                    isDefining = true;
                    bgmDefining = true;
                    if (bgmDefined)
                        Debug.LogWarning($"BGM have already been defined, these will be aded after. Error in line { i + 1 }");
                    continue;
                }
                if (string.Compare(cLine.ToUpper(), "DEFINE_CONVERSATION") == 0)
                {
                    definingConversation = new Conversation();
                    isDefining = true;
                    conversationDefining = true;
                    continue;
                }
            }
            else if (isDefining)
            {
                if (bgmDefining)
                {
                    if (cLine == "END_BGM_DEFINE")
                    {
                        isDefining = false;
                        bgmDefining = false;
                        bgmDefined = true;
                        continue;
                    }

                    string[] bgmCommand = cLine.Split("&&");
                    if (bgmCommand.Length != 2)
                    {
                        Debug.LogError($"Background music command must have 2 parameters in line {i + 1}");
                        continue;

                    }
                    AudioClip bgm = Resources.Load<AudioClip>(bgmCommand[0]);
                    bgm.LoadAudioData();
                    AudioSource bgmAudioSource = BGMLibrary.AddComponent<AudioSource>();
                    bgmAudioSource.volume = 0.1f;
                    bgmAudioSource.playOnAwake = false;
                    bgmAudioSource.clip = bgm;

                    BGMList.Add(maxBGM, new BackgroundMusicClass(bgmAudioSource, bgmCommand[1] == "1"));
                    maxBGM++;
                    continue;
                }
                else if (conversationDefining)
                {
                    if (cLine == "END_EVENT_CONVERSATION")
                    {
                        isDefining = false;
                        conversationDefining = false;
                        converList.Add(maxConversation, definingConversation);
                        definingConversation = null;
                        maxConversation++;
                        continue;
                    }

                    string[] cnvCommand = cLine.Split("&&");

                    if (cLine.Contains("WAIT&&"))
                    {
                        if (cnvCommand.Length != 2)
                        {
                            Debug.LogError($"Event WAIT command must have 2 parameters in line {i + 1}");
                            continue;
                        }
                        Voiceline newVoice = new Voiceline(float.Parse(cnvCommand[1], System.Globalization.CultureInfo.InvariantCulture));
                        voicelineList.Add(maxVoiceline, newVoice);
                        definingConversation.addVoiceline(maxVoiceline);
                        maxVoiceline++;
                        continue;
                    }

                    if (cnvCommand.Length != 5 && cnvCommand.Length != 6)
                    {
                        Debug.LogError($"Event voiceline command must have 5 parameters in line {i + 1}");
                        continue;
                    }
                    if (cnvCommand.Length == 5)
                    {
                        addVoiceline(cnvCommand[0], cnvCommand[1], cnvCommand[2], Int32.Parse(cnvCommand[3]), Int32.Parse(cnvCommand[4]));
                    }
                    else
                    {
                        addVoiceline(cnvCommand[0], cnvCommand[1], cnvCommand[2], Int32.Parse(cnvCommand[3]), Int32.Parse(cnvCommand[4]), Int32.Parse(cnvCommand[5]));
                    }
                    
                    
                }
            }

        }

        setUpBGM(0);
        if (eventAutoStart)
        {
            converList[0].addToBuffer(voicelineBuffer, false, false);
        }
    }

    

    private void FixedUpdate()
    {
        //BGM game loop
        /*if (bgmTimer > Time.fixedDeltaTime)
            bgmTimer -= Time.fixedDeltaTime;

        if (bgmTimer <= Time.fixedDeltaTime)
        {
            if (goToNext)
            {
                iteratorBGM++;
            }
            //currentBgm.stopClip();
            setUpBGM(iteratorBGM);
        }*/
        /*if(Time.fixedTime - bgmStartTime >= bgmTimer)
        {
            if (goToNext)
            {
                iteratorBGM++;
            }
            //currentBgm.stopClip();
            setUpBGM(iteratorBGM);
            
        }*/
        /*if (!currentBgm.getIsPlaying())
        {
            if (goToNext)
            {
                iteratorBGM++;
            }
            setUpBGM(iteratorBGM);
        }*/

    }

    // Update is called once per frame
    void Update()
    {
        

        //Voiceline game loop
        if (currentVoiceline == null)
        {
            if (voicelineBuffer.Count > 0)
            {
                currentVoiceline = voicelineList[voicelineBuffer.First.Value];
                voicelineBuffer.RemoveFirst();
                setUpVoiceline();
            }
        }
        else
        {
            if (voicelineTimer > 0)
            {
                voicelineTimer -= Time.deltaTime;
            }
            else
            {
                NameText.text = "";
                Subtitle.text = "";
                currentVoiceline = null;
            }
        }
    }
    void setUpVoiceline()
    {
        voicelineTimer = currentVoiceline.getLength();

        NameText.color = currentVoiceline.getNameColor();
        Subtitle.color = currentVoiceline.getSubtitleColor();

        NameText.text = currentVoiceline.getName();
        Subtitle.text = currentVoiceline.getSubtitle();
        if (currentVoiceline.throwsEvent >= 0)
        {
            m_ConvEvent.Invoke(currentVoiceline.throwsEvent);
        }
        currentVoiceline.playLine();
    }

    

    /*IEnumerator PlayAudio()
    {
        while (currentBgm.getIsPlaying())
            yield return null;

        if (goToNext)
        {
            iteratorBGM++;
        }
        setUpBGM(iteratorBGM);
        yield break;

    }*/
    IEnumerator PlayAudio()
    {
        yield return new WaitForSecondsRealtime(bgmTimer);
        if (goToNext)
        {
            iteratorBGM++;
        }
        setUpBGM(iteratorBGM);
        yield break;
    }

        void setUpBGM(int bgmID)
    {
        BackgroundMusicClass newBGM = BGMList[bgmID];
        bgmStartTime = Time.fixedTime;
        bgmTimer = newBGM.getLenght();// - Time.deltaTime;
        newBGM.playClip();
        goToNext = newBGM.getNextOnEnd();
        //if(currentBgm!=null)
        //    currentBgm.stopClip();
        currentBgm = newBGM;
        currentBGMCoroutine = StartCoroutine(PlayAudio());
    }

    public void switchBGM(int bgmID)
    {
        if (bgmID >= maxBGM)
        {
            Debug.LogError("Tried to set a BGM that doesn't exist. Remember that the BGM ID starts on 0");
            return;
        }
        if (currentBGMCoroutine != null)
        {
            StopCoroutine(currentBGMCoroutine);
            currentBGMCoroutine = null;
        }
        currentBgm.stopClip();
        iteratorBGM = bgmID;
        setUpBGM(iteratorBGM);
    }

    public int addVoiceline(string resourcePath, string inName, string inSubtitle, int inNameColor, int inSubtitleColor,int inThrowsEvent=-1)
    {
        AudioClip voiceline = Resources.Load<AudioClip>(resourcePath);
        voiceline.LoadAudioData();
        AudioSource voiceAudioSource = VoicelineLibrary.AddComponent<AudioSource>();
        voiceAudioSource.volume = 1f;
        voiceAudioSource.playOnAwake = false;
        voiceAudioSource.clip = voiceline;
        

        Color selectedNameColor = colorList[inNameColor];
        Color selectedSubtColor = colorList[inSubtitleColor];
        Voiceline newVoiceline = new Voiceline(voiceAudioSource, inName, inSubtitle, selectedNameColor, selectedSubtColor);
        newVoiceline.throwsEvent = inThrowsEvent;
        voicelineList.Add(maxVoiceline, newVoiceline);
        definingConversation.addVoiceline(maxVoiceline);
        int returnID = maxVoiceline;
        maxVoiceline++;
        return returnID;
    }

    public void TESTstarConversationAppend(int convID)
    {
        startConversation(convID, false, false, false);
    }
    public void TESTstartConversationInterrupt(int convID)
    {
        startConversation(convID, false, false, true);
    }
    public void TESTstartConversationInterruptClear(int convID)
    {
        startConversation(convID, false, true, false);
    }
    public void TESTstartConversationHardInterrupt(int convID)
    {
        startConversation(convID, true, false, true);
    }
    public void TESTstartConversationHardInterruptClear(int convID)
    {
        startConversation(convID, true, true, false);
    }

    public void startConversation(int convID, bool hardInterrupt, bool clearCurrent, bool prepend)
    {
        if (hardInterrupt)
        {
            if (currentVoiceline != null)
            {
                currentVoiceline.stopLine();
                currentVoiceline = null;
            }
            NameText.text = "";
            Subtitle.text = "";
        }
        if (convID >= maxConversation || convID < 0)
        {
            Debug.LogError($"Conversation ID must be between 0 and {maxConversation}");
            return;
        }
        converList[convID].addToBuffer(voicelineBuffer, clearCurrent, prepend);
    }

    public void playVoiceline(int voiceID,bool mustBeSubtitled,bool hardInterrupt, bool prepend)
    {
        if(voiceID>=maxVoiceline || voiceID < maxVoiceline)
        {
            Debug.LogError($"Conversation ID must be between 0 and {maxVoiceline}");
            return;
        }
        Voiceline voicelineToPlay = voicelineList[voiceID];
        if (currentVoiceline == null && voicelineBuffer.Count == 0)
        {
            voicelineBuffer.AddFirst(voiceID);
            return;
        }
        else
        {
            if (!mustBeSubtitled)
            {
                voicelineList[voiceID].playLine();
            }
            else
            {
                if (hardInterrupt)
                {
                    if (currentVoiceline != null)
                    {
                        currentVoiceline.stopLine();
                        currentVoiceline = null;
                    }
                    NameText.text = "";
                    Subtitle.text = "";
                }
                else
                {
                    if (prepend)
                    {
                        voicelineBuffer.AddFirst(voiceID);
                    }
                    else
                    {
                        voicelineBuffer.AddLast(voiceID);
                    }
                }
            }
        }
    }
}


public class Voiceline
{
    AudioSource voiceline;
    string name;
    string subtitle;
    Color nameColor;
    Color subtitleColor;
    float waitTime;
    public int throwsEvent { get; set; }

    public Voiceline(float inwaittime)
    {
        waitTime = inwaittime;
        name = "";
        subtitle = "";
        nameColor = Color.white;
        subtitleColor = Color.white;
        throwsEvent = -1;
    }

    public Voiceline(AudioSource inVoiceline, string inName, string inSubtitle, Color inNameColor, Color inSubtitleColor)
    {
        voiceline = inVoiceline;
        name = inName;
        subtitle = inSubtitle;
        nameColor = inNameColor;
        subtitleColor = inSubtitleColor;
    }
    public float getLength()
    {
        if (voiceline != null)
        {
            return voiceline.clip.length;
        }
        else
        {
            return waitTime;
        }

    }
    public void playLine()
    {
        if (voiceline != null)
        {
            voiceline.Play();
        }
    }
    public void stopLine()
    {
        if (voiceline != null)
        {
            voiceline.Stop();
        }
    }
    public string getName()
    {
        return name;
    }
    public string getSubtitle()
    {
        return subtitle;
    }
    public Color getNameColor()
    {
        return nameColor;
    }
    public Color getSubtitleColor()
    {
        return subtitleColor;

    }
}

public class BackgroundMusicClass
{
    AudioSource bgm;
    bool nextOnEnd;

    public BackgroundMusicClass(AudioSource inbgm, bool inNextOnEnd)
    {
        bgm = inbgm;
        nextOnEnd = inNextOnEnd;
    }

    public void playClip()
    {
        bgm.Play();
    }
    public void stopClip()
    {
        bgm.Stop();
    }

    public float getLenght()
    {
        return bgm.clip.length;
    }

    public bool getNextOnEnd()
    {
        return nextOnEnd;
    }

    public bool getIsPlaying()
    {
        return bgm.isPlaying;
    }
}

public class Conversation
{
    List<int> voicelineList;

    public Conversation()
    {
        voicelineList = new List<int>();
    }

    public void addVoiceline(int voiceID)
    {
        voicelineList.Add(voiceID);
    }

    public void addToBuffer(LinkedList<int> voicelineBuffer, bool clearCurrent, bool prepend)
    {
        if (clearCurrent)
        {
            voicelineBuffer.Clear();
        }
        if (prepend)
        {
            voicelineList.Reverse();
            foreach (int voiceID in voicelineList)
            {
                voicelineBuffer.AddFirst(voiceID);
            }
            voicelineList.Reverse();
        }
        else
        {
            foreach (int voiceID in voicelineList)
            {
                voicelineBuffer.AddLast(voiceID);
            }
        }

    }
}