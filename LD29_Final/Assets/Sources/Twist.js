// This script is placed in public domain. The author takes no responsibility for any possible harm.

// twist a mesh by this amount
var twist = 1.0;

var inputSensitivity = 1.5;

private var baseVertices : Vector3[];
private var baseNormals : Vector3[];

function Update ()
{
	var mesh : Mesh = GetComponent(MeshFilter).mesh;
	
	if (baseVertices == null)
		baseVertices = mesh.vertices;
	if (baseNormals == null)
		baseNormals = mesh.normals;
		
	var vertices = new Vector3[baseVertices.Length];
	var normals = new Vector3[baseVertices.Length];
	
	for (var i=0;i<vertices.Length;i++)
	{
		vertices[i] = DoTwist(baseVertices[i], baseVertices[i].y * twist);
		normals[i] = DoTwist(baseNormals[i], baseVertices[i].y * twist);
	}
	
	mesh.vertices = vertices;
	mesh.normals = vertices;
	
	mesh.RecalculateNormals();
	mesh.RecalculateBounds();
}

function DoTwist( pos : Vector3, t : float )
{
	var st = Mathf.Sin(t);
	var ct = Mathf.Cos(t);
	new_pos = Vector3.zero;
	
	new_pos.x = pos.x*ct - pos.y*st;
	new_pos.y = pos.x*st + pos.y*ct;
	new_pos.z = pos.z;

	return new_pos;
}
