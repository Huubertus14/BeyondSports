using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class MatchData : SingletonMonoBehaviour<MatchData>
{
    private string filePath = "Assets/Resources/match_data.dat";

    private List<Dictionary<int, Frame>> subDict;

    private int amountOfSubDicts; //the amount of frames that are bufferd on each side

    [SerializeField] private int currentFrameIndex;
    [SerializeField] private int firstFrame;
    [SerializeField] private int lastFrame;

    private Frame lastGivenFrame;
    StreamReader streamReader;

    private void Start()
    {
        amountOfSubDicts = 10;
        currentFrameIndex = 0;
        firstFrame = 0;
        lastFrame = int.MaxValue;

        streamReader = new StreamReader(filePath);

        subDict = new List<Dictionary<int, Frame>>();
        for (int i = 0; i < amountOfSubDicts; i++)
        {
            subDict.Add(new Dictionary<int, Frame>());
        }

        StartCoroutine(LoadAllFrames());
    }

    private IEnumerator LoadAllFrames()
    {
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
                currentFrameIndex = firstFrame;
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

        GameManager.SP.GetSimulationController.CreateSlider(firstFrame,lastFrame, currentFrameIndex);
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
        currentFrameIndex = Mathf.Clamp(currentFrameIndex, firstFrame, lastFrame);
        currentFrameIndex += direction;
        GameManager.SP.GetSimulationController.UpdateSlider(currentFrameIndex);
        if (GetDictionairy(currentFrameIndex).TryGetValue(currentFrameIndex, out Frame fr))
        {
            lastGivenFrame = fr;
            return fr;
        }
        else
        {
            Debug.Log("Frame does not exist " + currentFrameIndex + " start buffering");
            return lastGivenFrame;
        }

    }

    public int GetIndex => currentFrameIndex;

    public void SetIndex(int newIndex)
    {
        currentFrameIndex = newIndex;
    }
}
