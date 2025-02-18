using Godot;
using System;
using System.Collections.Generic;

public partial class BoxMesh : MeshInstance3D
{
    [Export] float Size;

    public override void _EnterTree()
    {
        GenerateBox();
    }

    public void GenerateBox() {
        var surfaceArray = new Godot.Collections.Array();
        surfaceArray.Resize((int)Mesh.ArrayType.Max);

        var verts = new List<Vector3>();
        var uvs = new List<Vector2>();
        var normals = new List<Vector3>();
        var indeces = new List<int>();

        verts.AddRange(new Vector3[]{
            new Vector3(1, 1, 1) * Size,
            new Vector3(-1, 1, 1) * Size,
            new Vector3(-1, -1, 1) * Size,
            new Vector3(1, -1, 1) * Size,
            new Vector3(1, 1, -1) * Size,
            new Vector3(-1, 1, -1) * Size,
            new Vector3(-1, -1, -1) * Size,
            new Vector3(1, -1, -1) * Size
        });

        uvs.AddRange(new Vector2[]{
            new Vector2(1, 1),
            new Vector2(0, 1),
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(1, 1),
            new Vector2(0, 1),
            new Vector2(0, 0),
            new Vector2(1, 0)
        });

        normals.AddRange(new Vector3[]{
            new Vector3(0, 0, 1),
            new Vector3(0, 0, 1),
            new Vector3(0, 0, 1),
            new Vector3(0, 0, 1),
            new Vector3(0, 0, -1),
            new Vector3(0, 0, -1),
            new Vector3(0, 0, -1),
            new Vector3(0, 0, -1)
        });

        indeces.AddRange(new int[] {
            0, 1, 2,
            0, 2, 3,
            2, 6, 3,
            3, 6, 7,
            1, 5, 2,
            2, 5, 6,
            0, 3, 7,
            0, 7, 4,
            5, 7, 6,
            4, 7, 5,
            1, 4, 5,
            0, 4, 1
        });


        surfaceArray[(int)Mesh.ArrayType.Vertex] = verts.ToArray();
        surfaceArray[(int)Mesh.ArrayType.TexUV] = uvs.ToArray();
        surfaceArray[(int)Mesh.ArrayType.Normal] = normals.ToArray();
        surfaceArray[(int)Mesh.ArrayType.Index] = indeces.ToArray();

        if(Mesh is ArrayMesh arrMesh) {
            arrMesh.AddSurfaceFromArrays(Mesh.PrimitiveType.Triangles, surfaceArray);
        }
    }
}
