using DataAccess;
using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchByName
{
    public partial class Form1 : Form
    {
        private List<string> _names = new List<string>();
        private List<string> _searchedNames = new List<string>();
        Stopwatch stopWatch = new Stopwatch();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Databaseden verileri hızlıca burada çektim.
            EfUserDal userDal = new EfUserDal();
            foreach (User user in userDal.GetAll())
            {
                _names.Add(user.Name);
            }
            SortNames();
        }
        public void SortNames()
        {
            _names = _names.OrderBy(x => x).ToList();
            _names = _names.ConvertAll(x => x.ToLower());
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            stopWatch.Reset();
            stopWatch.Start();
            _searchedNames.Clear();
            Search(textBox1.Text.ToString());
            dataGridView1.DataSource = _searchedNames.Select(x => new { Value = x }).ToList();
            stopWatch.Stop();
            label1.Text = stopWatch.Elapsed.TotalMilliseconds.ToString();
        }
        void Search(string text)
        {
            int index = GetStartIndex(_names,text);
            if (index<0)
            {
                return;
            }
            for (int i = index; i < _names.Count; i++)
            {
                if (_names.ElementAt(i).StartsWith(text))
                {
                    _searchedNames.Add(_names.ElementAt(i));
                }
                else
                {
                    if (_searchedNames.Count > 0)
                    {
                        return;
                    }
                }
            }
        }
        int GetStartIndex(List<string> nameList, string text)
        {
            int left = 0;
            int right = nameList.Count;
            while (left<= right)
            {
                var mid = (left + right) / 2;
                if (nameList.ElementAt(mid).StartsWith(text))
                {
                    for (int i = 0; i < right; i++)
                    {
                        if (mid-i<=0)
                        {
                            return 0;
                        }
                        if (!nameList.ElementAt(mid - i).StartsWith(text))
                        {
                            return mid - i;
                        }
                    }
                }
                if (String.Compare(nameList.ElementAt(mid), text)>0)
                {
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }
            }
            return -1;
        }
    }
}
