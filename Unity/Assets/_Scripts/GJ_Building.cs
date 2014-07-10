using UnityEngine;
using System.Collections;

public class GJ_Building : MonoBehaviour {

		public int size = 1 ;
		public int price = 0 ;
		public int moneyMade = 0 ;
		public int excitment = 0 ;

		public string description = "Lorem Ipsum";
		public string title = "Building_1";

		private void Awake() {

		}

		public int Collect_Money() {
				int val = moneyMade ;
				moneyMade = 0 ;
				return val ;
		}

}
