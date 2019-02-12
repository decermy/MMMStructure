using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace m3
{
	public class LevelCommandsManager : ILevelCommandsManager
	{
		private ILevelCommandsLogical levelCommandsLogical;
		private ILevelCommandsVisual levelCommandsVisual;

		public LevelCommandsManager(ILevelCommandsLogical levelCommandsLogical, ILevelCommandsVisual levelCommandsVisual)
		{
			this.levelCommandsLogical = levelCommandsLogical;
			this.levelCommandsVisual = levelCommandsVisual;
		}

		private void SwapItems(int column1, int row1, int column2, int row2)
		{
			levelCommandsLogical.SwapItems(column1, row1, column2, row2);
			levelCommandsVisual.SwapItems(column1, row1, column2, row2);
		}

		public bool SwapItemsIfItPosible(int column1, int row1, int column2, int row2)
		{
			if (levelCommandsLogical.IsActionWillHaveMatches(column1, row1, column2, row2))
			{
				levelCommandsVisual.SwapItems(column1, row1, column2, row2);
				return true;
			}

			return false;
		}

		private void RemoveItem(int column, int row)
		{
			levelCommandsVisual.RemoveItem(column, row);
			levelCommandsLogical.RemoveItem(column, row);
		}

		public void CheckMatches()
		{
			levelCommandsLogical.CheckMatches(RemoveItem);
		}

		public void RemoveWhiteSpace()
		{
			MoveItemsDown();
			SpawnNewOnWhiteSpace();
		}

		private void MoveItemsDown()
		{
			levelCommandsLogical.MoveItemsDown(SwapItems);
		}
		private void SpawnNewOnWhiteSpace()
		{
			levelCommandsLogical.SpawnNewOnWhiteSpace(SpawnNewItem);
		}

		private void SpawnNewItem(int column, int row)
		{
			levelCommandsLogical.SpawnNewItem(column, row);
			levelCommandsVisual.SpawnNewItem(column, row);
		}
	}
}
