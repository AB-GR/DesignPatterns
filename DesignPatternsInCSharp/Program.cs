namespace DesignPatternsInCSharp
{
	internal class Program
	{
		static void Main(string[] args)
		{
			//var opExample = new OpenClosedExample();
			//opExample.Run();
			//var liExample = new LiskovSubstitutionExample();
			//liExample.Run();
			var siExample = new SingletonExample();
			siExample.Run();	
			Console.ReadLine();
		}
	}
}