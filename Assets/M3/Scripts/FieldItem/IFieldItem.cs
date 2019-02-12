

namespace m3
{
	public interface IFieldItem
	{
		int Row { get; set; }
		int Column { get; set; }
		void SetIndex(int i, int j);
	}
}
