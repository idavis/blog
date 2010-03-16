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

#endregion

namespace MatrixMultiplication
{
    public static class Standard
    {
        public static TimeSpan Multiply( int N )
        {
            var C = new double[N,N];
            var A = new double[N,N];
            var B = new double[N,N];
            Util.Initialize( N, A, B, C );

            Stopwatch stopwatch = Stopwatch.StartNew();

            for ( int i = 0; i < N; i++ )
            {
                for ( int k = 0; k < N; k++ )
                {
                    for ( int j = 0; j < N; j++ )
                    {
                        C[i, j] += A[i, k] * B[k, j];
                    }
                }
            }

            stopwatch.Stop();
            return stopwatch.Elapsed;
        }
    }
}