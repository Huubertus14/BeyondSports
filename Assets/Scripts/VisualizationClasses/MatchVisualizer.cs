using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

public class MatchVisualizer : MonoBehaviour
{

    [Header("Prefabs")]
    [SerializeField] private TrackedObjectBahviour trackedObjectPrefab;
    [SerializeField] private BallBehaviour ballPrefab;

    //Cached refs
    private List<TrackedObjectBahviour> trackedObjectList;
    private BallBehaviour ball;

    private int beginFrame = 0;
    private int simulateIndex;
    private bool simulating = false;

    private void Start()
    {
        simulating = false;
        //Create match for first frame
        beginFrame = MatchData.SP.frames[0].GetFrameCount;
        simulateIndex = 0;
        CreateMatch(MatchData.SP.frames[0]);
    }

    public void CreateMatch(Frame frameData)
    {
        trackedObjectList = new List<TrackedObjectBahviour>();

        ball = Instantiate(ballPrefab, transform.position, Quaternion.identity, transform);
        ball.SetValues(frameData.GetBallData);

        Debug.Log("create " + frameData.GetTrackedObjects.Length + " players");

        for (int i = 0; i < frameData.GetTrackedObjects.Length; i++)
        {
            if (frameData.GetTrackedObjects[i] != null)
            {
                if (frameData.GetTrackedObjects[i].TeamName != "-1") //Dont create -1 teams yet, still need to find what there are
                {
                    TrackedObjectBahviour newPlayer = Instantiate(trackedObjectPrefab, Vector3.zero, Quaternion.identity, transform);
                    trackedObjectList.Add(newPlayer);
                    newPlayer.SetInitValues(frameData.GetTrackedObjects[i]);
                }
            }
        }
    }

    public void StartSimulating()
    {
        simulating = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            simulating = !simulating;
        }

        if (simulating)
        {
            try
            {
                if (simulateIndex < MatchData.SP.frames.Count)
                {
                    UpdateFrame(MatchData.SP.frames[simulateIndex]);
                    simulateIndex++;
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }
    }

    public void UpdateFrame(Frame frameData)
    {
        ball.SetValues(frameData.GetBallData);
        for (int i = 0; i < trackedObjectList.Count; i++)
        {
            trackedObjectList[i].SetPosition(frameData.GetTrackedObjects[i]);
        }
    }
}
