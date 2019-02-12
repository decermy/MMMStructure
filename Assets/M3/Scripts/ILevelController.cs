
namespace m3
{
	public enum LevelState { idle, selected, inAction }

	public interface ILevelController
	{
		LevelState LevelState { get; set; }
		ILevelData LevelData { get; }
		ILevelAnimator LevelAnimator { get; }
		IField Field { get; }

		void CreateLevel();
	}
}