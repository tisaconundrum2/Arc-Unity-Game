using UnityEngine;
using System.Collections;

public class bgMove : MonoBehaviour {
    public float speed;
    public Vector2 offset;
    private MeshRenderer meshRender;

    private void Update()
    {
        meshRender = GetComponent<MeshRenderer>();
        offset = meshRender.material.mainTextureOffset;
        offset.y += Time.deltaTime * speed;
        meshRender.material.mainTextureOffset = offset;
    }

    public void resetOffset()
    {
        meshRender.material.mainTextureOffset = Vector2.zero;
    }
}
