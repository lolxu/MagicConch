using UnityEngine;

public class BackgroundScaler : MonoBehaviour
{
    public float minWidth = 5f;
    public float minHeight = 5f;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            ScaleBackground();
        }
    }

    void Update()
    {
        // Re-adjust if the screen resolution changes
        ScaleBackground();
    }

    private void ScaleBackground()
    {
        // Get the current camera's orthographic view dimensions
        float cameraHeight = Camera.main.orthographicSize * 2f;
        float cameraWidth = cameraHeight * Camera.main.aspect;

        // Get the aspect ratio of the sprite
        float spriteAspectRatio = spriteRenderer.sprite.bounds.size.x / spriteRenderer.sprite.bounds.size.y;

        float targetWidth, targetHeight;

        // Calculate the new size based on which dimension needs to expand
        if (cameraWidth / cameraHeight > spriteAspectRatio)
        {
            // Camera is wider than the sprite. Match height.
            targetHeight = cameraHeight;
            targetWidth = targetHeight * spriteAspectRatio;
        }
        else
        {
            // Camera is taller than the sprite. Match width.
            targetWidth = cameraWidth;
            targetHeight = targetWidth / spriteAspectRatio;
        }

        // Apply minimum size constraints
        if (targetWidth < minWidth)
        {
            targetWidth = minWidth;
            targetHeight = targetWidth / spriteAspectRatio;
        }
        
        if (targetHeight < minHeight)
        {
            targetHeight = minHeight;
            targetWidth = targetHeight * spriteAspectRatio;
        }
        
        // Apply the new scale to the sprite
        transform.localScale = new Vector3(targetWidth / spriteRenderer.sprite.bounds.size.x, 
                                           targetHeight / spriteRenderer.sprite.bounds.size.y, 
                                           1f);
    }
}