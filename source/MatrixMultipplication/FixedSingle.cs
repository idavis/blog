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
    public class FixedSingle
    {
        private static int max = 300;

        public static unsafe TimeSpan Multiply( int N )
        {
            if ( N > max )
            {
                N = max;
            }

            int i, j, k;
            var accessor = new Arrays();
            for ( i = 0; i < N; i++ )
            {
                for ( j = 0; j < N; j++ )
                {
                    accessor.C[i * N + j] = 0;
                    accessor.B[i * N + j] = i * j;
                    accessor.A[i * N + j] = i * j;
                }
            }

            Stopwatch stopwatch = Stopwatch.StartNew();

            for ( i = 0; i < N; i++ )
            {
                for ( k = 0; k < N; k++ )
                {
                    for ( j = 0; j < N; j++ )
                    {
                        accessor.C[i * N + j] += accessor.A[i * N + k] * accessor.B[k * N + j];
                    }
                }
            }

            stopwatch.Stop();
            return stopwatch.Elapsed;
        }

        #region Nested type: Arrays

        public unsafe struct Arrays
        {
            public fixed double A [250000];
            public fixed double B [250000];
            public fixed double C [250000];
        }

        #endregion
    }
}