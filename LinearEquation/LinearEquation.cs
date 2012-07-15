using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumericalCalculator
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
        public LinearEquation(System.Windows.Forms.DataGridView dgvGauss, string changeFrom, string changeTo)
        {
            iloscZmiennych = dgvGauss.Rows.Count;

            wspolczynniki = new double[iloscZmiennych + 1, iloscZmiennych];

            for (int i = 0; i <= iloscZmiennych; i++)
                for (int j = 0; j < iloscZmiennych; j++)
                    wspolczynniki[i, j] = Convert.ToDouble(dgvGauss[i, j].Value.ToString().Replace(changeFrom, changeTo));

            niewiadome = new double[iloscZmiennych];
        }

        public LinearEquation(double[,] coefficients) //wspolczynniki
        {
            //to powinno byc odkreskowane, ale cos zle dziala :/
            //if (wspolczynniki.GetLength(0) != wspolczynniki.GetLength(1) - 1)
            //    throw new SystemException("Wielkosc tablicy jest niepoprawna. Oczekwiana wielkosc => [n, n-1].");

            this.wspolczynniki = coefficients;

            iloscZmiennych = coefficients.GetLength(1);

            niewiadome = new double[iloscZmiennych];
        }
    }
}
