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

namespace Sample_App
{
    public partial class SampleForm : Form
    {
        private string m_sFileNameCSV;
        private DataTable m_dtCSVData;
        public ConstructorIO constructorClient;

        private class Item
        {
            public string Text;
            public ConstructorIO.AutoCompleListType Value;

            public Item(string sName, ConstructorIO.AutoCompleListType acltType)
            {
                Text = sName;
                Value = acltType;
            }

            public ConstructorIO.AutoCompleListType GetListType()
            {
                return Value;
            }
        };

        public SampleForm()
        {
            m_dtCSVData = new DataTable();

            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            constructorClient  = new ConstructorIO(txtAPIToken.Text, txtKey.Text);

            //not working
            //bool isValid = constructorClient.Verify();
            Dictionary<string, object> objDic = new Dictionary<string, object>();
            //objDic.Add("url", "www.google1.com");

            Dictionary<string, object> values = ConstructorIO.CreateItemParams("a3", "Products",false,null);
            Dictionary<string, object> values1 = ConstructorIO.CreateItemParams("a4", "Products", false, null);


            object[] sItems = new object[] {values, values1 };   
            //objDic.Add("items", sItems);
            //bool bSuccess = constructorClient.BatchAdd(objDic, ConstructorIO.AutoCompleListType.SearchSuggestions);

            //bool success = constructorClient.Add("test4", "Products",objDic);
            //bool success = constructorClient.AddItem("power drill1", "Products",objDic);
            //success = constructorClient.AddItem("power drill1", "Products", objDic);

            //bool success = constructorClient.RemoveItem("test1", "Search Suggestions");
            //bool success = constructorClient.ModifyItem("power drill1", "super power drill", "Products", objDic);
            //bool bSuccess = constructorClient.RemoveItem("super power drill", "Products");
            //bool success = constructorClient.TrackSearch("King");

            //bool bSuccess = constructorClient.BatchAddItem(objDic, "Products");

        }

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

        private void LoadCSVToDataSet(ref List<string> lCSV)
        {
            try
            {
                char cDelimiter = ',';
                object[] arData;
                string[] sLine = lCSV[0].Split(cDelimiter);

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

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        { 
            backgroundWorkerUploadCSV.RunWorkerAsync(ConvertToType(comboBoxType.SelectedItem.ToString()));
        }

        private void backgroundWorkerUploadCSV_DoWork(object sender, DoWorkEventArgs e)
        {
            ConstructorIO.AutoCompleListType arg = (ConstructorIO.AutoCompleListType)e.Argument;
            try
            {
                this.ProcessUpload((ConstructorIO.AutoCompleListType)e.Argument);
            }
            catch (Exception ex)
            {

            }
        }

        private void backgroundWorkerUploadCSV_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }
        private void ProcessUpload(ConstructorIO.AutoCompleListType atType)
        {
            try
            {
                if (atType == ConstructorIO.AutoCompleListType.SearchSuggestions)
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
                            Dictionary<string, object> value = ConstructorIO.CreateItemParams(dataGridViewCSVData.Rows[i].Cells[0].Value.ToString(), "Search Suggestions", false, objParams);
                            lDictionary.Add(value);
                        }
                    }

                    object[] sItems = lDictionary.ToArray();
                    objDic.Add("items",sItems);
                    bool bSuccess = constructorClient.BatchAdd(objDic, ConstructorIO.AutoCompleListType.SearchSuggestions);
                }

                if (atType == ConstructorIO.AutoCompleListType.Product)
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
                            Dictionary<string, object> value = ConstructorIO.CreateItemParams(dataGridViewCSVData.Rows[i].Cells[0].Value.ToString(), "Products", false, objParams);
                            lDictionary.Add(value);
                        }
                    }

                    object[] sItems = lDictionary.ToArray();
                    objDic.Add("items", sItems);
                    bool bSuccess = constructorClient.BatchAdd(objDic, ConstructorIO.AutoCompleListType.Product);
                }

            }
            catch (Exception ex)
            {

            }
        }
 
        private ConstructorIO.AutoCompleListType ConvertToType(string sText)
        {
            ConstructorIO.AutoCompleListType objListType;

            if (sText == "Products")
                objListType = ConstructorIO.AutoCompleListType.Product;
            else
                objListType = ConstructorIO.AutoCompleListType.SearchSuggestions;

            return objListType;
        }

        private void LoadData(ref Dictionary<string, object> objDic)
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
}
