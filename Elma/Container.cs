namespace Elma
{
	public static class Container
	{
		public static IState NewState() => new State();
		public static IPlayer NewPlayer() => new Player();
		public static ITime NewTime() => new Time();
		public static IRecord NewRecord() => new Record();
	}
}