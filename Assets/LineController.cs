using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour {
    public LineRenderer lr1, lr2, fatRemainder;
    public GameObject Eraser;
    public GameObject SlidingPlane;
    public int maxNumberSimultaneousPoints = 500;
    public int IndexFromWhichToStartFATLine = 150;

    public float xShift = -12f;
    public float yShift = 5f;

    public float scaling = 2f;

    bool printFAT = false;
    DataModel model;

    int currentDataIndex = 0;
    int fatRemainderStart = 0;
    List<Vector3> allPoints;

	// Use this for initialization
	void Start () {
        model = FindObjectOfType<DataModel>();
        model.initValues();
        allPoints = new List<Vector3>();

        List<float> vals = model.getCSVValues();
        for (int i = 0; i < vals.Count; i++)
        {
            allPoints.Add(new Vector3(nfmod(i, maxNumberSimultaneousPoints) * 0.01f + xShift, vals[i] + yShift, 0f) * scaling);
        }

        //We have to initialize in this way, so we don't connect the starting point (0,0,0) uncontrollably
        lr1.positionCount = 0;
        lr2.positionCount = 0;
        fatRemainder.positionCount = 0;
        /*
        lr1.useWorldSpace = true;
        lr2.useWorldSpace = true;
        fatRemainder.useWorldSpace = true;
        */
        

    }
	
	// Use Fixed Update instead, as we want stable update frequency
	void Update () {
    }

    void FixedUpdate()
    {
        if(currentDataIndex < IndexFromWhichToStartFATLine)
        {
            addNextPointToLineRenderer(lr1);
        }
        else if (printFAT)
        {
            addNextPointToLineRenderer(lr2);

        } else if(nfmod(currentDataIndex, maxNumberSimultaneousPoints) != 0)
        {
            addPointToFatRemainder();
        
        } else
        {
            printFAT = true;
            lr1.gameObject.SetActive(false);
            fatRemainder.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Redundant, but didn't want to delete it. Simply sets all points on a specified LineRenderer
    /// </summary>
    /// <param name="linePoints"></param>
    /// <param name="lrToSet"></param>
    void setLinePoints(List<Vector3> linePoints, LineRenderer lrToSet)
    {
        lrToSet.positionCount = linePoints.Count;
        lrToSet.SetPositions(linePoints.ToArray());
    }

    /// <summary>
    /// Takes one LineRenderer as input and uses the currentDataIndex that is an attribute in this class.
    /// Takes the point on that index, that we have generated in allPoints and sets it as the next point
    /// in the specified LineRenderer. If the number of points exceeds maxNumberSimultaneousPoints, 
    /// the new point is put at the start etc.
    /// </summary>
    /// <param name="lr"></param>
    void addNextPointToLineRenderer(LineRenderer lr)
    {
        if(currentDataIndex > allPoints.Count)
        {
            Debug.LogWarning("NO MORE POINTS");
            return;
        }

        Vector3 curPoint = allPoints[currentDataIndex];
        if (lr.positionCount < maxNumberSimultaneousPoints)
        {
            lr.positionCount += 1;
            lr.SetPosition(lr.positionCount-1, curPoint);
            //lr.SetPosition(lr.positionCount - 1, new Vector3(curPoint.x + .05f, curPoint.y + .05f, -2.0f));
        } else
        {
            int modIndex = nfmod(currentDataIndex, maxNumberSimultaneousPoints);
            Debug.Log("MOD INDEX: " + modIndex);

            lr.SetPosition(modIndex, curPoint);
            //lr.SetPosition(modIndex, new Vector3(curPoint.x + .05f, curPoint.y + .05f, -2.0f));

        }

        //Eraser.transform.position = new Vector3(curPoint.x + .05f, curPoint.y, -1f); //TODO: Fix layers, sometimes Line seems to be in front, sometimes in back. Why?
        Eraser.transform.position = curPoint + Vector3.back * 0.00000001f; //We have to put the bar in front of the LineRenderers in order to obscure them
        //Eraser.transform.position = new Vector3(curPoint.x + .05f, 0.0f, 0.0f);
        //SlidingPlane.transform.position = new Vector3(curPoint.x + .05f, 0.0f, -1.0f);

        currentDataIndex++;
    }

    /// <summary>
    /// Use this when you want to start a fat line in the middle of the window
    /// </summary>
    void addPointToFatRemainder()
    {
        if (currentDataIndex > allPoints.Count)
        {
            Debug.LogWarning("NO MORE POINTS");
            return;
        }
        Vector3 curPoint = allPoints[currentDataIndex];

        fatRemainder.positionCount += 1;
        fatRemainder.SetPosition(fatRemainder.positionCount - 1, curPoint);
        //fatRemainder.SetPosition(fatRemainder.positionCount-1, new Vector3(curPoint.x + .05f, curPoint.y + .05f, -2.0f));

        //Eraser.transform.position = new Vector3(curPoint.x + .05f, 0.0f, -1.0f); //TODO: Fix layers, sometimes Line seems to be in front, sometimes in back. Why?
        Eraser.transform.position = curPoint + Vector3.back * 0.00000001f;
        //SlidingPlane.transform.position = curPoint + Vector3.back * 0.00000001f;
        //SlidingPlane.transform.position = new Vector3(curPoint.x + .05f, 0.0f, -1.0f);

        currentDataIndex++;
    }

    /// <summary>
    /// Normal % operator doesn't work as I want it to. Ermahgerd
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    int nfmod(int a, int b)
    {
        return (int)(a - b * Mathf.Floor(a / b));
    }
}
