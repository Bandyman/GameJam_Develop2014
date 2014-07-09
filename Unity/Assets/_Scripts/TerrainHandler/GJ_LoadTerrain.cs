using UnityEngine;
using System.Collections;

public class GJ_LoadTerrain : MonoBehaviour
{

		private enum TERRAIN_TYPE
		{
				FREE,
				OCCUPIED,
				WATER
		}

		private static GJ_LoadTerrain _instance ;
		public static GJ_LoadTerrain Instance {
				get {
						return _instance ;
				}
		}

		private int terrain_Height = 7;
		private int terrain_Width = 15;
		private TERRAIN_TYPE[,] Terrain;



		private void Awake () {
				_instance = this ;
		}

		// Use this for initialization
		void Start ()
		{
				Debug.Log ("Starting");
				Init_DefaultTerrain ();
		}

		private void Init_DefaultTerrain ()
		{
				Terrain = new TERRAIN_TYPE[terrain_Width, terrain_Height];

				for (int i = 0; i < terrain_Width; i++) {
						for (int j = 0; j < terrain_Height; j++) {
								Terrain [i, j] = TERRAIN_TYPE.FREE;
						}
				}
		}

		public bool Check_IfSpaceAvailable_ForAttraction (int size, int posX, int posY)
		{

				if( posX< 0 || posY<0 ){
						return false ;
				}
				if (Terrain [posX, posY] != TERRAIN_TYPE.FREE) {
						return false;
				}
				return true;
		}

				
}
