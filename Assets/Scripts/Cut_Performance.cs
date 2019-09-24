using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Fashion;
using Fashion.UIManager;

public class Cut_Performance : MonoBehaviour
{
    [SerializeField]
    private GameObject uiHelpersToInstantiate;

    int count1 = 10;
    int count2 = 10;
    int count3 = 10;
    static int check_count1 = 0;
    static int check_count2 = 0;
    static int check_count3 = 0;

    LaserPointer lp;

    public LaserPointer.LaserBeamBehavior laserBeamBehavior;

    private void Awake()
    {
        lp = FindObjectOfType<LaserPointer>();
        if (!lp)
        {
            if (uiHelpersToInstantiate)
            {
                GameObject.Instantiate(uiHelpersToInstantiate);
            }
            lp = FindObjectOfType<LaserPointer>();
            if (!lp)
            {
                Debug.LogError("UIBuilder requires use of a LaserPointer and will not function without it. Add one to your scene, or assign the UIHelpers prefab to the UIBuilder in the inspector.");
                return;
            }
        }
        lp.laserBeamBehavior = laserBeamBehavior;
        GetComponent<OVRRaycaster>().pointer = lp.gameObject;
    }

    void Start()
    {
        
    }

    void Update()
    {
  
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Cut_Collider_1")
        {
            check_count1++;
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "Cut_Collider_2")
        {
            check_count2++;
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "Cut_Collider_3")
        {
            check_count3++;
            Destroy(other.gameObject);
        }
    }
}
