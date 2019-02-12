
namespace m3
{
	public interface IField
	{
		int Width { get; }
		int Height { get; }

		int MaxItemValue { get; }

		IFieldItem[,] VisualField { get; set; }

		int[,] LogicalField { get; set; }
	}

}
