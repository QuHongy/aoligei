using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace ExamWeb.handler
{
    /// <summary>
    /// YanzhengmaHandler1 的摘要说明
    /// </summary>
    public class YanzhengmaHandler1 : IHttpHandler, IRequiresSessionState
    {
        public bool IsReusable => false;

        public void ProcessRequest(HttpContext context)
        {
            Random r = new Random();
            string str = null;
            for (int i = 0; i < 4; i++)
            {
                int rNumber = r.Next(0, 10);
                str += rNumber;
            }
            //保存验证码
            context.Session["sn"] = str;

            Bitmap bmp = new Bitmap(80, 20);//设置长度和宽度分别为80和20
            Graphics g = Graphics.FromImage(bmp);//Bitmap是Image的子类，所以可以替换Image放置在函数中

            for (int i = 0; i < 4; i++)
            {
                Point p = new Point(i * 20, 0);
                string[] fonts = { "微软雅黑", "宋体", "黑体", "隶书", "仿宋" };
                Color[] colors = { Color.Yellow, Color.White, Color.Red, Color.Green };
                g.DrawString(str[i].ToString(), new Font(fonts[r.Next(0, 5)], 15, FontStyle.Bold), new SolidBrush(colors[r.Next(0, 4)]), p);
            }

            //画线，可以不要
            for (int i = 0; i < 10; i++)
            {
                Point p1 = new Point(r.Next(0, bmp.Width), r.Next(0, bmp.Height));
                Point p2 = new Point(r.Next(0, bmp.Width), r.Next(0, bmp.Height));
                g.DrawLine(new Pen(Brushes.Green), p1, p2);
            }

            for (int i = 0; i < 100; i++)
            {
                Point p = new Point(r.Next(0, bmp.Width), r.Next(0, bmp.Height));
                bmp.SetPixel(p.X, p.Y, Color.Black);
            }
            bmp.Save(context.Response.OutputStream, ImageFormat.Jpeg);
            bmp.Dispose();
        }
    }
}