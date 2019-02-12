
using UnityEngine;

namespace m3
{
	public interface ILevelData
	{
		int Width { get; }
		int Height { get; }

		GameObject[] Prefabs { get; }
	}
}

