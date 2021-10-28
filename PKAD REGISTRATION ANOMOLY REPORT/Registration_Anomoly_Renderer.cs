using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
namespace PKAD_REGISTRATION_ANOMOLY_REPORT
{
    public class Registration_Anomoly_Renderer
    {
        private int width = 0, height = 0;
        private double totHeight = 1000;
        private Bitmap bmp = null;
        private Graphics gfx = null;
        private List<Registration_Anomoly_Model> data = null;
        Image logoImg = Image.FromFile(Path.Combine(Directory.GetCurrentDirectory(), "assets", "logo.png"));
        Image redFingerImg = Image.FromFile(Path.Combine(Directory.GetCurrentDirectory(), "assets", "red_finger.png"));
        Image ghostImg = Image.FromFile(Path.Combine(Directory.GetCurrentDirectory(), "assets", "ghost.png"));
        Image redFlagImg = Image.FromFile(Path.Combine(Directory.GetCurrentDirectory(), "assets", "red_flag.png"));
        public Registration_Anomoly_Renderer(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
        public int getDataCount()
        {
            if (this.data == null) return 0;
            else return this.data.Count;
        }
        public List<Registration_Anomoly_Model> getData()
        {
            return this.data;
        }
        public void setData(List<Registration_Anomoly_Model> data)
        {
            this.data = data;
        }
        public void setRenderSize(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
        public Point convertCoord(Point a)
        {
            double px = height / totHeight;

            Point res = new Point();
            res.X = (int)(a.X * px);
            res.Y = (int)((totHeight - a.Y) * px);
            return res;
        }
        public PointF convertCoord(PointF p)
        {
            double px = height / totHeight;
            PointF res = new PointF();
            res.X = (int)(p.X * px);
            res.Y = (int)((totHeight - p.Y) * px);
            return res;
        }
        public Bitmap getBmp()
        {
            return this.bmp;
        }
        public void drawFilledCircle(Brush brush, Point o, Size size)
        {
            double px = height / totHeight;
            size.Width = (int)(size.Width * px);
            size.Height = (int)(size.Height * px);

            Rectangle rect = new Rectangle(convertCoord(o), size);

            gfx.FillEllipse(brush, rect);
        }
        public void fillRectangle(Color color, Rectangle rect)
        {
            rect.Location = convertCoord(rect.Location);
            double px = height / totHeight;
            rect.Width = (int)(rect.Width * px);
            rect.Height = (int)(rect.Height * px);

            Brush brush = new SolidBrush(color);
            gfx.FillRectangle(brush, rect);
            brush.Dispose();

        }
        public void drawRectangle(Pen pen, Rectangle rect)
        {
            rect.Location = convertCoord(rect.Location);
            double px = height / totHeight;
            rect.Width = (int)(rect.Width * px);
            rect.Height = (int)(rect.Height * px);
            gfx.DrawRectangle(pen, rect);
        }

        public void drawImg(Image img, Point o, Size size)
        {
            double px = height / totHeight;
            o = convertCoord(o);
            Rectangle rect = new Rectangle(o, new Size((int)(size.Width * px), (int)(size.Height * px)));
            gfx.DrawImage(img, rect);

        }
        public void drawString(Color color, Point o, string content, int font = 15)
        {

            o = convertCoord(o);

            // Create font and brush.
            Font drawFont = new Font("Calibri", font);
            SolidBrush drawBrush = new SolidBrush(color);

            gfx.DrawString(content, drawFont, drawBrush, o.X, o.Y);

            drawFont.Dispose();
            drawBrush.Dispose();

        }

        public void drawCenteredString_withBorder(string content, Rectangle rect, Brush brush, Font font, Color borderColor)
        {

            //using (Font font1 = new Font("Calibri", fontSize, FontStyle.Bold, GraphicsUnit.Point))

            // Create a StringFormat object with the each line of text, and the block
            // of text centered on the page.
            double px = height / totHeight;
            rect.Location = convertCoord(rect.Location);
            rect.Width = (int)(px * rect.Width);
            rect.Height = (int)(px * rect.Height);

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            // Draw the text and the surrounding rectangle.
            gfx.DrawString(content, font, brush, rect, stringFormat);

            Pen borderPen = new Pen(new SolidBrush(borderColor), 2);
            gfx.DrawRectangle(borderPen, rect);
            borderPen.Dispose();
        }
        public void drawCenteredImg_withBorder(Image img, Rectangle rect, Brush brush, Font font, Color borderColor)
        {
            double px = height / totHeight;
            rect.Location = convertCoord(rect.Location);
            rect.Width = (int)(px * rect.Width);
            rect.Height = (int)(px * rect.Height);

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            // Draw the text and the surrounding rectangle.
            //gfx.DrawString(content, font, brush, rect, stringFormat);
            //drawImg(logoImg, new Point(20, 60), new Size(150, 50));
            gfx.DrawImage(img, rect);
            Pen borderPen = new Pen(new SolidBrush(borderColor), 2);
            gfx.DrawRectangle(borderPen, rect);
            borderPen.Dispose();
        }
        public void drawCenteredString(string content, Rectangle rect, Brush brush, Font font)
        {

            //using (Font font1 = new Font("Calibri", fontSize, FontStyle.Bold, GraphicsUnit.Point))

            // Create a StringFormat object with the each line of text, and the block
            // of text centered on the page.
            double px = height / totHeight;
            rect.Location = convertCoord(rect.Location);
            rect.Width = (int)(px * rect.Width);
            rect.Height = (int)(px * rect.Height);

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            // Draw the text and the surrounding rectangle.
            gfx.DrawString(content, font, brush, rect, stringFormat);
            //gfx.DrawRectangle(Pens.Black, rect);

        }
        private void fillPolygon(Brush brush, PointF[] points)
        {
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = convertCoord(points[i]);
            }
            gfx.FillPolygon(brush, points);
        }
        public void drawLine(Point p1, Point p2, Color color, int linethickness = 1)
        {
            if (color == null)
                color = Color.Gray;

            p1 = convertCoord(p1);
            p2 = convertCoord(p2);
            gfx.DrawLine(new Pen(color, linethickness), p1, p2);

        }
        public void drawString(Font font, Color brushColor, string content, Point o)
        {
            o = convertCoord(o);
            SolidBrush drawBrush = new SolidBrush(brushColor);
            gfx.DrawString(content, font, drawBrush, o.X, o.Y);
        }
        public void drawString(Point o, string content, int font = 15)
        {

            o = convertCoord(o);

            // Create font and brush.
            Font drawFont = new Font("Calibri", font);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            gfx.DrawString(content, drawFont, drawBrush, o.X, o.Y);

        }

        public void drawPie(Color color, Point o, Size size, float startAngle, float sweepAngle)
        {
            // Create location and size of ellipse.
            double px = height / totHeight;
            size.Width = (int)(size.Width * px);
            size.Height = (int)(size.Height * px);

            Rectangle rect = new Rectangle(convertCoord(o), size);
            // Draw pie to screen.            
            Brush grayBrush = new SolidBrush(color);
            gfx.FillPie(grayBrush, rect, startAngle, sweepAngle);
        }

        public void draw(int pageID = 1)
        {
            if (bmp == null)
                bmp = new Bitmap(width, height);
            else
            {
                if (bmp.Width != width || bmp.Height != height)
                {
                    bmp.Dispose();
                    bmp = new Bitmap(width, height);

                    gfx.Dispose();
                    gfx = Graphics.FromImage(bmp);
                    gfx.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                }
            }

            if (gfx == null)
            {
                gfx = Graphics.FromImage(bmp);
                gfx.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                //g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            }
            else
            {
                gfx.Clear(Color.Transparent);
            }

            if (data == null) return;
            int baseIndex = 12 * (pageID - 1);

            if (baseIndex >= data.Count) return;

            Pen blackBorderPen2 = new Pen(Color.Black, 2);
            Font percentFont = new Font("Calibri", 28, FontStyle.Bold, GraphicsUnit.Point);
            Font percentFont1 = new Font("Calibri", 28, FontStyle.Regular, GraphicsUnit.Point);
            Font numFont = new Font("Calibri", 45, FontStyle.Bold, GraphicsUnit.Point);
            Font numFont1 = new Font("Calibri", 80, FontStyle.Bold, GraphicsUnit.Point);
            Font numFont2 = new Font("Calibri", 110, FontStyle.Bold, GraphicsUnit.Point);
            Font h3Font = new Font("Calibri", 24, FontStyle.Bold, GraphicsUnit.Point);
            Font h4Font = new Font("Calibri", 20, FontStyle.Bold, GraphicsUnit.Point);


            Font headertitle = new Font("Calibri", 12, FontStyle.Bold, GraphicsUnit.Point);
            Font headerText = new Font("Calibri", 18, FontStyle.Regular, GraphicsUnit.Point);
            Font textFont10 = new Font("Calibri", 10, FontStyle.Regular, GraphicsUnit.Point);
            Font textFont8 = new Font("Calibri", 8, FontStyle.Regular, GraphicsUnit.Point);
            Font textFont7 = new Font("Calibri", 8, FontStyle.Regular, GraphicsUnit.Point);
            Font textFont6 = new Font("Calibri", 6, FontStyle.Regular, GraphicsUnit.Point);
            Font textFont5 = new Font("Calibri", 5, FontStyle.Regular, GraphicsUnit.Point);

            double px = height / totHeight;
            int totWidth = (int)(this.width / px);

            //Draw header Sectioin
            drawCenteredString("PKAD REGISTRATION\nANOMOLY REPORT", new Rectangle(0, 1000, 750, 120), Brushes.Black, percentFont);
            fillRectangle(Color.Black, new Rectangle(720, 998, totWidth - 720, 100));
            drawCenteredString(string.Format("R-{0}", data[baseIndex].registered), new Rectangle(720, 1000, 270, 80), Brushes.White, percentFont);
            fillRectangle(Color.White, new Rectangle(1000, 990, 400, 60));
            drawCenteredString(string.Format("{0,4:0000.##} {1}", data[baseIndex].precinct, data[baseIndex].precinct_name), new Rectangle(1000, 1000, 400, 80), Brushes.Black, percentFont);

            fillRectangle(Color.Green, new Rectangle(1400, 990, totWidth - 1410, 60));
            drawCenteredString(string.Format("V-{0}", data[baseIndex].voted), new Rectangle(1400, 990, (totWidth - 1410) / 2, 60), Brushes.White, percentFont);
            drawCenteredString(Math.Round(data[baseIndex].voted * 100 / (double)data[baseIndex].registered, 2).ToString() + "%", new Rectangle(totWidth - (totWidth - 1400) / 2, 990, (totWidth - 1400) / 2, 60), Brushes.Yellow, h3Font);

            //Draw Explanation Section
            fillRectangle(Color.Red, new Rectangle(0, 895, totWidth / 2, 200));
            drawCenteredString("VOTED IN 2020 ELECTION\nBUT THEN QUICKLY\nREMOVED FROM VOTER ROLLS", new Rectangle(0, 895, totWidth / 2, 200), Brushes.White, percentFont);


            fillRectangle(Color.Black, new Rectangle(totWidth / 2, 895, totWidth / 2, 200));
            drawCenteredString("GHOST VOTER: Voter appearing on Voter\nRegistration and voting but not appearing on\nany other public reports", new Rectangle(totWidth / 2, 895, totWidth / 2, 200), Brushes.White, percentFont);


            //Draw Image Section

            int tot_removals_voted = 0, tot_md_ghosts = 0;
            for (int i = 0; i < 12; i++)
            {
                tot_removals_voted += data[baseIndex + i].removals_voted;
                tot_md_ghosts += data[baseIndex + i].md_ghosts_voted_removed;
            }

            drawImg(redFingerImg, new Point(0, 680), new Size(400, 400));
            drawCenteredString( tot_removals_voted.ToString(), new Rectangle(270, 600, 700, 150), Brushes.Black, numFont2 );
            double percent = tot_removals_voted / (double)data[0].voted;
            drawCenteredString(Math.Round(percent * 100, 5).ToString() + "%", new Rectangle(270, 450, 700, 150), Brushes.Black, numFont1);
            if (percent > 0.000516)
            {
                drawImg(redFlagImg, new Point(770, 630), new Size(150, 150));
            }

            drawImg(ghostImg, new Point(totWidth / 2, 680), new Size(400, 400));
            drawCenteredString(tot_md_ghosts.ToString(), new Rectangle(totWidth / 2 + 270, 600, 700, 150), Brushes.Black, numFont2);
            percent = tot_md_ghosts / (double)data[0].voted;
            drawCenteredString(Math.Round(percent * 100, 5).ToString() + "%", new Rectangle(totWidth / 2 + 270, 450, 700, 150), Brushes.Black, numFont1);
            if (percent > 0.000516)
            {
                drawImg(redFlagImg, new Point(totWidth / 2 + 770, 630), new Size(150, 150));
            }


            //Draw Bottom section
            fillRectangle(Color.Black, new Rectangle(0, 280, totWidth, 200));
            drawCenteredString("2020 GENERAL\nELECTION MARGIN", new Rectangle(0, 280, totWidth/2, 200), Brushes.White, numFont);
            drawCenteredString("0.0516%", new Rectangle(totWidth / 2, 280, totWidth / 2, 200), Brushes.White, numFont2);
            

            drawImg(logoImg, new Point(30, 70), new Size(150, 60));
            string copyright = "©2021 Tesla Laboratories, llc & JHP";
            drawCenteredString(copyright, new Rectangle(1400, 50, 500, 50), Brushes.Black, headerText);


        }
    }
}
