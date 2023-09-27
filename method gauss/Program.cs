using System;

class GaussElimination
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

        // Решение системы
        double[] X = Solve(A, B);

        // Вывод решения
        Console.WriteLine("Решение:");
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"x{i + 1} = {X[i]}");
        }
    }

    // Метод для решения системы линейных уравнений методом Гаусса
    static double[] Solve(double[,] A, double[] B)
    {
        int n = B.Length;
        double[] X = new double[n];

        for (int i = 0; i < n; i++)
        {
            // Прямой ход (приведение матрицы к верхнетреугольному виду)
            for (int k = i + 1; k < n; k++)
            {
                double factor = A[k, i] / A[i, i];
                B[k] -= factor * B[i];
                for (int j = i; j < n; j++)
                {
                    A[k, j] -= factor * A[i, j];
                }
            }
        }

        // Обратный ход (нахождение значений переменных)
        for (int i = n - 1; i >= 0; i--)
        {
            X[i] = B[i] / A[i, i];
            for (int k = i - 1; k >= 0; k--)
            {
                B[k] -= A[k, i] * X[i];
            }
        }

        return X;
    }
}
