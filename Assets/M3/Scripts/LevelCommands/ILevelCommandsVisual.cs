
namespace m3
{
	public interface ILevelCommandsVisual : ILevelCommands
	{
		IField Field { get; }
		ILevelGenerator LevelGenerator { get; }

		void UpdateElementsPositions();
	}
}


