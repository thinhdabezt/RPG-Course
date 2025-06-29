using UnityEngine;

[System.Serializable]
public class ParallaxLayer
{
    [SerializeField] private Transform background;
    [SerializeField] private float parallaxMultiplier;
    [SerializeField] private float imageOffset;

    private float imageHalfWidth;
    private float imageFullWidth;

    public void Move(float distanceToMove)
    {
        if (background != null)
        {
            background.position += new Vector3(distanceToMove * parallaxMultiplier, 0);
        }
    }

    public void LoopBackground(float cameraRightEdge, float cameraLeftEdge)
    {
        if (background != null)
        {
            float imageRightEdge = (background.position.x + imageHalfWidth) - imageOffset;
            float imageLeftEdge = (background.position.x - imageHalfWidth) + imageOffset;

            if (imageRightEdge < cameraLeftEdge)
            {
                background.position += Vector3.right * imageFullWidth;
            }
            else if (imageLeftEdge > cameraRightEdge)
            {
                background.position += Vector3.left * imageFullWidth;
            }
        }
    }

    public void CalculateImageWidth()
    {
        imageFullWidth = background.GetComponent<SpriteRenderer>().bounds.size.x;
        imageHalfWidth = imageFullWidth / 2f;
    }
}
