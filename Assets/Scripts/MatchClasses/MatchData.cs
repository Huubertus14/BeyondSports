using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class MatchData : SingeltonMonobehaviour<MatchData>
{
    private string filePath = "Assets/Resources/match_data.dat";
    //private List<Frame> frames; //500 frames need to be used

    [SerializeField] private Frame[] frames;
   // private List<Frame> frames;
    private int bufferSize; //the amount of frames that are bufferd on eich side

    private bool isLoadingFrames;

    [SerializeField] private int currentFrameIndex;

    private void Start()
    {
        bufferSize = 50;
        isLoadingFrames = false;
        currentFrameIndex = 0;

        //load data in tracked objects
        // frames = new List<Frame>();
        frames = new Frame[bufferSize * 2];
        StartCoroutine(LoadFirstFrames(bufferSize * 2));
    }

    /// <summary>
    /// Used to load the first 500 frames
    /// </summary>
    private IEnumerator LoadFirstFrames(int amountOfFrames)
    {
        isLoadingFrames = true;
        StreamReader str = new StreamReader(filePath);
        int lineIndex = 0;

        while (!str.EndOfStream && lineIndex < amountOfFrames)
        {
            string lineOfData = str.ReadLine();
            frames[lineIndex] = new Frame(lineOfData);

            lineIndex++;
            if (lineIndex % 25 == 0)
            {
                yield return 0;
            }
        }

        str.Close();
        isLoadingFrames = false;
        yield return 0;
    }

    private IEnumerator LoadFrames(int startIndex, int direction, int previousFrameIndex, int amountOfFrames)
    {
        StreamReader str = new StreamReader(filePath);
        int lineChecks = 0;

        int index = startIndex;

        while (!str.EndOfStream && lineChecks < amountOfFrames  * 3)
        {
            string lineOfData = str.ReadLine();
            Frame tempFrame = new Frame(lineOfData);

            Debug.Log("Check: " + tempFrame.GetFrameCount + "/" + (previousFrameIndex + direction));
            if (tempFrame.GetFrameCount == (previousFrameIndex + direction)) //search for the right line of data
            {
                Debug.Log("Overwrite at: " + index + " /  " + (previousFrameIndex + direction));
                frames[index] = tempFrame;
                index = GetNextIndex(index, direction);
            }

            lineChecks++;
            if (lineChecks % 50 == 0)
            {
                yield return 0;
            }

        }

        yield return 0;
    }

    private bool IsFrameInOrder(Frame current, Frame next, int direction)
    {
        //Debug.Log("checked: " + current.GetFrameCount + " to " + next.GetFrameCount +" Direction: " + direction);
        return ((current.GetFrameCount + direction) == next.GetFrameCount);
    }

    private int GetNextIndex(int currentIndex, int direction)
    {
        if (currentIndex + direction < 0)
        {
            return frames.Length - 1;
        }
        else if (currentIndex + direction >= frames.Length)
        {
            return 0;
        }
        else
        {
            return currentIndex + direction;
        }
    }

    public Frame GetCurrentFrame(int direction)
    {
        currentFrameIndex = Mathf.Clamp(currentFrameIndex, 0, frames.Length - 1);

        //Check if the next index is good
        if (!IsFrameInOrder(frames[currentFrameIndex], frames[GetNextIndex(currentFrameIndex, direction)], direction))
        {
            Debug.Log("Not in order");
            // Debug.Break();
            StopAllCoroutines();
            StartCoroutine(LoadFrames(GetNextIndex(currentFrameIndex, direction), direction, frames[currentFrameIndex].GetFrameCount, bufferSize));
        }

        Frame toReturn = frames[currentFrameIndex];
        currentFrameIndex = GetNextIndex(currentFrameIndex, direction);
        return toReturn;
    }

    public Frame[] GetFrames
    {
        get { return frames; }
    }

    public int GetIndex => currentFrameIndex;
}
