using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIEnemyShow : MonoBehaviour
{
    private Canvas canvas;
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private MainGun maGun;
    [SerializeField]
    private GameObject gunPredictReticle;
    private RawImage gunPredictReticleImage;
    [SerializeField]
    private GameObject gunPointtReticle;
    private RawImage gunPointtReticleImage;
    private bool drawPredict = false;

    int currentSelected = -1;

    [SerializeField]
    private GameObject EnemySightPrefab;
    //private currentSelected;
    [SerializeField]
    private Texture unSelectedTexture;
    [SerializeField]
    private Color unSelectedColor;
    [SerializeField]
    private Texture selectedTexture;
    [SerializeField]
    private Color selectedColor;
    [SerializeField]
    private Color lockedColor;

    Dictionary<int, targetClass> enemyList;
    List<int> levelTargets;

    [SerializeField]
    GameObject finalEnemy;

    [SerializeField]
    private LevelManager levelManager;

    // Start is called before the first frame update
    void Awake()
    {
        //mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        levelTargets = new List<int>();
        canvas = GetComponent<Canvas>();
        enemyList = new Dictionary<int, targetClass>();
        gunPredictReticleImage = gunPredictReticle.GetComponent<RawImage>();
        gunPointtReticleImage = gunPointtReticle.GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 planePoint = mainCamera.transform.position + mainCamera.transform.forward * 10f;
        Plane canvasPlane = new Plane(mainCamera.transform.forward, planePoint);
        foreach(KeyValuePair<int, targetClass> enemy in enemyList)
        {
            Vector3 enemyPos = enemy.Value.enemy.transform.position;
            //Debug.Log(canvasPlane.GetSide(enemyPos));
            if (canvasPlane.GetSide(enemyPos))
            {
                Vector3 toEnemy = (enemyPos - mainCamera.transform.position).normalized;
                Ray ray = new Ray(mainCamera.transform.position, toEnemy);
                float enter = 0.0f;
                if (canvasPlane.Raycast(ray, out enter))
                {
                    //Get the point that is clicked
                    Vector3 hitPoint = ray.GetPoint(enter);
                    if (Vector3.Distance(planePoint, hitPoint) >= 10f)
                    {
                        enemy.Value.sightItem.transform.localPosition = new Vector3(-999, -999, -999);
                    }
                    else
                    {
                        enemy.Value.sightItem.GetComponent<RectTransform>().position = hitPoint;
                        if (enemy.Value.isSelected)
                        {
                            int dist = Mathf.RoundToInt(Vector3.Distance(mainCamera.transform.position, enemyPos));
                            enemy.Value.distance.text = dist.ToString();
                        }
                    }
                    
                }
                
            }
            else
            {
                enemy.Value.sightItem.transform.localPosition = new Vector3(999, 999, -999);
            }
        }
    }

    public void removeItem(int goID)
    {
        if(goID == finalEnemy.GetInstanceID())
        {
            levelManager.changeLevel("VictoryScreen");
        }

        if(currentSelected == goID)
        {
            currentSelected -= 1;
        }
        Destroy(enemyList[goID].sightItem);
        enemyList.Remove(goID);

        if (levelTargets.Contains(goID))
        {
            levelTargets.Remove(goID);
            if (levelTargets.Count == 0)
            {
                finalEnemy.GetComponent<RadarInfo>().enabled = true;
                finalEnemy.GetComponent<CabinaHealth>().canTakeDamage = true;
            }
        }
    }

    public void removeGunsights()
    {
        gunPredictReticle.transform.position = new Vector3(999, 999, -999);
        gunPointtReticle.transform.position = new Vector3(999, 999, -999);
    }
    public void moveReticles(Vector3 leadIndicator, Vector3 gunPosition)
    {
        Plane canvasPlane = new Plane(mainCamera.transform.forward, mainCamera.transform.position + mainCamera.transform.forward * 10f);
        if (canvasPlane.GetSide(leadIndicator))
        {
            Vector3 toEnemy = (leadIndicator - mainCamera.transform.position).normalized;
            Ray ray = new Ray(mainCamera.transform.position, toEnemy);
            float enter = 0.0f;
            if (canvasPlane.Raycast(ray, out enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                gunPredictReticle.GetComponent<RectTransform>().position = hitPoint;
            }

        }
        else
        {
            gunPredictReticle.transform.localPosition = new Vector3(999, -999, -999);
        }
        if (canvasPlane.GetSide(gunPosition))
        {
            Vector3 toEnemy = (gunPosition - mainCamera.transform.position).normalized;
            Ray ray = new Ray(mainCamera.transform.position, toEnemy);
            float enter = 0.0f;
            if (canvasPlane.Raycast(ray, out enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                gunPointtReticle.GetComponent<RectTransform>().position = hitPoint;
            }

        }
        else
        {
            gunPointtReticle.transform.localPosition = new Vector3(999, 999, -999);
        }
    }



    public void addEnemy(RadarInfo inEnemy)
    {
        enemyList.Add(inEnemy.gameObject.GetInstanceID(),new targetClass(inEnemy, EnemySightPrefab, transform, unSelectedColor));
        if (inEnemy.getIsTarget())
        {
            levelTargets.Add(inEnemy.gameObject.GetInstanceID());
        }
    }

    public void selectEnemy(int instanceID)
    {
        if (enemyList.ContainsKey(currentSelected))
        {
            enemyList[currentSelected].isSelected = false;
            enemyList[currentSelected].sight.texture = unSelectedTexture;
            enemyList[currentSelected].sight.color = unSelectedColor;
            enemyList[currentSelected].planeName.gameObject.SetActive(false);
            enemyList[currentSelected].planeName.color = unSelectedColor;
            enemyList[currentSelected].distance.gameObject.SetActive(false);
            enemyList[currentSelected].distance.color = unSelectedColor;

        }
        currentSelected = instanceID;
        enemyList[currentSelected].isSelected = true;
        enemyList[currentSelected].sight.color = selectedColor;
        enemyList[currentSelected].planeName.gameObject.SetActive(true);
        enemyList[currentSelected].planeName.color = selectedColor;
        enemyList[currentSelected].distance.gameObject.SetActive(true);
        enemyList[currentSelected].distance.color = selectedColor;

    }

    public void setLocked()
    {
        enemyList[currentSelected].sight.texture = selectedTexture;
        enemyList[currentSelected].sight.color = lockedColor;
        enemyList[currentSelected].planeName.color = lockedColor;
        enemyList[currentSelected].distance.color = lockedColor;
        gunPredictReticleImage.color = lockedColor;
        gunPointtReticleImage.color = lockedColor;
    }
    public void setUnlocked()
    {
        enemyList[currentSelected].sight.texture = unSelectedTexture;
        enemyList[currentSelected].sight.color = selectedColor;
        enemyList[currentSelected].planeName.color = selectedColor;
        enemyList[currentSelected].distance.color = selectedColor;
        gunPredictReticleImage.color = selectedColor;
        gunPointtReticleImage.color = selectedColor;
    }


}

public class targetClass
{
    public GameObject enemy;
    RadarInfo enRadInf;
    public GameObject sightItem;
    public RawImage sight;
    public TMP_Text planeName;
    public TMP_Text distance;
    public TMP_Text TGT;

    public bool isSelected = false;

    public targetClass(RadarInfo inRadInf,GameObject inPrefab,Transform inCanvas, Color inColor)
    {
        enemy = inRadInf.gameObject;
        enRadInf = inRadInf;
        sightItem = GameObject.Instantiate(inPrefab,inCanvas);
        sight = sightItem.GetComponent<RawImage>();
        planeName = sightItem.transform.GetChild(0).GetComponent<TMP_Text>();
        distance = sightItem.transform.GetChild(1).GetComponent<TMP_Text>();
        TGT = sightItem.transform.GetChild(2).GetComponent<TMP_Text>();

        sight.color = inColor;
        planeName.color = inColor;
        planeName.text = enRadInf.getPlaneName();
        distance.color = inColor;
        planeName.gameObject.SetActive(false);
        distance.gameObject.SetActive(false);

        if (!enRadInf.getIsTarget())
        {
            TGT.gameObject.SetActive(false);
        }
    }
}
