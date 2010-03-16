#region License

// 
// Author: Ian Davis <ian@innovatian.com>
// Copyright (c) 2010, Innovatian Software, LLC
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

namespace MatrixMultiplication
{
    internal class Util
    {
        internal static void Initialize( int N, double[][] A, double[][] B, double[][] C )
        {
            int index = 0;
            do
            {
                C[index] = new double[N];
                B[index] = new double[N];
                A[index] = new double[N];
                int column = 0;
                int value = 0;
                do
                {
                    C[index][column] = 0;
                    double num7 = value; // used so we only cast once
                    B[index][column] = num7;
                    A[index][column] = num7;
                    column++;
                    value = index + value;
                } while ( column < N );
                index++;
            } while ( index < N );
        }

        internal static void Initialize( int N, double[,] A, double[,] B, double[,] C )
        {
            int index = 0;
            do
            {
                int column = 0;
                int value = 0;
                do
                {
                    C[index, column] = 0;
                    double num7 = value; // used so we only cast once
                    B[index, column] = num7;
                    A[index, column] = num7;
                    column++;
                    value = index + value;
                } while ( column < N );
                index++;
            } while ( index < N );
        }

        internal static void Initialize( int N, double[] A, double[] B, double[] C )
        {
            for ( int i = 0; i < N; i++ )
            {
                for ( int j = 0; j < N; j++ )
                {
                    C[i * N + j] = 0;
                    B[i * N + j] = i * j;
                    A[i * N + j] = i * j;
                }
            }
        }
    }
}