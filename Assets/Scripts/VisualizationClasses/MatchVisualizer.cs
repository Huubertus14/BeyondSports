using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

public class MatchVisualizer : SingeltonMonobehaviour<MatchVisualizer>
{

    [Header("Prefabs")]
    [SerializeField] private TrackedObjectBahviour trackedObjectPrefab;
    [SerializeField] private BallBehaviour ballPrefab;

    //Cached refs
    private List<TrackedObjectBahviour> trackedObjectList;
    private BallBehaviour ball;

    private int direction = 0; //simulating direction

    private void Start()
    {
        direction = 0;
        CreateMatch(MatchData.SP.GetFrames[0]);
    }

    /// <summary>
    /// This method is called to create the match and al its players
    /// </summary>
    /// <param the first frame of the match="frameData"></param>
    public void CreateMatch(Frame frameData)
    {
        trackedObjectList = new List<TrackedObjectBahviour>();

        ball = Instantiate(ballPrefab, transform.position, Quaternion.identity, transform);
        ball.SetValues(frameData.GetBallData);

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

    /// <summary>
    /// Called to set the direction/speed of the simulation
    /// 0 = pause
    /// -1 = backwards
    /// 1 = forwards
    /// </summary>
    /// <param The desired directoin="value"></param>
    public void SetDirection(int value)
    {
        direction = value;
    }

    private void Update()
    {
        Controlls();

        if (direction != 0)
        {
            try
            {
                UpdateFrame(MatchData.SP.GetCurrentFrame(direction));
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }
    }

    private void Controlls()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            direction = -1;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            direction = 0;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            direction = 1;
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

    public BallBehaviour GetBall => ball;
}
