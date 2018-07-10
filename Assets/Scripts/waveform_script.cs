using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveform_script : MonoBehaviour {

    public int counter_update = 0;
    public int counter_update_all = 0;

    public List<float> csvfiledata = new List<float>();

    public GameObject LinePrefab;
    List<GameObject> linePrefabs;

    LineRenderer line1;
    LineRenderer line2;

    public GameObject EraserBar;

    int curLineIndex = 0;

    void ReadLineTest(int line_index, List<string> line)
    {
        csvfiledata.Add(float.Parse(line[1]));
    }

    /*
    void DrawParameterWave(List<float> data, int dataCount)
    {
        Debug.Log("counter_ua: " + counter_update_all + "Data all: " + csvfiledata[counter_update_all] + "counter_u: " + counter_update + " Data: " + data[counter_update]);
        int threshold = 500;
        if (counter_update_all == dataCount - 1)
        {
            counter_update_all = 0;
        } else if (counter_update_all < dataCount)
        {
            if (line1.positionCount <= counter_update)
            {
                line1.positionCount++;
            }
            if (line2.positionCount <= counter_update)
            {
                line2.positionCount++;
            }
            if (counter_update_all > threshold)
            {
                //line2.gameObject.SetActive(true);
                //Debug.Log("Vis2 " + counter_update_all);
                //line2.useWorldSpace = true;
                //line2.SetPosition(counter_update, new Vector3(counter_update * 0.01f - 6.0f, csvfiledata[counter_update_all], 0.0f));
                //line1.gameObject.SetActive(false);
                //GameObject.Find("EraserBar").transform.position = new Vector3(counter_update * 0.01f - 6.0f, 0.0f, -1.0f);

                line1.gameObject.SetActive(true);
                Debug.Log("Vis1 " + counter_update_all);
                line1.useWorldSpace = true;
                line1.SetPosition(counter_update, new Vector3(counter_update * 0.01f - 6.0f, csvfiledata[counter_update_all], 0.0f));
                line2.gameObject.SetActive(false);
                EraserBar.transform.position = new Vector3(counter_update * 0.01f - 6.0f, 0.0f, -1.0f);
            } else
            {
                line1.gameObject.SetActive(true);
                Debug.Log("Vis1 " + counter_update_all);
                line1.useWorldSpace = true;
                line1.SetPosition(counter_update, new Vector3(counter_update * 0.01f - 6.0f, csvfiledata[counter_update_all], 0.0f));
                line2.gameObject.SetActive(false);
                EraserBar.transform.position = new Vector3(counter_update * 0.01f - 6.0f, 0.0f, -1.0f);
            }

            //GameObject.Find("EraserBar").transform.position = new Vector3(counter_update * 0.01f - 6.0f, 0.0f, -1.0f);

            counter_update++;
            counter_update_all++;

         }
         else
         {
            counter_update_all = 0;
         }
        
    }
    */

    void drawSingleLine(float val)
    {
        int whenToBecomeSuperFat = 1000;
        int xPosL1 = line1.positionCount;
        int xPosL2 = line2.positionCount;
        Debug.Log("xPos1: " + xPosL1 + " xPos2: " + xPosL2);
        if (curLineIndex > whenToBecomeSuperFat)
        {
            line2.gameObject.SetActive(true);
            Debug.Log("Vis2 ");
            line2.useWorldSpace = true;
            line2.SetPosition(xPosL2-1, new Vector3((xPosL2 % 1200) * 0.01f - 6.0f, val, 0.0f));
            line1.gameObject.SetActive(false);
            EraserBar.transform.position = new Vector3(xPosL2 * 0.01f - 6.0f, 0.0f, -1.0f);
        }
        else
        {
            line1.gameObject.SetActive(true);
            Debug.Log("Vis1 ");
            line1.useWorldSpace = true;
            line1.SetPosition(xPosL1-1, new Vector3((xPosL1 % 1200) * 0.01f - 6.0f, val, 0.0f));
            line2.gameObject.SetActive(false);
            EraserBar.transform.position = new Vector3(xPosL1 * 0.01f - 6.0f, 0.0f, -1.0f);
        }

    }

    // Use this for initialization
    void Start () {
        fgCSVReader.LoadFromFile(Application.dataPath + "/sampl1hour.csv", new fgCSVReader.ReadLineDelegate(ReadLineTest));
        linePrefabs = new List<GameObject>();

        line1 = instantiateLine(new Vector3(-6f, 0f, 0f));
        line2 = instantiateLine(new Vector3(-6f, 0f, 0f));
        AnimationCurve curve = new AnimationCurve();
        curve.AddKey(0.0f, 0.5f);
        line2.widthCurve = curve;
        line2.widthMultiplier = 0.2f;
    }
	
	// Update is called once per frame
	void Update () {
    }

    private void FixedUpdate()
    {
        if(curLineIndex < csvfiledata.Count)
        {
            drawSingleLine(csvfiledata[curLineIndex]);
            curLineIndex++;
        } else
        {
            Debug.LogError("No more lines");
        }
        //DrawParameterWave(csvfiledata, 1200);
    }

    LineRenderer instantiateLine(Vector3 pos)
    {
        GameObject newLine = Instantiate(LinePrefab, pos, Quaternion.identity) as GameObject;
        linePrefabs.Add(newLine);
        return newLine.GetComponent<LineRenderer>();
    }
}
