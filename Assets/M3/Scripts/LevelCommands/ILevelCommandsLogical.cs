using System;

namespace m3
{
	public interface ILevelCommandsLogical : ILevelCommands
	{
		IField Field { get; }

		bool IsActionWillHaveMatches(int column1, int row1, int column2, int row2);
		void CheckMatches(Action<int, int> RemoveItemCallback);
		void MoveItemsDown(Action<int, int, int, int> SwapItemCallback);
		void SpawnNewOnWhiteSpace(Action<int, int> SpawnItemCallback);
	}
}
