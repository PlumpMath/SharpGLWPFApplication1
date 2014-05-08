using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using SharpGL.SceneGraph;
using SharpGL;

using System.Threading;
using System.Windows.Threading;


using System.Collections; //使用Hashtable时，必须引入这个命名空间
using System.Data;
using System.Data.Odbc;
using System.Configuration;

namespace SharpGLWPFApplication1
{
    /// <summary>
    /// PopWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PopWindow : Window
    {
        public PopWindow()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            InitializeComponent();
            using (ODBCClass o = new ODBCClass("my_box_msg"))
            {
                OdbcCommand oCommand = o.GetCommand("select * from box_location where car_id = '001' order by sequence");
                OdbcDataAdapter oAdapter = new OdbcDataAdapter(oCommand);
                //DataSet ds = new DataSet();
                oAdapter.Fill(ds);
                dt = ds.Tables[0];
                dataGrid.ItemsSource = dt.DefaultView;
                //增添已经画图列
                //ds.Tables[0].Columns.Add("have_drawed", typeof(bool), "false");
                //TO DO : Make use of the data set
                //grid.ColumnDefinitions.Add(new ColumnDefinition()); //添加列
                //for (int index = 0; index != ds.Tables[0].Rows.Count; ++index)
                //{
                //    DataRow dr = ds.Tables[0].Rows[index];
                //    RowDefinition myRow = new RowDefinition();
                //    grid.RowDefinitions.Add(myRow); //添加行
                //    dr = dt.NewRow();
                //    dataGrid.ItemsSource = dt.DefaultView;
                //    //for (int columIndex = 0; columIndex < dt.Columns.Count; columIndex++)
                //    //{
                //    //    dr[columIndex] = i.ToString() + " - " + columIndex.ToString();
                //    //    dt.Rows.Add(dr);
                //    //}
                //    //dataGrid.ItemsSource = null;
                //    //dataGrid.ItemsSource = dt.DefaultView;
                //}
            }
        }
    }
}
