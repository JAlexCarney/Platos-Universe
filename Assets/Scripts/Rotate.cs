using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotate : MonoBehaviour
{
    public GameObject button;
    private Text button_text;

    private Sim_State State;
    private float time;

    public float period_in_hours = 24;
    private bool isShowing;

    // Start is called before the first frame update
    void Awake()
    {
        State = GameObject.Find("State").GetComponent<Sim_State>();
        time = 0;
        isShowing = true;

        button_text = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        button_text.text = "Stars : On";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.eulerAngles = new Vector3(0,-(time*180.0f)/Mathf.PI,0);

        // time step
        time += State.Sim_Speed * Time.fixedDeltaTime * ((2 * Mathf.PI) / period_in_hours);
        if (time >= 2 * Mathf.PI) time = 0;
    }

    public void Flip_On_Off()
    {
        isShowing = !isShowing;
        GetComponent<MeshRenderer>().enabled = isShowing;
        if (isShowing)
            button_text.text = "Stars : On";
        else
            button_text.text = "Stars : Off";
    }
}
