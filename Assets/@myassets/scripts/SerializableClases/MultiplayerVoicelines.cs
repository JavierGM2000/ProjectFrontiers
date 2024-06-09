using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MultiplayerVoicelines : NetworkBehaviour
{
    float cooldown = 0;
    float cooldownRes = 5f;

    int numOfLines;
    Character chara;

    List<AudioClip> audios;

    [SerializeField]
    AudioSource myAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        TextAsset targetFile = Resources.Load<TextAsset>("Crimson1Ser");
        chara = JsonUtility.FromJson<Character>(targetFile.text);

        numOfLines = chara.insultLines.Length;

        audios = new List<AudioClip>();
        audios.Add(loadFromVoiceline(chara.introLine));

        foreach(VoicelineClass vc in chara.insultLines)
        {
            audios.Add(loadFromVoiceline(vc));
        }
        
        myAudioSource = gameObject.GetComponent<AudioSource>();
    }

    AudioClip loadFromVoiceline(VoicelineClass voicein)
    {
        return Resources.Load<AudioClip>(voicein.path);
    }

    [ServerRpc(RequireOwnership = false)]
    public void soundTimeServerRpc()
    {
        cooldown = cooldownRes;
        int selected = Random.Range(1, numOfLines);
        playRandomAudioClientRpc(selected);
    }

    [ClientRpc]
    public void playRandomAudioClientRpc(int audio)
    {

            

            myAudioSource.PlayOneShot(audios[audio]);
    }

}
