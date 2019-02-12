using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace m3
{
	public class LevelGenerator : ILevelGenerator
	{
		private ILevelData levelData;
		public ILevelData LevelInfo
		{
			get
			{
				return levelData;
			}
		}
		private IField field;
		public IField Field
		{
			get
			{
				return field;
			}
		}

		private RectTransform fieldRectTransform;
		public RectTransform FieldRectTransform
		{
			get
			{
				return fieldRectTransform;
			}
		}

		private ILevelAnimator levelAnimator;

		public void GenerateField()
		{
			GenerateLogicalField();
			GenerateVisualField();
		}

		private void GenerateLogicalField()
		{
			field.LogicalField = new int[field.Width, field.Height];
			for (int i = 0; i < field.Width; i++)
			{
				for (int j = 0; j < field.Height; j++)
				{
					field.LogicalField[i, j] = Random.Range(0, field.MaxItemValue);
				}
			}
		}

		private void GenerateVisualField()
		{
			IFieldItem[,] result = new FieldItem[levelData.Width, levelData.Height];
			for (int i = 0; i < levelData.Width; i++)
				for (int j = 0; j < levelData.Height; j++)
				{
					result[i, j] = GenerateItem(i, j);
				}
			field.VisualField = result;
		}

		public IFieldItem GenerateItem(int coloumn, int row)
		{
			GameObject[] prefabs = levelData.Prefabs;
			int value = field.LogicalField[coloumn, row];
			GameObject item = GameObject.Instantiate(prefabs[value], fieldRectTransform) as GameObject;
			item.transform.SetAsFirstSibling();
			IFieldItem result = item.AddComponent<FieldItem>();

			(result as FieldItem).Init(levelAnimator, coloumn, row);

			return result;
		}

		public LevelGenerator(ILevelData levelData, IField field, RectTransform fieldRectTransform, ILevelAnimator levelAnimator)
		{
			this.levelData = levelData;
			this.fieldRectTransform = fieldRectTransform;
			this.field = field;
			this.levelAnimator = levelAnimator;
		}
	}

}
