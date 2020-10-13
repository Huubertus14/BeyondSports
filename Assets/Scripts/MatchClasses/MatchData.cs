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

    /// <summary>
    /// Start loading all frames and create the match after the first few frames
    /// </summary>
    /// <returns></returns>
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
                GameManager.SP.GetMatchVisualizer.CreateMatch(GetCurrentFrame());
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

    /// <summary>
    /// Call this to get the right dictionairy for the given frame
    /// </summary>
    /// <param FrameCount="index"></param>
    /// <returns>The Dictionairy where the Frame is in</returns>
    private Dictionary<int, Frame> GetDictionairy(int index)
    {
        //Get the last number of the index
        int returnIndex = index % amountOfSubDicts;
        return subDict[returnIndex];
    }

    /// <summary>
    /// Get the current frame and increase the index in the direction
    /// 0 = still
    /// 1  = forward
    /// -1 = backwards
    /// </summary>
    /// <param name="direction"></param>
    /// <returns>The frame of the current index</returns>
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
            Debug.Log("Frame does not exist " + GetIndex);
            return lastGivenFrame;
        }

    }

    /// <summary>
    /// Get the current frame Index
    /// </summary>
    public int GetIndex { get; private set; }

    /// <summary>
    /// Set the current frame index, used to skip ahead and back in the simulation
    /// </summary>
    /// <param the new Index="newIndex"></param>
    public void SetIndex(int newIndex)
    {
        GetIndex = newIndex;
    }
}
