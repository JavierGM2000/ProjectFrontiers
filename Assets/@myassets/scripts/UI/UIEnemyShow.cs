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




    // Start is called before the first frame update
    void Awake()
    {
        //mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        canvas = GetComponent<Canvas>();
        enemyList = new Dictionary<int, targetClass>();
    }

    // Update is called once per frame
    void Update()
    {
        Plane canvasPlane = new Plane(mainCamera.transform.forward, mainCamera.transform.position + mainCamera.transform.forward * 10f);
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
                    enemy.Value.sightItem.GetComponent<RectTransform>().position = hitPoint;
                    if (enemy.Value.isSelected)
                    {
                        int dist = Mathf.RoundToInt(Vector3.Distance(mainCamera.transform.position, enemyPos));
                        enemy.Value.distance.text = dist.ToString();
                    }
                }
                
            }
            else
            {
                enemy.Value.sightItem.transform.localPosition = new Vector3(0, 0, -10);
            }
        }
    }

    public void addEnemy(RadarInfo inEnemy)
    {
        enemyList.Add(inEnemy.gameObject.GetInstanceID(),new targetClass(inEnemy, EnemySightPrefab, transform, unSelectedColor));
    }

    public void selectEnemy(int instanceID)
    {
        if (currentSelected >= 0)
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
    }
    public void setUnlocked()
    {
        enemyList[currentSelected].sight.texture = unSelectedTexture;
        enemyList[currentSelected].sight.color = selectedColor;
        enemyList[currentSelected].planeName.color = selectedColor;
        enemyList[currentSelected].distance.color = selectedColor;
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

    public bool isSelected = false;

    public targetClass(RadarInfo inRadInf,GameObject inPrefab,Transform inCanvas, Color inColor)
    {
        enemy = inRadInf.gameObject;
        enRadInf = inRadInf;
        sightItem = GameObject.Instantiate(inPrefab,inCanvas);
        sight = sightItem.GetComponent<RawImage>();
        planeName = sightItem.transform.GetChild(0).GetComponent<TMP_Text>();
        distance = sightItem.transform.GetChild(1).GetComponent<TMP_Text>();

        sight.color = inColor;
        planeName.color = inColor;
        planeName.text = enRadInf.getPlaneName();
        distance.color = inColor;
        planeName.gameObject.SetActive(false);
        distance.gameObject.SetActive(false);
    }
}
