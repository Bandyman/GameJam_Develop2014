using UnityEngine;
using System.Collections;

public class GJ_ManageBuilding : MonoBehaviour
{
		private static GJ_ManageBuilding _instance ;
		public static GJ_ManageBuilding Instance {
				get {
						return _instance ;
				}
		}

		public static int terrain_Height = 12;
		public static int terrain_Width = 8;
		private IntVector2 exitOne = new IntVector2(3,0);
		private IntVector2 exitTwo = new IntVector2(4,0);
		private PierPathSolver.NodeType[,] Terrain;
        private PierPathSolver solver;
	
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
				Terrain = new PierPathSolver.NodeType[terrain_Width, terrain_Height];
				Terrain [exitOne.x, exitOne.y] = PierPathSolver.NodeType.END;
				Terrain [exitTwo.x, exitTwo.y] = PierPathSolver.NodeType.END;

				for (int i = 0; i < terrain_Width; i++) {
						for (int j = 0; j < terrain_Height; j++) {
						Terrain [i, j] = PierPathSolver.NodeType.FREE;
						}
				}
                
                solver.Tiles = Terrain;
		}

		public bool Check_IfSpaceAvailable_ForAttraction (int size, int posX, int posY)
		{
				if( posX< 0 || posY<0  || posX>terrain_Width || posY>terrain_Height){
						return false ;
				}
				for( int i=0; i< size ; i++ ){
						for( int j=0; j<size; j++ ){
								if( posX+i>=terrain_Width || posY+j>=terrain_Height ){
										return false ;
								}

								if (Terrain [posX + i, posY+j] != PierPathSolver.NodeType.FREE) {
										return false ;
								}
						}
				}
				return true;
		}

		public void Update_MapOfBuildings_ForAttraction(int size, int posX, int posY){
				Debug.Log( "Checking at " + posX + "-"+posY + " :: "+size );
				if( posX< 0 || posY<0 || posX>terrain_Width || posX>terrain_Height){
						return  ;
				}
				for( int i=0; i< size ; i++ ){
						for( int j=0; j<size; j++ ){
						Terrain [posX + i, posY+j] = PierPathSolver.NodeType.OCCUPIED;
						}
				}
                
                solver.Tiles = Terrain;
		}

		public bool Check_IfCanMove ( int index_W, int index_H){
				if( index_W >= terrain_Width || index_W < 0 )
						return false ;
				if( index_H >= terrain_Height || index_H <0  )
						return false ;

				if( Terrain[index_W, index_H] != TERRAIN_TYPE.FREE) return false ;
				else return true ;
		}
				
}
