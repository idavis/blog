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
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

#endregion

namespace MatrixMultiplication
{
    public class OptimizedParallel
    {
        private static int ChunkFactor { get; set; }

        public static TimeSpan Multiply( int N )
        {
            return Multiply( N, Environment.ProcessorCount );
        }

        public static TimeSpan Multiply( int N, int chunkFactor )
        {
            ChunkFactor = chunkFactor;

            var C = new double[N][];
            var A = new double[N][];
            var B = new double[N][];

            Util.Initialize( N, A, B, C );

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            IEnumerable<Tuple<int, double[][]>> data = PartitionData( N, A );
            Parallel.ForEach( data, item => Multiply( item, B, C ) );
            stopwatch.Stop();
            return stopwatch.Elapsed;
        }

        private static IEnumerable<Tuple<int, double[][]>> PartitionData( int N, double[][] A )
        {
            int pieces = ( ( N % ChunkFactor ) == 0 )
                             ? N / ChunkFactor
                             : ( (int) ( ( N ) / ( (float) ChunkFactor ) ) + 1 );

            int remaining = N;
            int currentRow = 0;

            while ( remaining > 0 )
            {
                if ( remaining < ChunkFactor )
                {
                    ChunkFactor = remaining;
                }

                remaining = remaining - ChunkFactor;
                var ai = new double[ChunkFactor][];
                for ( int i = 0; i < ChunkFactor; i++ )
                {
                    ai[i] = A[currentRow + i];
                }

                int oldRow = currentRow;
                currentRow += ChunkFactor;
                yield return new Tuple<int, double[][]>( oldRow, ai );
            }
        }

        private static void Multiply( Tuple<int, double[][]> A, double[][] B, double[][] C )
        {
            int size = A.Item2.GetLength( 0 );
            int cols = B[0].Length;
            double[][] ai = A.Item2;

            int i = 0;
            int offset = A.Item1;
            do
            {
                int k = 0;
                do
                {
                    int j = 0;
                    do
                    {
                        double[] ci = C[offset];
                        ci[j] = ( ai[i][k] * B[k][j] ) + ci[j];
                        j++;
                    } while ( j < cols );
                    k++;
                } while ( k < cols );
                i++;
                offset++;
            } while ( i < size );
        }
    }
}