using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSettings : MonoBehaviour
{
    //Missile Settings half inspired on aim9L
    public Transform target;
    public GameObject engine;
    public float missileMass = 85.3f;//kg
    //public float maxSpeed=2800f;
    public float initSpeed = 350f;//ms
    public float maxSpeed = 950f;
    public float thrustTime = 2.2f;
    public float thurstForce = 17800f;//N
    public float lifeTime = 60f;
    public float DegreesPerSecond = 20f;
    public float DegreesPerFrame = 3f;
    public float DegreeLock = 45f;
    public float referenceRange = 4000f;

    //Enemy
    public float enemySpeed = 400f;//ms
    public float spawnPosRand = 300f;

    public Material engineOn;
    public Material engineOff;
}
