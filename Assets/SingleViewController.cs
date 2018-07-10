using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleViewController : MonoBehaviour {
    public LineRenderer lr1, lr2, fatRemainder;
    //TODO:
    /*
     * 1: Take Scaling and Positioning as arguments
     *  -> recalculate each point for the visalization accordingly.
     *  -> Visualize from here
     *  */

    float shiftX, shiftY, scalingFactor;

    int currentDataIndex = 0;
    int fatRemainderStart = 0;
    List<Vector3> allPoints;
    bool printFAT = false;

    public GameObject Eraser;
    public GameObject SlidingPlane;

    public int maxNumberSimultaneousPoints = 500; //Maybe control these two values from main controller
    public int IndexFromWhichToStartFATLine = 150;


    // Use this for initialization
    void Start () {
        //We have to initialize in this way, so we don't connect the starting point (0,0,0) uncontrollably
        lr1.positionCount = 0;
        lr2.positionCount = 0;
        fatRemainder.positionCount = 0;
        allPoints = new List<Vector3>();
    }

    public void initWindow(float _shiftX, float _shiftY, float _scalingFactor)
    {
        shiftX = _shiftX;
        shiftY = _shiftY;
        scalingFactor = _scalingFactor;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        if(allPoints.Count > currentDataIndex)
        {
            if (currentDataIndex < IndexFromWhichToStartFATLine)
            {
                addNextPointToLineRenderer(lr1);
            }
            else if (printFAT)
            {
                addNextPointToLineRenderer(lr2);

            }
            else if (nfmod(currentDataIndex, maxNumberSimultaneousPoints) != 0)
            {
                addPointToFatRemainder();

            }
            else
            {
                printFAT = true;
                lr1.gameObject.SetActive(false);
                fatRemainder.gameObject.SetActive(false);
            }
        }
    }

    void addNextPointToLineRenderer(LineRenderer lr)
    {
        Vector3 curPoint = allPoints[currentDataIndex];
        if (lr.positionCount < maxNumberSimultaneousPoints)
        {
            lr.positionCount += 1;
            lr.SetPosition(lr.positionCount - 1, curPoint);
            //lr.SetPosition(lr.positionCount - 1, new Vector3(curPoint.x + .05f, curPoint.y + .05f, -2.0f));
        }
        else
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

    public void addPoint(float val)
    {
        allPoints.Add(new Vector3(nfmod(currentDataIndex, maxNumberSimultaneousPoints) * 0.01f + shiftX, val + shiftY, 0f) * scalingFactor);
        //currentDataIndex++;
    }

    int nfmod(int a, int b)
    {
        return (int)(a - b * Mathf.Floor(a / b));
    }
}
