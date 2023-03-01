using UnityEngine;
[CreateAssetMenu(fileName ="IngredientConfiguration_Base", menuName = "UnderCooked/IngredientConfiguration")]
public class IngredientConfiguration : ScriptableObject
{
    public IngredientComponent Ingredient;
    public float ScaleFactor;
    public Vector3 Scale;
}
