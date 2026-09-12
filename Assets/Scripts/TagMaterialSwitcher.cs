using UnityEngine;

[ExecuteInEditMode]
public class TagMaterialSwitcher : MonoBehaviour
{
    public Material defaultMaterial;
    public Material specialMaterial;

    private string lastTag;

    void OnValidate()
    {
        // Runs when the object is changed in the editor
        if (lastTag != gameObject.tag)
        {
            lastTag = gameObject.tag;
            ApplyMaterialBasedOnTag();
        }
    }

    void ApplyMaterialBasedOnTag()
    {
        var renderer = GetComponent<Renderer>();
        if (renderer == null) return;

        if (gameObject.tag == "Special" && specialMaterial != null)
            renderer.sharedMaterial = specialMaterial;
        else if (defaultMaterial != null)
            renderer.sharedMaterial = defaultMaterial;
    }
}
