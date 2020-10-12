using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MatchData : SingeltonMonobehaviour<MatchData>
{
    private string filePath = "Assets/Resources/match_data.dat";

    public List<Frame> frames;

    private void Start()
    {
        //load data in tracked objects
        frames = new List<Frame>();
        LoadFirstFrames(500);
    }

    /// <summary>
    /// Used to load the first 500 frames
    /// </summary>
    private void LoadFirstFrames(int amountOfFrames)
    {
        StreamReader str = new StreamReader(filePath);
        int x = 0;
        while (!str.EndOfStream && x < amountOfFrames)
        {
            string lineOfData = str.ReadLine();
            //Get frame index
            string[] indexString = lineOfData.Split(':');
            int frameCount = int.Parse(indexString[0]);
            string ballData = indexString[2];
            frames.Add(new Frame(lineOfData));
        }

        str.Close();
    }


}
