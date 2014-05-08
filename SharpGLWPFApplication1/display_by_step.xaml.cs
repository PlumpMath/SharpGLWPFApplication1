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
    /// display_by_step.xaml 的交互逻辑
    /// </summary>
    public partial class display_by_step : Window
    {
        OpenGL gl;
        DataSet ds = new DataSet();
        int second_count = 0;
        //是否已经描绘的标志数组
        bool[] tag_arr = new bool[38];
        private float rotation = 0.1f;
        Hashtable ColorHashTable = new Hashtable();

        public display_by_step()
        {
            InitializeComponent();

            //获取数据库数据
            using (ODBCClass o = new ODBCClass("my_box_msg"))
            {
                OdbcCommand oCommand = o.GetCommand("select * from box_location where car_id = '001' order by sequence");
                OdbcDataAdapter oAdapter = new OdbcDataAdapter(oCommand);
                //DataSet ds = new DataSet();
                oAdapter.Fill(ds);
                //增添已经画图列
                //ds.Tables[0].Columns.Add("have_drawed", typeof(bool), "false");
                //TO DO : Make use of the data set
            }
        }

        private void make_change(object sender, EventArgs e)
        {
            second_count++;
            second_count %= 38;
            this.Title = second_count.ToString();
            //System.Threading.Thread.Sleep(5000);
            OpenGL gl = myGLCon.OpenGL;
            ////gl = e.OpenGL;
            ////  Load the identity matrix.
            ////gl.LoadIdentity();
            ////  Set the clear color.
            //gl.ClearColor(1, 1, 1, 0);
            //////  Clear the color and depth buffer.
            //gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            //gl.LoadIdentity();
            //gl.Flush();
            for (int index = 0; index != ds.Tables[0].Rows.Count; ++index)
            {
                DataRow dr = ds.Tables[0].Rows[index];
                if (tag_arr[index] == true)
                    continue;
                else
                {
                    myGLCon_drawWireFram((float)dr["lx"], (float)dr["lz"], (float)dr["ly"], (float)dr["hx"], (float)dr["hz"], (float)dr["hy"]);
                    myGLCon_drawCube((float)dr["lx"], (float)dr["lz"], (float)dr["ly"], (float)dr["hx"], (float)dr["hz"], (float)dr["hy"], 0.5f, 0.5f, 0.5f);
                    tag_arr[index] = true;
                    break;
                }
            }

        }

        //动画演示，初始化
        private void myGLCon_OpenGLDraw(object sender, OpenGLEventArgs args)
        {
            myTextBlock.Text = "hello";
            gl = myGLCon.OpenGL;
            //  Clear the color and depth buffer.
            //gl.ClearColor(1, 1, 1, 0);
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            //  Load the identity matrix.
            //gl.LoadIdentity();

            //  Rotate around the Y axis.
            gl.Rotate(rotation, 0.0f, 1.0f, 0.0f);
            //画车厢线框图
            myGLCon_drawWireFram(0.0f, 0.0f, 0.0f, 5.0f, 2.0f, 2.0f);
            ////动态展示箱子
            //全部置为false
            for (int index = 0; index != ds.Tables[0].Rows.Count; ++index)
            {
                tag_arr[index] = false;
            }
            //取得箱子总数
            int box_number = ds.Tables[0].Rows.Count;
            DispatcherTimer timer = new DispatcherTimer();//定时器
            timer.Interval = TimeSpan.FromMilliseconds(10);//定时10毫秒
            //timer.Interval = TimeSpan.FromSeconds(5);//定时10毫秒
            timer.Tick += new EventHandler(make_change);
            timer.IsEnabled = true;
            timer.Start();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                float Blue, Red, Green;
                string temp;

                temp = dr["distance"].ToString();
                if (!ColorHashTable.ContainsKey(dr["distance"].ToString()))
                {
                    Random ra = new Random();
                    Blue = (float)ra.NextDouble();
                    Red = (float)ra.NextDouble();
                    Green = (float)ra.NextDouble();
                    ColorHashTable.Add(dr["distance"].ToString(), Blue.ToString() + " " + Red.ToString() + " " + Green.ToString());
                }
                else
                {
                    string AllColor = ColorHashTable[dr["distance"].ToString()].ToString();
                    string[] ColorArray = AllColor.Split(' ');
                    Blue = (float)Convert.ToDecimal(ColorArray[0]);
                    Red = (float)Convert.ToDecimal(ColorArray[1]);
                    Green = (float)Convert.ToDecimal(ColorArray[2]);
                }
            //    //画箱子线框图
            //    //drawWireFram((float)dr["lx"], (float)dr["ly"], (float)dr["lz"], (float)dr["hx"], (float)dr["hy"], (float)dr["hz"]);
            //    //drawCube((float)dr["lx"], (float)dr["ly"], (float)dr["lz"], (float)dr["hx"], (float)dr["hy"], (float)dr["hz"], Red, Green, Blue);
            //    //画图部分被注释
            //    //myGLCon_drawWireFram((float)dr["lx"], (float)dr["lz"], (float)dr["ly"], (float)dr["hx"], (float)dr["hz"], (float)dr["hy"]);
            //    //myGLCon_drawCube((float)dr["lx"], (float)dr["lz"], (float)dr["ly"], (float)dr["hx"], (float)dr["hz"], (float)dr["hy"], Red, Green, Blue);

            //    second_count++;
            }
        }

        private void myGLCon_drawWireFram(float Lx, float Ly, float Lz, float Hx, float Hy, float Hz)
        {
            //  Get the OpenGL object.
            OpenGL gl = myGLCon.OpenGL;

            //下平面线框图,顺时针画
            gl.LineWidth(2.5f);
            gl.Color(1.0f, 1.0f, 0.0f);
            gl.Begin(OpenGL.GL_LINES);
            gl.Vertex(Lx, Ly, Lz);
            gl.Vertex(Hx, Ly, Lz);
            gl.End();

            gl.LineWidth(2.5f);
            gl.Begin(OpenGL.GL_LINES);
            gl.Vertex(Hx, Ly, Lz);
            gl.Vertex(Hx, Ly, Hz);
            gl.End();

            gl.LineWidth(2.5f);
            gl.Begin(OpenGL.GL_LINES);
            gl.Vertex(Hx, Ly, Hz);
            gl.Vertex(Lx, Ly, Hz);
            gl.End();

            gl.LineWidth(2.5f);
            gl.Begin(OpenGL.GL_LINES);
            gl.Vertex(Lx, Ly, Hz);
            gl.Vertex(Lx, Ly, Lz);
            gl.End();

            //上平面线框图， 顺时针画
            gl.LineWidth(2.5f);
            gl.Begin(OpenGL.GL_LINES);
            gl.Vertex(Lx, Hy, Lz);
            gl.Vertex(Hx, Hy, Lz);
            gl.End();

            gl.LineWidth(2.5f);
            gl.Begin(OpenGL.GL_LINES);
            gl.Vertex(Hx, Hy, Lz);
            gl.Vertex(Hx, Hy, Hz);
            gl.End();

            gl.LineWidth(2.5f);
            gl.Begin(OpenGL.GL_LINES);
            gl.Vertex(Hx, Hy, Hz);
            gl.Vertex(Lx, Hy, Hz);
            gl.End();

            gl.LineWidth(2.5f);
            gl.Begin(OpenGL.GL_LINES);
            gl.Vertex(Lx, Hy, Hz);
            gl.Vertex(Lx, Hy, Lz);
            gl.End();

            //四个直立线框，顺时针画

            gl.LineWidth(2.5f);
            gl.Begin(OpenGL.GL_LINES);
            gl.Vertex(Lx, Ly, Lz);
            gl.Vertex(Lx, Hy, Lz);
            gl.End();

            gl.LineWidth(2.5f);
            gl.Begin(OpenGL.GL_LINES);
            gl.Vertex(Hx, Ly, Lz);
            gl.Vertex(Hx, Hy, Lz);
            gl.End();

            gl.LineWidth(2.5f);
            gl.Begin(OpenGL.GL_LINES);
            gl.Vertex(Hx, Ly, Hz);
            gl.Vertex(Hx, Hy, Hz);
            gl.End();

            gl.LineWidth(2.5f);
            gl.Begin(OpenGL.GL_LINES);
            gl.Vertex(Lx, Ly, Hz);
            gl.Vertex(Lx, Hy, Hz);
            gl.End();

            gl.Flush();
        }

        private void myGLCon_drawCube(float Lx, float Ly, float Lz, float Hx, float Hy, float Hz, float Red, float Green, float Blue)
        {
            //  Get the OpenGL object.
            OpenGL gl = myGLCon.OpenGL;

            gl.Color(Red, Green, Blue);
            //下平面
            gl.Begin(OpenGL.GL_POLYGON);
            gl.Color(Blue, Red, Green);
            gl.Vertex(Lx, Ly, Lz);
            gl.Vertex(Lx, Ly, Hz);
            gl.Vertex(Hx, Ly, Hz);
            gl.Vertex(Hx, Ly, Lz);
            gl.Vertex(Lx, Ly, Lz);
            gl.End();

            //上平面
            gl.Begin(OpenGL.GL_POLYGON);
            gl.Color(Blue, Red, Green);
            gl.Vertex(Lx, Hy, Lz);
            gl.Vertex(Lx, Hy, Hz);
            gl.Vertex(Hx, Hy, Hz);
            gl.Vertex(Hx, Hy, Lz);
            gl.Vertex(Lx, Hy, Lz);
            gl.End();

            //左平面
            gl.Begin(OpenGL.GL_POLYGON);
            gl.Color(Blue, Red, Green);
            gl.Vertex(Lx, Ly, Lz);
            gl.Vertex(Lx, Ly, Hz);
            gl.Vertex(Lx, Hy, Hz);
            gl.Vertex(Lx, Hy, Lz);
            gl.Vertex(Lx, Ly, Lz);
            gl.End();

            //右平面
            gl.Begin(OpenGL.GL_POLYGON);
            gl.Color(Blue, Red, Green);
            gl.Vertex(Hx, Ly, Lz);
            gl.Vertex(Hx, Ly, Hz);
            gl.Vertex(Hx, Hy, Hz);
            gl.Vertex(Hx, Hy, Lz);
            gl.Vertex(Hx, Ly, Lz);
            gl.End();

            //后平面
            gl.Begin(OpenGL.GL_POLYGON);
            gl.Color(Blue, Red, Green);
            gl.Vertex(Lx, Ly, Lz);
            gl.Vertex(Hx, Ly, Lz);
            gl.Vertex(Hx, Hy, Lz);
            gl.Vertex(Lx, Hy, Lz);
            gl.Vertex(Lx, Ly, Lz);
            gl.End();

            //前平面
            gl.Begin(OpenGL.GL_POLYGON);
            gl.Color(Blue, Red, Green);
            gl.Vertex(Lx, Ly, Hz);
            gl.Vertex(Hx, Ly, Hz);
            gl.Vertex(Hx, Hy, Hz);
            gl.Vertex(Lx, Hy, Hz);
            gl.Vertex(Lx, Ly, Hz);
            gl.End();
        }

        //单步版本
        private void myGLCon_OpenGLInitialized(object sender, OpenGLEventArgs args)
        {
            //  TODO: Initialise OpenGL here.

            //  Get the OpenGL object.
            gl = myGLCon.OpenGL;
            //  Set the clear color.
            gl.ClearColor(1, 1, 1, 0);
        }

        //单步版本
        private void myGLControl_Resized(object sender, OpenGLEventArgs args)
        {
            //  TODO: Set the projection matrix here.

            //  Get the OpenGL object.
            OpenGL gl = myGLCon.OpenGL;
            //  Set the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);

            //  Load the identity.
            gl.LoadIdentity();

            //  Create a perspective transformation.
            gl.Perspective(60.0f, (double)Width / (double)Height, 0.01, 100.0);

            //  Use the 'look at' helper function to position and aim the camera.
            gl.LookAt(7, 5, 5, 0, 0, 0, 0, 1, 0);

            //  Set the modelview matrix.
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }
    }
}
