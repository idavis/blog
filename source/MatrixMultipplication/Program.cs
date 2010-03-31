#region License

// 
// Author: Ian Davis <ian@innovatian.com>
// Copyright (c) 2010, Innovatian Software, LLC
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using System;
using System.IO;

#endregion

namespace MatrixMultiplication
{
    internal class Program
    {
        private const int Min = 100;
        private const int Max = 2000;
        private const int Step = 100;
        private const int Tries = 3;

        private static void Main()
        {
            try
            {
                /*
                Run("Dumb", Dumb.Multiply);
                Run("FixedSingle", FixedSingle.Multiply);
                Run("Jagged", Jagged.Multiply);
                Run("JaggedFromCpp", JaggedFromCpp.Multiply);
                Run("OptimizedParallel", OptimizedParallel.Multiply);
                Run("SimpleParallel", SimpleParallel.Multiply);
                Run("Single", Single.Multiply);
                Run("Standard", Standard.Multiply);
                Run("UnsafeSingle", UnsafeSingle.Multiply);
                 */
                //RunParallelAdjustingChunkFactor( "ParallelTuning", OptimizedParallel.Multiply, 1 );
            }
            finally
            {
                Console.WriteLine( "Press Enter to Exit" );
                Console.ReadLine();
            }
        }

        public static void Run( string method, Func<int, TimeSpan> multiplier )
        {
            try
            {
                var current = new TimeSpan[Tries];

                for ( int N = Min; N <= Max; N += Step )
                {
                    for ( int i = 0; i < Tries; i++ )
                    {
                        current[i] = multiplier( N );
                    }
                    var total = new TimeSpan();
                    for ( int i = 0; i < Tries; i++ )
                    {
                        total += current[i];
                    }
                    RecordTiming( method + ".txt", method, N, total.TotalSeconds / Tries );
                }
            }
            catch ( Exception ex )
            {
                Console.WriteLine( ex );
            }
        }

        public static void RunParallelAdjustingChunkFactor( string method, Func<int, int, TimeSpan> multiplier,
                                                            int chunkFactorStep )
        {
            try
            {
                var current = new TimeSpan[Tries];

                for ( int N = Min; N <= Max; N += Step )
                {
                    for ( int c = Environment.ProcessorCount;
                          c <= ( N / Environment.ProcessorCount );
                          c += chunkFactorStep )
                    {
                        for ( int i = 0; i < Tries; i++ )
                        {
                            current[i] = multiplier( N, c );
                        }
                        var total = new TimeSpan();
                        for ( int i = 0; i < Tries; i++ )
                        {
                            total += current[i];
                        }
                        RecordTiming( method + ".txt", method, N, total.TotalSeconds / Tries, c );
                    }
                }
            }
            catch ( Exception ex )
            {
                Console.WriteLine( ex );
            }
        }

        private static void RecordTiming( string fileName, string type, int size, double totalSeconds )
        {
            double w = Math.Pow( size, 3 ) / totalSeconds;
            string perf = String.Format( "{0},{1},{2},{3}", size, totalSeconds, w, Environment.NewLine );
            Console.WriteLine( "[{0}]\t{1}", type, perf );
            File.AppendAllText( fileName, perf );
        }

        private static void RecordTiming( string fileName, string type, int size, double totalSeconds, int chunkFactor )
        {
            double w = Math.Pow( size, 3 ) / totalSeconds;
            string perf = String.Format( "{0},{1},{2},{3},{4}", size, chunkFactor, totalSeconds, w, Environment.NewLine );
            Console.WriteLine( "[{0}]\t{1}", type, perf );
            File.AppendAllText( fileName, perf );
        }
    }
}