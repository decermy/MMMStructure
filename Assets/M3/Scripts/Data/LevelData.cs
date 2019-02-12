using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace m3
{
	[CreateAssetMenu(fileName = "LevelData", menuName = "M3/LevelData")]
	public class LevelData : ScriptableObject, ILevelData
	{
		[SerializeField]
		private int width;
		public int Width
		{
			get
			{
				return width;
			}
		}

		[SerializeField]
		private int height;
		public int Height
		{
			get
			{
				return height;
			}
		}

		[SerializeField]
		private GameObject[] prefabs;
		public GameObject[] Prefabs
		{
			get
			{
				return prefabs;
			}
		}
	}
}
