using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataModel : MonoBehaviour {

    List<float> CSVFloats = new List<float>();

    // Use this for initialization
    void Start () {

    }

    public void initValues()
    {
        fgCSVReader.LoadFromFile(Application.dataPath + "/sampl1hour.csv", new fgCSVReader.ReadLineDelegate(ReadLineTest));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void ReadLineTest(int line_index, List<string> line)
    {
        CSVFloats.Add(float.Parse(line[1]));
    }

    public List<float> getCSVValues()
    {
        return CSVFloats;
    }
}
