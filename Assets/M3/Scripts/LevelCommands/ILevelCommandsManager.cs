

namespace m3
{
	public interface ILevelCommandsManager
	{
		void CheckMatches();
		void RemoveWhiteSpace();
		bool SwapItemsIfItPosible(int column1, int row1, int column2, int row2);
	}
}

