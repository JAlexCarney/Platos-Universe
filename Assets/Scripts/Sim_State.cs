using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sim_State : MonoBehaviour
{
    public GameObject CANVAS;

    public GameObject SPEED_BUTTON;
    public GameObject SCALE_BUTTON;
    public GameObject SHOW_PATHS_BUTTON;
    public GameObject VIEW_BUTTON;

    public GameObject PERSPECTIVE_CAMERA;
    public GameObject TOP_DOWN_CAMERA;
    public GameObject EARTH_CAMERA;

    private bool isShowing;

    private float INCREMENT = 0.5f;
    private float MAX_SIM_SPEED = 3f;
    
    public int current_view_index;
    private string[] views = { "Perspective", "Top Down", "Earth" };
    public float Sim_Speed;
    public bool Show_Paths;
    public float Scale;

    // Start is called before the first frame update
    void Start()
    {
        isShowing = true;

        Sim_Speed = 1f;
        Show_Paths = true;
        current_view_index = 0;
        Scale = 1f;

        SPEED_BUTTON.transform.GetChild(0).GetComponent<Text>().text = "Speed : " + Sim_Speed.ToString("F2") + "hr/sec";
        SHOW_PATHS_BUTTON.transform.GetChild(0).GetComponent<Text>().text = "Show Paths : True";
        VIEW_BUTTON.transform.GetChild(0).GetComponent<Text>().text = "View : " + views[0];
        SCALE_BUTTON.transform.GetChild(0).GetComponent<Text>().text = "Scale : Defualt";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            isShowing = !isShowing;
            CANVAS.SetActive(isShowing);
        }
    }

    void Set_Active(string name)
    {
        if (name == "Perspective")
        {
            PERSPECTIVE_CAMERA.SetActive(true);
            TOP_DOWN_CAMERA.SetActive(false);
            EARTH_CAMERA.SetActive(false);
        }
        else if (name == "Top Down")
        {
            TOP_DOWN_CAMERA.SetActive(true);
            PERSPECTIVE_CAMERA.SetActive(false);
            EARTH_CAMERA.SetActive(false);
        }
        else if (name == "Earth")
        {
            TOP_DOWN_CAMERA.SetActive(false);
            PERSPECTIVE_CAMERA.SetActive(false);
            EARTH_CAMERA.SetActive(true);
        }
        VIEW_BUTTON.transform.GetChild(0).GetComponent<Text>().text = "View : " + name;
    }

    public void Speed_Button()
    {
        Sim_Speed += INCREMENT;
        if (Sim_Speed > MAX_SIM_SPEED + 0.5f*INCREMENT) Sim_Speed = 0;
        SPEED_BUTTON.transform.GetChild(0).GetComponent<Text>().text = "Speed : " + Sim_Speed.ToString("F2") + "hr/sec";
    }

    public void Scale_Button()
    {
        if (Scale < 1f)
        {
            Scale = 1f;
            SCALE_BUTTON.transform.GetChild(0).GetComponent<Text>().text = "Scale : Defualt";
        }
        else if (Scale == 1f)
        {
            Scale = 2f;
            SCALE_BUTTON.transform.GetChild(0).GetComponent<Text>().text = "Scale : Large";
        }
        else
        {
            Scale = 0.5f;
            SCALE_BUTTON.transform.GetChild(0).GetComponent<Text>().text = "Scale : Small";
        }
    }

    public void Show_Paths_Button()
    {
        if (Show_Paths)
        {
            Show_Paths = false;
            SHOW_PATHS_BUTTON.transform.GetChild(0).GetComponent<Text>().text = "Show Paths : False";
        }
        else
        {
            Show_Paths = true;
            SHOW_PATHS_BUTTON.transform.GetChild(0).GetComponent<Text>().text = "Show Paths : True";
        }
    }

    public void View_Button()
    {
        current_view_index = (current_view_index + 1) % views.Length;
        Set_Active(views[current_view_index]);
    }

}
