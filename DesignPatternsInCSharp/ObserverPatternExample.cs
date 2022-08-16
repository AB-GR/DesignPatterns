namespace DesignPatternsInCSharp
{
	internal class ObserverPatternExample
	{
		public void Run()
		{
			var weatherProvider = new Publisher();
			var currentConditionsDisplay = new CurrentConditionsDisplay(weatherProvider);
			weatherProvider.SetMeasurements(34, 80, 90);
		}
	}

	interface IPublisher
	{
		void AddSubscriber(ISubscriber subscriber);
		void RemoveSubscriber(ISubscriber subscriber);
		void NotifySubscribers();
	}

	interface ISubscriber
	{
		void Update(WeatherData data);
	}

	class Publisher : IPublisher
	{
		List<ISubscriber> ListOfSubscribers;
		WeatherData data;

		public Publisher()
		{
			ListOfSubscribers = new List<ISubscriber>();
		}

		public void AddSubscriber(ISubscriber subscriber)
		{
			ListOfSubscribers.Add(subscriber);
		}

		public void NotifySubscribers()
		{
			foreach (var sub in ListOfSubscribers)
			{
				sub.Update(data);
			}
		}

		public void RemoveSubscriber(ISubscriber subscriber)
		{
			ListOfSubscribers.Remove(subscriber);
		}

		public void SetMeasurements(float temp, float humid, float pres)
		{
			data = new WeatherData(temp, humid, pres);
			MeasurementsChanged();
		}

		private void MeasurementsChanged()
		{
			NotifySubscribers();
		}
	}

	class CurrentConditionsDisplay : ISubscriber
	{
		WeatherData data;
		IPublisher weatherData;

		public CurrentConditionsDisplay(IPublisher weatherDataProvider)
		{
			weatherData = weatherDataProvider;
			weatherData.AddSubscriber(this);
		}

		public void Display()
		{
			Console.WriteLine("Current Conditions : Temp = {0} Deg | Humidity = {1}% | Pressure = {2} bar", data.Temperature, data.Humidity, data.Pressure);

		}

		public void Update(WeatherData data)
		{
			this.data = data;
			Display();
		}
	}
}
