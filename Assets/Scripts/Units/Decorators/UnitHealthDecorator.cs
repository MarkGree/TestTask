using UnityEngine;

public class UnitHealthDecorator : MonoBehaviour
{
    [SerializeField] private Unit unit;
    [SerializeField] private SkinnedMeshRenderer meshRenderer;

    private void Awake()
    {
        Material material = meshRenderer.sharedMaterials[0];
        meshRenderer.materials[0] = new Material(material);
    }

    private void OnEnable()
    {
        unit.Parameters.OnHealthChanged += DecorateUnit;

        DecorateUnit();
    }

    private void DecorateUnit()
    {
        float lerpValue = unit.Parameters.Health / unit.Parameters.StartHealth;

        meshRenderer.materials[0].color = Color.LerpUnclamped(Color.red, Color.white, lerpValue);
    }
}
