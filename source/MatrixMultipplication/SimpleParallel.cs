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
using System.Diagnostics;
using System.Threading.Tasks;

#endregion

namespace MatrixMultiplication
{
    public static class SimpleParallel
    {
        public static TimeSpan Multiply( int N )
        {
            var C = new double[N][];
            var A = new double[N][];
            var B = new double[N][];
            Util.Initialize( N, A, B, C );

            Stopwatch stopwatch = Stopwatch.StartNew();

            Parallel.For( 0, N, i =>
                                {
                                    for ( int k = 0; k < N; k++ )
                                    {
                                        for ( int j = 0; j < N; j++ )
                                        {
                                            double[] Ci = C[i];
                                            Ci[j] = ( A[i][k] * B[k][j] ) + Ci[j];
                                        }
                                    }
                                } );

            stopwatch.Stop();
            return stopwatch.Elapsed;
        }
    }
}