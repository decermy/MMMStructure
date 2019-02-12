

namespace m3
{
	public interface ILevelCommands
	{
		void SwapItems(int coloumn1, int row1, int coloumn2, int row2);

		void SpawnNewItem(int coloumn, int row);

		void RemoveItem(int coloumn, int row);
	}
}

