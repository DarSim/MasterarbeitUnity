using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPoint {
    Vector3 coordinates;
    int lineRendererIndex;

    public DataPoint(Vector3 coords, int LR)
    {
        Coordinates = coords;
        LineRendererIndex = LR;
    }

    public Vector3 Coordinates
    {
        get
        {
            return coordinates;
        }

        set
        {
            coordinates = value;
        }
    }

    public int LineRendererIndex
    {
        get
        {
            return lineRendererIndex;
        }

        set
        {
            lineRendererIndex = value;
        }
    }
}
