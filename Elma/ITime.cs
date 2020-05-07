namespace Elma
{
	public interface ITime
	{
		string ToString();
		bool FromString(string time);
		int ToHSeconds();
		bool FromHSeconds(int hseconds);
	}
}
