using UnityEngine;
using Random = UnityEngine.Random;

public class RandomHueShader : MonoBehaviour
{
    private static readonly int RandomHue = Shader.PropertyToID("_Random_Hue");

    private void Awake()
    {
        GetComponent<Renderer>().material.SetInt(RandomHue, Mathf.RoundToInt(Random.Range(0, 360)));
    }
}
