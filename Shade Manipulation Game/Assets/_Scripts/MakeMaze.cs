using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeMaze : MonoBehaviour {

	[SerializeField] private float seed;
	//create slider from 0 to 1
	[Range(0,1)] public float mazeDensity;

	private int mazeWidth;
	private int mazeDepth;

	// Use this for initialization
	void Start () 
	{
		//get size of floor
		mazeWidth =(int) transform.lossyScale.x;
		mazeDepth = (int)transform.lossyScale.z;
		MakeWalls (mazeWidth, mazeDepth);
		GenerateMaze (mazeWidth, mazeDepth, mazeDensity);
	}
	//makes the perimeter
	void MakeWalls(int width, int depth)
	{
		//moving to pereimeter to move from center, which is halfway in the center
		width = width / 2;
		depth = depth / 2;

		//
		for (int x = -width; x <= width; x++) {
			GameObject wallTop = GameObject.CreatePrimitive (PrimitiveType.Cube);
			wallTop.transform.localScale = new Vector3 (1, 10, 1);
			wallTop.transform.position = new Vector3 (x, 5, width-1);

			GameObject wallBottom = GameObject.CreatePrimitive (PrimitiveType.Cube);
			wallBottom.transform.localScale = new Vector3 (1, 10, 1);
			wallBottom.transform.position = new Vector3 (x, 5, -width+1);

		}

		for (int z = -depth; z <= depth; z++) {
			GameObject wallRight = GameObject.CreatePrimitive (PrimitiveType.Cube);
			wallRight.transform.localScale = new Vector3 (1, 10, 1);
			wallRight.transform.position = new Vector3 (depth, 5, z);

			GameObject wallLeft = GameObject.CreatePrimitive (PrimitiveType.Cube);
			wallLeft.transform.localScale = new Vector3 (1, 10, 1);
			wallLeft.transform.position = new Vector3 (-depth, 5, z);
		}
	}

	void GenerateMaze(int width, int depth, float density){
		width= width / 2;
		depth = depth / 2;

		for (int x = -width; x < width; x++) {
			for (int z = -depth; z <= depth; z++) {
				float p = Mathf.PerlinNoise (x * seed, z * seed);

				float centerDensity =(float) Mathf.Abs(x)/width + Mathf.Abs(z)/depth;

				//checking if noise is less than what we want for density(deciding whether or not to put a cube there
				if (p < density) {
					GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);

					if (Random.Range (0.0f, 10.0f) > 5.0f) {
						cube.transform.localScale = new Vector3 (Random.Range (1, 10), 10, 1);
					} else {
						cube.transform.localScale = new Vector3 (1, 10, Random.Range (1, 10));
					}


					//<> allows us to specify the type of component in a function call
					cube.GetComponent<Renderer> ().material.color = new Vector4 (Random.RandomRange(0,1f),1,1,.5f);

					//cube.transform.localScale = new Vector3 (1, 5, 1);
					cube.transform.position = new Vector3 (x, 2.5f, z);
				}
			}
		}
	}

}
