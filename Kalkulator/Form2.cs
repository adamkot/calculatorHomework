using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kalkulator
{
    public partial class Form2 : Form
    {

        //DataGridViewRow row;
        int id = 0;
        Form1 form1;

        public Form2(Form1 f)
        {
            InitializeComponent();
            form1 = f;
        }

        public void addRow(String equal, String operation)
        {
            DataGridViewRow row;
            row = (DataGridViewRow)Table.Rows[0].Clone();
            row.Cells[0].Value = ++id;
            row.Cells[1].Value = operation;
            row.Cells[2].Value = "Load";
            row.Cells[3].Value = equal;
            Table.Rows.Add(row);

            double sum = 0;
            int i = 0;
            for (; i < Table.Rows.Count; ++i)
            {
                sum += Convert.ToDouble(Table.Rows[i].Cells[3].Value);
            }
            sumLabel.Text = sum.ToString();
            averageLabel.Text = (sum / i).ToString();

        }

        private void Table_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.Table.Rows[e.RowIndex];
            form1.backFromTable(row.Cells[3].Value.ToString());
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            Table.DataSource = null;
            Table.Rows.Clear();
        }
    }
}

