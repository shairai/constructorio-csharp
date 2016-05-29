using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConstructorIOClient;
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
        public ConstructorIOClient.ConstructorIO m_constructorClient;
        public ConstructorIOClient.ConstructorIO.AutoCompleteListType m_currentType;

        private class Item
        {
            public string Text;
            public ConstructorIOClient.ConstructorIO.AutoCompleteListType CurrentType;

            public Item(string sName, ConstructorIOClient.ConstructorIO.AutoCompleteListType acltType)
            {
                Text = sName;
                CurrentType = acltType;
            }

            public ConstructorIOClient.ConstructorIO.AutoCompleteListType GetListType()
            {
                return CurrentType;
            }
        };

        /// <summary>
        /// Sample Form
        /// </summary>
        public SampleForm()
        {
            m_dtCSVData = new DataTable();

            InitializeComponent();



            ConstructorIOClient.ConstructorIO c = new ConstructorIOClient.ConstructorIO("", "");
            bool valid = c.Verify();
            c.Add("a", "b");
        }

        /// <summary>
        /// LoadData()
        /// </summary>
        private void Init()
        {
            m_constructorClient = new ConstructorIOClient.ConstructorIO(txtAPIToken.Text, txtKey.Text);
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
                string[] sLine = lCSV[0].Split(cDelimiter);

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
        private void btnUpload_Click(object sender, EventArgs e)
        {
            this.Init();
            pictureBoxLoading.Visible = true;
            backgroundWorkerUploadCSV.RunWorkerAsync(ConvertToType(comboBoxType.SelectedItem.ToString()));
        }

        /// <summary>
        /// backgroundWorkerUploadCSV_DoWork
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorkerUploadCSV_DoWork(object sender, DoWorkEventArgs e)
        {
            ConstructorIOClient.ConstructorIO.AutoCompleteListType arg = (ConstructorIOClient.ConstructorIO.AutoCompleteListType)e.Argument;
            try
            {
                this.ProcessUpload((ConstructorIOClient.ConstructorIO.AutoCompleteListType)e.Argument);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                
            }
        }

        /// <summary>
        /// backgroundWorkerUploadCSV_RunWorkerCompleted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorkerUploadCSV_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pictureBoxLoading.Visible = false;
        }

        /// <summary>
        /// ProcessUpload
        /// </summary>
        /// <param name="atType"></param>
        private void ProcessUpload(ConstructorIOClient.ConstructorIO.AutoCompleteListType atType)
        {
            try
            {
                if (atType == ConstructorIOClient.ConstructorIO.AutoCompleteListType.SearchSuggestions)
                {
                    m_dtCSVData.EndLoadData();

                    List<object> lDictionary = new List<object>();
                    Dictionary<string, object> objDic = new Dictionary<string, object>();
                    Dictionary<string, object> objParams = new Dictionary<string, object>();
                    this.LoadData(ref objParams);

                    for (int i = 0; i < dataGridViewCSVData.Rows.Count; i++)
                    {
                        if (null != dataGridViewCSVData.Rows[i].Cells[0].Value)
                        {
                            Dictionary<string, object> value = ConstructorIOClient.ConstructorIO.CreateItemParams(dataGridViewCSVData.Rows[i].Cells[0].Value.ToString(), "Search Suggestions", false, objParams);
                            lDictionary.Add(value);
                        }
                    }

                    object[] sItems = lDictionary.ToArray();
                    objDic.Add("items", sItems);
                    bool bSuccess = m_constructorClient.AddBatch(objDic, StringEnum.GetStringValue(ConstructorIOClient.ConstructorIO.AutoCompleteListType.SearchSuggestions));
                }

                if (atType == ConstructorIOClient.ConstructorIO.AutoCompleteListType.Product)
                {
                    m_dtCSVData.EndLoadData();

                    List<object> lDictionary = new List<object>();
                    Dictionary<string, object> objDic = new Dictionary<string, object>();
                    Dictionary<string, object> objParams = new Dictionary<string, object>();

                    this.LoadData(ref objParams);
                    for (int i = 0; i < dataGridViewCSVData.Rows.Count; i++)
                    {
                        if (null != dataGridViewCSVData.Rows[i].Cells[0].Value)
                        {
                            Dictionary<string, object> value = ConstructorIOClient.ConstructorIO.CreateItemParams(dataGridViewCSVData.Rows[i].Cells[0].Value.ToString(), "Products", false, objParams);
                            lDictionary.Add(value);
                        }
                    }

                    object[] sItems = lDictionary.ToArray();
                    objDic.Add("items", sItems);
                    bool bSuccess = m_constructorClient.AddBatch(objDic, StringEnum.GetStringValue(ConstructorIOClient.ConstructorIO.AutoCompleteListType.Product));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// ConvertToType
        /// </summary>
        /// <param name="sText"></param>
        /// <returns>ConstructorIO.AutoCompleteListType</returns>
        private ConstructorIOClient.ConstructorIO.AutoCompleteListType ConvertToType(string sText)
        {
            ConstructorIOClient.ConstructorIO.AutoCompleteListType objListType;

            if (sText == "Products")
                objListType = ConstructorIOClient.ConstructorIO.AutoCompleteListType.Product;
            else
                objListType = ConstructorIOClient.ConstructorIO.AutoCompleteListType.SearchSuggestions;

            return objListType;
        }


        /// <summary>
        /// LoadData
        /// </summary>
        /// <param name="objDic"></param>
        private void LoadData(ref Dictionary<string, object> objDic)
        {
            if (this.m_currentType == ConstructorIOClient.ConstructorIO.AutoCompleteListType.Product)
            {

                if (dataGridViewCSVData.Rows[0].Cells["Score"].Value != null)
                    objDic.Add("suggested_score", Convert.ToInt32(dataGridViewCSVData.Rows[0].Cells["Score"].Value));

                if (dataGridViewCSVData.Rows[0].Cells["Url"].Value != null)
                    objDic.Add("url", Convert.ToString(dataGridViewCSVData.Rows[0].Cells["Url"].Value));

                if (dataGridViewCSVData.Rows[0].Cells["Image Url"].Value != null)
                    objDic.Add("image_url", Convert.ToString(dataGridViewCSVData.Rows[0].Cells["Url"].Value));

                if (dataGridViewCSVData.Rows[0].Cells["Description"].Value != null)
                    objDic.Add("description", Convert.ToString(dataGridViewCSVData.Rows[0].Cells["Description"].Value));
            }
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            this.Init();
            backgroundWorkerVerify.RunWorkerAsync();
        }

        private void backgroundWorkerVerify_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (m_constructorClient.Verify())
                    MessageBox.Show("Successful authentication!");
                else
                    MessageBox.Show("Not successful authentication!");
            }
            catch (Exception ex)
            {

            }

        }

        private void comboBoxType_SelectedValueChanged(object sender, EventArgs e)
        {
            this.m_currentType = ConvertToType(comboBoxType.SelectedItem.ToString());
        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
