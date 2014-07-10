using UnityEngine;
using System.Collections;

public class PlayerProfile
{
		private static PlayerProfile _instance;

		public static PlayerProfile Instance {
				get {
			
						if (_instance == null) {
								_instance = new PlayerProfile ();
						}
						return _instance;
				}
		}

		private void Awake ()
		{			
				_instance = this;
		}

		private int  score = 5000;

		public int Score {
				get {
						return score;
				}
				set {
						score = value;
				}
		}

		private int money = 5000;

		public int Money {
				get {
						return money;
				}
				set {
						money = value;
				}
		}
}
