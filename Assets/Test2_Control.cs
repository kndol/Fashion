using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test2_Control : MonoBehaviour {
    public Canvas Start_UI;
    public Canvas Mode_UI;

    public OVRCameraRig cam1;
    

    //public OVRCameraRig Menu_Camera;
    public Camera Select_Design_Camera;
    public Camera Pattern_Camera;
    public Camera Pattern_Camera2;
    //public Camera Pattern_Camera3;
    public Camera Cut_Camera;
    //public Camera Seam_Camera;
    public Camera Cut_Camera2;
    public Camera Sweing_Camera;
    public Camera Sweing_Camera2;
    public Camera Fitting_Camera;

    void Start() {
        Start_UI.enabled = true;
        Mode_UI.enabled = false;

        //cam1.transform.position;
        //Menu_Camera.enabled = true;
        /*Select_Design_Camera.enabled = false;
        Pattern_Camera.enabled = false;
        Pattern_Camera2.enabled = false;
        //Pattern_Camera3.enabled = false;
        Cut_Camera.enabled = false;
        //Seam_Camera.enabled = false;
        Cut_Camera2.enabled = false;
        Sweing_Camera.enabled = false;
        Sweing_Camera2.enabled = false;
        Fitting_Camera.enabled = false;*/
    }

    void Update()
    {
        
    }

    public void Start_Btn() {
        Start_UI.enabled = false;
        Mode_UI.enabled = true;
    }

    public void Tutorial_Btn() {
        Mode_UI.enabled = false;
        //Menu_Camera.enabled = false;
        //Select_Design_Camera.enabled = true;
        cam1.transform.position = Select_Design_Camera.transform.position;
        cam1.transform.rotation = Select_Design_Camera.transform.rotation;
    }

    public void Btn()
    {
        cam1.transform.position = Pattern_Camera.transform.position;
        cam1.transform.rotation = Pattern_Camera.transform.rotation;
        //Select_Design_Camera.enabled = false;
        //Pattern_Camera.enabled = true;
    }
    public void Btn2()
    {
        cam1.transform.position = Pattern_Camera2.transform.position;
        cam1.transform.rotation = Pattern_Camera2.transform.rotation;
        //Pattern_Camera.enabled = false;
        //Pattern_Camera2.enabled = true;
    }
    public void Btn3()
    {
        cam1.transform.position = Cut_Camera.transform.position;
        cam1.transform.rotation = Cut_Camera.transform.rotation;
        //Pattern_Camera2.enabled = false;
        //Cut_Camera.enabled = true;
    }

    public void Btn4()
    {
        cam1.transform.position = Cut_Camera2.transform.position;
        cam1.transform.rotation = Cut_Camera2.transform.rotation;
        //Cut_Camera.enabled = false;
        //Cut_Camera2.enabled = true;
        //Seam_Camera.enabled = true;
    }
    public void Btn5()
    {
        cam1.transform.position = Sweing_Camera.transform.position;
        cam1.transform.rotation = Sweing_Camera.transform.rotation;
        //Cut_Camera2.enabled = false;
        //Seam_Camera.enabled = false;
        //Sweing_Camera.enabled = true;
    }
    public void Btn6()
    {
        cam1.transform.position = Sweing_Camera2.transform.position;
        cam1.transform.rotation = Sweing_Camera2.transform.rotation;
        //Sweing_Camera.enabled = false;
        //Sweing_Camera2.enabled = true;
    }
    public void Btn7()
    {
        cam1.transform.position = Fitting_Camera.transform.position;
        cam1.transform.rotation = Fitting_Camera.transform.rotation;
        //Sweing_Camera2.enabled = false;
        //Fitting_Camera.enabled = true;
    }
    public void Btn8()
    {
        Init_Data();
    }
    void Init_Data()
    {
        Start_UI.enabled = true;
        Mode_UI.enabled = false;
        /*Menu_Camera.enabled = true;
        Select_Design_Camera.enabled = false;
        Pattern_Camera.enabled = false;
        Pattern_Camera2.enabled = false;
        //Pattern_Camera3.enabled = false;
        Cut_Camera.enabled = false;
        //Seam_Camera.enabled = false;
        Cut_Camera2.enabled = false;
        Sweing_Camera.enabled = false;
        Sweing_Camera2.enabled = false;
        Fitting_Camera.enabled = false;*/
    }
}
