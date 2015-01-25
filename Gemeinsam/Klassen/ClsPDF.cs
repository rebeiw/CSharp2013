using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp.Pdf;
using PdfSharp;
using PdfSharp.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Diagnostics;

namespace Helper
{
    public class ClsPDF
    {
        public struct ClsPDFBorders
        {
            public double Left;
            public double Top;
            public double Right;
            public double Bottom;
        }
        public struct ClsPDFArea
        {
            public double Left;
            public double Top;
            public double Height;
            public double Width;
        }
        
        private string m_FileName;
        private string m_FontName;
        private int m_Size1;
        private int m_Size2;
        private int m_Size3;
        private int m_Size4;




        private int m_LeftBorder;
        private int m_TopBorder;
        private int m_RightBorder;
        private int m_BottomBorder;
        

        private PdfDocument m_PdfDokument;
        private PdfPage m_PdfPage;
        private int m_PageNo;
        private XFont m_FontSize1Bold;
        private XFont m_FontSize2Bold;
        private XFont m_FontSize3Bold;
        private XFont m_FontSize4Bold;

        private XSolidBrush m_XBrush;
        private XPen m_XPen;
        private XGraphics m_XGraphics;
        private Rectangle m_Rectangle;

        private List<ClsPDFBorders> m_ListBorders;

        private List<ClsPDFArea> m_ListPrintArea;
        private List<ClsPDFArea> m_ListHeadArea;
        private List<ClsPDFArea> m_ListFootArea;

        private ClsSingeltonLanguage m_language;

        public ClsPDF()
        {
            this.m_ListBorders = new List<ClsPDFBorders>();
            this.m_ListFootArea = new List<ClsPDFArea>();
            this.m_ListHeadArea = new List<ClsPDFArea>();
            this.m_ListPrintArea = new List<ClsPDFArea>();


            this.m_language = ClsSingeltonLanguage.CreateInstance();
            this.m_Rectangle=new Rectangle();

            this.m_XBrush=new XSolidBrush(XBrushes.Black);

            this.m_XPen=new XPen(Color.Black,1);
            this.m_FileName = "pdftest.pdf";
            this.m_FontName = "Arial";
            this.m_Size1 = 22;
            this.m_Size2 = 18;
            this.m_Size3 = 12;
            this.m_Size4 = 10;
            this.m_FontSize1Bold = new XFont(this.m_FontName, this.m_Size1, XFontStyle.Bold);
            this.m_FontSize2Bold = new XFont(this.m_FontName, this.m_Size2, XFontStyle.Bold);
            this.m_FontSize3Bold = new XFont(this.m_FontName, this.m_Size3, XFontStyle.Bold);
            this.m_FontSize4Bold = new XFont(this.m_FontName, this.m_Size4, XFontStyle.Bold);

            this.m_PdfDokument = new PdfDocument();
        }

        public void ShowDocument()
        {
            this.m_PdfDokument.Save(this.m_FileName);
            Process.Start(this.m_FileName);
        }

        public void PrintHead(string title)
        {

            double left = 0.0;
            double top = 0.0;
            this.DrawRectangle(0, 0, 2, 17.5);


            ClsPDFBorders pdf_borders;
            pdf_borders = (ClsPDFBorders)this.m_ListBorders[this.m_PageNo];


        }

        public void DrawRectangle(double borderLeft, double borderTop, double rectangleHeight, double rectangleWidth)
        {
            int left = this.m_LeftBorder + this.GetPointX(borderLeft);
            int top = this.m_TopBorder + this.GetPointY(borderTop);
            int width = this.GetPointX(rectangleWidth);
            int height = this.GetPointY(rectangleHeight);
            this.m_Rectangle.X = left;
            this.m_Rectangle.Y = top;
            this.m_Rectangle.Height = height;
            this.m_Rectangle.Width = width;
            this.m_XGraphics.DrawRectangle(this.m_XPen, this.m_Rectangle);

        }

        public void DrawString(string text,double borderLeft,double borderTop)
        {
            int left = this.m_LeftBorder + this.GetPointX(borderLeft);
            int top = this.m_TopBorder + this.GetPointY(borderTop);
            this.m_XGraphics.DrawString(this.m_language.GetTranslation(text),this.m_FontSize1Bold, this.m_XBrush, left, top);

        }
        private int GetPointY(double cm)
        {
            int retval = (int)(this.m_PdfPage.Height.Point / this.m_PdfPage.Height.Centimeter * cm);
            return retval;
        }

        private int GetPointX(double cm)
        {
            int retval = (int)(this.m_PdfPage.Width.Point / this.m_PdfPage.Width.Centimeter * cm);
            return retval;
        }

        public void SetPage(int pageNo)
        {
            if (this.m_XGraphics != null)
            {
                this.m_XGraphics.Dispose();
            }
            PdfPage pdf_page = this.m_PdfDokument.Pages[pageNo];
            this.m_XGraphics = XGraphics.FromPdfPage(pdf_page);
            

            this.m_LeftBorder = this.GetPointX(this.m_ListBorders[pageNo].Left);
            this.m_TopBorder = this.GetPointY(this.m_ListBorders[pageNo].Top);
            this.m_RightBorder = this.GetPointX(this.m_ListBorders[pageNo].Right);
            this.m_BottomBorder = this.GetPointY(this.m_ListBorders[pageNo].Bottom);
            this.m_PageNo = pageNo;
        }

        public void AddPage(PageSize pageSize = PageSize.A4, 
                            PageOrientation pageOrientation = PageOrientation.Portrait, 
                            double leftBorder = 1.5, 
                            double topBorder = 0.5, 
                            double rightBorder=0.5, 
                            double bottomBorder=0.5)
        {
            ClsPDFBorders pdf_borders;
            pdf_borders.Left = leftBorder;
            pdf_borders.Top = topBorder;
            pdf_borders.Right = rightBorder;
            pdf_borders.Bottom = bottomBorder;
            this.m_ListBorders.Add(pdf_borders);

            this.m_PdfPage = new PdfPage();
            this.m_PdfPage.Size = pageSize;
            this.m_PdfPage.Orientation = pageOrientation;
            this.m_PdfDokument.AddPage(this.m_PdfPage);

            ClsPDFArea pdf_area;
            pdf_area.Width = this.m_PdfPage.Width.Centimeter - leftBorder - rightBorder;
            pdf_area.Left = leftBorder;
            pdf_area.Top = topBorder;

            pdf_area.Height = 1.0;
            this.m_ListFootArea.Add(pdf_area);
            pdf_area.Height = 2.0;
            this.m_ListHeadArea.Add(pdf_area);

            int page_no=this.m_PdfDokument.PageCount - 1;
            double asd=this.m_ListHeadArea[page_no].Height;

            pdf_area.Height = this.m_PdfPage.Height.Centimeter - topBorder - bottomBorder;
            this.m_ListPrintArea.Add(pdf_area);



            SetPage(this.m_PdfDokument.PageCount - 1);
        }

        ~ClsPDF()
        {
        }
    }
}
