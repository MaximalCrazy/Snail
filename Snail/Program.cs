using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Snail
{
    // https://www.codewars.com/kata/521c2db8ddc89b9b7a0000c1
    /*
     Time: 2639ms Passed: 5Failed: 0
     Test Results:
     SnailTest
     SnailRandomTest
     SnailTest1
     SnailTest2
     SnailTest2Empty
     SnailTestOne
     Completed in 94.0840ms
     */
    public class Program
    {
        static void Main(string[] args)
        {
            //var array3 = new int[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            var demoArray3 = new int[][] {
                new []{1, 2, 3},
                new []{4, 5, 6},
                new []{7, 8, 9}
            };

            var snailSolution = new SnailSolution(demoArray3);

        }
    }

    public class SnailSolution
    {
        private static List<int> ergebnisResult;

        public SnailSolution(int[][] demoArray3)
        {
            Snail(demoArray3);
        }

        public static int Schrittweite { get; set; }

        public static List<int> AktuellePosition { get; set; }

        public static int Durchläufe { get; set; }

        public static List<int> ErgebnisResult
        {
            #region Debug

            //ZeigePosition(AktuellePosition);
            //ZeigeWert(ergebnisResult);

            #endregion
            get => ergebnisResult;
            set => ergebnisResult = value;
        }

        public static int[] Snail(int[][] array)
        {
            if (array[0].Length == 0) return new int[] { };

            ErgebnisResult = new List<int>{};
            Schrittweite = array.GetLength(0);
            AktuellePosition = new List<int> { 0, 0 };
            Durchläufe = array.GetLength(0) * array.GetLength(0);
            
            do
            {
                #region Prüfung auf Schrittweite

                if (Durchläufe == 0 || Schrittweite == 0) break;

                #endregion

                //Rechts
                SchritteRechts(array);

                #region Prüfung auf Schrittweite + Reduktion

                Schrittweite--;
                if (Durchläufe == 0 || Schrittweite == 0) break;

                #endregion

                //Runter
                SchritteRunter(array);

                #region Prüfung auf Schrittweite

                if (Durchläufe == 0 || Schrittweite == 0) break;

                #endregion

                //Links
                SchritteLinks(array);

                #region Prüfung auf Schrittweite + Reduktion

                if (Durchläufe == 0 || Schrittweite == 0) break;

                Schrittweite--;

                #endregion

                //Hoch
                SchritteHoch(array);

            } while (Durchläufe >= 0);
            
            return ErgebnisResult.ToArray();
        }
        
        private static void SchritteHoch(int[][] array)
        {
            var endWert = AktuellePosition[1] - Schrittweite;
            for (var y = AktuellePosition[1] - 1; y >= endWert; y--)
            {
                //Limit
                Durchläufe--;

                //AktuellePosition y ermitteln
                AktuellePosition[1] = y; //ToDo Value Check

                ErgebnisResult.Add(GibWertZurück(array));
            }
        }

        private static void SchritteLinks(int[][] array)
        {
            var endWert = AktuellePosition[0] - Schrittweite;
            for (var x = AktuellePosition[0] - 1; x >= endWert; x--)
            {
                //Limit
                Durchläufe--;

                //AktuellePosition x ermitteln
                AktuellePosition[0] = x;

                ErgebnisResult.Add(GibWertZurück(array));
            }
        }

        private static void SchritteRunter(int[][] array)
        {
            var endWert = AktuellePosition[1] + Schrittweite;
            for (var y = AktuellePosition[1] + 1; y <= endWert; y++)
            {
                //Limit
                Durchläufe--;

                //AktuellePosition y ermitteln
                AktuellePosition[1] = y;

                ErgebnisResult.Add(GibWertZurück(array));
            }
        }

        private static void SchritteRechts(int[][] array)
        {
            //Sonderfall AktuellePosition 1
            if (AktuellePosition[0] == 0 && AktuellePosition[1] == 0)
            {
                var endWert = AktuellePosition[0] + Schrittweite - 1;
                for (var x = AktuellePosition[0]; x <= endWert; x++)
                {
                    //Limit
                    Durchläufe--;

                    //AktuellePosition x ermitteln
                    AktuellePosition[0] = x;

                    ErgebnisResult.Add(GibWertZurück(array));
                }
            }
            else
            {
                var endWert = AktuellePosition[0] + Schrittweite;
                for (var x = AktuellePosition[0] + 1; x <= endWert; x++)
                {
                    //Limit
                    Durchläufe--;

                    //AktuellePosition x ermitteln
                    AktuellePosition[0] = x;

                    ErgebnisResult.Add(GibWertZurück(array));
                }
            }
        }

        private static int GibWertZurück(int[][] array)
        {
            //Gibt Wert der AktuellePosition im Array wieder
            return array[AktuellePosition[1]][AktuellePosition[0]];
        }

        #region Debugging

        private static void ZeigePosition(List<int> value)
        {
            //Gibt AktuellePosition im Array wieder
            Console.WriteLine("AktuellePosition X|Y = " + value[0] + "|" + value[1]);
        }
        private static void ZeigeWert(List<int> value)
        {
            //Gibt AktuellePosition im Array wieder
            Console.WriteLine("Aktueller Wert = " + value.LastOrDefault());
        }

        //private void ZeigeArray(int[,] array)
        //{
        //    if (array.Length > 0)
        //    {
        //        for (int i = 0; i < array.GetLength(0); i++)
        //        {
        //            for (int j = 0; j < array.GetLength(1); j++)
        //            {
        //                Console.Write(array[i, j] + "\t");
        //            }
        //            Console.WriteLine();
        //        }
        //    }
        //    Console.WriteLine();
        //}

        #endregion

    }
    
}
