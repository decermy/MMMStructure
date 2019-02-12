using UnityEngine;

namespace m3
{
	public class LevelController : MonoBehaviour, ILevelController
	{
		[SerializeField]
		private LevelState levelState;
		public LevelState LevelState
		{
			get
			{
				return levelState;
			}
			set
			{
				levelState = value;
			}
		}

		[SerializeField]
		private LevelData levelData;
		public ILevelData LevelData
		{
			get
			{
				return levelData;
			}
		}

		[SerializeField]
		private RectTransform UIPanel;

		private ILevelAnimator levelAnimator;
		public ILevelAnimator LevelAnimator
		{
			get
			{
				return levelAnimator;
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

		[SerializeField]
		private Updater updater;

		private ILevelCommandsManager levelCommandsManager;
		private IInputController inputController;

		public void CreateLevel()
		{
			field = new Field(LevelData.Width, LevelData.Height, LevelData.Prefabs.Length);

			levelAnimator = SetLevelAnimator();

			ILevelGenerator levelGenerator = new LevelGenerator(levelData, field, UIPanel, levelAnimator);
			levelGenerator.GenerateField();

			ILevelCommandsLogical levelCommandsLogical = new LevelCommandsLogical(field);
			ILevelCommandsVisual levelCommandsVisual = new LevelCommandsVisual(field, levelGenerator);

			levelCommandsManager = new LevelCommandsManager(levelCommandsLogical, levelCommandsVisual);

			inputController = new InputController(levelCommandsManager, this);
			InitUpdater();

			levelAnimator.OnAnimationsStarted += () => { levelState = LevelState.inAction; };
			levelAnimator.OnAnimationsFinished += () => { levelState = LevelState.idle; };
		}

		private void Start()
		{
			CreateLevel();
		}

		private void Loop()
		{
			if (levelState == LevelState.idle)
			{
				levelCommandsManager.CheckMatches();
			}

			levelCommandsManager.RemoveWhiteSpace();
		}

		private ILevelAnimator SetLevelAnimator()
		{
			GameObject levelAnimatorGo = new GameObject();
			levelAnimatorGo.name = "LevelAnimator";
			return levelAnimatorGo.AddComponent<LevelAnimator>();
		}

		private void InitUpdater()
		{
			GameObject updaterGameObject = Instantiate((updater as MonoBehaviour).gameObject) as GameObject;
			updater = updaterGameObject.GetComponent<Updater>();
			updater.TickTac += Loop;
			updater.Init();
		}
	}
}

