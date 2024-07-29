using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class blackFog : MonoBehaviour
{
    public enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }

    public Direction selectedShader = Direction.Up;

    public Shader left;
    public Shader right;
    public Shader up;
    public Shader down;

    private Image image;
    private Material material;

    void Awake()
    {
        left = Shader.Find("Shader Graphs/black Left");
        right = Shader.Find("Shader Graphs/black Right");
        up = Shader.Find("Shader Graphs/black Up");
        down = Shader.Find("Shader Graphs/black Down");

        image = GetComponent<Image>();
        // Initialize with the first shader by default
        ApplySelectedShader();
    }

    public void ApplySelectedShader()
    {
        switch (selectedShader)
        {
            case Direction.Left:
                SetShader(left);
                break;
            case Direction.Right:
                SetShader(right);
                break;
            case Direction.Up:
                SetShader(up);
                break;
            case Direction.Down:
                SetShader(down);
                break;
        }
    }

    public void SetShader(Shader shader)
    {
        if (image == null)
        {
            image = GetComponent<Image>();
        }

        if (shader == null)
        {
            Debug.LogError("Shader is null!");
            return;
        }

        // Create a new material with the selected shader
        if (material != null)
        {
#if UNITY_EDITOR
            if (Application.isPlaying)
            {
                Destroy(material);
            }
            else
            {
                DestroyImmediate(material);
            }
#else
			Object.Destroy(material);
#endif
        }
        material = new Material(shader);
        image.material = material;
        // Calculate and set the _Segments property
        UpdateSegments();
    }

    public void UpdateSegments()
    {
        if (material == null)
        {
            Debug.LogError("Material is not assigned!");
            return;
        }

        // Calculate the aspect ratio of the image
        RectTransform rectTransform = GetComponent<RectTransform>();
        float aspectRatio = rectTransform.rect.width / rectTransform.rect.height;
        if (selectedShader == Direction.Left || selectedShader == Direction.Right)
        {
            aspectRatio = rectTransform.rect.height / rectTransform.rect.width;
        }

        // Update the _Segments property in the material
        material.SetFloat("_Segments", aspectRatio);
    }

    private void OnValidate()
    {
        ApplySelectedShader();
    }

    private void OnDestroy()
    {
        if (image != null)
        {
            image.material = null;  //This makes so that when the component is removed, the UI material returns to null
        }

        Destroy(material);
        image = null;
        material = null;
    }

}