using UnityEngine;

public class RunnerNameShaderController : MonoBehaviour
{

    public Material _material;

    private float offset;
    private const float startOffset = -6f;
    private const float endOffset = 5f;
    private const float revealSpeed = 5f;

    private void OnEnable()
    {
        offset = _material.GetFloat("_DissolveMaskOffset");
        _material.SetFloat("_DissolveMaskOffset", -5);
    }

    private void OnDisable()
    {
        offset = startOffset;
        _material.SetFloat("_DissolveMaskOffset", -5);
    }

    private void Update()
    {
        if (offset < endOffset)
        {
            offset += Time.deltaTime * revealSpeed;
            _material.SetFloat("_DissolveMaskOffset", offset);
        }
            
    }
}
