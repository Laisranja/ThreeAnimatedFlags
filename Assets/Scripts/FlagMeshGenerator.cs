
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class FlagMeshGenerator : MonoBehaviour
{
    public int widthSegments = 50;
    public int heightSegments = 25;

    void Start()
    {
        GenerateMesh();
    }

    void GenerateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.name = "FlagMesh";

        Vector3[] vertices = new Vector3[(widthSegments + 1) * (heightSegments + 1)];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[widthSegments * heightSegments * 6];

        int index = 0;

        for (int z = 0; z <= heightSegments; z++)
        {
            for (int x = 0; x <= widthSegments; x++)
            {
                float u = (float)x / widthSegments;
                float v = (float)z / heightSegments;

                // Largura no X e altura no Z local
                vertices[index] = new Vector3(
                    u * 10f - 5f,
                    0f,
                    v * 5f - 2.5f
                );

                uv[index] = new Vector2(u, v);
                index++;
            }
        }

        int triangleIndex = 0;

        for (int z = 0; z < heightSegments; z++)
        {
            for (int x = 0; x < widthSegments; x++)
            {
                int i = z * (widthSegments + 1) + x;

                triangles[triangleIndex++] = i;
                triangles[triangleIndex++] = i + widthSegments + 1;
                triangles[triangleIndex++] = i + 1;

                triangles[triangleIndex++] = i + 1;
                triangles[triangleIndex++] = i + widthSegments + 1;
                triangles[triangleIndex++] = i + widthSegments + 2;
            }
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        GetComponent<MeshFilter>().mesh = mesh;
    }
}
