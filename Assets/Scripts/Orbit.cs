using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Orbit : MonoBehaviour
{
    public GameObject button;
    private Text button_text;
    private bool isOn = true;

    private GameObject Planet;
    private Sim_State State;

    private LineRenderer lineRend;
    private List<Vector3> line_verts;
    private int num_verts = 400;

    private float time = 0;
    public float distance;
    public Vector2[] epicicles = new Vector2[0];
    public float period_in_hours = 24;
    public Color color;

    // Start is called before the first frame update
    void Awake()
    {
        Planet = transform.GetChild(0).gameObject;
        State = GameObject.Find("State").GetComponent<Sim_State>();

        float offset = Random.Range(0.0f, 2 * Mathf.PI);
        time += offset;

        lineRend = Planet.GetComponent<LineRenderer>();
        lineRend.positionCount = num_verts;
        line_verts = new List<Vector3>();
        lineRend.startColor = color;
        lineRend.endColor = color;

        isOn = true;
        button_text = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        button_text.text = Planet.name + " : On";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isOn)
        {
            // orbit
            float x = distance * Mathf.Cos(time);
            float y = distance * Mathf.Sin(time);

            //epicicles
            foreach (Vector2 epicicle in epicicles)
            {
                x += epicicle[0] * Mathf.Cos(epicicle[1] * time);
                y += epicicle[0] * Mathf.Sin(epicicle[1] * time);
            }
            // update position
            Planet.transform.localPosition = new Vector3(x, 0, y);

            // time step
            time += State.Sim_Speed * Time.fixedDeltaTime * ((2 * Mathf.PI) / period_in_hours);
            if (time > 2 * Mathf.PI) time = 0;

            // paths
            if (State.Show_Paths)
                Draw_Path();
            else if (line_verts.Count != 0)
            {
                line_verts.Clear();
                lineRend.SetPositions(new Vector3[num_verts]);
            }

            Planet.transform.localScale = new Vector3(State.Scale, State.Scale, State.Scale);
        }
    }

    void Draw_Path()
    {
        line_verts.Add(Planet.transform.position);
        if (line_verts.Count > num_verts)
        {
            line_verts.RemoveAt(0);
        }
        int i = 0;
        while (i < line_verts.Count)
        {
            lineRend.SetPosition(i, line_verts[i]);
            i++;
        }
        while (i < num_verts)
        {
            lineRend.SetPosition(i, line_verts[line_verts.Count - 1]);
            i++;
        }
    }

    public void Flip_on_off()
    {
        if (isOn)
        {
            button_text.text = Planet.name + " : Off";
            Planet.GetComponent<MeshRenderer>().enabled = false;
            if (Planet.name.Equals("Sun"))
                ((Behaviour)Planet.GetComponent("Halo")).enabled = false;
            line_verts.Clear();
            lineRend.SetPositions(new Vector3[num_verts]);
        }
        else
        {
            button_text.text = Planet.name + " : On";
            Planet.GetComponent<MeshRenderer>().enabled = true;
            if (Planet.name.Equals("Sun"))
                ((Behaviour)Planet.GetComponent("Halo")).enabled = true;
        }
        isOn = !isOn;
    }
}
