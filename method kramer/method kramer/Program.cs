using System;

class CramerMethod
{
    static void Main()
    {
        // Введите количество уравнений
        Console.Write("Введите количество уравнений: ");
        int n = int.Parse(Console.ReadLine());

        // Создайте матрицу коэффициентов A и вектор правой части B
        double[,] A = new double[n, n];
        double[] B = new double[n];

        Console.WriteLine("Введите коэффициенты для каждого уравнения и правую часть:");

        // Ввод коэффициентов и правой части
        for (int i = 0; i < n; i++)
        {
            Console.Write($"Уравнение {i + 1}: ");
            string[] input = Console.ReadLine().Split(' ');
            if (input.Length != n + 1)
            {
                Console.WriteLine("Неверное количество коэффициентов. Повторите ввод.");
                i--;
                continue;
            }

            for (int j = 0; j < n; j++)
            {
                A[i, j] = double.Parse(input[j]);
            }
            B[i] = double.Parse(input[n]);
        }

        // Решение системы методом Крамера
        double[] X = SolveWithCramer(A, B);

        // Вывод решения
        if (X != null)
        {
            Console.WriteLine("Решение:");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"x{i + 1} = {X[i]}");
            }
        }
        else
        {
            Console.WriteLine("Система уравнений не имеет решения.");
        }
    }

    // Метод для решения системы линейных уравнений методом Крамера
    static double[] SolveWithCramer(double[,] A, double[] B)
    {
        int n = B.Length;
        double[] X = new double[n];

        // Вычисление определителя матрицы коэффициентов A
        double detA = Determinant(A);

        if (Math.Abs(detA) < 1e-10)
        {
            // Определитель равен нулю, система вырождена
            return null;
        }

        for (int i = 0; i < n; i++)
        {
            // Создаем копию матрицы A
            double[,] tempA = new double[n, n];
            for (int j = 0; j < n; j++)
            {
                for (int k = 0; k < n; k++)
                {
                    tempA[j, k] = A[j, k];
                }
            }

            // Заменяем i-ый столбец матрицы tempA на вектор B
            for (int j = 0; j < n; j++)
            {
                tempA[j, i] = B[j];
            }

            // Вычисляем определитель матрицы tempA
            double detTempA = Determinant(tempA);

            // Находим значение переменной xi
            X[i] = detTempA / detA;
        }

        return X;
    }

    // Метод для вычисления определителя матрицы
    static double Determinant(double[,] matrix)
    {
        int n = matrix.GetLength(0);

        if (n == 1)
        {
            return matrix[0, 0];
        }

        if (n == 2)
        {
            return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
        }

        double det = 0.0;
        for (int i = 0; i < n; i++)
        {
            double[,] subMatrix = new double[n - 1, n - 1];
            for (int j = 1; j < n; j++)
            {
                for (int k = 0; k < n; k++)
                {
                    if (k < i)
                    {
                        subMatrix[j - 1, k] = matrix[j, k];
                    }
                    else if (k > i)
                    {
                        subMatrix[j - 1, k - 1] = matrix[j, k];
                    }
                }
            }
            det += (i % 2 == 0 ? 1 : -1) * matrix[0, i] * Determinant(subMatrix);
        }
        return det;
    }
}
