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

    private Stack<Frame> previousFrames;
    // private List<Frame> frames;
    private int bufferSize; //the amount of frames that are bufferd on eich side
    private bool isLoadingFrames;
    [SerializeField] private int currentFrameIndex;

    [SerializeField] private int lastCheckedLine;

    StreamReader streamReader;

    private void Start()
    {
        lastCheckedLine = 0;
        bufferSize = 50;
        isLoadingFrames = false;
        currentFrameIndex = 0;

        streamReader = new StreamReader(filePath);
        //load data in tracked objects
        // frames = new List<Frame>();
        frames = new Frame[bufferSize * 2];
        previousFrames = new Stack<Frame>();
        StartCoroutine(LoadFirstFrames(bufferSize * 2));
    }

    /// <summary>
    /// Used to load the first 500 frames
    /// </summary>
    private IEnumerator LoadFirstFrames(int amountOfFrames)
    {
        isLoadingFrames = true;
        // StreamReader str = new StreamReader(filePath);
        int lineIndex = 0;

        while (!streamReader.EndOfStream && lineIndex < amountOfFrames)
        {
            string lineOfData = streamReader.ReadLine();
            frames[lineIndex] = new Frame(lineOfData);
            lastCheckedLine++;
            lineIndex++;
            if (lineIndex % 25 == 0)
            {
                yield return 0;
            }
        }

        // str.Close();
        isLoadingFrames = false;
        yield return 0;
    }

    private IEnumerator LoadFrames(int startIndex, int direction, int previousFrameIndex, int amountOfFrames)
    {
        isLoadingFrames = true;
        //StreamReader str = new StreamReader(filePath);
        int lineChecks = 0;

        int index = startIndex;

        while (!streamReader.EndOfStream && lineChecks < amountOfFrames)
        {
            string lineOfData = streamReader.ReadLine();
            Frame tempFrame = new Frame(lineOfData);

            Debug.Log("Check: " + tempFrame.GetFrameCount + "/" + (previousFrameIndex + direction));
            //if (tempFrame.GetFrameCount == (previousFrameIndex + direction)) //search for the right line of data
            //{
            Debug.Log("Overwrite at: " + index + " /  " + (previousFrameIndex + direction));
            frames[index] = tempFrame;
            index = GetNextIndex(index, direction);
            previousFrameIndex = tempFrame.GetFrameCount;
            // }

            lineChecks++;
            if (lineChecks % 50 == 0)
            {
                yield return 0;
            }

        }

        isLoadingFrames = false;
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

            if (!isLoadingFrames)
            {
                StopAllCoroutines();
                StartCoroutine(LoadFrames(GetNextIndex(currentFrameIndex, direction), direction, frames[currentFrameIndex].GetFrameCount, bufferSize));
            }
        }


        Frame toReturn = frames[currentFrameIndex];
        previousFrames.Push(toReturn);
        currentFrameIndex = GetNextIndex(currentFrameIndex, direction);
        return toReturn;
    }

    public Frame[] GetFrames
    {
        get { return frames; }
    }

    public Frame GetFrame => frames[currentFrameIndex];

    public int GetIndex => currentFrameIndex;
}
