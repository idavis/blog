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
    public static class JaggedFromCpp
    {
        public static TimeSpan Multiply( int N )
        {
            var C = new double[N][];
            var A = new double[N][];
            var B = new double[N][];
            Util.Initialize( N, A, B, C );

            Stopwatch stopwatch = Stopwatch.StartNew();
            int i = 0;
            if ( 0 < N )
            {
                do
                {
                    int k = 0;
                    do
                    {
                        int j = 0;
                        do
                        {
                            double[] Ci = C[i];
                            Ci[j] = ( A[i][k] * B[k][j] ) + Ci[j];
                            j++;
                        } while ( j < N );
                        k++;
                    } while ( k < N );
                    i++;
                } while ( i < N );
            }
            stopwatch.Stop();
            return stopwatch.Elapsed;
        }
    }
}