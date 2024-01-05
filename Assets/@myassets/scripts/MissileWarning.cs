using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Maybe the real Top Gun was the friends we made along the way
// - A scientologist
public class MissileWarning : MonoBehaviour
{
    [SerializeField]
    AudioSource warningAudio;
    [SerializeField]
    Transform warningPosition;
    [SerializeField]
    GameObject warningSerialize;

    int missileCount=0;
    Dictionary<int, warningObject> activeMissiles;

    public void addMissile(GameObject missile)
    {
        GameObject newWarning = Instantiate(warningSerialize, warningPosition);
        newWarning.transform.localPosition = new Vector3(0, 0, 0);
        newWarning.transform.LookAt(missile.transform);
        activeMissiles.Add(missile.GetInstanceID(),new warningObject(missile,newWarning));

        if(++missileCount>0 && !warningAudio.isPlaying)
        {
            warningAudio.Play();
        }
    }

    public void removeMissiles(int goID)
    {
        Destroy(activeMissiles[goID].warningGO);
        activeMissiles.Remove(goID);
        if (--missileCount == 0)
        {
            warningAudio.Stop();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        activeMissiles = new Dictionary<int, warningObject>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(KeyValuePair<int,warningObject> missile in activeMissiles)
        {
            missile.Value.warningGO.transform.LookAt(missile.Value.missile.transform);
        }
    }
}

public class warningObject
{
    public GameObject missile;
    public GameObject warningGO;

    public warningObject(GameObject inMissile,GameObject inWarningGO)
    {
        missile = inMissile;
        warningGO = inWarningGO;
    }
}
