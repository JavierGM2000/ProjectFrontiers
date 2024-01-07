using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// You! Solitary! NOW!
// - AWACS Bandog
public class RadarInfo : MonoBehaviour
{
    [SerializeField]
    int team;
    [SerializeField]
    string IFFCode;
    [SerializeField]
    string planeName;
    [SerializeField]
    string callsign;
    [SerializeField]
    bool revealsTeam;
    [SerializeField]
    bool isTarget = false;


    UIEnemyShow canvas;

    private void Start()
    {
        canvas = FindObjectOfType<UIEnemyShow>();
        canvas.addEnemy(this);
    }


    public int getTeam()
    {
        return team;
    }
    public bool getRevealsTeam()
    {
        return revealsTeam;
    }

    public string getCallsign()
    {
        return callsign;
    }

    public string getPlaneName()
    {
        return planeName;
    }

    public string getIFFCode()
    {
        return IFFCode;
    }

    public Transform getTransform()
    {
        return transform;
    }

    public bool getIsTarget()
    {
        return isTarget;
    }

    private void OnDestroy()
    {
        canvas.removeItem(gameObject.GetInstanceID());
    }
}
