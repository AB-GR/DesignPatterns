namespace DesignPatternsInCSharp
{
	public class WeatherData
	{
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public float Pressure { get; set; }

        public WeatherData(float temp, float humid, float pres)
        {
            Temperature = temp;
            Humidity = humid;
            Pressure = pres;
        }
    }
}