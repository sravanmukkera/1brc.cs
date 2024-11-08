namespace TemperatureCalculation
{
    public class TempCalculation
    {
        public TempCalculation() { }
        public void CalculateTemperature()
        {
            string path = @"C:\sravan\VSSApps\TemperatureFile.txt";
            IEnumerable<string> fileContents = File.ReadAllLines(path);
            List<Temperature> temperatures = new List<Temperature>();

            foreach (string line in fileContents)
            {
                string[] data = line.Split(";");

                string city = data[0];

                if (city == string.Empty) continue;

                decimal heat = Convert.ToDecimal(data[1]);
                Temperature temp = temperatures.Where(x => x.City == city).FirstOrDefault();

                if (temp != null)
                {
                    //Find the min temp
                    if (heat < temp.MinTemp)
                    {
                        temp.MinTemp = heat;
                    }

                    //Find the max temp
                    if (heat > temp.MaxTemp)
                    {
                        temp.MaxTemp = heat;
                    }


                    //Sum the temp
                    temp.TotalTemp = temp.TotalTemp + heat;

                    temp.TotalRecords = temp.TotalRecords + 1;
                }
                else
                {
                    temp = new Temperature();

                    temp.City = city;
                    temp.MinTemp = heat;
                    temp.MaxTemp = heat;
                    temp.TotalTemp = heat;
                    temp.TotalRecords = 1;
                    temperatures.Add(temp);
                }
            }

            temperatures = temperatures.OrderBy(x => x.City).ToList();
            foreach (Temperature temperature in temperatures)
            {
                string output = $"{temperature.City} city min temp is : {temperature.MinTemp} and max temp is : {temperature.MaxTemp} and Avg temp is : {temperature.TotalTemp / temperature.TotalRecords} ";
                Console.WriteLine(output);
            }
        }
    }

    public class Temperature
    {
        public string City { get; set; } = string.Empty;
        public decimal MinTemp { get; set; }
        public decimal MaxTemp { get; set; }

        public decimal TotalTemp { get; set; }
        public long TotalRecords { get; set; }
    }
}
