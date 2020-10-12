using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class MatchData : SingletonMonoBehaviour<MatchData>
{
    private string filePath = "Assets/Resources/match_data.dat";

    private List<Dictionary<int, Frame>> subDict;

    private int amountOfSubDicts;
    private int firstFrame;
    private int lastFrame;

    private Frame lastGivenFrame; //work around to keer simulation going when there is a empty or wrong frame

    private void Start()
    {
        amountOfSubDicts = 10;
        GetIndex = 0;
        firstFrame = 0;
        lastFrame = int.MaxValue;

        subDict = new List<Dictionary<int, Frame>>();
        for (int i = 0; i < amountOfSubDicts; i++)
        {
            subDict.Add(new Dictionary<int, Frame>());
        }

        StartCoroutine(LoadAllFrames());
    }

    private IEnumerator LoadAllFrames()
    {
        StreamReader streamReader = new StreamReader(filePath);
        int lineIndex = 0;
        Frame tempFrame = null;
        while (!streamReader.EndOfStream)
        {
            string lineOfData = streamReader.ReadLine();

            tempFrame = new Frame(lineOfData);

            GetDictionairy(tempFrame.GetFrameCount).Add(tempFrame.GetFrameCount, tempFrame);

            if (firstFrame == 0)
            {
                lastGivenFrame = tempFrame;
                firstFrame = tempFrame.GetFrameCount;
                GetIndex = firstFrame;
                MatchVisualizer.SP.CreateMatch(GetCurrentFrame());
            }

            lineIndex++;
            if (lineIndex % 50 == 0)
            {
                yield return 0;
            }
        }

        lastFrame = tempFrame.GetFrameCount;
        if (lastFrame == 0)
        {
            Debug.LogWarning("Last frame not found");
        }

        GameManager.SP.GetSimulationController.CreateSlider(firstFrame, lastFrame, GetIndex);
        streamReader.Close();
        yield return 0;
    }

    private Dictionary<int, Frame> GetDictionairy(int index)
    {
        //Get the last number of the index
        int returnIndex = index % amountOfSubDicts;
        return subDict[returnIndex];
    }

    public Frame GetCurrentFrame(int direction = 0)
    {
        GetIndex = Mathf.Clamp(GetIndex, firstFrame, lastFrame);
        GetIndex += direction;
        GameManager.SP.GetSimulationController.UpdateSlider(GetIndex);
        if (GetDictionairy(GetIndex).TryGetValue(GetIndex, out Frame fr))
        {
            lastGivenFrame = fr;
            return fr;
        }
        else
        {
            Debug.Log("Frame does not exist " + GetIndex + " start buffering");
            return lastGivenFrame;
        }

    }

    public int GetIndex { get; private set; }

    public void SetIndex(int newIndex)
    {
        GetIndex = newIndex;
    }
}
