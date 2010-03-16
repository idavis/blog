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
    public static class UnsafeSingle
    {
        public static unsafe void MultiplySmall( int N )
        {
            double* C = stackalloc double[N * N];
            double* A = stackalloc double[N * N];
            double* B = stackalloc double[N * N];
            int i, j, k;

            for ( i = 0; i < N; i++ )
            {
                for ( j = 0; j < N; j++ )
                {
                    C[i * N + j] = 0;
                    B[i * N + j] = i * j;
                    A[i * N + j] = i * j;
                }
            }

            DateTime now = DateTime.Now;

            for ( i = 0; i < N; i++ )
            {
                for ( k = 0; k < N; k++ )
                {
                    for ( j = 0; j < N; j++ )
                    {
                        C[i * N + j] += A[i * N + k] * B[k * N + j];
                    }
                }
            }

            Console.WriteLine( "Unsafe Small Single:\t{0}", ( DateTime.Now - now ) );
        }

        public static unsafe TimeSpan Multiply( int N )
        {
            var C = new double[N * N];
            var A = new double[N * N];
            var B = new double[N * N];
            Util.Initialize( N, A, B, C );

            Stopwatch stopwatch = Stopwatch.StartNew();

            fixed ( double* ptrA = A )
            {
                fixed ( double* ptrB = B )
                {
                    fixed ( double* ptrC = C )
                    {
                        for ( int i = 0; i < N; i++ )
                        {
                            for ( int k = 0; k < N; k++ )
                            {
                                for ( int j = 0; j < N; j++ )
                                {
                                    ptrC[i * N + j] += ptrA[i * N + k] * ptrB[k * N + j];
                                }
                            }
                        }
                    }
                }
            }

            stopwatch.Stop();
            return stopwatch.Elapsed;
        }
    }
}