using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace m3
{
	public class LevelCommandsVisual : ILevelCommandsVisual
	{
		private IField field;
		IField ILevelCommandsVisual.Field
		{
			get
			{
				return field;
			}
		}
		private ILevelGenerator levelGenerator;
		ILevelGenerator ILevelCommandsVisual.LevelGenerator
		{
			get
			{
				return levelGenerator;
			}
		}

		public LevelCommandsVisual(IField field, ILevelGenerator levelGenerator)
		{
			this.field = field;
			this.levelGenerator = levelGenerator;

			(this as ILevelCommandsVisual).UpdateElementsPositions();
		}

		private void SetAnchors(RectTransform rect, IFieldItem fieldItem)
		{
			rect.anchorMin = new Vector2((float)fieldItem.Column / field.Width, 1 - (float)(fieldItem.Row + 1) / field.Height);
			rect.anchorMax = new Vector2((float)(fieldItem.Column + 1) / field.Width, 1 - (float)(fieldItem.Row) / field.Height);
		}

		private void SetTransformAndMove(IFieldItem fieldItem)
		{
			if (fieldItem == null)
				return;
			RectTransform rect = (fieldItem as MonoBehaviour).transform as RectTransform;
			Vector3 oldPos = rect.transform.localPosition;
			SetAnchors(rect, fieldItem);
			rect.transform.localPosition = oldPos;

			(fieldItem as FieldItem).GetItemFieldComp<MoveableComponent>().Move(Vector2.zero);
		}

		private void SetTransformInstantly(IFieldItem fieldItem)
		{
			if (fieldItem == null)
				return;

			RectTransform rect = (fieldItem as MonoBehaviour).transform as RectTransform;
			SetAnchors(rect, fieldItem);
			rect.sizeDelta = rect.anchoredPosition = Vector2.zero;
			rect.localScale = Vector3.one;
		}


		void ILevelCommandsVisual.UpdateElementsPositions()
		{
			for (int coloumn = 0; coloumn < field.VisualField.GetLength(0); coloumn++)
				for (int row = 0; row < field.VisualField.GetLength(1); row++)
				{
					SetTransformInstantly(field.VisualField[coloumn, row] as FieldItem);
				}
		}

		void ILevelCommands.SwapItems(int coloumn1, int row1, int coloumn2, int row2)
		{
			field.VisualField[coloumn1, row1].SetIndex(coloumn2, row2);
			field.VisualField[coloumn2, row2].SetIndex(coloumn1, row1);

			var obj = field.VisualField[coloumn1, row1];
			field.VisualField[coloumn1, row1] = field.VisualField[coloumn2, row2];
			field.VisualField[coloumn2, row2] = obj;

			SetTransformAndMove(field.VisualField[coloumn1, row1]);
			SetTransformAndMove(field.VisualField[coloumn2, row2]);
		}

		void ILevelCommands.SpawnNewItem(int coloumn, int row)
		{
			if (field.VisualField[coloumn, row] != null)
				GameObject.Destroy((field.VisualField[coloumn, row] as MonoBehaviour).gameObject);
			field.VisualField[coloumn, row] = levelGenerator.GenerateItem(coloumn, row);
			SetTransformInstantly(field.VisualField[coloumn, row] as FieldItem);
			((RectTransform)(field.VisualField[coloumn, row] as MonoBehaviour).transform).anchoredPosition += Vector2.up * 100;
			(field.VisualField[coloumn, row] as FieldItem).GetItemFieldComp<MoveableComponent>().Move(Vector2.zero);
		}

		void ILevelCommands.RemoveItem(int coloumn, int row)
		{
			(field.VisualField[coloumn, row] as FieldItem).GetItemFieldComp<RemovableComponent>().Remove();
		}
	}

}

