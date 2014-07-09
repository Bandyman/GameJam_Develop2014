using UnityEngine;
using System.Collections;

public class GJ_LoadTerrain : MonoBehaviour {

		private enum TERRAIN_TYPE {
				SEA,
				BORDER, 
				ATTRACTION, 
				WALKWAY
		}

		private int terrain_Size = 100 ;
		private TERRAIN_TYPE[,] Terrain ; 

		[SerializeField] private GameObject[] terrain_Sprite ;

		private void Awake() {

				Terrain = new TERRAIN_TYPE[terrain_Size,terrain_Size];
		}



		// Use this for initialization
		void Start () {
				Debug.Log("Starting") ;
				Init_DefaultTerrain() ;

				Load_NewTerrain() ;
		}




		private void Load_NewTerrain () {
				Vector2 tPos = Vector2.zero ;

				for( int i=0; i<terrain_Size ; i++ ) {
						for(int j=0; j<terrain_Size; j++ ){
								tPos.x = i; tPos.y = j ;
								Instantiate_SpriteAt( Terrain[i,j], tPos );
						}
				}
		}

		private void Init_DefaultTerrain() {
				for( int i=0; i<terrain_Size ; i++ ) {
						for(int j=0; j<terrain_Size; j++ ){
								Terrain[i,j] = TERRAIN_TYPE.SEA ;
						}
				}
		}

		private void Instantiate_SpriteAt( TERRAIN_TYPE spriteType, Vector2 pos ){
				int type = -1 ;

				// We need to recenter everything 
				pos.x -= terrain_Size/2 ;;
				pos.y -= terrain_Size/2;

				// Which type to instantiate
				switch( spriteType ){
				case TERRAIN_TYPE.SEA : type = 0  ; break ;
				default : Debug.LogError( "LoadTerrain : You suck massive ball mate. " + pos.x +""+ pos.y);
						return ;
				}

				Instantiate( terrain_Sprite[type], pos, Quaternion.identity );
		}
				
}
