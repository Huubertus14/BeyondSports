using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MatchData : MonoBehaviour
{
    private string filePath = "Assets/Resources/match_data.dat";

    public List<Frame> frames;
    public object test;

    private void Start()
    {
        //load data in tracked objects
        frames = new List<Frame>();
        StartCoroutine(LoadData());
    }

    private IEnumerator LoadData()
    {
        StreamReader str = new StreamReader(filePath);
        int x = 0;
        int maxFound = 500;
        while (!str.EndOfStream && x < maxFound)
        {
            string lineOfData = str.ReadLine();
            //Get frame index
            string[] indexString = lineOfData.Split(':');
            int frameCount = int.Parse(indexString[0]);
            //string[] trackedObjects = indexString[1].Split(';');
            string ballData = indexString[2];

           // Debug.Log("Line: " + lineOfData);
            //Debug.Log("Frame: " + frameCount);
            /*foreach (var item in trackedObjects)
            {
                Debug.Log("trObj: " + item);
            }*/
            //Debug.Log("ball: " + ballData);

            frames.Add(new Frame(lineOfData));
            
            x++;
            if (x % 10 == 0)
            {
                //yield return 0;
            }
        }

        str.Close();
        yield return 0;
    }

    
}
