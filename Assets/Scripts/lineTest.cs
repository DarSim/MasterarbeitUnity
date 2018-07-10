    using UnityEngine;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class lineTest : MonoBehaviour
    {
        
        public Color c1 = Color.yellow;
    	public Color c2 = Color.red;
    	

        public int counter_update = 0;
        public int counter_update_all = 0;

        public LineRenderer lineRenderer;
        public LineRenderer line2;

    	public List<float> csvfiledata = new List<float>();

    	void ReadLineTest(int line_index, List<string> line)
    	{
            csvfiledata.Add(float.Parse(line[1]));
    	}

        void DrawParameterWave(List<float> data, int data_count)
        {
            if (counter_update_all == csvfiledata.Count - 1)
            {
                counter_update_all = 0;
            } else if (counter_update < data_count)
            {
                if (lineRenderer.positionCount <= counter_update)
                {
                    lineRenderer.positionCount++;
                }
            lineRenderer.useWorldSpace = true;

            lineRenderer.SetPosition(counter_update, new Vector3(counter_update * 0.01f- 6.0f, csvfiledata[counter_update_all], 0.0f));

            
            //GameObject.Find("Eraser Bar").transform.Translate() = new Vector3(counter_update * 0.0106f-6.25f, 3.86f, 5.0f);
            GameObject.Find("EraserBar").transform.position = new Vector3(counter_update * 0.01f- 6.0f, 0.0f, -1.0f);

                counter_update++;
                counter_update_all++;

            }else
            {
                counter_update = 0;
            }
        }

    	void Start()
    	{

        fgCSVReader.LoadFromFile(Application.dataPath +"/sampl1hour.csv", new fgCSVReader.ReadLineDelegate(ReadLineTest));
        lineRenderer = GetComponent<LineRenderer>();
        }



        void FixedUpdate()
        {
            DrawParameterWave(csvfiledata, 1200);
        }






    /*
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.widthMultiplier = 0.2f;
        lineRenderer.positionCount = lengthOfLineRenderer;

        // A simple 2 color gradient with a fixed alpha of 1.0f.
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        lineRenderer.colorGradient = gradient;


using UnityEngine;
using System.Collections;

public class ExampleClass : MonoBehaviour
{
public Transform meeple;
public GameObject grandChild;

public void Example()
{
    //Assigns the transform of the first child of the Game Object this script is attached to.
    meeple = this.gameObject.transform.GetChild(0);

    //Assigns the first child of the first child of the Game Object this script is attached to.
    grandChild = this.gameObject.transform.GetChild(0).GetChild(0).gameObject;
}
}

    


    public Transform child;
    public LineRenderer childLine;

    public void GetChildren()
    {
        child = this.gameObject.transform.GetChild(0);
        childLine = child.GetComponent<LineRenderer>();
    }
*/

}

