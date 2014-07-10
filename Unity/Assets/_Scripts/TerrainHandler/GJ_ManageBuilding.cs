using UnityEngine;
using System.Collections;

public class GJ_ManageBuilding : MonoBehaviour
{

		private enum TERRAIN_TYPE
		{
				FREE,
				OCCUPIED,
				WATER
		}

		private static GJ_ManageBuilding _instance ;
		public static GJ_ManageBuilding Instance {
				get {
						return _instance ;
				}
		}

		public static int terrain_Height = 12;
		public static int terrain_Width = 8;
		private TERRAIN_TYPE[,] Terrain;



		private void Awake () {
				_instance = this ;
		}

		// Use this for initialization
		void Start ()
		{
				Debug.Log ("Starting");
				Init_DefaultTerrain ();
				GJ_AI.Instance.Init() ;
				GJ_AI.Instance.Print_Table() ;
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
				Debug.Log( "Checking at " + posX + "-"+posY + " :: "+size );
				if( posX< 0 || posY<0  || posX>terrain_Width || posY>terrain_Height){
						return false ;
				}
				for( int i=0; i< size ; i++ ){
						for( int j=0; j<size; j++ ){
								if( posX+i>=terrain_Width || posY+j>=terrain_Height ){
										return false ;
								}

								if (Terrain [posX + i, posY+j] != TERRAIN_TYPE.FREE) {
										return false ;
										Debug.Log("Nope can't build you shihead");
								}
						}
				}
				Debug.Log("Yup okay, you can build homie");
				return true;
		}

		public void Update_MapOfBuildings_ForAttraction(int size, int posX, int posY){
				Debug.Log( "Checking at " + posX + "-"+posY + " :: "+size );
				if( posX< 0 || posY<0 || posX>terrain_Width || posX>terrain_Height){
						return  ;
				}
				for( int i=0; i< size ; i++ ){
						for( int j=0; j<size; j++ ){
								Terrain [posX + i, posY+j] = TERRAIN_TYPE.OCCUPIED  ;
						}
				}
		}
				
}
