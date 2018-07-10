using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wavenew : MonoBehaviour {

    public List<float> csvfiledata = new List<float>();
    public GameObject LinePrefab;
    LineRenderer l1, l2;
    int counterIndex = 0;

    bool finishedReading = false;
    Vector3[] points;
    int counter = 0;

    List<DataPoint> dataPoints;

    public GameObject visParticle;

    void ReadCSV(int line_index, List<string> line)
    {
        csvfiledata.Add(float.Parse(line[1]));
    }

    void FillPositions()
    {
        //points = new Vector3[csvfiledata.Count];
        for (int i = 0; i < csvfiledata.Count; i++)
        {
            //Vector3 coordinates = new Vector3(i, csvfiledata[i], 0.0f);
            //dataPoints.Add(new DataPoint(coordinates, i));

            points[i] = new Vector3(i, csvfiledata[i], 0.0f);
        }
    }

    void ShowData()
    {
        //l1.SetPositions(points);
        Debug.Log(points[0] + "," + points[1] + "," + points[2]);

        visParticle.transform.position = new Vector3( points[counter].x * 0.01f - 6f, points[counter].y, points[counter].z);

        //Debug.Log("SHOW DATA 1");
        //for(int i = 0; i < dataPoints.Count; i++)
        //{
        //        Debug.Log("L1");
        //        l1.SetPosition(i, dataPoints[i].Coordinates);


            
        //}
        
        //Debug.Log("NUM OF ELEMENTS IN CSV LIST = " + csvfiledata.Count);
        //int xPosL1 = nfmod(l1.positionCount, 1200);

        //l1.SetPosition(counterIndex, new Vector3(xPosL1, csvfiledata[counterIndex], 0.0f));
        //Debug.Log("cIndex: " + counterIndex + " xPos: " + l1.positionCount);
        //counterIndex++;


    }

    IEnumerator readCSVCoroutine()
    {
        yield return null;
        fgCSVReader.LoadFromFile(Application.dataPath + "/sampl1hour.csv", new fgCSVReader.ReadLineDelegate(ReadCSV));
        finishedReading = true;
    }

    LineRenderer instantiateLine(Vector3 pos)
    {
        GameObject newLine = Instantiate(LinePrefab, pos, Quaternion.identity) as GameObject;
        return newLine.GetComponent<LineRenderer>();
    }


    // Use this for initialization
    void Start () {
        finishedReading = false;
        StartCoroutine(readCSVCoroutine());
        l1 = instantiateLine(new Vector3(-6f, 0f, 0f));
        l2 = instantiateLine(new Vector3(-6f, 0f, 0f));
        AnimationCurve curve = new AnimationCurve();
        curve.AddKey(0.0f, 0.5f);
        l2.widthCurve = curve;
        l2.widthMultiplier = 0.2f;
        FillPositions();

        dataPoints = new List<DataPoint>();
    }
	
	// Update is called once per frame
	void Update () {
        if(!finishedReading)
        {
            Debug.Log("WAITING FOR CSV READING PROCESS");
        } else
        {
            points = new Vector3[csvfiledata.Count];
            FillPositions();
            ShowData();
            counter++;
        }  

	}

    int nfmod(int a, int b)
    {
        return (int) (a - b * Mathf.Floor(a / b));
    }
}
