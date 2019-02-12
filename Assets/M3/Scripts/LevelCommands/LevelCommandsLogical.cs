using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace m3
{
	public class LevelCommandsLogical : ILevelCommandsLogical
	{
		private IField field;
		IField ILevelCommandsLogical.Field
		{
			get
			{
				return field;
			}
		}

		public LevelCommandsLogical(IField field)
		{
			this.field = field;
		}

		void ILevelCommands.SwapItems(int column1, int row1, int column2, int row2)
		{
			int t = field.LogicalField[column1, row1];
			field.LogicalField[column1, row1] = field.LogicalField[column2, row2];
			field.LogicalField[column2, row2] = t;
		}

		void ILevelCommands.SpawnNewItem(int column, int row)
		{
			field.LogicalField[column, row] = UnityEngine.Random.Range(0, field.MaxItemValue);
		}

		void ILevelCommands.RemoveItem(int column, int row)
		{
			field.LogicalField[column, row] = -1;
		}

		private List<IFieldItem> GetMatches(int column1, int row1, int column2, int row2)
		{
			List<IFieldItem> matches = (field as Field).FindMatches(field.VisualField).ToList();
			return matches;
		}

		private bool IsInsideField(int column, int row)
		{
			return (column >= 0 && column < field.Width && row >= 0 && row < field.Height);
		}

		bool ILevelCommandsLogical.IsActionWillHaveMatches(int column1, int row1, int column2, int row2)
		{
			if (field.LogicalField[column1, row1] == field.LogicalField[column2, row2] || IsInsideField(column1, row1) == false || IsInsideField(column2, row2) == false)
				return false;

			(this as ILevelCommands).SwapItems(column1, row1, column2, row2);

			var matches = GetMatches(column1, row1, column2, row2);
			if (matches.Count == 0)
			{
				(this as ILevelCommands).SwapItems(column1, row1, column2, row2);
				return false;
			}
			else
			{
				return true;
			}
		}

		void ILevelCommandsLogical.CheckMatches(Action<int, int> RemoveItemCallback)
		{
			var matches = (field as Field).FindMatches(field.VisualField).ToList();
			if (matches.Count > 0)
			{
				foreach (var item in matches)
				{
					RemoveItemCallback(item.Column, item.Row);
				}
			}
		}

		void ILevelCommandsLogical.MoveItemsDown(Action<int, int, int, int> SwapItemCallback)
		{
			for (int coloumn = 0; coloumn < field.Width; coloumn++)
				for (int row = field.Height - 2; row >= 0; row--)
				{
					if (field.LogicalField[coloumn, row] == -1)
						continue;
					int offset = 0;
					for (int i = 1; row + i < field.Height; i++)
						if (field.LogicalField[coloumn, row + i] == -1)
							offset = i;
						else
							break;
					if (offset != 0)
						SwapItemCallback(coloumn, row, coloumn, row + offset);
				}
		}

		void ILevelCommandsLogical.SpawnNewOnWhiteSpace(Action<int, int> SpawnItemCallback)
		{
			for (int coloumn = 0; coloumn < field.Width; coloumn++)
			{
				for (int row = 0; row < field.Height; row++)
				{
					if (field.LogicalField[coloumn, row] == -1)
					{
						SpawnItemCallback(coloumn, 0);
					}
					else
					{
						break;
					}
				}
			}
		}
	}
}
