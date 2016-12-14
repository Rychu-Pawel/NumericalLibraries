using System;
using NumericalLibraries.LinearEquation.Exceptions;

namespace NumericalLibraries.LinearEquation
{
    public class LinearEquation
    {
    //ZMIENNE --------------------------
        private int iloscZmiennych;
        double[,] wspolczynniki;
        double[] niewiadome;

    //METODY ---------------------------
        private void Mnozenie(int n)
        {
            for (int i = n; i < iloscZmiennych; i++)
            {
                double mnoznik = (wspolczynniki[n - 1, n - 1] / wspolczynniki[n - 1, i]); // wartosc przez ktora mnozymy caly wiersz macierzy

                if (mnoznik != 1)
                    for (int j = n - 1; j <= iloscZmiennych; j++)
                        wspolczynniki[j, i] *= mnoznik;
            }
        }

        private void Odejmowanie(int n)
        {
            for (int i = n; i < iloscZmiennych; i++)
                for (int j = n - 1; j <= iloscZmiennych; j++)
                    wspolczynniki[j, i] -= wspolczynniki[j, n - 1];
        }

        private void WyliczNiewiadome()
        {
            for (int i = iloscZmiennych - 1; i >= 0; i--)
            {
                //Obliczam ostateczne r
                for(int j = iloscZmiennych - i - 1; j > 0; j--)
                    wspolczynniki[iloscZmiennych, i] -= (wspolczynniki[iloscZmiennych - j, i] * niewiadome[iloscZmiennych - j]);

                //Obliczam i-tą niewiadomą
                niewiadome[i] = wspolczynniki[iloscZmiennych, i] / wspolczynniki[i, i];

                if (double.IsNaN(niewiadome[i]))
                    throw new InconsistentSystemOfEquationsException();

                //Formatowanie niewiadomoej, żeby 4,0000000000001 wypluł jako 4
                if (Math.Abs(niewiadome[i] - Math.Floor(niewiadome[i])) < 0.000000001)
                    niewiadome[i] = Math.Floor(niewiadome[i]);
                else if (Math.Abs(niewiadome[i] - Math.Ceiling(niewiadome[i])) < 0.000000001)
                    niewiadome[i] = Math.Ceiling(niewiadome[i]);
            }
        }

        /// <summary>
        /// Compute variables
        /// </summary>
        /// <returns></returns>
        public double[] Compute()
        {
            //Eliminacja Gaussa (otrzymujemy zera pod diagonala)
            for (int i = 1; i < iloscZmiennych; i++)
            {
                Mnozenie(i);

                Odejmowanie(i);
            }

            WyliczNiewiadome();

            return niewiadome;
        }

        //KONSTRUKTOR ----------------------
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dgvGauss">Data grid view with coefficients standing by the variables</param>
        public LinearEquation(System.Windows.Forms.DataGridView dgvGauss)
        {
            if (dgvGauss.Rows.Count + 1 != dgvGauss.Columns.Count)
                throw new RowsNumberMustBeOneLessThenColumnsNumberException();

            iloscZmiennych = dgvGauss.Rows.Count;

            wspolczynniki = new double[iloscZmiennych + 1, iloscZmiennych];

            for (int i = 0; i <= iloscZmiennych; i++)
                for (int j = 0; j < iloscZmiennych; j++)
                    wspolczynniki[i, j] = Convert.ToDouble(dgvGauss[i, j].Value);

            niewiadome = new double[iloscZmiennych];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coefficients">Coefficients standing by the variables. Double[rows, rows + 1]</param>
        public LinearEquation(double[,] coefficients) //wspolczynniki
        {
            //Sprawdzenie rozmiaru tablicy
            if (coefficients.GetLength(0) + 1 != coefficients.GetLength(1))
                throw new RowsNumberMustBeOneLessThenColumnsNumberException();

            //niestety algorytm zostal napisany źle i trzeba przepisać tablice
            this.wspolczynniki = new double[coefficients.GetLength(1), coefficients.GetLength(0)];

            for (int i = 0; i < coefficients.GetLength(0); i++)
                for (int j = 0; j < coefficients.GetLength(1); j++)
                    wspolczynniki[j, i] = coefficients[i, j];

            iloscZmiennych = wspolczynniki.GetLength(1);

            niewiadome = new double[iloscZmiennych];
        }
    }
}
