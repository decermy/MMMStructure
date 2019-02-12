
using UnityEngine;


namespace m3
{
	public interface ILevelGenerator
	{
		IField Field { get; }
		ILevelData LevelInfo { get; }
		RectTransform FieldRectTransform { get; }

		void GenerateField();
		IFieldItem GenerateItem(int coloumn, int row);
	}
}