using UnityEngine;
using System.Collections;

public class GJ_AI {

		private static GJ_AI _instance ;
		public static GJ_AI Instance {
				get {
						if( _instance == null ){
								_instance = new GJ_AI() ;
						}
						return _instance ;
				}
		}
				

		private int[,] shortestDistance ;

		public void Init () {
				shortestDistance = new int[GJ_ManageBuilding.terrain_Width,GJ_ManageBuilding.terrain_Height];
				for( int i=0; i<GJ_ManageBuilding.terrain_Width; i++){
						for(int j=0; j<GJ_ManageBuilding.terrain_Height; j++ ){
								shortestDistance[i,j] = -1 ;
						}
				}
				Check_Down( 3, 0, 1 );
				Check_Left( 3, 0, 1 );
				Check_Up(4,0,1) ;
				Check_Left( 4, 0, 1 );
		}

		public void Print_Table() {
				string sentence = "";
				for( int i=0; i<GJ_ManageBuilding.terrain_Width; i++){
						for(int j=0; j<GJ_ManageBuilding.terrain_Height; j++ ){
								sentence += shortestDistance[i,j] + " ";
						}
						Debug.Log(sentence);
						sentence = "";
				}
		}

		private void Check_Down( int w_Index, int h_Index, int dist ) {
				if( w_Index<0 ) return ;
				if( shortestDistance[w_Index,h_Index] != -1 ) return ;

				shortestDistance[w_Index,h_Index] = dist ;
				Check_Down( w_Index-1, h_Index, dist +1 ) ;
				Check_Left( w_Index, h_Index+1, dist+1 ) ;
		}

		private void Check_Left( int w_Index, int h_Index, int dist ) {
				if( h_Index>=GJ_ManageBuilding.terrain_Height) return ;

				if( shortestDistance[w_Index,h_Index] != -1 ) return ;
				shortestDistance[w_Index,h_Index] = dist ;
				Check_Left( w_Index, h_Index+1, dist +1 ) ;
		}

		private void Check_Up( int w_Index, int h_Index, int dist ) {
				Debug.Log( "Checking up on " + w_Index + " " + h_Index) ;
				if( w_Index >= GJ_ManageBuilding.terrain_Width ) return ;
				if( shortestDistance[w_Index,h_Index] != -1 ) return ;

				Debug.Log("Update the distance");
				shortestDistance[w_Index,h_Index] = dist ;
				Check_Up( w_Index+1, h_Index, dist +1 ) ;
				Check_Left( w_Index, h_Index+1, dist+1 ) ;
		}
}
