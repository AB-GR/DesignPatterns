namespace DesignPatternsInCSharp
{
	internal class SingletonExample
	{
		public void Run()
		{
			var singleton = Singleton.Instance();
		}
	}

	public class Singleton
	{
		private static Singleton instance;
		private static object syncRoot = new object();

		private Singleton()
		{
		}

		public static Singleton Instance()
		{
			if(instance == null)
			{
				lock(syncRoot)
				{
					if (instance == null)
						instance = new Singleton();
				}
			}

			return instance;
		}
	}
}
