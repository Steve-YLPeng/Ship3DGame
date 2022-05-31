using UnityEngine;
using System.Collections;
 
public class ImageProjector : MonoBehaviour
{
    public Texture ProjectionTexture = null;
    public Camera camera = null;
    public GameObject[] ProjectionReceivers = null;
    public float Angle = 0.0f;
 
    Vector4 Vec3ToVec4(Vector3 vec3, float w)
    {
        return new Vector4(vec3.x, vec3.y, vec3.z, w);
    }
 
    // Use this for initialization
    void Start()
    {
 
    }
 
    // Update is called once per frame
    void Update()
    {
        Matrix4x4 matProj = Matrix4x4.Perspective(this.camera.fieldOfView, 1, this.camera.nearClipPlane, this.camera.farClipPlane);
 
        Matrix4x4 matView = Matrix4x4.identity;
        matView = Matrix4x4.TRS(Vector3.zero, this.camera.transform.rotation, Vector3.one);
 
        float x = Vector3.Dot(this.camera.transform.right,      -this.camera.transform.position);
        float y = Vector3.Dot(this.camera.transform.up,         -this.camera.transform.position);
        float z = Vector3.Dot(this.camera.transform.forward,    -this.camera.transform.position);
 
        matView.SetRow(3, new Vector4(x, y, z, 1));
 
        Matrix4x4 LightViewProjMatrix = matView * matProj;
 
        if (ProjectionReceivers == null || ProjectionReceivers.Length <= 0)
        {
            return;
        }
 
        foreach (GameObject imageReceiver in ProjectionReceivers)
        {
            ProjectionTexture.wrapMode = TextureWrapMode.Clamp;
            imageReceiver.GetComponent<Renderer>().sharedMaterial.SetTexture("_ShadowMap", ProjectionTexture);
            imageReceiver.GetComponent<Renderer>().sharedMaterial.SetMatrix("_LightViewProj", LightViewProjMatrix);
            imageReceiver.GetComponent<Renderer>().sharedMaterial.SetFloat("_Angle", Angle);
        }
    }
 
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
 
        Gizmos.DrawLine(this.camera.transform.position, this.camera.transform.position + (this.camera.transform.forward * 100.0f));
    }
}
 