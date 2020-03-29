using System;
using CodingCampusCSharpHomework;
using System.Globalization;

namespace HomeworkTemplate
{
    class Program
    {
        static void Main(string[] args)
        {
            static float DegreesToRadians(float value)
            {
                return MathF.PI * value / 180;
            }

            Func<Task3, string> TaskSolver = task =>
            {
                NumberFormatInfo formatProvider = new NumberFormatInfo();
                formatProvider.NumberDecimalSeparator = ",";

                float UserLongitude = float.Parse(task.UserLongitude, formatProvider);
                float UserLatitude = float.Parse(task.UserLatitude, formatProvider);
                UserLongitude = DegreesToRadians(UserLongitude);
                UserLongitude = DegreesToRadians(UserLatitude);

                int placesAmount = task.DefibliratorStorages.Length;

                int minIndex = 0;
                float minDistance = float.MaxValue;
                for (int i = 0; i < placesAmount; i++)
                {
                    string[] defibliratorStorage = task.DefibliratorStorages[i].Split(';');
                    if (defibliratorStorage.Length == 4)
                    {
                        float longitude = float.Parse(defibliratorStorage[2], formatProvider);
                        float latitude = float.Parse(defibliratorStorage[3], formatProvider);
                        longitude = DegreesToRadians(longitude);
                        latitude = DegreesToRadians(latitude);
                        // calculating distance with Harvesine Formula http://www.movable-type.co.uk/scripts/latlong.html
                        float x = (longitude - UserLongitude) * MathF.Cos((latitude + UserLatitude) / 2);
                        float y = latitude - UserLatitude;
                        float distance = MathF.Sqrt(x * x + y * y) * 6371;
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            minIndex = i;
                        }
                    }
                }

                string[] result = task.DefibliratorStorages[minIndex].Split(';');

                return $"Name: {result[0]}; Address: {result[1]}";
            };

            Task3.CheckSolver(TaskSolver);
        }
    }
}
