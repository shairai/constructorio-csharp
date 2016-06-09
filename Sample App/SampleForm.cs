using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConstructorIO;
using System.Collections;
using System.IO;
using System.Windows.Controls;
using System.Net;

namespace Sample_App
{
    public partial class SampleForm : Form
    {
        private string m_sFileNameCSV;
        private DataTable m_dtCSVData;
        public ConstructorIOAPI m_constructorClient;
        public ListItemAutocompleteType m_currentType;

        /// <summary>
        /// Sample Form
        /// </summary>
        public SampleForm()
        {
            m_dtCSVData = new DataTable();

            InitializeComponent();
        }

        /// <summary>
        /// LoadData()
        /// </summary>
        private void Init()
        {
            m_constructorClient = new ConstructorIOAPI(txtAPIToken.Text, txtKey.Text);
        }

        /// <summary>
        /// btnLoadCSV_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadCSV_Click(object sender, EventArgs e)
        {
            openFileDialogCSV = new OpenFileDialog();
            openFileDialogCSV.Filter = "CSV files | *.csv";
            openFileDialogCSV.Title = "Select CSV file";
            if (openFileDialogCSV.ShowDialog() == DialogResult.OK)
            {
                txtFile.Text = m_sFileNameCSV = openFileDialogCSV.FileName;
                this.LoadCSVFile();
            }
        }

        /// <summary>
        /// LoadCSVFile
        /// </summary>
        private void LoadCSVFile()
        {
            try
            {
                List<string> lCSV = new List<string>();
                using (var textReader = new StreamReader(m_sFileNameCSV))
                {
                    while (!textReader.EndOfStream)
                    {
                        lCSV.Add(textReader.ReadLine());
                    }

                    this.LoadCSVToDataSet(ref lCSV);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// LoadCSVToDataSet
        /// </summary>
        /// <param name="lCSV"></param>
        private void LoadCSVToDataSet(ref List<string> lCSV)
        {
            try
            {
                char cDelimiter = ',';
                object[] arData;
                string[] sLine = lCSV[0].Split(new char[] { cDelimiter }, StringSplitOptions.RemoveEmptyEntries);

                m_dtCSVData.Clear();
                m_dtCSVData.Columns.Clear();

                for (int i = 0; i < sLine.Length; i++)
                {
                    m_dtCSVData.Columns.Add(sLine[i]);
                }

                for (int i = 1; i < lCSV.Count; i++)
                {
                    arData = lCSV[i].Split(cDelimiter);
                    m_dtCSVData.Rows.Add(arData);
                }
                dataGridViewCSVData.DataSource = m_dtCSVData;

                m_dtCSVData.EndLoadData();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// btnExit_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// btnUpload_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnUpload_Click(object sender, EventArgs e)
        {
            this.Init();
            pictureBoxLoading.Visible = true;
            bool success = await this.ProcessUpload(m_currentType, false);
            if(success)
            {
                MessageBox.Show("Sucessfully uploaded.");
            }
            else
            {

            }

            pictureBoxLoading.Visible = false;
        }

        private async void btnUploadUpdate_Click(object sender, EventArgs e)
        {
            this.Init();
            pictureBoxLoading.Visible = true;
            bool success = await this.ProcessUpload(m_currentType, true);
            if (success)
            {
                MessageBox.Show("Sucessfully uploaded.");
            }
            else
            {

            }

            pictureBoxLoading.Visible = false;
        }

        /// <summary>
        /// ProcessUpload
        /// </summary>
        /// <param name="atType"></param>
        private async Task<bool> ProcessUpload(ListItemAutocompleteType atType, bool updateExisting)
        {
            try
            {
                var datagridRows = dataGridViewCSVData.Rows.Cast<DataGridViewRow>();
                var mappedElements = datagridRows.Select(dgr => RowToListItem(dgr));

                if (updateExisting)
                    return await m_constructorClient.AddOrUpdateBatchAsync(mappedElements, atType);
                else
                    return await m_constructorClient.AddBatchAsync(mappedElements, atType);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// LoadData
        /// </summary>
        /// <param name="objDic"></param>
        private ListItem RowToListItem(DataGridViewRow datagridRow)
        {
            var newItem = new ListItem(
                Name: TryGetColumn(datagridRow, "Name"),
                Description: TryGetColumn(datagridRow, "Description"),
                ID: TryGetColumn(datagridRow, "ID"),
                Url: TryGetColumn(datagridRow, "Url"),
                ImageUrl: TryGetColumn(datagridRow, "ImageUrl"));

            if (TryGetColumn(datagridRow, "keywords") != null)
                newItem.Keywords = TryGetColumn(datagridRow, "Keywords").Split(';').ToList();

            foreach (DataGridViewColumn column in datagridRow.DataGridView.Columns)
            {
                if (column.Name != "Name" && column.Name != "Description" && column.Name != "ID" && column.Name != "Url" && column.Name != "ImageUrl")
                {
                    newItem[column.Name] = datagridRow.Cells[column.Name].Value;
                }
            }

            return newItem;
        }

        private string TryGetColumn(DataGridViewRow datagridRow, string ColumnName)
        {
            if(datagridRow.DataGridView.Columns.Contains(ColumnName) && datagridRow.Cells[ColumnName] != null)
            {
                return datagridRow.Cells[ColumnName].Value.ToString();
            }
            return null;
        }

        private async void btnVerify_Click(object sender, EventArgs e)
        {
            this.Init();
            bool result = await m_constructorClient.VerifyAsync();
            if(result)
            {
                MessageBox.Show("Valid Credentials.");
                //TODO: Enable view
            }
            else
            {
                MessageBox.Show("Invalid Credentials.");
            }
        }

        private void comboBoxType_SelectedValueChanged(object sender, EventArgs e)
        {
            Enum.TryParse<ListItemAutocompleteType>(comboBoxType.SelectedText, out this.m_currentType);
        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
