using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace test
{


    class Program
    {
        static void Main()
        {
            //Graph theGraph = new Graph(6);

            //DateTime dt1 = DateTime.Now;
            //theGraph.AddVertex("CS1");
            //theGraph.AddVertex("CS2");
            //theGraph.AddVertex("DS");
            //theGraph.AddVertex("OS");
            //theGraph.AddVertex("ALG");
            //theGraph.AddVertex("AL");
            //theGraph.AddEdge(0, 1);
            //theGraph.AddEdge(1, 2);
            //theGraph.AddEdge(1, 5);
            //theGraph.AddEdge(2, 3);
            //theGraph.AddEdge(2, 4);
            //theGraph.TopSort();
            //Console.WriteLine();
            //Console.WriteLine("Finished.");
            //DateTime dt2 = DateTime.Now;
            //TimeSpan ts = dt2 - dt1;
            //Console.WriteLine(ts.TotalSeconds);

            #region 元组

            //var testTuple6 = new Tuple<int, int, int, int, int, int>(1, 2, 3, 4, 5, 6);
            //Console.WriteLine($"Item 1: {testTuple6.Item1}, Item 6: {testTuple6.Item6}");

            //var testTuple10 = new Tuple<int, int, int, int, int, int, int, Tuple<int, int, int>>(1, 2, 3, 4, 5, 6, 7, new Tuple<int, int, int>(8, 9, 10));
            //Console.WriteLine($"Item 1: {testTuple10.Item1}, Item 10: {testTuple10.Rest.Item3}");

            var testTuple6 = Tuple.Create<int, int, int, int, int, int>(1, 2, 3, 4, 5, 6);
            Console.WriteLine($"Item 1: {testTuple6.Item1}, Item 6: {testTuple6.Item6}");

            //这里构建出来的Tuple类型其实是Tuple<int, int, int, int, int, int, int, Tuple<int>>，因此testTuple8.Rest取到的数据类型是Tuple<int>，因此要想获取准确值需要取Item1属性。
            var testTuple8 = Tuple.Create<int, int, int, int, int, int, int, int>(1, 2, 3, 4, 5, 6, 7, 8);
            Console.WriteLine($"Item 1: {testTuple8.Item1}, Item 8: {testTuple8.Rest.Item1}");

            //错误
            // Console.WriteLine($"Item 1: {testTuple8.Item1}, Item 8: {testTuple8.Item8}");


            var studentInfo = Tuple.Create<string, int, uint>("Bob", 28, 175);
            Console.WriteLine($"Student Information: Name [{studentInfo.Item1}], Age [{studentInfo.Item2}], Height [{studentInfo.Item3}]");

            RunTest();

            #endregion



            Console.ReadKey();
        }

        static Tuple<string, int, uint> GetStudentInfo(string name)
        {
            return new Tuple<string, int, uint>("Bob", 28, 175);
        }

        static void RunTest()
        {
            var studentInfo = GetStudentInfo("Bob");
            Console.WriteLine($"Student Information: Name [{studentInfo.Item1}], Age [{studentInfo.Item2}], Height [{studentInfo.Item3}]");
        }

        //static void Main(string[] args)
        //{
        //    //Heater heater = new Heater("001-type","dongtai");
        //    Heater heater = new Heater();
        //    Alarm alarm = new Alarm();
        //    Display display = new Display();

        //    heater.Boiled += alarm.MakeAlert;
        //    heater.Boiled += display.ShowMsg;

        //    heater.BoiledWater();

        //    Console.ReadKey();
        //}

        public class Heater
        {
            public delegate void BoiledWaterHandler(object sender, BoiledEventArgs e);

            public event BoiledWaterHandler Boiled;

            public int Temperature { get; set; }
            public string Type { get; set; } = "002";
            public string Area { get; set; } = "dongtai";

            //public Heater(string type, string area)
            //{
            //    this.Type = type;
            //    this.Area = area;
            //}
            public class BoiledEventArgs : EventArgs
            {
                public readonly int temperature;

                public BoiledEventArgs(int temperature)
                {
                    this.temperature = temperature;
                }
            }

            protected virtual void OnBoiled(BoiledEventArgs e)
            {
                if (Boiled != null)
                    Boiled(this, e);
            }
            //IList;
            //    List

            public void BoiledWater()
            {
                for (int i = 1; i < 100; i++)
                {
                    this.Temperature = i;
                    if (Temperature > 95)
                    {
                        BoiledEventArgs e = new BoiledEventArgs(Temperature);
                        OnBoiled(e);
                    }
                }
            }
        }

        public class Alarm
        {
            public void MakeAlert(object sender, Heater.BoiledEventArgs e)
            {
                Heater heater = (Heater)sender;
                Console.WriteLine($"Alarm: {heater.Area}-{heater.Type}");
                Console.WriteLine($"Alarm: now the temperature is {e.temperature}");

            }
        }

        public class Display
        {
            public void ShowMsg(object sender, Heater.BoiledEventArgs e)
            {
                Heater heater = (Heater)sender;
                Console.WriteLine($"Display: {heater.Area}-{heater.Type}");
                Console.WriteLine($"Display: now the temperature is {e.temperature}");
            }
        }

        //static void Main(string[] args)
        //{
        //    Console.WriteLine("Hello World!");
        //    ReadTemplate("test.txt");
        //}

        //private static string ReadTemplate(string templateName)
        //{
        //    var currentAssembly = Assembly.GetExecutingAssembly();
        //    var text = string.Empty;
        //    var resourceName = $"{currentAssembly.GetName().Name}.CodeTemplate.{templateName}";
        //    using (var stream = currentAssembly.GetManifestResourceStream(resourceName))
        //    {
        //        if (stream != null)
        //        {
        //            using (var reader = new StreamReader(stream))
        //            {
        //                text = reader.ReadToEnd();
        //            }
        //        }
        //    }
        //    return text;
        //}

    }
}
