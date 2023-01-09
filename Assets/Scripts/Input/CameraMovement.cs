using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
  public Vector3 cameraOffset;
  public float cameraSpeed = 0.01f;
  public List<Transform> targets;
  private Vector3 velocity;
  public float minZoom = 4f;
  public float maxZoom = 6f;
  public float zoomLimiter = 50f;
  private Camera cam;
  private float newZoom;
  private float lerpZoom;
  private float zoom;

  void Awake()
  {
    targets.Clear();
    cam = GetComponent<Camera>();
  }

  void Start()
  {
    SetTargets();
  }

  void FixedUpdate()
  {
    if (targets.Count == 0)
      return;

    CameraMove();
    CameraZoom();
  }

  private void CameraZoom()
  {
    newZoom = Mathf.MoveTowards(minZoom, maxZoom, GetDistance() / zoomLimiter);
    lerpZoom = Mathf.Lerp(cam.orthographicSize, newZoom, cameraSpeed / 2);
    cam.orthographicSize = lerpZoom;
  }

  private void CameraMove()
  {
    Vector3 centerPoint = GetCenterPoint();
    Vector3 newPosition = centerPoint + cameraOffset;
    Vector3 lerpPosition = Vector3.Lerp(transform.position, newPosition, cameraSpeed);
    transform.position = lerpPosition;
  }

  float GetDistance()
  {
    var bounds = new Bounds(targets[0].position, Vector3.zero);
    if (targets.Count == 2)
    {
      bounds.Encapsulate(targets[1].position);
    }
    return bounds.size.magnitude;
  }

  Vector3 GetCenterPoint()
  {
    if (targets.Count == 1)
    {
      return targets[0].position;
    }

    var bounds = new Bounds(targets[0].position, Vector3.zero);
    if (targets.Count == 2)
    {
      bounds.Encapsulate(targets[1].position);
    }
    return bounds.center;
  }
  public void SetTargets()
  {
    if (GameObject.FindWithTag("RedPlayer") != null && !targets.Contains(GameObject.FindWithTag("RedPlayer").GetComponent<Transform>()))
    {
      targets.Add(GameObject.FindWithTag("RedPlayer").GetComponent<Transform>());
    }
    if (GameObject.FindWithTag("BluePlayer") != null && !targets.Contains(GameObject.FindWithTag("BluePlayer").GetComponent<Transform>()))
    {
      targets.Add(GameObject.FindWithTag("BluePlayer").GetComponent<Transform>());
    }
    else
    {
      targets.Clear();
      return;
    }
  }

}