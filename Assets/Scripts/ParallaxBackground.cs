using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private ParallaxLayer[] backgroundLayers;

    private Camera mainCamera;

    private float lastCameraPositionX;
    private float cameraHalfWidth;

    private void Awake()
    {
        mainCamera = Camera.main;
        cameraHalfWidth = mainCamera.orthographicSize * mainCamera.aspect;
    }

    private void FixedUpdate()
    {
        float currentCameraPositionX = mainCamera.transform.position.x;
        float distanceToMove = currentCameraPositionX - lastCameraPositionX;
        lastCameraPositionX = currentCameraPositionX;

        float cameraRightEdge = currentCameraPositionX + cameraHalfWidth;
        float cameraLeftEdge = currentCameraPositionX - cameraHalfWidth;

        foreach (var layer in backgroundLayers)
        {
            layer.Move(distanceToMove);
            layer.LoopBackground(cameraRightEdge, cameraLeftEdge);
        }
    }

    private void CalculateBackgroundWidth()
    {
        foreach (var layer in backgroundLayers)
        {
            layer.CalculateImageWidth();
        }
    }
}
