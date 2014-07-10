using UnityEngine;
using System.Collections;

public class GJ_AI {

		public enum DIRECTION {
				DOWN,
				UP,
				LEFT,
				RIGHT
		}

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
		private int[,] excitmentMap;
		private bool[,] allowedMovement ;



		public void Init () {
				shortestDistance = new int[GJ_ManageBuilding.terrain_Width,GJ_ManageBuilding.terrain_Height];
				excitmentMap = new int[GJ_ManageBuilding.terrain_Width,GJ_ManageBuilding.terrain_Height];

				for( int i=0; i<GJ_ManageBuilding.terrain_Width; i++){
						for(int j=0; j<GJ_ManageBuilding.terrain_Height; j++ ){
								shortestDistance[i,j] = -1 ;
								excitmentMap[i,j] = 0 ;
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
								sentence += excitmentMap[i,j] + " ";
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
				if( w_Index >= GJ_ManageBuilding.terrain_Width ) return ;
				if( shortestDistance[w_Index,h_Index] != -1 ) return ;

				shortestDistance[w_Index,h_Index] = dist ;
				Check_Up( w_Index+1, h_Index, dist +1 ) ;
				Check_Left( w_Index, h_Index+1, dist+1 ) ;
		}



		public void Update_ExcitmentMap( int w_Index, int h_Index, int value ){
				excitmentMap[w_Index, h_Index] = value ;
				Propagate_Excitement( DIRECTION.DOWN, w_Index, h_Index, value-1 );
				Propagate_Excitement( DIRECTION.UP, w_Index, h_Index, value-1 );
				Propagate_Excitement( DIRECTION.LEFT, w_Index, h_Index, value-1 );
				Propagate_Excitement( DIRECTION.RIGHT, w_Index, h_Index, value-1 );
		}
		private void Propagate_Excitement ( DIRECTION d, int w_Index, int h_Index, int value ){
				if( value <= 0 )
						return ;

				switch( d ) {
				case DIRECTION.DOWN : w_Index-=1 ;break ;
				case DIRECTION.UP : w_Index+=1 ;break ;
				case DIRECTION.LEFT : h_Index+=1 ;break ;
				case DIRECTION.RIGHT : h_Index-=1 ;break ;
				}

				if( w_Index >= GJ_ManageBuilding.terrain_Width || w_Index <0 ) return ;
				if( h_Index >= GJ_ManageBuilding.terrain_Height || h_Index <0 ) return ;
				if( value <= excitmentMap [w_Index, h_Index])  return ;

				excitmentMap[w_Index, h_Index] = value ;

				switch( d ) {
				case DIRECTION.DOWN :
						Propagate_Excitement( DIRECTION.LEFT, w_Index, h_Index, value-1 );
						Propagate_Excitement( DIRECTION.RIGHT, w_Index, h_Index, value-1 );
						Propagate_Excitement( DIRECTION.DOWN, w_Index, h_Index, value-1 );
						break ;
				case DIRECTION.UP : 
						Propagate_Excitement( DIRECTION.UP, w_Index, h_Index, value-1 );
						Propagate_Excitement( DIRECTION.LEFT, w_Index, h_Index, value-1 );
						Propagate_Excitement( DIRECTION.RIGHT, w_Index, h_Index, value-1 );
						break ;
				case DIRECTION.LEFT : 
						Propagate_Excitement( DIRECTION.DOWN, w_Index, h_Index, value-1 );
						Propagate_Excitement( DIRECTION.UP, w_Index, h_Index, value-1 );
						Propagate_Excitement( DIRECTION.LEFT, w_Index, h_Index, value-1 );
						break ;
				case DIRECTION.RIGHT : 
						Propagate_Excitement( DIRECTION.DOWN, w_Index, h_Index, value-1 );
						Propagate_Excitement( DIRECTION.UP, w_Index, h_Index, value-1 );
						Propagate_Excitement( DIRECTION.RIGHT, w_Index, h_Index, value-1 );
						break ;
				}

		}
	
		public bool Check_ForAttractions(int currentPos_W, int currentPos_H){
				bool val = false ;
				// LEFT
				if( !GJ_ManageBuilding.Instance.Check_IfCanMove(currentPos_W, currentPos_H+1) ){
						if( currentPos_H+1 < GJ_ManageBuilding.terrain_Height ){
								val = true ;
						}
				}
				if( !GJ_ManageBuilding.Instance.Check_IfCanMove(currentPos_W, currentPos_H-1) ){
						if( currentPos_H-1 >=0 ){
								val = true ;
						}
				}
				if( !GJ_ManageBuilding.Instance.Check_IfCanMove(currentPos_W+1, currentPos_H) ){
						if( currentPos_W+1 < GJ_ManageBuilding.terrain_Width ){
								val = true ;
						}
				}
				if( !GJ_ManageBuilding.Instance.Check_IfCanMove(currentPos_W-1, currentPos_H) ){
						if( currentPos_W-1 >= 0 ){
								val = true ;
						}
				}
				return val ;
		} 

		public DIRECTION Get_NewDirection (int currentPos_W, int currentPos_H ) {
				DIRECTION best = DIRECTION.LEFT ;
				int bestVal = 0 ;
				bool found = false ;

				// LEFT
				if( GJ_ManageBuilding.Instance.Check_IfCanMove(currentPos_W, currentPos_H+1) ){

						if( excitmentMap[currentPos_W, currentPos_H+1] > bestVal ){
								bestVal = excitmentMap[currentPos_W, currentPos_H+1] ;
								best = DIRECTION.LEFT;
								found = true ;
						}
				}

				//RIGHT 
				if( GJ_ManageBuilding.Instance.Check_IfCanMove(currentPos_W, currentPos_H-1) ){
						if( excitmentMap[currentPos_W, currentPos_H-1] > bestVal ){
								bestVal = excitmentMap[currentPos_W, currentPos_H-1] ;
								best = DIRECTION.RIGHT;
								found = true ;
						}
				}

				//UP 
				if( GJ_ManageBuilding.Instance.Check_IfCanMove(currentPos_W+1, currentPos_H) ){
						if( excitmentMap[currentPos_W+1, currentPos_H] > bestVal ){
								bestVal = excitmentMap[currentPos_W+1, currentPos_H] ;
								best = DIRECTION.UP ;
								found = true ;
						}
				}

				// DOWN
				if( GJ_ManageBuilding.Instance.Check_IfCanMove(currentPos_W-1, currentPos_H) ){
						if( excitmentMap[currentPos_W-1, currentPos_H] > bestVal ){
								bestVal = excitmentMap[currentPos_W-1, currentPos_H] ;
								best = DIRECTION.DOWN ;
								found = true ;
						}
				}

				if( !found )
						return GetRandom () ;

				return best ;
		}



		public DIRECTION Get_NewDirectionLEAVING (int currentPos_W, int currentPos_H ) {
				DIRECTION best = DIRECTION.LEFT ;
				int bestVal = 0 ;
				Debug.Log( "New check");

				// LEFT
				if( GJ_ManageBuilding.Instance.Check_IfCanMove(currentPos_W, currentPos_H+1) ){

						if( excitmentMap[currentPos_W, currentPos_H+1] > bestVal ){
								bestVal = shortestDistance[currentPos_W, currentPos_H+1] ;
								best = DIRECTION.LEFT;
						}
				}

				//RIGHT 
				if( GJ_ManageBuilding.Instance.Check_IfCanMove(currentPos_W, currentPos_H-1) ){
						if( excitmentMap[currentPos_W, currentPos_H-1] > bestVal ){
								bestVal = shortestDistance[currentPos_W, currentPos_H-1] ;
								best = DIRECTION.RIGHT;
						}
				}

				//UP 
				if( GJ_ManageBuilding.Instance.Check_IfCanMove(currentPos_W+1, currentPos_H) ){
						if( excitmentMap[currentPos_W+1, currentPos_H] > bestVal ){
								bestVal = shortestDistance[currentPos_W+1, currentPos_H] ;
								best = DIRECTION.UP ;
						}
				}

				// DOWN
				if( GJ_ManageBuilding.Instance.Check_IfCanMove(currentPos_W-1, currentPos_H) ){
						if( excitmentMap[currentPos_W-1, currentPos_H] > bestVal ){
								bestVal = shortestDistance[currentPos_W-1, currentPos_H] ;
								best = DIRECTION.DOWN ;
						}
				}

				return best ;
		}

		private DIRECTION GetRandom () {
				int v = Random.Range(0,3) ;
				switch(v){
				case 0 : 
						return DIRECTION.LEFT ;
				case 1 : return DIRECTION.RIGHT ;
				case 2 : return DIRECTION.UP;
				case 3 : return DIRECTION.DOWN ;
				}
				return DIRECTION.RIGHT ;
		}
}
