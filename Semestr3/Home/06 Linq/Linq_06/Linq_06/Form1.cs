using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Linq_06
{
    public partial class Form1 : Form
    {
        City[] cities = new City[]
        {
              new City{ Name = "Rivne", Country="Ukraine", Population = 250000, IsCapital = false },
              new City{ Name = "Warsaw", Country="Poland", Population = 1800000, IsCapital = true },
              new City{ Name = "Lviv", Country="Ukraine", Population = 720000, IsCapital = false },
              new City{ Name = "Krakow", Country="Poland", Population = 780000, IsCapital = false },
              new City{ Name = "Odessa", Country="Ukraine", Population = 1020000, IsCapital = false },
              new City{ Name = "London", Country="Great Britain", Population = 8900000, IsCapital = true },
              new City{ Name = "Paris", Country="France", Population = 2180000, IsCapital = true },
              new City{ Name = "Berlin", Country="Germany", Population = 3600000, IsCapital = true },
              new City{ Name = "Wroclaw", Country="Poland", Population = 640000, IsCapital = false },
              new City{ Name = "Kyiv", Country="Ukraine", Population = 3000000, IsCapital = true },
              new City{ Name = "Munich", Country="Germany", Population = 1480000, IsCapital = false },
              new City{ Name = "Dnipro", Country="Ukraine", Population = 980000, IsCapital = false },
              new City{ Name = "Cologne", Country="Germany", Population = 1000000, IsCapital = false }
        };
        int[] numbers = new[] { 2, 9, 47, 69, 20, -1, 13, -26, 37, -40, 18, 70, -31, 7, -47, -7, 1 };

        public Form1()
        {
            InitializeComponent();
            dataGridView1.DataSource = cities;

            foreach (var i in numbers)
            { label2.Text += $"[{i}], "; }

            var capital = cities.Where(x=> x.IsCapital).Select(x => new string ( x.Name));
        }

//        Використовуючи синтаксис запитів вивести на екран:
//> Із "cities"
//   - інформацію про столиці(назву, країну та населення);
//   - назви міст, що містять букву "і" у назві міста;
//   - назви столиць разом із населенням у порядку спадання населення;
//   - назви країн, що містять міста, назви яких закінчуютсья на букву "w";
//   - назви міст, де назва країни закінчується на "e", а назви міст починаються на букву "a";
//> Із "numbers"
//   - всі непарні числа;
//   - додатні числа із "numbers" у порядку зростання;
//   - від'ємні числа із "numbers" у порядку спадання.


        void Print()
        {
          

        }


    }
}
