namespace Elma
{
	public interface IRecord
	{
		ITime Time { get; set; }
		string Player { get; set; }
	}
}