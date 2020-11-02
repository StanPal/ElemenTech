using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashCamera : MonoBehaviour
{
    // Approximate time for the camera to refocus
    [SerializeField]
    private float m_DampTime = 0.2f;
    // Space between the top/bottom most target and the screen edge.
    [SerializeField]
    private float m_ScreenEdgeBuffer = 4f;
    // The smallest orthographic size the camera can be.
    [SerializeField]
    private float m_MinSize = 6.5f;
    PlayerManager playerManager;

    private Camera mCamera;
    // Reference speed for the smooth damping of the orthographic size.
    [SerializeField]
    private float m_ZoomSpeed;
    // Reference velocity for the smooth damping of the position.
    [SerializeField]
    private Vector3 m_MoveVelocity;
    // The position the camera is moving towards.
    [SerializeField]
    private Vector3 m_DesiredPosition;


    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        mCamera = GetComponent<Camera>();
    }


    private void FixedUpdate()
    {
        Move();
        Zoom();
    }


    private void Move()
    {
        // Find the average position of the targets.
        FindAveragePosition();

        // Smoothly transition to that position.
        transform.position = Vector3.SmoothDamp(transform.position, m_DesiredPosition, ref m_MoveVelocity, m_DampTime);
    }


    private void FindAveragePosition()
    {
        Vector3 averagePos = new Vector3();
        int numTargets = 0;

        // Go through all the targets and add their positions together.
        for (int i = 0; i < playerManager.mPlayersList.ToArray().Length; i++)
        {
            // If the target isn't active, go on to the next one.
            if (!playerManager.mPlayersList[i].gameObject.activeSelf)
                continue;

            // Add to the average and increment the number of targets in the average.
            averagePos += playerManager.mPlayersList[i].transform.position;
            numTargets++;
        }

        // If there are targets divide the sum of the positions by the number of them to find the average.
        if (numTargets > 0)
            averagePos /= numTargets;

        // Keep the same y value.
        averagePos.y = transform.position.y;

        // The desired position is the average position;
        m_DesiredPosition = averagePos;
    }


    private void Zoom()
    {
        // Find the required size based on the desired position and smoothly transition to that size.
        float requiredSize = FindRequiredSize();
        mCamera.orthographicSize = Mathf.SmoothDamp(mCamera.orthographicSize, requiredSize, ref m_ZoomSpeed, m_DampTime);
    }


    private float FindRequiredSize()
    {
        // Find the position the camera rig is moving towards in its local space.
        Vector3 desiredLocalPos = transform.InverseTransformPoint(m_DesiredPosition);

        // Start the camera's size calculation at zero.
        float size = 0f;

        for (int i = 0; i < playerManager.mPlayersList.ToArray().Length; i++)
        {

            if (!playerManager.mPlayersList[i].gameObject.activeSelf)
                continue;

            // find the position of the target in the camera's local space.
            Vector3 targetLocalPos = transform.InverseTransformPoint(playerManager.mPlayersList[i].transform.position);

            // Find the position of the target from the desired position of the camera's local space.
            Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;

            // Choose the largest out of the current size and the distance of the player 'up' or 'down' from the camera.
            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.y));

            // Choose the largest out of the current size and the calculated size based on the player being to the left or right of the camera.
            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.x) / mCamera.aspect);
        }

        // Add the edge buffer to the size.
        size += m_ScreenEdgeBuffer;

        // Make sure the camera's size isn't below the minimum.
        size = Mathf.Max(size, m_MinSize);

        return size;
    }


    public void SetStartPositionAndSize()
    {
        // Find the desired position.
        FindAveragePosition();

        // Set the camera's position to the desired position without damping.
        transform.position = m_DesiredPosition;

        // Find and set the required size of the camera.
        mCamera.orthographicSize = FindRequiredSize();
    }
}
